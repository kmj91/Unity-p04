using UnityEngine;

using MyStruct;
using MyEnum;
using UnityEditor;

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
            damage = monsterInfo.currentMonsterData.maxAtk,
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
            case AsphaltGolemState.A_Skill_01:
                atkInfo.type = AttackType.Upper;
                atkInfo.power = 5f;
                break;
            case AsphaltGolemState.A_Skill_02:
                atkInfo.type = AttackType.Down;
                atkInfo.power = 3f;
                break;
            case AsphaltGolemState.A_Skill_03:
                atkInfo.type = AttackType.Normal;
                atkInfo.power = 5f;
                break;
            default:
                atkInfo.type = AttackType.None;
                atkInfo.power = 5f;
                return false;
        }

        return true;
    }
}
