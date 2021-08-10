using UnityEngine;

using MyStruct;
using MyEnum;

public partial class AsphaltGolemAI : MonsterAI
{
    public void OnTriggerRight()
    {
        rightBoxCollider.isTrigger = true;
        rightBoxCollider.enabled = true;
    }

    public void OffTriggerRight()
    {
        rightBoxCollider.isTrigger = false;
        rightBoxCollider.enabled = false;
    }

    public void OnTriggerLeft()
    {
        leftBoxCollider.isTrigger = true;
        leftBoxCollider.enabled = true;
    }

    public void OffTriggerLeft()
    {
        leftBoxCollider.isTrigger = false;
        leftBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != mask.value) return;

        var hit = other.GetComponent<LivingEntity>();
        if (hit == null)
            return;

        // 공격 타입
        AttackInfo atkInfo;
        if (!CurrentAttackType(out atkInfo))
            return;

        DamageMessage msg = new DamageMessage
        {
            damager = monsterInfo.gameObject,
            damage = atkInfo.damage,
            criticalRate = monsterInfo.currentMonsterData.criticalRate,
            criticalDamage = monsterInfo.currentMonsterData.criticalDamage,
            accuracy = monsterInfo.currentMonsterData.accuracy,
            partialDamage = monsterInfo.currentMonsterData.partialDamage,
            power = atkInfo.power,
            attackType = atkInfo.type,
            hitDir = targetEntity.transform.position - monsterTransform.position
        };
        hit.ApplyDamage(ref msg);
    }

    private bool CurrentAttackType(out AttackInfo atkInfo)
    {
        switch (state)
        {
            case AsphaltGolemState.A_Skill_01:  // 어퍼
                atkInfo.type = AttackType.Upper;
                atkInfo.power = 5f;
                atkInfo.damage = Random.Range(monsterInfo.currentMonsterData.minAtk, monsterInfo.currentMonsterData.maxAtk);
                break;
            case AsphaltGolemState.A_Skill_02:  // 광역 내려찍기
                atkInfo.type = AttackType.Down;
                atkInfo.power = 3f;
                atkInfo.damage = Random.Range(monsterInfo.currentMonsterData.minAtk, monsterInfo.currentMonsterData.maxAtk) * 1.5f;
                break;
            case AsphaltGolemState.A_Skill_03:  // 방패 기둥 꺼내서 전방 충격파, 찍뎀 + 충격파 스킬
                atkInfo.type = AttackType.Normal;
                atkInfo.power = 5f;
                atkInfo.damage = Random.Range(monsterInfo.currentMonsterData.minAtk, monsterInfo.currentMonsterData.maxAtk) * 0.5f;
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
