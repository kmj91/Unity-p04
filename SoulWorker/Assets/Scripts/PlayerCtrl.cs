using UnityEngine;

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
    public HaruInfo playerInfo;             // 플레이어 정보
    public ItemHaruWeapon weapon;
    public float moveSpeed = 5.0f;          // 이동 속도
    public float dashSpeedGob = 2.0f;       // 대쉬 속도 = moveSpeed * dashSpeedGob
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
    public bool evade { get; protected set; }   // 회피 상태 플래그
    private bool normalAtk;
    private bool lockInput;
    private bool moveAttack;            // 공격시 전진 플래그
    private bool moveStand;             // 공격후 제자리 플래그
    private bool isAttacking;           // 공격 트리거 on off 플래그
    private bool cameraDirAtk = false;          // 카메라 방향으로 공격 플래그
    private bool isUIObject = false;


    public void FSM_Hit(ref DamageMessage damageMessage)
    {
        // 공중에 뜸
        if (upp)
        {
            // 공중 피격
            ChangeFlagFalse();
            state = HaruState.KD_Upp_Air_Hit;
            ChangeFlagTrue();
            moveAnimeDir = damageMessage.hitDir;
            turnDir = -moveAnimeDir;
            return;
        }
        // 이미 다운된 상태
        else if (down)
        {
            // 다운 피격
            ChangeFlagFalse();
            state = HaruState.KD_Upp_Down_Hit;
            ChangeFlagTrue();
            return;
        }

        switch (damageMessage.attackType)
        {
            case AttackType.Normal:
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    ChangeFlagFalse();
                    state = HaruState.DMG_L;
                    ChangeFlagTrue();
                }
                else
                {
                    ChangeFlagFalse();
                    state = HaruState.DMG_R;
                    ChangeFlagTrue();
                }
                turnDir = -damageMessage.hitDir;
                break;
            case AttackType.Upper:
                ChangeFlagFalse();
                state = HaruState.KD_Upp;
                ChangeFlagTrue();

                fsmChangeTime = Time.realtimeSinceStartup;
                currentVelocityY = damageMessage.power;
                moveAnimeDir = damageMessage.hitDir;
                turnDir = -moveAnimeDir;
                break;
            case AttackType.Down:
                // 0 보다 크면 뒤쪽에서 맞음
                if (0 < Vector3.Dot(modelTransform.forward, damageMessage.hitDir))
                {
                    ChangeFlagFalse();
                    state = HaruState.KD_Ham_B;
                    ChangeFlagTrue();

                    fsmChangeTime = Time.realtimeSinceStartup;
                    turnDir = damageMessage.hitDir;
                }
                else
                {
                    ChangeFlagFalse();
                    state = HaruState.KD_Ham_F;
                    ChangeFlagTrue();

                    fsmChangeTime = Time.realtimeSinceStartup;
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

    // 캐릭터 정보창에서 보여지는 경우
    public void SetUIObject()
    {
        // Update 처리 X
        isUIObject = true;
        // N_Stand 상태
        SetTrigerNormalStand();
        // 무기 착용 X
        weapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!isUIObject)
            return;

        // N_Stand 상태
        SetTrigerNormalStand();
        // 무기 착용 X
        weapon.gameObject.SetActive(false);
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
        moveUpdate[(int)HaruState.FirstBlade] = Move_FirstBlade;
        moveUpdate[(int)HaruState.FirstBlade02] = Move_FirstBlade02;
        moveUpdate[(int)HaruState.PierceStep] = Move_PierceStep;
        moveUpdate[(int)HaruState.SpinCutter] = Move_SpinCutter;

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
        animeUpdate[(int)HaruState.FirstBlade] = Ani_FirstBlade;
        animeUpdate[(int)HaruState.FirstBlade02] = Ani_FirstBlade02;
        animeUpdate[(int)HaruState.PierceStep] = Ani_PierceStep;
        animeUpdate[(int)HaruState.SpinCutter] = Ani_SpinCutter;

        changeState = new bool[(int)HaruState.End, (int)HaruState.End];

        changeState[(int)HaruState.Idle, (int)HaruState.Run] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.Jump] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.Evade] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.NormalAttack1] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.FirstBlade] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.PierceStep] = true;
        changeState[(int)HaruState.Idle, (int)HaruState.SpinCutter] = true;

        changeState[(int)HaruState.Run, (int)HaruState.Idle] = true;
        changeState[(int)HaruState.Run, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Run, (int)HaruState.Jump] = true;
        changeState[(int)HaruState.Run, (int)HaruState.Evade] = true;
        changeState[(int)HaruState.Run, (int)HaruState.NormalAttack1] = true;
        changeState[(int)HaruState.Run, (int)HaruState.FirstBlade] = true;
        changeState[(int)HaruState.Run, (int)HaruState.PierceStep] = true;
        changeState[(int)HaruState.Run, (int)HaruState.SpinCutter] = true;

        changeState[(int)HaruState.Dash, (int)HaruState.Idle] = true;
        changeState[(int)HaruState.Dash, (int)HaruState.DashJump] = true;

        changeState[(int)HaruState.Jump, (int)HaruState.Land] = true;

        changeState[(int)HaruState.DashJump, (int)HaruState.DashLand] = true;

        changeState[(int)HaruState.Land, (int)HaruState.Run] = true;
        changeState[(int)HaruState.Land, (int)HaruState.Dash] = true;
        changeState[(int)HaruState.Land, (int)HaruState.Jump] = true;

        changeState[(int)HaruState.DashLand, (int)HaruState.Run] = true;

        changeState[(int)HaruState.Evade, (int)HaruState.NormalAttack1] = true;

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

        changeState[(int)HaruState.FirstBlade, (int)HaruState.FirstBlade02] = true;
        changeState[(int)HaruState.FirstBlade, (int)HaruState.Run] = true;
        changeState[(int)HaruState.FirstBlade, (int)HaruState.Evade] = true;
        changeState[(int)HaruState.FirstBlade, (int)HaruState.NormalAttack1] = true;

        changeState[(int)HaruState.FirstBlade02, (int)HaruState.Run] = true;
        changeState[(int)HaruState.FirstBlade02, (int)HaruState.Evade] = true;
        changeState[(int)HaruState.FirstBlade02, (int)HaruState.NormalAttack1] = true;

        // 무기 장착
        weapon.transform.parent = weaponholder;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.transform.localScale = Vector3.one;

        playerInfo.UpdateInfo();
    }

    private void FixedUpdate()
    {
        if (isUIObject)
            return;

        // 이동 업데이트
        moveUpdate[(int)state]();
        PlayerRotation();
        AimRotation();
    }

    private void Update()
    {
        if (isUIObject)
            return;

        // 애니메이션 업데이트
        animeUpdate[(int)state]();
    }


    // 상태 확인
    private bool CheckState(HaruState left, HaruState right)
    {
        return changeState[(int)left, (int)right];
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

    // 현재 상태가 바뀌기 때문에
    // 이전 상태의 플래그들 false로 변경
    private void ChangeFlagFalse()
    {
        switch (state)
        {
            case HaruState.Idle:
                break;
            case HaruState.Run:
                break;
            case HaruState.Dash:
                dash = false;
                SetDashFalse();
                break;
            case HaruState.Jump:
                jump = false;
                SetJumpFalse();
                break;
            case HaruState.DashJump:
                dash = false;
                jump = false;
                SetDashFalse();
                SetJumpFalse();
                break;
            case HaruState.Land:
                lockInput = false;
                break;
            case HaruState.DashLand:
                lockInput = false;
                break;
            case HaruState.Evade:
                evade = false;
                lockInput = false;
                break;
            case HaruState.DMG_L:
                break;
            case HaruState.DMG_R:
                break;
            case HaruState.KB:
                break;
            case HaruState.KD_Ham_F:
                upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Ham_B:
                upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Str:
                break;
            case HaruState.KD_Upp:
                upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Upp_End:
                down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Down:
                down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Air_Hit:
                upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Upp_Down_Hit:
                down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Raise:
                down = false;
                SetDownFalse();
                break;
            case HaruState.NormalAttack1:
            case HaruState.NormalAttack2:
            case HaruState.NormalAttack3:
            case HaruState.NormalAttack4:
            case HaruState.NormalAttack5:
                normalAtk = false;
                lockInput = false;
                moveAttack = false;
                moveStand = false;
                SetTrigerNormalAttackEnd();
                SetNormalAttackCnt(0);
                if (isAttacking)
                {
                    isAttacking = false;
                    // 무기 충돌 트리거 OFF
                    weapon.OffTrigger();
                }
                break;
            case HaruState.FirstBlade:
            case HaruState.FirstBlade02:
                normalAtk = false;
                lockInput = false;
                moveAttack = false;
                moveStand = false;
                if (isAttacking)
                {
                    isAttacking = false;
                    // 무기 충돌 트리거 OFF
                    weapon.OffTrigger();
                }
                break;
            case HaruState.PierceStep:
                break;
            case HaruState.SpinCutter:
                break;
            default:
                break;
        }
    }

    // 현재 상태가 바뀌기 때문에
    // 이전 상태의 플래그들 true로 변경
    private void ChangeFlagTrue()
    {
        switch (state)
        {
            case HaruState.Idle:
                SetSpeedZero();
                break;
            case HaruState.Run:
                break;
            case HaruState.Dash:
                dash = true;
                SetDashTrue();
                break;
            case HaruState.Jump:
                jump = true;
                SetJumpTrue();
                break;
            case HaruState.DashJump:
                dash = true;
                jump = true;
                SetDashTrue();
                SetJumpTrue();
                break;
            case HaruState.Land:
                lockInput = true;
                SetSpeedZero();
                break;
            case HaruState.DashLand:
                lockInput = true;
                SetSpeedZero();
                break;
            case HaruState.Evade:
                evade = true;
                lockInput = true;
                SetTrigerEvade();
                break;
            case HaruState.DMG_L:
                SetTrigerDMGL();
                break;
            case HaruState.DMG_R:
                SetTrigerDMGR();
                break;
            case HaruState.KB:
                break;
            case HaruState.KD_Ham_F:
                upp = true;
                SetUppTrue();
                SetTrigerKDHamF();
                break;
            case HaruState.KD_Ham_B:
                upp = true;
                SetUppTrue();
                SetTrigerKDHamB();
                break;
            case HaruState.KD_Str:
                break;
            case HaruState.KD_Upp:
                upp = true;
                SetUppTrue();
                SetTrigerKDUpp();
                break;
            case HaruState.KD_Upp_End:
                down = true;
                SetDownTrue();
                SetTrigerKDUppEnd();
                break;
            case HaruState.KD_Upp_Down:
                down = true;
                SetDownTrue();
                break;
            case HaruState.KD_Upp_Air_Hit:
                upp = true;
                SetUppTrue();
                SetTrigerKDUppAirHit();
                break;
            case HaruState.KD_Upp_Down_Hit:
                down = true;
                SetDownTrue();
                SetTrigerKDUppDownHit();
                break;
            case HaruState.KD_Upp_Raise:
                SetTrigerKDUppRaise();
                break;
            case HaruState.NormalAttack1:
                SetTrigerNormalAttackStart();
                StartNormalAttack(1);
                break;
            case HaruState.NormalAttack2:
                SetTrigerNormalAttackStart();
                StartNormalAttack(2);
                break;
            case HaruState.NormalAttack3:
                SetTrigerNormalAttackStart();
                StartNormalAttack(3);
                break;
            case HaruState.NormalAttack4:
                SetTrigerNormalAttackStart();
                StartNormalAttack(4);
                break;
            case HaruState.NormalAttack5:
                SetTrigerNormalAttackStart();
                StartNormalAttack(5);
                break;
            case HaruState.FirstBlade:
                lockInput = true;
                moveAttack = true;
                moveStand = false;
                isAttacking = false;
                SetTrigerSkillFirstBlade();
                SetAttackDir();
                break;
            case HaruState.FirstBlade02:
                lockInput = true;
                moveAttack = true;
                moveStand = false;
                isAttacking = false;
                SetTrigerSkillFirstBlade02();
                SetAttackDir();
                break;
            case HaruState.PierceStep:
                lockInput = true;
                moveAttack = true;
                moveStand = false;
                isAttacking = false;
                SetTrigerSkillPierceStep();
                SetAttackDir();
                break;
            case HaruState.SpinCutter:
                lockInput = true;
                moveAttack = true;
                moveStand = false;
                isAttacking = false;
                SetTrigerSkillSpinCutter();
                SetAttackDir();
                break;
            default:
                break;
        }
    }

    private bool GetStateOfSkillSlot(int index, out HaruState state)
    {
        switch (playerInfo.skillSlot[index, 0])
        {
            case HaruSkill.FirstBlade:
                state = HaruState.FirstBlade;
                break;
            case HaruSkill.PierceStep:
                state = HaruState.PierceStep;
                break;
            case HaruSkill.SpinCutter:
                state = HaruState.SpinCutter;
                break;
            default:
                state = HaruState.End;
                return false;
        }

        return true;
    }
}
