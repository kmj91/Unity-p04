using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaruWeapon : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        //boxCollider.isTrigger = false;
    }
}
