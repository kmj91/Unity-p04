using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using MyEnum;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5.0f;          // 이동 속도
    public float mouseSpeed = 100.0f;       // 마우스 속도
    public float rotationSpeed = 10.0f;     // 회전 속도
    public float jumpVelocity = 20.0f;      // 점프력
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
    private Vector3 moveDir = Vector3.forward;

    private float speedSmoothVelocity;
    private float currentVelocityY;
    private float targetSpeed;          // SmoothDamp가 적용된 이동 속도
    private Action[] animeUpdate;


    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();

        animeUpdate = new Action[(int)PlayerState.End];
        animeUpdate[(int)PlayerState.Idle] = Idle;
        animeUpdate[(int)PlayerState.Run] = Run;

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
        state = PlayerState.Idle;
    }

    public void MoveFF()
    {
        moveInput = new Vector2(0, 1.0f);
        state = PlayerState.Run;
    }

    public void MoveFL()
    {
        moveInput = new Vector2(-1.0f, 1.0f);
        state = PlayerState.Run;
    }

    public void MoveFR()
    {
        moveInput = new Vector2(1.0f, 1.0f);
        state = PlayerState.Run;
    }

    public void MoveBB()
    {
        moveInput = new Vector2(0, -1.0f);
        state = PlayerState.Run;
    }

    public void MoveBL()
    {
        moveInput = new Vector2(-1.0f, -1.0f);
        state = PlayerState.Run;
    }

    public void MoveBR()
    {
        moveInput = new Vector2(1.0f, -1.0f);
        state = PlayerState.Run;
    }

    public void MoveLL()
    {
        moveInput = new Vector2(-1.0f, 0);
        state = PlayerState.Run;
    }

    public void MoveRR()
    {
        moveInput = new Vector2(1.0f, 0);
        state = PlayerState.Run;
    }

    // 이동
    private void Move()
    {
        if (moveInput == Vector2.zero)
        {
            characterController.Move(Vector3.zero);
            return;
        }

        float smoothTime = characterController.isGrounded ? speedSmoothTime : speedSmoothTime / airControlPercent;
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, smoothTime);
        
        moveDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded)
            currentVelocityY = 0.0f;
    }

    private void PlayerRotation()
    {
        // 방향이 같지 않음
        if (modelTransform.forward != moveDir)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDir);
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

    private void Idle()
    {
        hairAnime.SetFloat("Speed", 0);
        faceAnime.SetFloat("Speed", 0);
        bodyAnime.SetFloat("Speed", 0);
        pantsAnime.SetFloat("Speed", 0);
        handsAnime.SetFloat("Speed", 0);
        footAnime.SetFloat("Speed", 0);
    }

    private void Run()
    {
        float speedPer = targetSpeed / moveSpeed;

        hairAnime.SetFloat("Speed", speedPer);
        faceAnime.SetFloat("Speed", speedPer);
        bodyAnime.SetFloat("Speed", speedPer);
        pantsAnime.SetFloat("Speed", speedPer);
        handsAnime.SetFloat("Speed", speedPer);
        footAnime.SetFloat("Speed", speedPer);
    }
}
