using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    [SerializeField] private MonsterInfo monsterInfo;       // 몬스터 정보
    [SerializeField] private BoxCollider boxCollider;       // 충돌체

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
        boxCollider = GetComponent<BoxCollider>();
        // 몬스터와 충돌
        mask = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != mask.value) return;

        var hit = other.GetComponent<LivingEntity>();
        Debug.Log(other);
    }
}
