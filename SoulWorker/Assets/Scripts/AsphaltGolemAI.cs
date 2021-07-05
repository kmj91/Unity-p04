using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;
using UnityEngine.AI;

public class AsphaltGolemAI : MonoBehaviour
{
    public Animator bodyAnime;      // 몸 애니메이터
    public Animator armsAnime;      // 팔 애니메이터
    public float moveSpeed = 3.0f;  // 이동 속도
    public float rotationSpeed = 10.0f;     // 회전 속도
    public float damage = 100.0f;   // 공격력
    public float hp = 2000.0f;      // 체력

    private Transform mTransform;
    private AsphaltGolemState state = AsphaltGolemState.Idle;   // 상태
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        mTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
