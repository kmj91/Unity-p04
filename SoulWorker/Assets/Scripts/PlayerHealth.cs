using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;

public class PlayerHealth : LivingEntity
{
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        // UI 갱신
        UpdateUI();
    }

    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        // 공격 받음

        // UI 갱신
        UpdateUI();

        return true;
    }

    public override void Die()
    {
        base.Die();

        // 사망

        // UI 갱신
        UpdateUI();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // UI 갱신
        //UpdateUI();
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // UI 갱신
        UIManager.Instance.UpdateHpTxt(health, startingHealth);
    }
}
