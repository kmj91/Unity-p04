using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class HaruWeapon : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    private LayerMask mask;



    public void OnTrigger()
    {
        boxCollider.isTrigger = true;
    }

    public void OffTrigger()
    {
        boxCollider.isTrigger = false;
    }

    private void Awake()
    {
        mask = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mask.value) return;

        Debug.Log("충돌");
        Debug.Log(other);
    }
}
