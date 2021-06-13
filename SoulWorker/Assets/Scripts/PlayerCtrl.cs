using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5.0f;     // 이동속도

    private Transform playerTransform;
    private float h = 0.0f;
    private float v = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 vecDir = (Vector3.forward * v) + (Vector3.right * h);
        playerTransform.Translate(vecDir.normalized * moveSpeed * Time.deltaTime, Space.World);


    }
}
