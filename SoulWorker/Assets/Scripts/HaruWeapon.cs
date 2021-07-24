using UnityEngine;

using MyEnum;
using MyStruct;
using System.IO;
using System.Collections.Generic;

public class HaruWeapon : Item
{
    public PlayerInfo playerInfo;

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

        abilityDatas = new List<AbilityData>();

        // 타입
        type = ItemType.HaruWeapon;

        // 무기 정보 파일 읽기
        string filePath = Path.Combine(Application.streamingAssetsPath, "WeaponInfo.txt");
        string fileText = Utility.ReadText(filePath);
        string weponInfo = "";
        if (!Utility.Parser_GetArea(fileText, gameObject.name, out weponInfo))
            return;

        // 무기 스텟
        if (!Utility.Parser_GetValue_String(weponInfo, "name", out itemName))
            return;
        if (!Utility.Parser_GetValue_Float(weponInfo, "level", out useLevel))
            return;

        AbilityData data = new AbilityData();
        if (!Utility.Parser_GetValue_Float(weponInfo, "atk", out data.amount))
            return;
        data.type = AbilityType.Attack;
        abilityDatas.Add(data);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mask.value) return;



        var hit = other.GetComponent<LivingEntity>();
        DamageMessage msg = new DamageMessage
        {
            damager = gameObject,
            damage = playerInfo.currentPlayerData.maxAtk,
            criticalRate = playerInfo.currentPlayerData.criticalRate,
            criticalDamage = playerInfo.currentPlayerData.criticalDamage,
            accuracy = playerInfo.currentPlayerData.accuracy,
            partialDamage = playerInfo.currentPlayerData.partialDamage
        };
        hit.ApplyDamage(msg);
    }
}
