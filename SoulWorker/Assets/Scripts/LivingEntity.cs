using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;
using MyDelegate;

public class LivingEntity : MonoBehaviour, IDamageable
{
    // 레벨 호출 함수
    public DelRetfloat DelCurrentLevel;
    
    public float currentLevel
    {
        get
        {
            if (DelCurrentLevel == null) return 1f;

            return DelCurrentLevel();
        }
        private set { }
    }

    // 체력 호출 함수
    public DelRetfloat DelCurrentHp;
    public float currentHp
    {
        get
        {
            if (DelCurrentHp == null) return 100f;

            return DelCurrentHp();
        }
        private set { }
    }

    // 방어도 호출 함수
    public DelRetfloat DelCurrentDefense;
    public float currentDefense
    {
        get
        {
            if (DelCurrentDefense == null) return 0f;

            return DelCurrentDefense();
        }
        private set { }
    }

    public float startingHealth;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action OnDeath;    // 사망할 때 호출할 콜백 함수

    // 0.1초 내에 공격이 여러번 들어오면 무시
    private const float minTimeBetDamaged = 0.1f;
    private float lastDamagedTime;

    protected bool IsInvulnerable
    {
        get 
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false;

            return true;
        }
    }


    protected virtual void OnEnable()
    {
        dead = false;
        startingHealth = currentHp;
        health = startingHealth;
    }

    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        if (IsInvulnerable || damageMessage.damager == gameObject || dead) return false;

        lastDamagedTime = Time.time;

        // 대미지 감소율 공식
        // 방어도 / (방어도 + (캐릭터 레벨 * 50))
        float defenseRate = currentDefense / (currentDefense + (currentLevel * 50f));
        health -= damageMessage.amount * (1f - defenseRate);

        if (health <= 0) Die();

        return true;
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health += newHealth;
    }

    public virtual void  Die()
    {
        if (OnDeath != null) OnDeath();

        dead = true;
    }
}
