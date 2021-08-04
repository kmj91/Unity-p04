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

    [SerializeField] private MonsterInfo monsterInfo;       // 몬스터 정보
    [SerializeField] private Animator bodyAnime;            // 몸 애니메이터
    [SerializeField] private Animator armsAnime;            // 팔 애니메이터
    [SerializeField] private BoxCollider rightBoxCollider;  // 오른팔 충돌체
    [SerializeField] private BoxCollider leftBoxCollider;   // 왼팔 충돌체
    public float moveSpeed = 3.0f;          // 이동 속도
    public float rotationSpeed = 3.0f;      // 회전 속도
    public float damage = 100.0f;           // 공격력
    public LivingEntity targetEntity;       // 추적 대상
    public LayerMask whatIsTarget;

    public Transform attackRoot;
    public Transform eyeTrasform;
    public float attackRadius = 1.3f;
    public float attackDistance;
    public float fieldOfView = 50.0f;
    public float viewDistance = 10.0f;
    public float patrolSpeed = 2.0f;
    public float meleeDistance = 4f;
    
    private Transform monsterTransform;
    private AsphaltGolemState state = AsphaltGolemState.Idle;   // 상태
    private NavMeshAgent agent;
    private LayerMask mask;
    private RaycastHit[] hits = new RaycastHit[10];
    private List<LivingEntity> lastAttackdTargts = new List<LivingEntity>();
    private bool hasTarget => targetEntity != null && !targetEntity.dead;

    private MonsterSkillInfo[,] skillGroup;
    private Action[] animeUpdate;
    private Action[,] monsterAI;
    private Target target = Target.End;
    private Phase phase = Phase.End;
    private bool isSuperArmourBreak = true;
    private bool isActionEnd = true;
    private bool isTargetFollow = true;



    private enum Target { No, Yes, End }            // 타겟 - 타겟이 있는가
    private enum Phase { Phase_1, Phase_2, End }    // 페이즈 - 일정 체력을 잃으면 페이즈 전환


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(attackRoot.position, attackRadius);
        }

        if (eyeTrasform != null)
        {

            var leftEyeRotation = Quaternion.AngleAxis(-fieldOfView * 0.5f, Vector3.up);
            var leftRayDirection = leftEyeRotation * transform.forward;
            Handles.color = new Color(1f, 0f, 0f, 0.2f);
            Handles.DrawSolidArc(eyeTrasform.position, Vector3.up, leftRayDirection, fieldOfView, viewDistance);


            Handles.color = new Color(1f, 0f, 0f, 0.2f);
            Handles.DrawSolidDisc(eyeTrasform.position, Vector3.up, meleeDistance);

            Handles.color = new Color(1f, 1f, 1f, 0.2f);
            Handles.DrawSolidDisc(eyeTrasform.position, Vector3.up, viewDistance);
        }
    }
#endif


    private void Awake()
    {
        monsterTransform = transform;
        agent = GetComponent<NavMeshAgent>();

        //var attackPivot = attackRoot.position;
        //attackPivot.y = monsterTransform.position.y;
        //attackDistance = Vector3.Distance(monsterTransform.position, attackPivot) + attackRadius;

        agent.stoppingDistance = meleeDistance - 1f;
        agent.speed = patrolSpeed;

        // 플레이어와 충돌
        mask = LayerMask.NameToLayer("Player");

        // 몬스터 정보 초기화
        MonsterInfoInit();
    }

    private void Start()
    {
        animeUpdate = new Action[(int)AsphaltGolemState.End];
        animeUpdate[(int)AsphaltGolemState.Idle] = Ani_Idle;
        animeUpdate[(int)AsphaltGolemState.Run] = Ani_Run;
        animeUpdate[(int)AsphaltGolemState.DMG_L] = Ani_DMG_L;
        animeUpdate[(int)AsphaltGolemState.DMG_R] = Ani_DMG_R;
        animeUpdate[(int)AsphaltGolemState.KB] = Ani_KB;
        animeUpdate[(int)AsphaltGolemState.KD_Ham] = Ani_KD_Ham;
        animeUpdate[(int)AsphaltGolemState.KD_Str] = Ani_KD_Str;
        animeUpdate[(int)AsphaltGolemState.KD_Upp] = Ani_KD_Upp;
        animeUpdate[(int)AsphaltGolemState.Death] = Ani_Death;
        animeUpdate[(int)AsphaltGolemState.A_Skill_01] = Ani_A_Skill_01;
        animeUpdate[(int)AsphaltGolemState.A_Skill_01] = Ani_A_Skill_01;
        animeUpdate[(int)AsphaltGolemState.A_Skill_02] = Ani_A_Skill_02;
        animeUpdate[(int)AsphaltGolemState.A_Skill_03] = Ani_A_Skill_03;

        monsterAI = new Action[(int)Target.End, (int)Phase.End];
        monsterAI[(int)Target.No, (int)Phase.Phase_1] = AI_NoTarget;
        monsterAI[(int)Target.No, (int)Phase.Phase_2] = AI_NoTarget;
        monsterAI[(int)Target.Yes, (int)Phase.Phase_1] = AI_Phase_1;
        monsterAI[(int)Target.Yes, (int)Phase.Phase_2] = AI_Phase_2;

        // 스킬 그룹
        skillGroup = new MonsterSkillInfo[3, 3];
        // 근거리 잡기
        skillGroup[0, 0] = new MonsterSkillInfo(4, 100f, 0f);
        // 일반 1
        skillGroup[1, 0] = new MonsterSkillInfo(3, 15f, 0f);
        skillGroup[1, 1] = new MonsterSkillInfo(2, 25f, 0f);
        skillGroup[1, 2] = new MonsterSkillInfo(1, 100f, 0f);
        // 일반 2
        skillGroup[2, 0] = new MonsterSkillInfo(2, 15f, 0f);
        skillGroup[2, 1] = new MonsterSkillInfo(3, 25f, 0f);
        skillGroup[2, 2] = new MonsterSkillInfo(1, 100f, 0f);

        // 코루틴
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if (dead)
            return;

        //if (state == AsphaltGolemState.Run &&
        //    Vector3.Distance(targetEntity.transform.position, mTransform.position) <= attackDistance)
        //{
        //    //BeginAttack();
        //}

        // 애니메이션 업데이트
        animeUpdate[(int)state]();
    }

    private void FixedUpdate()
    {
        if (dead)
            return;

        // 회전
        Rotation();
    }

    private IEnumerator UpdatePath() 
    {
        // 1초
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (!dead)
        {
            // 시간 감소
            // ...

            // 타겟 검사
            CheckTarget();

            // 하나의 행동이 끝나면 다음 행동을 받음
            if (isActionEnd)
                UpdateAI();

            if (hasTarget)
            {
                //// 정찰 상태면 추적 상태로
                //if (state == AsphaltGolemState.Idle)
                //{
                //    state = AsphaltGolemState.Run;
                //}

                //agent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                /*
                if (targetEntity != null) targetEntity = null;

                // 정찰 상태가 아니면 정찰 상태로
                if (state != AsphaltGolemState.Run)
                {
                    state = AsphaltGolemState.Run;
                }

                if (agent.remainingDistance <= 3.0f)
                {
                    var patrolTargetPosition = Utility.GetRandomPointOnNavMesh(mTransform.position, 20.0f, NavMesh.AllAreas);
                    agent.SetDestination(patrolTargetPosition);
                }

                // 시야 내 콜라이더들을 가져옴
                var colliders = Physics.OverlapSphere(eyeTrasform.position, viewDistance, whatIsTarget);

                foreach (var collider in colliders)
                {
                    // 시야에 존재하는지 확인
                    if (!IsTargetOnSight(collider.transform))
                    {
                        continue;
                    }

                    var livingEntity = collider.GetComponent<LivingEntity>();

                    if (livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;
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
        if (!hasTarget || !isTargetFollow)
            return;

        Vector3 targetDir = targetEntity.transform.position - monsterTransform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        monsterTransform.rotation = Quaternion.Lerp(monsterTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void CheckTarget()
    {
        if (!hasTarget)
            return;

        // 범위 내 플레이어 콜라이더들을 가져옴
        var colliders = Physics.OverlapSphere(eyeTrasform.position, viewDistance, whatIsTarget);

        foreach (var collider in colliders)
        {
            var livingEntity = collider.GetComponent<LivingEntity>();

            if (livingEntity != null && !livingEntity.dead)
            {
                targetEntity = livingEntity;
                break;
            }
        }
    }

    private bool IsTargetOnSight(Transform target)
    {
        var direction = target.position - eyeTrasform.position;
        direction.y = eyeTrasform.forward.y;

        if (Vector3.Angle(direction, eyeTrasform.forward) > fieldOfView * 0.5f)
            return false;

        //direction = target.position - eyeTrasform.position;

        RaycastHit hit;

        if (Physics.Raycast(eyeTrasform.position, direction, out hit, viewDistance, whatIsTarget))
        {
            if (hit.transform == target)
                return true;
        }
        
        return false;
    }

    private void UpdateAI()
    {
        // 타겟
        if (hasTarget)
            target = Target.Yes;
        else
            target = Target.No;

        // 페이즈
        if (startingHealth * 0.5 <= health)
            phase = Phase.Phase_1;
        else
            phase = Phase.Phase_2;

        // AI 처리
        monsterAI[(int)target, (int)phase]();
    }

    private void AI_NoTarget()
    {
        
    }

    private void AI_Phase_1()
    {
        int skill;

        // 타겟과의 거리
        var direction = targetEntity.transform.position - eyeTrasform.position;
        direction.y = eyeTrasform.forward.y;

        float rand = Random.Range(0f, 99f);

        // 이동
        if (direction.magnitude > meleeDistance)
        {
            state = AsphaltGolemState.Run;
            isActionEnd = false;
            return;
        }
        // 근거리 잡기
        else if (direction.magnitude <= meleeDistance && rand < 30f)
        {
            SkillCondition(ref skillGroup, 0, out skill);
        }
        // 일반 2
        else if (rand < 60f)
        {
            SkillCondition(ref skillGroup, 2, out skill);
        }
        // 일반 1
        else
        {
            SkillCondition(ref skillGroup, 1, out skill);
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
                state = AsphaltGolemState.A_Skill_01;
                SetTrigerASkill_01();
                return;
            case 2: // 광역 내려찍기
                state = AsphaltGolemState.A_Skill_02;
                SetTrigerASkill_02();
                return;
            case 3: // 방패 기둥 꺼내서 전방 충격파, 찍뎀 + 충격파 스킬
                state = AsphaltGolemState.A_Skill_03;
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
