using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    private float rotationDamping = 3.0f;

    void LateUpdate()
    {
        if (!target)
            return;


        var wantedRotationAngleY = target.eulerAngles.y;
        var wantedRotationAngleX = target.eulerAngles.x;

        var currentRotationAngleY = transform.eulerAngles.y;
        var currentRotationAngleX = transform.eulerAngles.x;


        currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDamping * Time.deltaTime);
        currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        transform.LookAt(target);
    }
}
