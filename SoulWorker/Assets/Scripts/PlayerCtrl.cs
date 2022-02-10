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
    public GameManager m_gameManager;       // 게임 매니저
    public HaruInfo m_playerInfo;           // 플레이어 정보
    public ItemHaruWeapon m_weapon;
    public float m_moveSpeed = 5.0f;        // 이동 속도
    public float m_dashSpeedGob = 2.0f;     // 대쉬 속도 = m_moveSpeed * m_dashSpeedGob
    public float m_mouseSpeed = 100.0f;     // 마우스 속도
    public float m_rotationSpeed = 10.0f;   // 회전 속도
    public float m_jumpVelocity = 10.0f;     // 점프력
    [Range(0.01f, 1.0f)] public float m_airControlPercent;
    public float m_speedSmoothTime = 0.1f;
    // 캐릭터 컬라이더가 실제 움직인 거리
    public float m_currentSpeed =>
        new Vector2(m_characterController.velocity.x, m_characterController.velocity.z).magnitude;

    public Transform m_modelTransform;
    public Transform m_cameraTransform;
    public Transform m_weaponholder;
    public Transform m_aimTransform;
    public Animator m_hairAnime;
    public Animator m_faceAnime;
    public Animator m_bodyAnime;
    public Animator m_pantsAnime;
    public Animator m_handsAnime;
    public Animator m_footAnime;

    private CharacterController m_characterController;
    private Transform m_playerTransform;
    public HaruState m_state { get; protected set; }    // 상태
    private Vector2 m_moveInput = Vector2.zero;
    private Vector2 m_oldInput = Vector2.zero;          // 대쉬 점프할 때 입력 값 저장
    private Vector3 m_moveAnimeDir = Vector3.zero;
    private Vector3 m_turnDir = Vector3.forward;

    private bool[,] m_changeState;
    private Action[] m_moveUpdate;
    private Action[] m_animeUpdate;
    private float m_speedSmoothVelocity;
    private float m_currentVelocityY;
    private float m_targetSpeed;        // SmoothDamp가 적용된 이동 속도
    private float m_lastJumpTime;       // 상태가 점프로 변경된 시간

    private const float m_idleChangeDelay = 0.1f;   // 0.1초 정도 대기후 변환
    private float lastIdleChangeTime;   // 상태가 대기로 변경된 시간
    
    private bool m_jump;                // 점프 상태 플래그
    private bool m_dash;                // 대쉬 상태 플래그
    private bool m_upp;                 // 어퍼 상태 플래그
    private bool m_down;                // 다운 상태 플래그
    public bool m_evade { get; protected set; }     // 회피 상태 플래그
    private bool m_normalAtk;
    private bool m_lockInput;
    private bool m_moveAttack;          // 공격시 전진 플래그
    private bool m_moveStand;           // 공격후 제자리 플래그
    private bool m_isAttacking;         // 공격 트리거 on off 플래그
    private bool m_cameraDirAtk = false;        // 카메라 방향으로 공격 플래그
    private bool m_isUIObject = false;


    public void FSM_Hit(ref DamageMessage damageMessage)
    {
        // 공중에 뜸
        if (m_upp)
        {
            // 공중 피격
            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_Air_Hit;
            ChangeFlagTrue();
            m_moveAnimeDir = damageMessage.hitDir;
            m_turnDir = -m_moveAnimeDir;
            return;
        }
        // 이미 다운된 상태
        else if (m_down)
        {
            // 다운 피격
            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_Down_Hit;
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
                    m_state = HaruState.DMG_L;
                    ChangeFlagTrue();
                }
                else
                {
                    ChangeFlagFalse();
                    m_state = HaruState.DMG_R;
                    ChangeFlagTrue();
                }
                m_turnDir = -damageMessage.hitDir;
                break;
            case AttackType.Upper:
                ChangeFlagFalse();
                m_state = HaruState.KD_Upp;
                ChangeFlagTrue();

                m_lastJumpTime = Time.time;
                m_currentVelocityY = damageMessage.power;
                m_moveAnimeDir = damageMessage.hitDir;
                m_turnDir = -m_moveAnimeDir;
                break;
            case AttackType.Down:
                // 0 보다 크면 뒤쪽에서 맞음
                if (0 < Vector3.Dot(m_modelTransform.forward, damageMessage.hitDir))
                {
                    ChangeFlagFalse();
                    m_state = HaruState.KD_Ham_B;
                    ChangeFlagTrue();

                    m_lastJumpTime = Time.time;
                    m_turnDir = damageMessage.hitDir;
                }
                else
                {
                    ChangeFlagFalse();
                    m_state = HaruState.KD_Ham_F;
                    ChangeFlagTrue();

                    m_lastJumpTime = Time.time;
                    m_turnDir = -damageMessage.hitDir;
                }

                m_currentVelocityY = damageMessage.power;
                m_moveAnimeDir = damageMessage.hitDir;
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
        m_isUIObject = true;
        // N_Stand 상태
        SetTrigerNormalStand();
        // 무기 착용 X
        m_weapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!m_isUIObject)
            return;

        // N_Stand 상태
        SetTrigerNormalStand();
        // 무기 착용 X
        m_weapon.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_playerTransform = GetComponent<Transform>();
        m_characterController = GetComponent<CharacterController>();
        m_state = HaruState.Idle;

        m_moveUpdate = new Action[(int)HaruState.End];
        m_moveUpdate[(int)HaruState.Idle] = Move_Idle;
        m_moveUpdate[(int)HaruState.Run] = Move_Run;
        m_moveUpdate[(int)HaruState.RunEnd] = Move_RunEnd;
        m_moveUpdate[(int)HaruState.Dash] = Move_Dash;
        m_moveUpdate[(int)HaruState.DashEnd] = Move_DashEnd;
        m_moveUpdate[(int)HaruState.Jump] = Move_Jump;
        m_moveUpdate[(int)HaruState.DashJump] = Move_DashJump;
        m_moveUpdate[(int)HaruState.Land] = Move_Land;
        m_moveUpdate[(int)HaruState.DashLand] = Move_DashLand;
        m_moveUpdate[(int)HaruState.Evade] = Move_Evade;
        m_moveUpdate[(int)HaruState.DMG_L] = Move_Idle;
        m_moveUpdate[(int)HaruState.DMG_R] = Move_Idle;
        m_moveUpdate[(int)HaruState.KB] = Move_Idle;
        m_moveUpdate[(int)HaruState.KD_Ham_F] = Move_KD_Ham_F;
        m_moveUpdate[(int)HaruState.KD_Ham_B] = Move_KD_Ham_B;
        m_moveUpdate[(int)HaruState.KD_Str] = Move_KD_Str;
        m_moveUpdate[(int)HaruState.KD_Upp] = Move_KD_Upp;
        m_moveUpdate[(int)HaruState.KD_Upp_End] = Move_Idle;
        m_moveUpdate[(int)HaruState.KD_Upp_Down] = Move_Idle;
        m_moveUpdate[(int)HaruState.KD_Upp_Air_Hit] = Move_Idle;
        m_moveUpdate[(int)HaruState.KD_Upp_Down_Hit] = Move_Idle;
        m_moveUpdate[(int)HaruState.KD_Upp_Raise] = Move_Idle;
        m_moveUpdate[(int)HaruState.NormalAttack1] = Move_NormalAttack1;
        m_moveUpdate[(int)HaruState.NormalAttack2] = Move_NormalAttack2;
        m_moveUpdate[(int)HaruState.NormalAttack3] = Move_NormalAttack3;
        m_moveUpdate[(int)HaruState.NormalAttack4] = Move_NormalAttack4;
        m_moveUpdate[(int)HaruState.NormalAttack5] = Move_NormalAttack5;
        m_moveUpdate[(int)HaruState.FirstBlade] = Move_FirstBlade;
        m_moveUpdate[(int)HaruState.FirstBlade02] = Move_FirstBlade02;
        m_moveUpdate[(int)HaruState.PierceStep] = Move_PierceStep;
        m_moveUpdate[(int)HaruState.SpinCutter] = Move_SpinCutter;

        m_animeUpdate = new Action[(int)HaruState.End];
        m_animeUpdate[(int)HaruState.Idle] = Ani_Idle;
        m_animeUpdate[(int)HaruState.Run] = Ani_Run;
        m_animeUpdate[(int)HaruState.RunEnd] = Ani_RunEnd;
        m_animeUpdate[(int)HaruState.Dash] = Ani_Dash;
        m_animeUpdate[(int)HaruState.DashEnd] = Ani_DashEnd;
        m_animeUpdate[(int)HaruState.Jump] = Ani_Jump;
        m_animeUpdate[(int)HaruState.DashJump] = Ani_DashJump;
        m_animeUpdate[(int)HaruState.Land] = Ani_Land;
        m_animeUpdate[(int)HaruState.DashLand] = Ani_DashLand;
        m_animeUpdate[(int)HaruState.Evade] = Ani_Evade;
        m_animeUpdate[(int)HaruState.DMG_L] = Ani_DMG_L;
        m_animeUpdate[(int)HaruState.DMG_R] = Ani_DMG_R;
        m_animeUpdate[(int)HaruState.KB] = Ani_KB;
        m_animeUpdate[(int)HaruState.KD_Ham_F] = Ani_KD_Ham_F;
        m_animeUpdate[(int)HaruState.KD_Ham_B] = Ani_KD_Ham_B;
        m_animeUpdate[(int)HaruState.KD_Str] = Ani_KD_Str;
        m_animeUpdate[(int)HaruState.KD_Upp] = Ani_KD_Upp;
        m_animeUpdate[(int)HaruState.KD_Upp_End] = Ani_KD_Upp_End;
        m_animeUpdate[(int)HaruState.KD_Upp_Down] = Ani_KD_Upp_Down;
        m_animeUpdate[(int)HaruState.KD_Upp_Air_Hit] = Ani_KD_Upp_Air_Hit;
        m_animeUpdate[(int)HaruState.KD_Upp_Down_Hit] = Ani_KD_Upp_Down_Hit;
        m_animeUpdate[(int)HaruState.KD_Upp_Raise] = Ani_KD_Upp_Raise;
        m_animeUpdate[(int)HaruState.NormalAttack1] = Ani_NormalAttack1;
        m_animeUpdate[(int)HaruState.NormalAttack2] = Ani_NormalAttack2;
        m_animeUpdate[(int)HaruState.NormalAttack3] = Ani_NormalAttack3;
        m_animeUpdate[(int)HaruState.NormalAttack4] = Ani_NormalAttack4;
        m_animeUpdate[(int)HaruState.NormalAttack5] = Ani_NormalAttack5;
        m_animeUpdate[(int)HaruState.FirstBlade] = Ani_FirstBlade;
        m_animeUpdate[(int)HaruState.FirstBlade02] = Ani_FirstBlade02;
        m_animeUpdate[(int)HaruState.PierceStep] = Ani_PierceStep;
        m_animeUpdate[(int)HaruState.SpinCutter] = Ani_SpinCutter;

        m_changeState = new bool[(int)HaruState.End, (int)HaruState.End];

        m_changeState[(int)HaruState.Idle, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.Dash] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.Jump] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.Evade] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.NormalAttack1] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.FirstBlade] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.PierceStep] = true;
        m_changeState[(int)HaruState.Idle, (int)HaruState.SpinCutter] = true;

        m_changeState[(int)HaruState.Run, (int)HaruState.Idle] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.Dash] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.Jump] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.Evade] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.NormalAttack1] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.FirstBlade] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.PierceStep] = true;
        m_changeState[(int)HaruState.Run, (int)HaruState.SpinCutter] = true;

        m_changeState[(int)HaruState.RunEnd, (int)HaruState.Dash] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.Jump] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.Evade] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.NormalAttack1] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.FirstBlade] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.PierceStep] = true;
        m_changeState[(int)HaruState.RunEnd, (int)HaruState.SpinCutter] = true;

        m_changeState[(int)HaruState.Dash, (int)HaruState.Idle] = true;
        m_changeState[(int)HaruState.Dash, (int)HaruState.DashJump] = true;

        m_changeState[(int)HaruState.Jump, (int)HaruState.Land] = true;

        m_changeState[(int)HaruState.DashJump, (int)HaruState.DashLand] = true;

        m_changeState[(int)HaruState.Land, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.Land, (int)HaruState.Dash] = true;
        m_changeState[(int)HaruState.Land, (int)HaruState.Jump] = true;

        m_changeState[(int)HaruState.DashLand, (int)HaruState.Run] = true;

        m_changeState[(int)HaruState.Evade, (int)HaruState.NormalAttack1] = true;

        m_changeState[(int)HaruState.KD_Upp_Down, (int)HaruState.Run] = true;

        m_changeState[(int)HaruState.NormalAttack1, (int)HaruState.NormalAttack2] = true;
        m_changeState[(int)HaruState.NormalAttack1, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.NormalAttack1, (int)HaruState.Evade] = true;

        m_changeState[(int)HaruState.NormalAttack2, (int)HaruState.NormalAttack3] = true;
        m_changeState[(int)HaruState.NormalAttack2, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.NormalAttack2, (int)HaruState.Evade] = true;

        m_changeState[(int)HaruState.NormalAttack3, (int)HaruState.NormalAttack4] = true;
        m_changeState[(int)HaruState.NormalAttack3, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.NormalAttack3, (int)HaruState.Evade] = true;

        m_changeState[(int)HaruState.NormalAttack4, (int)HaruState.NormalAttack5] = true;
        m_changeState[(int)HaruState.NormalAttack4, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.NormalAttack4, (int)HaruState.Evade] = true;

        m_changeState[(int)HaruState.NormalAttack5, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.NormalAttack5, (int)HaruState.Evade] = true;

        m_changeState[(int)HaruState.FirstBlade, (int)HaruState.FirstBlade02] = true;
        m_changeState[(int)HaruState.FirstBlade, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.FirstBlade, (int)HaruState.Evade] = true;
        m_changeState[(int)HaruState.FirstBlade, (int)HaruState.NormalAttack1] = true;

        m_changeState[(int)HaruState.FirstBlade02, (int)HaruState.Run] = true;
        m_changeState[(int)HaruState.FirstBlade02, (int)HaruState.Evade] = true;
        m_changeState[(int)HaruState.FirstBlade02, (int)HaruState.NormalAttack1] = true;

        // 무기 장착
        m_weapon.transform.SetParent(m_weaponholder, false);

        m_playerInfo.UpdateInfo();
    }

    private void FixedUpdate()
    {
        if (m_isUIObject)
            return;

        // 이동 업데이트
        m_moveUpdate[(int)m_state]();
        PlayerRotation();
        AimRotation();
    }

    private void Update()
    {
        if (m_isUIObject)
            return;

        // 애니메이션 업데이트
        m_animeUpdate[(int)m_state]();
    }


    // 상태 확인
    private bool CheckState(HaruState left, HaruState right)
    {
        if (!m_changeState[(int)left, (int)right])
            return false;

        // 너무 빠르게 상태를 변환하려고 함
        if (Time.time <= lastIdleChangeTime + m_idleChangeDelay)
            return false;

        return true;
    }

    private void PlayerRotation()
    {
        // 방향이 같지 않음
        if (m_modelTransform.forward != m_turnDir)
        {
            Quaternion rotation = Quaternion.LookRotation(m_turnDir);
            m_modelTransform.rotation = Quaternion.Lerp(m_modelTransform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
        }
    }

    // 에임 회전
    private void AimRotation()
    {
        float yRotation = Input.GetAxis("Mouse X");
        float xRotation = Input.GetAxis("Mouse Y");

        // X축 회전 제한
        Quaternion rotation = m_aimTransform.rotation * Quaternion.Euler(new Vector3(-xRotation * m_mouseSpeed * Time.deltaTime, 0, 0));
        float xAngle = Mathf.Round(rotation.eulerAngles.x);
        if (180.0f <= xAngle && xAngle < 310.0f)
        {
            xAngle = 310.0f;
        }
        else if (180.0f > xAngle && xAngle > 50.0f)
        {
            xAngle = 50.0f;
        }

        m_aimTransform.rotation = Quaternion.Euler(new Vector3(xAngle, m_aimTransform.eulerAngles.y, m_aimTransform.eulerAngles.x));

        // Y축 자전
        m_aimTransform.RotateAround(m_playerTransform.position, Vector3.up, yRotation * m_mouseSpeed * Time.deltaTime);
    }

    // 현재 상태가 바뀌기 때문에
    // 이전 상태의 플래그들 false로 변경
    private void ChangeFlagFalse()
    {
        switch (m_state)
        {
            case HaruState.Idle:
                break;
            case HaruState.Run:
                SetSpeedZero();
                break;
            case HaruState.RunEnd:
                break;
            case HaruState.Dash:
                m_dash = false;
                SetDashFalse();
                break;
            case HaruState.DashEnd:
                break;
            case HaruState.Jump:
                m_jump = false;
                SetJumpFalse();
                break;
            case HaruState.DashJump:
                m_dash = false;
                m_jump = false;
                SetDashFalse();
                SetJumpFalse();
                break;
            case HaruState.Land:
                m_lockInput = false;
                break;
            case HaruState.DashLand:
                m_lockInput = false;
                break;
            case HaruState.Evade:
                m_evade = false;
                m_lockInput = false;
                break;
            case HaruState.DMG_L:
                break;
            case HaruState.DMG_R:
                break;
            case HaruState.KB:
                break;
            case HaruState.KD_Ham_F:
                m_upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Ham_B:
                m_upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Str:
                break;
            case HaruState.KD_Upp:
                m_upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Upp_End:
                m_down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Down:
                m_down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Air_Hit:
                m_upp = false;
                SetUppFalse();
                break;
            case HaruState.KD_Upp_Down_Hit:
                m_down = false;
                SetDownFalse();
                break;
            case HaruState.KD_Upp_Raise:
                m_down = false;
                SetDownFalse();
                break;
            case HaruState.NormalAttack1:
            case HaruState.NormalAttack2:
            case HaruState.NormalAttack3:
            case HaruState.NormalAttack4:
            case HaruState.NormalAttack5:
                m_normalAtk = false;
                m_lockInput = false;
                m_moveAttack = false;
                m_moveStand = false;
                SetTrigerNormalAttackEnd();
                SetNormalAttackCnt(0);
                if (m_isAttacking)
                {
                    m_isAttacking = false;
                    // 무기 충돌 트리거 OFF
                    m_weapon.OffTrigger();
                }
                break;
            case HaruState.FirstBlade:
            case HaruState.FirstBlade02:
            case HaruState.PierceStep:
            case HaruState.SpinCutter:
                m_normalAtk = false;
                m_lockInput = false;
                m_moveAttack = false;
                m_moveStand = false;
                if (m_isAttacking)
                {
                    m_isAttacking = false;
                    // 무기 충돌 트리거 OFF
                    m_weapon.OffTrigger();
                }
                break;
            default:
                break;
        }
    }

    // 현재 상태가 바뀌기 때문에
    // 이전 상태의 플래그들 true로 변경
    private void ChangeFlagTrue()
    {
        switch (m_state)
        {
            case HaruState.Idle:
                SetSpeedZero();
                SetTrigerBattleStandDuration();
                break;
            case HaruState.Run:
                SetTrigerBattleRun();
                break;
            case HaruState.RunEnd:
                break;
            case HaruState.Dash:
                m_dash = true;
                SetDashTrue();
                SetTrigerBattleDash();
                break;
            case HaruState.DashEnd:
                break;
            case HaruState.Jump:
                m_jump = true;
                SetJumpTrue();
                SetTrigerBattleJump();
                break;
            case HaruState.DashJump:
                m_dash = true;
                m_jump = true;
                SetDashTrue();
                SetJumpTrue();
                SetTrigerBattleDashJump();
                break;
            case HaruState.Land:
                m_lockInput = true;
                SetSpeedZero();
                break;
            case HaruState.DashLand:
                m_lockInput = true;
                SetSpeedZero();
                break;
            case HaruState.Evade:
                m_evade = true;
                m_lockInput = true;
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
                m_upp = true;
                SetUppTrue();
                SetTrigerKDHamF();
                break;
            case HaruState.KD_Ham_B:
                m_upp = true;
                SetUppTrue();
                SetTrigerKDHamB();
                break;
            case HaruState.KD_Str:
                break;
            case HaruState.KD_Upp:
                m_upp = true;
                SetUppTrue();
                SetTrigerKDUpp();
                break;
            case HaruState.KD_Upp_End:
                m_down = true;
                SetDownTrue();
                SetTrigerKDUppEnd();
                break;
            case HaruState.KD_Upp_Down:
                m_down = true;
                SetDownTrue();
                break;
            case HaruState.KD_Upp_Air_Hit:
                m_upp = true;
                SetUppTrue();
                SetTrigerKDUppAirHit();
                break;
            case HaruState.KD_Upp_Down_Hit:
                m_down = true;
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
                m_lockInput = true;
                m_moveAttack = true;
                m_moveStand = false;
                m_isAttacking = false;
                SetTrigerSkillFirstBlade();
                SetAttackDir();
                break;
            case HaruState.FirstBlade02:
                m_lockInput = true;
                m_moveAttack = true;
                m_moveStand = false;
                m_isAttacking = false;
                SetTrigerSkillFirstBlade02();
                SetAttackDir();
                break;
            case HaruState.PierceStep:
                m_lockInput = true;
                m_moveAttack = true;
                m_moveStand = false;
                m_isAttacking = false;
                SetTrigerSkillPierceStep();
                SetAttackDir();
                break;
            case HaruState.SpinCutter:
                m_lockInput = true;
                m_moveAttack = true;
                m_moveStand = false;
                m_isAttacking = false;
                SetTrigerSkillSpinCutter();
                SetAttackDir();
                break;
            default:
                break;
        }
    }

    // 공격 처리
    // _fTime : 애니메이션 표준화 시간
    void AttackProc(HaruSkillDamage _enSkill, float _fTime)
    {
        // 애니메이션 시간이 표준화 범위를 벗어나면 예외
        if (_fTime < 0 || 1 < _fTime)
            return;

        int iAttackCount;
        if (!m_playerInfo.CheckAttackTime(_enSkill, _fTime, out iAttackCount))
        {
            if (m_isAttacking)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            return;
        }

        m_isAttacking = true;
        // 무기 충돌 트리거 ON
        m_weapon.OnTrigger();
        // 공격 타입
        m_weapon.m_attackType = AttackType.Normal;
        // 데미지 정보
        float damage;
        if (!m_playerInfo.GetSkillDamage(_enSkill, iAttackCount, out damage))
            return;
        m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
    }
}
