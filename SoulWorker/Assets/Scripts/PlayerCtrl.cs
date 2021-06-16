using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5.0f;     // 이동속도
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

    private Transform playerTransform;
    private float mouseSpeed = 100.0f;
    private float rotationSpeed = 15.0f;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GetComponent<Transform>();

        weapon.parent = weaponholder;
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.localScale = Vector3.one;
    }

    private void FixedUpdate()
    {
        Move();
        AimRotation();
    }

    // 이동
    private void Move()
    {
        // 이동 방향키
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0.0f && vertical == 0.0f)
            return;

        Vector3 forwardDir = modelTransform.forward;
        Vector3 moveDir = (cameraTransform.forward * vertical) + (cameraTransform.right * horizontal);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        // 방향이 같지 않음
        if (forwardDir != moveDir)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDir);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
        }

        playerTransform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        hairAnime.SetFloat("Speed", vertical);
        faceAnime.SetFloat("Speed", vertical);
        bodyAnime.SetFloat("Speed", vertical);
        pantsAnime.SetFloat("Speed", vertical);
        handsAnime.SetFloat("Speed", vertical);
        footAnime.SetFloat("Speed", vertical);
    }

    // 에임 회전
    private void AimRotation()
    {
        float yRotation = Input.GetAxis("Mouse X");
        float xRotation = Input.GetAxis("Mouse Y");

        // X축 회전 제한
        Quaternion rotation = aimTransform.rotation * Quaternion.Euler(new Vector3(-xRotation * mouseSpeed * Time.fixedDeltaTime, 0, 0));
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
        aimTransform.RotateAround(playerTransform.position, Vector3.up, yRotation * mouseSpeed * Time.fixedDeltaTime);
    }
}
