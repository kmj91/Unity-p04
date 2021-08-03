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
        AttackType type;
        if (!CurrentAttackType(out type))
            return;

        DamageMessage msg = new DamageMessage
        {
            damager = monsterInfo.gameObject,
            damage = monsterInfo.currentMonsterData.maxAtk,
            criticalRate = monsterInfo.currentMonsterData.criticalRate,
            criticalDamage = monsterInfo.currentMonsterData.criticalDamage,
            accuracy = monsterInfo.currentMonsterData.accuracy,
            partialDamage = monsterInfo.currentMonsterData.partialDamage,
            attackType = type
        };
        hit.ApplyDamage(ref msg);
    }

    private bool CurrentAttackType(out AttackType type)
    {
        switch (state)
        {
            case AsphaltGolemState.A_Skill_01:
                type = AttackType.Upper;
                break;
            case AsphaltGolemState.A_Skill_02:
                type = AttackType.Down;
                break;
            case AsphaltGolemState.A_Skill_03:
                type = AttackType.Normal;
                break;
            default:
                type = AttackType.None;
                return false;
        }

        return true;
    }
}
