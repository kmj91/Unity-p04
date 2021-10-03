using UnityEngine;

using MyStruct;
using MyEnum;

public partial class AsphaltGolemAI : MonsterAI
{
    public void OnTriggerRight()
    {
        m_rightBoxCollider.isTrigger = true;
        m_rightBoxCollider.enabled = true;
    }

    public void OffTriggerRight()
    {
        m_rightBoxCollider.isTrigger = false;
        m_rightBoxCollider.enabled = false;
    }

    public void OnTriggerLeft()
    {
        m_leftBoxCollider.isTrigger = true;
        m_leftBoxCollider.enabled = true;
    }

    public void OffTriggerLeft()
    {
        m_leftBoxCollider.isTrigger = false;
        m_leftBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != m_mask.value) return;

        var hit = other.GetComponent<LivingEntity>();
        if (hit == null)
            return;

        // 공격 타입
        AttackInfo atkInfo;
        if (!CurrentAttackType(out atkInfo))
            return;

        DamageMessage msg = new DamageMessage
        {
            damager = m_monsterInfo.gameObject,
            damage = atkInfo.damage,
            criticalRate = m_monsterInfo.currentMonsterData.criticalRate,
            criticalDamage = m_monsterInfo.currentMonsterData.criticalDamage,
            accuracy = m_monsterInfo.currentMonsterData.accuracy,
            partialDamage = m_monsterInfo.currentMonsterData.partialDamage,
            power = atkInfo.power,
            attackType = atkInfo.type,
            hitDir = m_targetEntity.transform.position - m_monsterTransform.position
        };
        hit.ApplyDamage(ref msg);
    }

    private bool CurrentAttackType(out AttackInfo atkInfo)
    {
        switch (m_state)
        {
            case AsphaltGolemState.A_Skill_01:  // 어퍼
                atkInfo.type = AttackType.Upper;
                atkInfo.power = 5f;
                atkInfo.damage = Random.Range(m_monsterInfo.currentMonsterData.minAtk, m_monsterInfo.currentMonsterData.maxAtk);
                break;
            case AsphaltGolemState.A_Skill_02:  // 광역 내려찍기
                atkInfo.type = AttackType.Down;
                atkInfo.power = 3f;
                atkInfo.damage = Random.Range(m_monsterInfo.currentMonsterData.minAtk, m_monsterInfo.currentMonsterData.maxAtk) * 1.5f;
                break;
            case AsphaltGolemState.A_Skill_03:  // 방패 기둥 꺼내서 전방 충격파, 찍뎀 + 충격파 스킬
                atkInfo.type = AttackType.Normal;
                atkInfo.power = 5f;
                atkInfo.damage = Random.Range(m_monsterInfo.currentMonsterData.minAtk, m_monsterInfo.currentMonsterData.maxAtk) * 0.5f;
                break;
            default:
                atkInfo.type = AttackType.None;
                atkInfo.power = 5f;
                atkInfo.damage = 0f;
                return false;
        }

        return true;
    }
}
