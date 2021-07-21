using UnityEngine;

using MyStruct;
using System.IO;

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
        boxCollider = GetComponent<BoxCollider>();
        // 무기를 휘두르는 플레이어와 충돌 X
        mask = LayerMask.NameToLayer("Player");
        
        // 무기 정보 텍스트 읽어옴
        string filePath = Path.Combine(Application.streamingAssetsPath, "WeaponInfo.txt");
        string info = Utility.ReadText(filePath);
        // 무기 오브젝트 이름으로
        Debug.Log(gameObject.name);
        // 읽어들인 텍스트 파서
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mask.value) return;

        var hit = other.GetComponent<LivingEntity>();
        DamageMessage msg = new DamageMessage
        {
            damager = gameObject,
            amount = 10f
        };
        hit.ApplyDamage(msg);
    }
}
