using System;
using UnityEngine;

using MyStruct;
using MyDelegate;
using Random = UnityEngine.Random;

public class LivingEntity : MonoBehaviour, IDamageable
{
    // 레벨 호출 함수
    public DelRetfloat m_DelCurrentLevel;
    
    public float m_currentLevel
    {
        get
        {
            if (m_DelCurrentLevel == null) return 1f;

            return m_DelCurrentLevel();
        }
        private set { }
    }

    // 체력 호출 함수
    public DelRetfloat m_DelCurrentHp;
    public float m_currentHp
    {
        get
        {
            if (m_DelCurrentHp == null) return 100f;

            return m_DelCurrentHp();
        }
        private set { }
    }

    // 방어도 호출 함수
    public DelRetfloat m_DelCurrentDefense;
    public float m_currentDefense
    {
        get
        {
            if (m_DelCurrentDefense == null) return 0f;

            return m_DelCurrentDefense();
        }
        private set { }
    }

    // 회피도 호출 함수
    public DelRetfloat m_DelCurrentEvade;
    public float m_currentEvade
    {
        get
        {
            if (m_DelCurrentEvade == null) return 0f;

            return m_DelCurrentEvade();
        }
        private set { }
    }

    public DelRetfloat m_DelCurrentCriticalResistance;
    public float m_currentCriticalResistance
    {
        get
        {
            if (m_DelCurrentCriticalResistance == null) return 0f;

            return m_DelCurrentCriticalResistance();
        }
        private set { }
    }

    public float m_startingHealth;
    public float m_health { get; protected set; }
    public bool m_dead { get; protected set; }
    public event Action m_OnDeath;      // 사망할 때 호출할 콜백 함수

    // 0.1초 내에 공격이 여러번 들어오면 무시
    private const float m_minTimeBetDamaged = 0.1f;
    private float m_lastDamagedTime;

    protected bool m_IsInvulnerable
    {
        get 
        {
            if (Time.time >= m_lastDamagedTime + m_minTimeBetDamaged) return false;

            return true;
        }
    }


    protected virtual void OnEnable()
    {
        m_dead = false;
        m_startingHealth = m_currentHp;
        m_health = m_startingHealth;
    }

    public virtual bool ApplyDamage(ref DamageMessage damageMessage)
    {
        if (m_IsInvulnerable || damageMessage.damager == gameObject || m_dead) return false;

        m_lastDamagedTime = Time.time;

        // 적중도 공식
        // (공격자의 적중도 - 방어자의 회피도)/1000 * 100%
        float accuracy = (damageMessage.accuracy - m_currentEvade) / 1000f * 100f;
        float rand = Random.Range(0f, 100f);
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
            float criticalRate = damageMessage.criticalRate - m_currentCriticalResistance + (damageMessage.accuracy - m_currentEvade) / 50;
            rand = Random.Range(0f, 100f);
            if (criticalRate >= rand)
            {
                // 치명타 공식
                // 1.8AD + 치피옵션
                damageMessage.damage = damageMessage.damage * 1.8f + damageMessage.criticalDamage;
            }
        }

        // 대미지 감소율 공식
        // 방어도 / (방어도 + (캐릭터 레벨 * 50))
        float defenseRate = m_currentDefense / (m_currentDefense + (m_currentLevel * 50f));
        m_health -= damageMessage.damage * (1f - defenseRate);

        if (m_health <= 0) Die();

        return true;
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if (m_dead) return;

        m_health += newHealth;
    }

    public virtual void  Die()
    {
        if (m_OnDeath != null) m_OnDeath();

        m_dead = true;
    }
}
