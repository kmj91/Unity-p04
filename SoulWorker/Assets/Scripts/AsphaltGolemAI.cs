using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;
using UnityEngine.AI;
using UnityEditor.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AsphaltGolemAI : LivingEntity
{
    public Animator bodyAnime;      // 몸 애니메이터
    public Animator armsAnime;      // 팔 애니메이터
    public float moveSpeed = 3.0f;  // 이동 속도
    public float rotationSpeed = 10.0f;     // 회전 속도
    public float damage = 100.0f;   // 공격력
    [HideInInspector] public LivingEntity targetEntity;     // 추적 대상
    public LayerMask whatIsTarget;

    public Transform attackRoot;
    public Transform eyeTrasform;
    public float attackRadius = 2.0f;
    public float attackDistance;
    public float fieldOfView = 50.0f;
    public float viewDistance = 10.0f;
    public float patrolSpeed = 2.0f;

    private Transform mTransform;
    private AsphaltGolemState state = AsphaltGolemState.Idle;   // 상태
    private NavMeshAgent agent;
    private RaycastHit[] hits = new RaycastHit[10];
    private List<LivingEntity> lastAttackdTargts = new List<LivingEntity>();
    private bool hasTarget => targetEntity != null && !targetEntity.dead;


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackRoot != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawSphere(attackRoot.position, 2.0f);
        }

        if (eyeTrasform != null)
        {
            var leftEyeRotation = Quaternion.AngleAxis(-fieldOfView * 0.5f, Vector3.up);
            var leftRayDirection = leftEyeRotation * transform.forward;
            Handles.color = new Color(1f, 1f, 1f, 0.2f);
            Handles.DrawSolidArc(eyeTrasform.position, Vector3.up, leftRayDirection, fieldOfView, viewDistance);
        }
    }
#endif


    public void SetUp(float health, float damage, float runSpeed, float patrolSpeed, Color skinColor)
    {
        startingHealth = health;
        this.health = health;
        this.damage = damage;
        moveSpeed = runSpeed;
        this.patrolSpeed = patrolSpeed;

        agent.speed = patrolSpeed;
    }

    private void Awake()
    {
        mTransform = transform;
        agent = GetComponent<NavMeshAgent>();

        var attackPivot = attackRoot.position;
        attackPivot.y = mTransform.position.y;
        attackDistance = Vector3.Distance(mTransform.position, attackPivot) + attackRadius;

        agent.stoppingDistance = attackDistance;
        agent.speed = patrolSpeed;
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if (dead)
            return;

        if (state == AsphaltGolemState.Run &&
            Vector3.Distance(targetEntity.transform.position, mTransform.position) <= attackDistance)
        {
            //BeginAttack();
        }

        // 이동 애니메이션 처리
        float velocity = agent.desiredVelocity.magnitude;
        bodyAnime.SetFloat("Speed", velocity);
        armsAnime.SetFloat("Speed", velocity);
    }

    private void FixedUpdate()
    {
        if (dead)
            return;

        //if(state == AsphaltGolemState.)
    }

    private IEnumerator UpdatePath() 
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        while (!dead)
        {
            if (hasTarget)
            {
                agent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                if (targetEntity != null) targetEntity = null;

                if (agent.remainingDistance <= 1.0f)
                {
                    var patrolTargetPosition = Utility.GetRandomPointOnNavMesh(mTransform.position, 20.0f, NavMesh.AllAreas);
                    agent.SetDestination(patrolTargetPosition);
                    Debug.Log("이동");
                }
            }

            // 시야 내 콜라이더들을 가져옴
            var colliders = Physics.OverlapSphere(eyeTrasform.position, viewDistance, whatIsTarget);

            foreach (var collider in colliders)
            {
                // 시야에 존재하는지 확인
                if(!IsTargetOnSight(collider.transform))
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

            yield return wait;
        }
    }




    private bool IsTargetOnSight(Transform target)
    {
        RaycastHit hit;

        var direction = target.position - eyeTrasform.position;
        direction.y = eyeTrasform.forward.y;

        if (Vector3.Angle(direction, eyeTrasform.forward) > fieldOfView * 0.5f)
            return false;

        direction = target.position - eyeTrasform.position;

        if (Physics.Raycast(eyeTrasform.position, direction, out hit, viewDistance, whatIsTarget))
        {
            if (hit.transform == target)
                return true;
        }

        return false;
    }
}
