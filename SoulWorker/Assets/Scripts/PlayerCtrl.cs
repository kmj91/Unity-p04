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
    private float cameraSpeed = 100.0f;
    private float h = 0.0f;
    private float v = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();

        weapon.parent = weaponholder;
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        // 이동 방향키
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 vecDir = (aimTransform.forward * v) + (aimTransform.right * h);
        vecDir.y = 0.0f;
        playerTransform.Translate(vecDir.normalized * moveSpeed * Time.deltaTime, Space.World);

        hairAnime.SetFloat("Speed", v);
        faceAnime.SetFloat("Speed", v);
        bodyAnime.SetFloat("Speed", v);
        pantsAnime.SetFloat("Speed", v);
        handsAnime.SetFloat("Speed", v);
        footAnime.SetFloat("Speed", v);

        AimRotation();
    }

    // 에임 회전
    void AimRotation()
    {
        var yRotation = Input.GetAxis("Mouse X");
        var xRotation = Input.GetAxis("Mouse Y");

        // X축 회전 제한
        var rotation = aimTransform.rotation * Quaternion.Euler(new Vector3(-xRotation, 0, 0));
        var xAngle = Mathf.Round(rotation.eulerAngles.x);
        if (180.0f <= xAngle && xAngle < 310.0f)
        {
            xAngle = 310.0f;
        }
        else if (180.0f > xAngle && xAngle > 50.0f)
        {
            xAngle = 50.0f;
        }

        aimTransform.rotation = Quaternion.Euler(new Vector3(xAngle, aimTransform.rotation.eulerAngles.y, aimTransform.rotation.eulerAngles.x));

        // Y축 자전
        aimTransform.RotateAround(playerTransform.position, Vector3.up, yRotation * cameraSpeed * Time.deltaTime);
    }
}
