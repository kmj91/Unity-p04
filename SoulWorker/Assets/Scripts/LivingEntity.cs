using System;
using UnityEngine;

using MyStruct;
using MyDelegate;
using Random = UnityEngine.Random;

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

    // 회피도 호출 함수
    public DelRetfloat DelCurrentEvade;
    public float currentEvade
    {
        get
        {
            if (DelCurrentEvade == null) return 0f;

            return DelCurrentEvade();
        }
        private set { }
    }

    public DelRetfloat DelCurrentCriticalResistance;
    public float currentCriticalResistance
    {
        get
        {
            if (DelCurrentCriticalResistance == null) return 0f;

            return DelCurrentCriticalResistance();
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

        // 적중도 공식
        // (공격자의 적중도 - 방어자의 회피도)/1000 * 100%
        float accuracy = (damageMessage.accuracy - currentEvade) / 1000f * 100f;
        int rand = Random.Range(0, 100);
        if (accuracy < rand)
        {
            // 빗맞힘
            // 원래 피해량 * 빗맞힘 피해
            damageMessage.damage *= (damageMessage.partialDamage / 100);
        }
        else
        {
            // 치명타 공식
            // 실제 치확(%) = 공격자 치확 - 방어자 치저 + (공격자의 적중도 - 방어자의 회피도) / 50
            float criticalRate = damageMessage.criticalRate - currentCriticalResistance + (damageMessage.accuracy - currentEvade) / 50;
            rand = Random.Range(0, 100);
            if (criticalRate >= rand)
            {
                // 치명타 공식
                // 1.8AD + 치피옵션
                damageMessage.damage = damageMessage.damage * 1.8f + damageMessage.criticalDamage;
            }
        }

        // 대미지 감소율 공식
        // 방어도 / (방어도 + (캐릭터 레벨 * 50))
        float defenseRate = currentDefense / (currentDefense + (currentLevel * 50f));
        health -= damageMessage.damage * (1f - defenseRate);

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
