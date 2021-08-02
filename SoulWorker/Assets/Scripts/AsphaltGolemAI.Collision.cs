using UnityEngine;

using MyStruct;

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
        DamageMessage msg = new DamageMessage
        {
            damager = monsterInfo.gameObject,
            damage = monsterInfo.currentMonsterData.maxAtk,
            criticalRate = monsterInfo.currentMonsterData.criticalRate,
            criticalDamage = monsterInfo.currentMonsterData.criticalDamage,
            accuracy = monsterInfo.currentMonsterData.accuracy,
            partialDamage = monsterInfo.currentMonsterData.partialDamage
        };
        hit.ApplyDamage(ref msg);
    }
}
