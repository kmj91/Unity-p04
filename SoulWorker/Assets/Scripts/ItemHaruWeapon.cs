using UnityEngine;

using MyEnum;
using MyStruct;
using System.IO;
using System.Collections.Generic;

public class ItemHaruWeapon : Item
{
    [SerializeField] private PlayerInfo m_playerInfo;
    [SerializeField] private BoxCollider m_boxCollider;

    public AttackType m_attackType; // 공격 타입
    public float m_attackDamage;      // 공격 대미지

    private LayerMask m_mask;



    public void OnTrigger()
    {
        m_boxCollider.isTrigger = true;
    }

    public void OffTrigger()
    {
        m_boxCollider.isTrigger = false;
    }

    private void Awake()
    {
        m_boxCollider = GetComponent<BoxCollider>();
        // 몬스터와 충돌
        m_mask = LayerMask.NameToLayer("Monster");

        m_abilityDatas = new List<AbilityData>();

        // 타입
        m_type = ItemType.HaruWeapon;

        // 무기 정보 파일 읽기
        string filePath = Path.Combine(Application.streamingAssetsPath, "WeaponInfo.txt");
        string fileText = Utility.ReadText(filePath);
        string weponInfo = "";
        if (!Utility.Parser_GetArea(fileText, gameObject.name, out weponInfo))
            return;

        // 무기 스텟
        if (!Utility.Parser_GetValue_String(weponInfo, "name", out m_itemName))
            return;
        float outUsePlayer;
        if (!Utility.Parser_GetValue_Float(weponInfo, "usePlayer", out outUsePlayer))
            return;
        m_usePlayer = (UsePlayer)outUsePlayer;
        if (!Utility.Parser_GetValue_Float(weponInfo, "level", out m_useLevel))
            return;

        AbilityData data = new AbilityData();
        if (!Utility.Parser_GetValue_Float(weponInfo, "atk", out data.amount))
            return;
        data.type = AbilityType.Attack;
        m_abilityDatas.Add(data);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != m_mask.value) return;

        var hit = other.GetComponent<LivingEntity>();
        if (hit == null)
            return;

        DamageMessage msg = new DamageMessage
        {
            damager = m_playerInfo.gameObject,
            damage = m_attackDamage,
            criticalRate = m_playerInfo.currentPlayerData.criticalRate,
            criticalDamage = m_playerInfo.currentPlayerData.criticalDamage,
            accuracy = m_playerInfo.currentPlayerData.accuracy,
            partialDamage = m_playerInfo.currentPlayerData.partialDamage,
            attackType = m_attackType
        };
        hit.ApplyDamage(ref msg);
    }
}
