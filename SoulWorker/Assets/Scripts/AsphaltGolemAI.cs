using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using System;
using MyStruct;
using MyEnum;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class AsphaltGolemAI : MonsterAI
{

    [SerializeField] private MonsterInfo m_monsterInfo;         // 몬스터 정보
    [SerializeField] private Animator m_bodyAnime;              // 몸 애니메이터
    [SerializeField] private Animator m_armsAnime;              // 팔 애니메이터
    [SerializeField] private BoxCollider m_rightBoxCollider;    // 오른팔 충돌체
    [SerializeField] private BoxCollider m_leftBoxCollider;     // 왼팔 충돌체
    public float m_moveSpeed = 3.0f;        // 이동 속도
    public float m_rotationSpeed = 3.0f;    // 회전 속도
    public float m_damage = 100.0f;         // 공격력
    public LivingEntity m_targetEntity;     // 추적 대상
    public LayerMask m_whatIsTarget;        // 안써도 될 것 같음 m_mask 로 대체

    public Transform m_attackRoot;          // 안쓰는 것 같은데... 나중에 확인 할 것
    public Transform m_eyeTrasform;
    public float m_attackRadius = 1.3f;     // 안쓰는 것 같음
    public float m_attackDistance;          // 안쓰는 것 같음
    public float m_fieldOfView = 50.0f;
    public float m_viewDistance = 10.0f;
    public float m_patrolSpeed = 2.0f;      // 안써도 될 것 같음 m_moveSpeed 확인할 것
    public float m_meleeDistance = 4f;
    
    private Transform m_monsterTransform;
    private AsphaltGolemState m_state = AsphaltGolemState.Idle;   // 상태
    private NavMeshAgent m_agent;
    private LayerMask m_mask;
    private RaycastHit[] m_hits = new RaycastHit[10];   // 안쓰는 것 같음
    private List<LivingEntity> m_lastAttackdTargts = new List<LivingEntity>();  // 안쓰는 것 같음
    private bool m_hasTarget => m_targetEntity != null && !m_targetEntity.m_dead;

    private MonsterSkillInfo[,] m_skillGroup;
    private Action[] m_animeUpdate;
    private Action[,] m_monsterAI;
    private Target m_target = Target.End;
    private Phase m_phase = Phase.End;
    private bool m_isSuperArmourBreak = true;
    private bool m_isActionEnd = true;
    private bool m_isTargetFollow = true;



    private enum Target { No, Yes, End }            // 타겟 - 타겟이 있는가
    private enum Phase { Phase_1, Phase_2, End }    // 페이즈 - 일정 체력을 잃으면 페이즈 전환


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (m_attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            //Gizmos.DrawSphere(m_attackRoot.position, m_attackRadius);
        }

        if (m_eyeTrasform != null)
        {

            var leftEyeRotation = Quaternion.AngleAxis(-m_fieldOfView * 0.5f, Vector3.up);
            var leftRayDirection = leftEyeRotation * transform.forward;
            Handles.color = new Color(1f, 0f, 0f, 0.2f);
            //Handles.DrawSolidArc(m_eyeTrasform.position, Vector3.up, leftRayDirection, m_fieldOfView, m_viewDistance);


            Handles.color = new Color(1f, 0f, 0f, 0.2f);
            Handles.DrawSolidDisc(m_eyeTrasform.position, Vector3.up, m_meleeDistance);

            Handles.color = new Color(1f, 1f, 1f, 0.2f);
            Handles.DrawSolidDisc(m_eyeTrasform.position, Vector3.up, m_viewDistance);
        }
    }
#endif


    private void Awake()
    {
        m_monsterTransform = transform;
        m_agent = GetComponent<NavMeshAgent>();

        //var attackPivot = m_attackRoot.position;
        //attackPivot.y = m_monsterTransform.position.y;
        //m_attackDistance = Vector3.Distance(m_monsterTransform.position, attackPivot) + m_attackRadius;

        m_agent.stoppingDistance = m_meleeDistance - 1f;
        m_agent.speed = m_patrolSpeed;

        // 플레이어와 충돌
        m_mask = LayerMask.NameToLayer("Player");

        // 몬스터 정보 초기화
        MonsterInfoInit();
    }

    private void Start()
    {
        m_animeUpdate = new Action[(int)AsphaltGolemState.End];
        m_animeUpdate[(int)AsphaltGolemState.Idle] = Ani_Idle;
        m_animeUpdate[(int)AsphaltGolemState.Run] = Ani_Run;
        m_animeUpdate[(int)AsphaltGolemState.DMG_L] = Ani_DMG_L;
        m_animeUpdate[(int)AsphaltGolemState.DMG_R] = Ani_DMG_R;
        m_animeUpdate[(int)AsphaltGolemState.KB] = Ani_KB;
        m_animeUpdate[(int)AsphaltGolemState.KD_Ham] = Ani_KD_Ham;
        m_animeUpdate[(int)AsphaltGolemState.KD_Str] = Ani_KD_Str;
        m_animeUpdate[(int)AsphaltGolemState.KD_Upp] = Ani_KD_Upp;
        m_animeUpdate[(int)AsphaltGolemState.Death] = Ani_Death;
        m_animeUpdate[(int)AsphaltGolemState.A_Skill_01] = Ani_A_Skill_01;
        m_animeUpdate[(int)AsphaltGolemState.A_Skill_01] = Ani_A_Skill_01;
        m_animeUpdate[(int)AsphaltGolemState.A_Skill_02] = Ani_A_Skill_02;
        m_animeUpdate[(int)AsphaltGolemState.A_Skill_03] = Ani_A_Skill_03;

        m_monsterAI = new Action[(int)Target.End, (int)Phase.End];
        m_monsterAI[(int)Target.No, (int)Phase.Phase_1] = AI_NoTarget;
        m_monsterAI[(int)Target.No, (int)Phase.Phase_2] = AI_NoTarget;
        m_monsterAI[(int)Target.Yes, (int)Phase.Phase_1] = AI_Phase_1;
        m_monsterAI[(int)Target.Yes, (int)Phase.Phase_2] = AI_Phase_2;

        // 스킬 그룹
        m_skillGroup = new MonsterSkillInfo[3, 3];
        // 근거리 잡기
        m_skillGroup[0, 0] = new MonsterSkillInfo(4, 100f, 0f);
        // 일반 1
        m_skillGroup[1, 0] = new MonsterSkillInfo(3, 15f, 0f);
        m_skillGroup[1, 1] = new MonsterSkillInfo(2, 25f, 0f);
        m_skillGroup[1, 2] = new MonsterSkillInfo(1, 100f, 0f);
        // 일반 2
        m_skillGroup[2, 0] = new MonsterSkillInfo(2, 15f, 0f);
        m_skillGroup[2, 1] = new MonsterSkillInfo(3, 25f, 0f);
        m_skillGroup[2, 2] = new MonsterSkillInfo(1, 100f, 0f);

        // 코루틴
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if (m_dead)
            return;

        //if (m_state == AsphaltGolemState.Run &&
        //    Vector3.Distance(m_targetEntity.transform.position, mTransform.position) <= m_attackDistance)
        //{
        //    //BeginAttack();
        //}

        // 애니메이션 업데이트
        m_animeUpdate[(int)m_state]();
    }

    private void FixedUpdate()
    {
        if (m_dead)
            return;

        // 회전
        Rotation();
    }

    private IEnumerator UpdatePath() 
    {
        // 1초
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (!m_dead)
        {
            // 시간 감소
            // ...

            // 타겟 검사
            CheckTarget();

            // 하나의 행동이 끝나면 다음 행동을 받음
            if (m_isActionEnd)
                UpdateAI();

            if (m_hasTarget)
            {
                //// 정찰 상태면 추적 상태로
                //if (m_state == AsphaltGolemState.Idle)
                //{
                //    m_state = AsphaltGolemState.Run;
                //}

                //m_agent.SetDestination(m_targetEntity.transform.position);
            }
            else
            {
                /*
                if (m_targetEntity != null) m_targetEntity = null;

                // 정찰 상태가 아니면 정찰 상태로
                if (m_state != AsphaltGolemState.Run)
                {
                    m_state = AsphaltGolemState.Run;
                }

                if (m_agent.remainingDistance <= 3.0f)
                {
                    var patrolTargetPosition = Utility.GetRandomPointOnNavMesh(mTransform.position, 20.0f, NavMesh.AllAreas);
                    m_agent.SetDestination(patrolTargetPosition);
                }

                // 시야 내 콜라이더들을 가져옴
                var colliders = Physics.OverlapSphere(m_eyeTrasform.position, m_viewDistance, m_whatIsTarget);

                foreach (var collider in colliders)
                {
                    // 시야에 존재하는지 확인
                    if (!IsTargetOnSight(collider.transform))
                    {
                        continue;
                    }

                    var livingEntity = collider.GetComponent<LivingEntity>();

                    if (livingEntity != null && !livingEntity.m_dead)
                    {
                        m_targetEntity = livingEntity;
                        break;
                    }
                }
                */
            }

            yield return wait;
        }
    }


    private void Rotation()
    {
        if (!m_hasTarget || !m_isTargetFollow)
            return;

        Vector3 targetDir = m_targetEntity.transform.position - m_monsterTransform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        m_monsterTransform.rotation = Quaternion.Lerp(m_monsterTransform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
    }

    private void CheckTarget()
    {
        if (!m_hasTarget)
            return;

        // 범위 내 플레이어 콜라이더들을 가져옴
        var colliders = Physics.OverlapSphere(m_eyeTrasform.position, m_viewDistance, m_whatIsTarget);

        foreach (var collider in colliders)
        {
            var livingEntity = collider.GetComponent<LivingEntity>();

            if (livingEntity != null && !livingEntity.m_dead)
            {
                m_targetEntity = livingEntity;
                break;
            }
        }
    }

    private bool IsTargetOnSight(Transform m_target)
    {
        var direction = m_target.position - m_eyeTrasform.position;
        direction.y = m_eyeTrasform.forward.y;

        if (Vector3.Angle(direction, m_eyeTrasform.forward) > m_fieldOfView * 0.5f)
            return false;

        //direction = m_target.position - m_eyeTrasform.position;

        RaycastHit hit;

        if (Physics.Raycast(m_eyeTrasform.position, direction, out hit, m_viewDistance, m_whatIsTarget))
        {
            if (hit.transform == m_target)
                return true;
        }
        
        return false;
    }

    private void UpdateAI()
    {
        // 타겟
        if (m_hasTarget)
            m_target = Target.Yes;
        else
            m_target = Target.No;

        // 페이즈
        if (m_startingHealth * 0.5 <= m_health)
            m_phase = Phase.Phase_1;
        else
            m_phase = Phase.Phase_2;

        // AI 처리
        m_monsterAI[(int)m_target, (int)m_phase]();
    }

    private void AI_NoTarget()
    {
        
    }

    private void AI_Phase_1()
    {
        int skill;

        // 타겟과의 거리
        var direction = m_targetEntity.transform.position - m_eyeTrasform.position;
        direction.y = m_eyeTrasform.forward.y;

        float rand = Random.Range(0f, 99f);

        // 이동
        if (direction.magnitude > m_meleeDistance)
        {
            m_state = AsphaltGolemState.Run;
            m_isActionEnd = false;
            return;
        }
        // 근거리 잡기
        else if (direction.magnitude <= m_meleeDistance && rand < 30f)
        {
            SkillCondition(ref m_skillGroup, 0, out skill);
        }
        // 일반 2
        else if (rand < 60f)
        {
            SkillCondition(ref m_skillGroup, 2, out skill);
        }
        // 일반 1
        else
        {
            SkillCondition(ref m_skillGroup, 1, out skill);
        }

        // 공격 상태로 전환
        FSM_Skill(skill);
    }

    private void AI_Phase_2()
    {

    }

    void FSM_Skill(int skill)
    {
        switch (skill)
        {
            case 1: // 어퍼
                m_state = AsphaltGolemState.A_Skill_01;
                SetTrigerASkill_01();
                return;
            case 2: // 광역 내려찍기
                m_state = AsphaltGolemState.A_Skill_02;
                SetTrigerASkill_02();
                return;
            case 3: // 방패 기둥 꺼내서 전방 충격파, 찍뎀 + 충격파 스킬
                m_state = AsphaltGolemState.A_Skill_03;
                SetTrigerASkill_03();
                return;
            case 4: // 잡기
                return;
            default:
                // 없음
                return;

        }
    }
}
