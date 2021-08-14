﻿using UnityEngine;

using System;
using MyEnum;
using MyStruct;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public partial class PlayerCtrl : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInfo playerInfo;           // 플레이어 정보
    public HaruWeapon weapon;
    public float moveSpeed = 5.0f;          // 이동 속도
    public float mouseSpeed = 100.0f;       // 마우스 속도
    public float rotationSpeed = 10.0f;     // 회전 속도
    public float jumpVelocity = 5.0f;       // 점프력
    [Range(0.01f, 1.0f)] public float airControlPercent;
    public float speedSmoothTime = 0.1f;
    // 캐릭터 컬라이더가 실제 움직인 거리
    public float currentSpeed =>
        new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

    public Transform modelTransform;
    public Transform cameraTransform;
    public Transform weaponholder;
    public Transform aimTransform;
    public Animator hairAnime;
    public Animator faceAnime;
    public Animator bodyAnime;
    public Animator pantsAnime;
    public Animator handsAnime;
    public Animator footAnime;

    private CharacterController characterController;
    private Transform playerTransform;
    public HaruState state { get; protected set; }    // 상태
    private Vector2 moveInput = Vector2.zero;
    private Vector2 oldInput = Vector2.zero;            // 대쉬 점프할 때 입력 값 저장
    private Vector3 moveAnimeDir = Vector3.zero;
    private Vector3 turnDir = Vector3.forward;

    private bool[,] changeState;
    private Action[] moveUpdate;
    private Action[] animeUpdate;
    private float speedSmoothVelocity;
    private float currentVelocityY;
    private float targetSpeed;          // SmoothDamp가 적용된 이동 속도
    private float fsmChangeTime;        // 상태가 변경되는 순간의 시간
    private bool jump;                  // 점프 상태 플래그
    private bool dash;                  // 대쉬 상태 플래그
    private bool upp;                   // 어퍼 상태 플래그
    private bool down;                  // 다운 상태 플래그
    private bool normalAtk;
    private bool lockInput;
    private bool moveAttack;            // 공격시 전진 플래그
    private bool moveStand;             // 공격후 제자리 플래그
    private bool isAttacking;           // 공격 트리거 on off 플래그
    private bool cameraDirAtk = false;          // 카메라 방향으로 공격 플래그


    public void FSM_Hit(ref DamageMessage damageMessage)
    {
        // 조작 플래그 초기화
        if (normalAtk || lockInput)
        {
            normalAtk = false;
            lockInput = false;
            moveAttack = false;
            moveStand = false;
            SetNormalAttackFalse();
            SetNormalAttackCnt(0);
            if (isAttacking)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
        }

        // 공중에 뜸
        if (upp)
        {
            // 공중 피격
            state = HaruState.KD_Upp_Air_Hit;
            SetTrigerKDUppAirHit();
            moveAnimeDir = damageMessage.hitDir;
            turnDir = -moveAnimeDir;
            return;
        }
        // 이미 다운된 상태
        else if (down)
        {
            // 다운 피격
            state = HaruState.KD_Upp_Down_Hit;
            SetTrigerKDUppDownHit();
            return;
        }

        switch (damageMessage.attackType)
        {
            case AttackType.Normal:
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    state = HaruState.DMG_L;
                    // 애니메이션 트리거
                    SetTrigerDMGL();
                }
                else
                {
                    state = HaruState.DMG_R;
                    // 애니메이션 트리거
                    SetTrigerDMGR();
                }
                turnDir = -damageMessage.hitDir;
                break;
            case AttackType.Upper:
                state = HaruState.KD_Upp;
                currentVelocityY = damageMessage.power;
                moveAnimeDir = damageMessage.hitDir;
                turnDir = -moveAnimeDir;
                break;
            case AttackType.Down:
                // 0 보다 크면 뒤쪽에서 맞음
                if (0 < Vector3.Dot(modelTransform.forward, damageMessage.hitDir))
                {
                    state = HaruState.KD_Ham_B;
                    turnDir = damageMessage.hitDir;
                }
                else
                {
                    state = HaruState.KD_Ham_F;
                    turnDir = -damageMessage.hitDir;
                }

                currentVelocityY = damageMessage.power;
                moveAnimeDir = damageMessage.hitDir;
                break;
            case AttackType.Break:
                break;
            case AttackType.Strike:
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        state = HaruState.Idle;

        moveUpdate = new Action[(int)HaruState.End];
        moveUpdate[(int)HaruState.Idle] = Move_Idle;
        moveUpdate[(int)HaruState.Run] = Move_Run;
        moveUpdate[(int)HaruState.Dash] = Move_Dash;
        moveUpdate[(int)HaruState.Jump] = Move_Jump;
        moveUpdate[(int)HaruState.DashJump] = Move_DashJump;
        moveUpdate[(int)HaruState.Land] = Move_Land;
        moveUpdate[(int)HaruState.DashLand] = Move_DashLand;
        moveUpdate[(int)HaruState.Evade] = Move_Evade;
        moveUpdate[(int)HaruState.DMG_L] = Move_Idle;
        moveUpdate[(int)HaruState.DMG_R] = Move_Idle;
        moveUpdate[(int)HaruState.KB] = Move_Idle;
        moveUpdate[(int)HaruState.KD_Ham_F] = Move_KD_Ham_F;
        moveUpdate[(int)HaruState.KD_Ham_B] = Move_KD_Ham_B;
        moveUpdate[(int)HaruState.KD_Str] = Move_KD_Str;
        moveUpdate[(int)HaruState.KD_Upp] = Move_KD_Upp;
        moveUpdate[(int)HaruState.KD_Upp_End] = Move_Idle;
        moveUpdate[(int)HaruState.KD_Upp_Down] = Move_Idle;
        moveUpdate[(int)HaruState.KD_Upp_Air_Hit] = Move_Idle;
        moveUpdate[(int)HaruState.KD_Upp_Down_Hit] = Move_Idle;
        moveUpdate[(int)HaruState.KD_Upp_Raise] = Move_Idle;
        moveUpdate[(int)HaruState.NormalAttack1] = Move_NormalAttack1;
        moveUpdate[(int)HaruState.NormalAttack2] = Move_NormalAttack2;
        moveUpdate[(int)HaruState.NormalAttack3] = Move_NormalAttack3;
        moveUpdate[(int)HaruState.NormalAttack4] = Move_NormalAttack4;
        moveUpdate[(int)HaruState.NormalAttack5] = Move_NormalAttack5;

        animeUpdate = new Action[(int)HaruState.End];
        animeUpdate[(int)HaruState.Idle] = Ani_Idle;
        animeUpdate[(int)HaruState.Run] = Ani_Run;
        animeUpdate[(int)HaruState.Dash] = Ani_Dash;
        animeUpdate[(int)HaruState.Jump] = Ani_Jump;
        animeUpdate[(int)HaruState.DashJump] = Ani_DashJump;
        animeUpdate[(int)HaruState.Land] = Ani_Land;
        animeUpdate[(int)HaruState.DashLand] = Ani_DashLand;
        animeUpdate[(int)HaruState.Evade] = Ani_Evade;
        animeUpdate[(int)HaruState.DMG_L] = Ani_DMG_L;
        animeUpdate[(int)HaruState.DMG_R] = Ani_DMG_R;
        animeUpdate[(int)HaruState.KB] = Ani_KB;
        animeUpdate[(int)HaruState.KD_Ham_F] = Ani_KD_Ham_F;
        animeUpdate[(int)HaruState.KD_Ham_B] = Ani_KD_Ham_B;
        animeUpdate[(int)HaruState.KD_Str] = Ani_KD_Str;
        animeUpdate[(int)HaruState.KD_Upp] = Ani_KD_Upp;
        animeUpdate[(int)HaruState.KD_Upp_End] = Ani_KD_Upp_End;
        animeUpdate[(int)HaruState.KD_Upp_Down] = Ani_KD_Upp_Down;
        animeUpdate[(int)HaruState.KD_Upp_Air_Hit] = Ani_KD_Upp_Air_Hit;
        animeUpdate[(int)HaruState.KD_Upp_Down_Hit] = Ani_KD_Upp_Down_Hit;
        animeUpdate[(int)HaruState.KD_Upp_Raise] = Ani_KD_Upp_Raise;
        animeUpdate[(int)HaruState.NormalAttack1] = Ani_NormalAttack1;
        animeUpdate[(int)HaruState.NormalAttack2] = Ani_NormalAttack2;
        animeUpdate[(int)HaruState.NormalAttack3] = Ani_NormalAttack3;
        animeUpdate[(int)HaruState.NormalAttack4] = Ani_NormalAttack4;
        animeUpdate[(int)HaruState.NormalAttack5] = Ani_NormalAttack5;

        changeState = new bool[(int)HaruState.End, (int)HaruState.End];

        changeState[(int)HaruState.Idle, (int)HaruState.Run] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.Jump] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.NormalAttack1] = true;

        changeState[(int)HaruState.Run, (int)HaruState.Idle] = true;
        changeState[(int)HaruState.Run, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Run, (int)HaruState.Jump] = true;
        changeState[(int)HaruState.Run, (int)HaruState.NormalAttack1] = true;

        changeState[(int)HaruState.Dash, (int)HaruState.Idle] = true;
        changeState[(int)HaruState.Dash, (int)HaruState.DashJump] = true;

        changeState[(int)HaruState.Jump, (int)HaruState.Land] = true;

        changeState[(int)HaruState.DashJump, (int)HaruState.DashLand] = true;

        changeState[(int)HaruState.Land, (int)HaruState.Run] = true;
        changeState[(int)HaruState.Land, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Land, (int)HaruState.Jump] = true;

        changeState[(int)HaruState.DashLand, (int)HaruState.Run] = true;

        changeState[(int)HaruState.KD_Upp_Down, (int)HaruState.Run] = true;

        changeState[(int)HaruState.NormalAttack1, (int)HaruState.NormalAttack2] = true;
        changeState[(int)HaruState.NormalAttack1, (int)HaruState.Run] = true;
        changeState[(int)HaruState.NormalAttack1, (int)HaruState.Evade] = true;

        changeState[(int)HaruState.NormalAttack2, (int)HaruState.NormalAttack3] = true;
        changeState[(int)HaruState.NormalAttack2, (int)HaruState.Run] = true;
        changeState[(int)HaruState.NormalAttack2, (int)HaruState.Evade] = true;

        changeState[(int)HaruState.NormalAttack3, (int)HaruState.NormalAttack4] = true;
        changeState[(int)HaruState.NormalAttack3, (int)HaruState.Run] = true;
        changeState[(int)HaruState.NormalAttack3, (int)HaruState.Evade] = true;

        changeState[(int)HaruState.NormalAttack4, (int)HaruState.NormalAttack5] = true;
        changeState[(int)HaruState.NormalAttack4, (int)HaruState.Run] = true;
        changeState[(int)HaruState.NormalAttack4, (int)HaruState.Evade] = true;

        changeState[(int)HaruState.NormalAttack5, (int)HaruState.Run] = true;
        changeState[(int)HaruState.NormalAttack5, (int)HaruState.Evade] = true;

        // 무기 장착
        weapon.transform.parent = weaponholder;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.transform.localScale = Vector3.one;

        playerInfo.UpdateInfo();
    }

    private void FixedUpdate()
    {
        // 이동 업데이트
        moveUpdate[(int)state]();
        PlayerRotation();
        AimRotation();
    }

    private void Update()
    {
        // 애니메이션 업데이트
        animeUpdate[(int)state]();
    }


    // 상태 확인
    private bool CheckState(HaruState left, HaruState right)
    {
        return changeState[(int)left, (int)right];
    }

    // 이동
    private void Move()
    {
        // 착지
        if (state == HaruState.Land)
            return;

        // 방향키 입력이 없음
        if (moveInput == Vector2.zero && !jump)
        {
            characterController.Move(Vector3.down);
            return;
        }

        float smoothTime = characterController.isGrounded ? speedSmoothTime : speedSmoothTime / airControlPercent;
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, smoothTime);
        
        Vector3 moveDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void PlayerRotation()
    {
        // 방향이 같지 않음
        if (modelTransform.forward != turnDir)
        {
            Quaternion rotation = Quaternion.LookRotation(turnDir);
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    // 에임 회전
    private void AimRotation()
    {
        float yRotation = Input.GetAxis("Mouse X");
        float xRotation = Input.GetAxis("Mouse Y");

        // X축 회전 제한
        Quaternion rotation = aimTransform.rotation * Quaternion.Euler(new Vector3(-xRotation * mouseSpeed * Time.deltaTime, 0, 0));
        float xAngle = Mathf.Round(rotation.eulerAngles.x);
        if (180.0f <= xAngle && xAngle < 310.0f)
        {
            xAngle = 310.0f;
        }
        else if (180.0f > xAngle && xAngle > 50.0f)
        {
            xAngle = 50.0f;
        }

        aimTransform.rotation = Quaternion.Euler(new Vector3(xAngle, aimTransform.eulerAngles.y, aimTransform.eulerAngles.x));

        // Y축 자전
        aimTransform.RotateAround(playerTransform.position, Vector3.up, yRotation * mouseSpeed * Time.deltaTime);
    }
}
