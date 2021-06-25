using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using MyEnum;
using UnityEditorInternal;

public class PlayerCtrl : MonoBehaviour
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
    private Vector3 playerDir = Vector3.forward;

    private bool[,] changeState;
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
        changeState[(int)PlayerState.Land, (int)PlayerState.Run] = false;
        changeState[(int)PlayerState.Land, (int)PlayerState.Dash] = false;
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
        Move();
        PlayerRotation();
        AimRotation();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    public void MoveNone()
    {
        moveInput = new Vector2(0, 0);

        if (CheckState(state, PlayerState.Idle))
            state = PlayerState.Idle;
    }

    public void MoveFF()
    {
        moveInput = new Vector2(0, 1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            state = PlayerState.Run;
        }
    }

    public void MoveFL()
    {
        moveInput = new Vector2(-1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveFR()
    {
        moveInput = new Vector2(1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBB()
    {
        moveInput = new Vector2(0, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBL()
    {
        moveInput = new Vector2(-1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBR()
    {
        moveInput = new Vector2(1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveLL()
    {
        moveInput = new Vector2(-1.0f, 0);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveRR()
    {
        moveInput = new Vector2(1.0f, 0);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void Dash()
    {
        if (CheckState(state, PlayerState.Dash))
        {
            state = PlayerState.Dash;
            dash = true;
        }
    }

    public void Jump()
    {
        if (dash)
        {
            if (CheckState(state, PlayerState.DashJump) && characterController.isGrounded)
            {
                state = PlayerState.DashJump;
                currentVelocityY = jumpVelocity;
                jumpTime = Time.realtimeSinceStartup;
            }
        }
        else
        {
            if (CheckState(state, PlayerState.Jump) && characterController.isGrounded)
            {
                state = PlayerState.Jump;
                currentVelocityY = jumpVelocity;
                jumpTime = Time.realtimeSinceStartup;
            }
        }
    }

    public void SkillSlot1()
    {

    }

    public void SkillSlot2()
    {

    }

    public void SkillSlot3()
    {

    }

    public void SkillSlot4()
    {

    }

    public void SkillSlot5()
    {

    }

    public void SkillSlot6()
    {

    }


    // 상태 확인
    private bool CheckState(PlayerState left, PlayerState right)
    {
        return changeState[(int)left, (int)right];
    }

    // 이동
    private void Move()
    {
        if (state == PlayerState.Land)
            return;

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

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            playerDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void PlayerRotation()
    {
        // 방향이 같지 않음
        if (modelTransform.forward != playerDir)
        {
            Quaternion rotation = Quaternion.LookRotation(playerDir);
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

    // 애니메이션
    private void UpdateAnimation()
    {
        animeUpdate[(int)state]();
    }

    private void Ani_Idle()
    {
        hairAnime.SetFloat("Speed", 0);
        faceAnime.SetFloat("Speed", 0);
        bodyAnime.SetFloat("Speed", 0);
        pantsAnime.SetFloat("Speed", 0);
        handsAnime.SetFloat("Speed", 0);
        footAnime.SetFloat("Speed", 0);

        if (dash) 
        {
            dash = false;

            hairAnime.SetBool("Dash", false);
            faceAnime.SetBool("Dash", false);
            bodyAnime.SetBool("Dash", false);
            pantsAnime.SetBool("Dash", false);
            handsAnime.SetBool("Dash", false);
            footAnime.SetBool("Dash", false);
        }
    }

    private void Ani_Run()
    {
        float speedPer = targetSpeed / moveSpeed;

        hairAnime.SetFloat("Speed", speedPer);
        faceAnime.SetFloat("Speed", speedPer);
        bodyAnime.SetFloat("Speed", speedPer);
        pantsAnime.SetFloat("Speed", speedPer);
        handsAnime.SetFloat("Speed", speedPer);
        footAnime.SetFloat("Speed", speedPer);
    }

    private void Ani_Dash()
    {
        hairAnime.SetBool("Dash", true);
        faceAnime.SetBool("Dash", true);
        bodyAnime.SetBool("Dash", true);
        pantsAnime.SetBool("Dash", true);
        handsAnime.SetBool("Dash", true);
        footAnime.SetBool("Dash", true);
    }

    private void Ani_Jump()
    {
        if (!jump)
        {
            jump = true;
            hairAnime.SetBool("Jump", true);
            faceAnime.SetBool("Jump", true);
            bodyAnime.SetBool("Jump", true);
            pantsAnime.SetBool("Jump", true);
            handsAnime.SetBool("Jump", true);
            footAnime.SetBool("Jump", true);
        }
        else
        {
            // 착지
            if (characterController.isGrounded)
            {
                // 점프 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - jumpTime <= 0.5f)
                    return;

                    jump = false;
                state = PlayerState.Land;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);
            }
        }
    }

    private void Ani_DashJump()
    {
        if (!jump)
        {
            jump = true;
            hairAnime.SetBool("Jump", true);
            faceAnime.SetBool("Jump", true);
            bodyAnime.SetBool("Jump", true);
            pantsAnime.SetBool("Jump", true);
            handsAnime.SetBool("Jump", true);
            footAnime.SetBool("Jump", true);
        }
        else
        {
            // 착지
            if (characterController.isGrounded)
            {
                // 점프 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - jumpTime <= 0.5f)
                    return;

                jump = false;
                state = PlayerState.DashLand;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);
            }
        }
    }

    private void Ani_Land()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.B_Jump_Land_C") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = PlayerState.Idle;
        }
    }

    private void Ani_DashLand()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.B_Dash_Jump_End") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = PlayerState.Idle;
        }
    }
}
