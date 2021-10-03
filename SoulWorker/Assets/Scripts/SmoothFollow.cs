using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform m_target;
    public float m_distance = 3.0f;
    private float m_rotationDamping = 3.0f;

    void LateUpdate()
    {
        if (!m_target)
            return;


        var wantedRotationAngleY = m_target.eulerAngles.y;
        var wantedRotationAngleX = m_target.eulerAngles.x;

        var currentRotationAngleY = transform.eulerAngles.y;
        var currentRotationAngleX = transform.eulerAngles.x;


        currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, m_rotationDamping * Time.deltaTime);
        currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, m_rotationDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

        transform.position = m_target.position;
        transform.position -= currentRotation * Vector3.forward * m_distance;

        transform.LookAt(m_target);
    }
}
