using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using MyEnum;

public partial class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5.0f;          // 이동 속도
    public float mouseSpeed = 100.0f;       // 마우스 속도
    public float rotationSpeed = 10.0f;     // 회전 속도
    public float jumpVelocity = 5.0f;      // 점프력
    [Range(0.01f, 1.0f)] public float airControlPercent;
    public float speedSmoothTime = 0.1f;
    // 캐릭터 컬라이더가 실제 움직인 거리
    public float currentSpeed =>
        new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

    public Transform modelTransform;
    public Transform cameraTransform;
    public Transform weaponholder;
    public Transform weapon;
    public Transform aimTransform;
    public Animator hairAnime;
    public Animator faceAnime;
    public Animator bodyAnime;
    public Animator pantsAnime;
    public Animator handsAnime;
    public Animator footAnime;

    private CharacterController characterController;
    private Transform playerTransform;
    private PlayerState state = PlayerState.Idle;
    private Vector2 moveInput = Vector2.zero;
    private Vector3 moveAnimeDir = Vector3.zero;
    private Vector3 turnDir = Vector3.forward;

    private bool[,] changeState;
    private Action[] moveUpdate;
    private Action[] animeUpdate;
    private float speedSmoothVelocity;
    private float currentVelocityY;
    private float targetSpeed;          // SmoothDamp가 적용된 이동 속도
    private float jumpTime;
    private bool jump = false;
    private bool dash = false;


    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();

        moveUpdate = new Action[(int)PlayerState.End];
        moveUpdate[(int)PlayerState.Idle] = Move_Idle;
        moveUpdate[(int)PlayerState.Run] = Move_Run;
        moveUpdate[(int)PlayerState.Dash] = Move_Dash;
        moveUpdate[(int)PlayerState.Jump] = Move_Jump;
        moveUpdate[(int)PlayerState.DashJump] = Move_DashJump;
        moveUpdate[(int)PlayerState.Land] = Move_Land;
        moveUpdate[(int)PlayerState.DashLand] = Move_DashLand;

        animeUpdate = new Action[(int)PlayerState.End];
        animeUpdate[(int)PlayerState.Idle] = Ani_Idle;
        animeUpdate[(int)PlayerState.Run] = Ani_Run;
        animeUpdate[(int)PlayerState.Dash] = Ani_Dash;
        animeUpdate[(int)PlayerState.Jump] = Ani_Jump;
        animeUpdate[(int)PlayerState.DashJump] = Ani_DashJump;
        animeUpdate[(int)PlayerState.Land] = Ani_Land;
        animeUpdate[(int)PlayerState.DashLand] = Ani_DashLand;

        changeState = new bool[(int)PlayerState.End, (int)PlayerState.End];
        changeState[(int)PlayerState.Idle, (int)PlayerState.Idle] = false;
        changeState[(int)PlayerState.Idle, (int)PlayerState.Run] = true;
        changeState[(int)PlayerState.Idle, (int)PlayerState.Dash] = true;
        changeState[(int)PlayerState.Idle, (int)PlayerState.Jump] = true;
        changeState[(int)PlayerState.Idle, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.Idle, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.Idle, (int)PlayerState.DashLand] = false;

        changeState[(int)PlayerState.Run, (int)PlayerState.Idle] = true;
        changeState[(int)PlayerState.Run, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.Run, (int)PlayerState.Dash] = true;
        changeState[(int)PlayerState.Run, (int)PlayerState.Jump] = true;
        changeState[(int)PlayerState.Run, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.Run, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.Run, (int)PlayerState.DashLand] = false;

        changeState[(int)PlayerState.Dash, (int)PlayerState.Idle] = true;
        changeState[(int)PlayerState.Dash, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.Dash, (int)PlayerState.Dash] = false;
        changeState[(int)PlayerState.Dash, (int)PlayerState.Jump] = false;
        changeState[(int)PlayerState.Dash, (int)PlayerState.DashJump] = true;
        changeState[(int)PlayerState.Dash, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.Dash, (int)PlayerState.DashLand] = false;

        changeState[(int)PlayerState.Jump, (int)PlayerState.Idle] = false;
        changeState[(int)PlayerState.Jump, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.Jump, (int)PlayerState.Dash] = false;
        changeState[(int)PlayerState.Jump, (int)PlayerState.Jump] = false;
        changeState[(int)PlayerState.Jump, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.Jump, (int)PlayerState.Land] = true;
        changeState[(int)PlayerState.Jump, (int)PlayerState.DashLand] = false;

        changeState[(int)PlayerState.DashJump, (int)PlayerState.Idle] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.Dash] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.Jump] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.DashJump, (int)PlayerState.DashLand] = true;

        changeState[(int)PlayerState.Land, (int)PlayerState.Idle] = false;
        changeState[(int)PlayerState.Land, (int)PlayerState.Run] = true;
        changeState[(int)PlayerState.Land, (int)PlayerState.Dash] = true;
        changeState[(int)PlayerState.Land, (int)PlayerState.Jump] = true;
        changeState[(int)PlayerState.Land, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.Land, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.Land, (int)PlayerState.DashLand] = false;

        changeState[(int)PlayerState.DashLand, (int)PlayerState.Idle] = false;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.Dash] = false;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.Jump] = true;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.DashJump] = false;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.Land] = false;
        changeState[(int)PlayerState.DashLand, (int)PlayerState.DashLand] = false;

        weapon.parent = weaponholder;
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.localScale = Vector3.one;
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
    private bool CheckState(PlayerState left, PlayerState right)
    {
        return changeState[(int)left, (int)right];
    }

    // 이동
    private void Move()
    {
        // 착지
        if (state == PlayerState.Land)
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
