using UnityEngine;

using MyStruct;
using MyEnum;

public partial class AsphaltGolemAI : MonsterAI
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override bool ApplyDamage(ref DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(ref damageMessage)) return false;

        // UI 갱신
        UpdateUI();

        // 죽었으면 나감
        if (m_dead)
            return false;

        // 타겟이 없으면
        if (m_targetEntity == null)
        {
            // 공격자를 타겟으로
            m_targetEntity = damageMessage.damager.GetComponent<LivingEntity>();
        }
        // 타겟이 있으면
        else
        {
            // 대미지 어그로 확률 1/10000
            var damager = damageMessage.damager.GetComponent<LivingEntity>();
            if (m_targetEntity != damager)
            {
                int aggro = Random.Range(0, 10000);
                // 타겟 변경
                if (aggro < 1)
                    m_targetEntity = damager;
            }
        }

        // 슈퍼아머 브레이크 상태
        if (!m_isSuperArmourBreak)
            return true;

        // 공격 타입에 따른 상태 변환
        FSM_Hit(damageMessage.attackType);

        return true;
    }

    public override void Die()
    {
        base.Die();

        // 컬라이더 비활성화
        GetComponent<Collider>().enabled = false;
        // 에이전트 비활성화
        m_agent.enabled = false;
        // 상태
        m_state = AsphaltGolemState.Death;
        // 애니메이션 트리거
        SetTrigerDeath();
    }

    public void SetUp(float health, float damage, float runSpeed, float patrolSpeed, Color skinColor)
    {
        m_startingHealth = health;
        m_health = health;
        m_damage = damage;
        m_moveSpeed = runSpeed;
        m_patrolSpeed = patrolSpeed;

        m_agent.speed = patrolSpeed;
    }



    private void MonsterInfoInit()
    {
        #region
        //public float level;             // 레벨
        //public float hp;                // 체력

        //public float maxAtk;            // 최대 공격력
        //public float minAtk;            // 최소 공격력 = 최대 공격력 * 0.8
        //public float criticalRate;      // 치명타 확률[%]
        //public float criticalDamage;    // 치명타 피해
        //public float accuracy;          // 적중도
        //public float armourBreak;       // 적 방어도 관통[%]

        //public float defense;           // 방어도
        //public float m_evade;             // 회피도
        //public float damageReduction;   // 피해 감소[%]
        //public float criticalResistance;// 치명타 저항[%]

        //public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)

        //public float superArmor;        // 슈퍼아머
        //public bool CCImmunity;         // CC 면역
        #endregion
        // 몬스터 정보 초기화
        MonsterData data = new MonsterData
        {
            level = 5f,
            hp = 15000f,

            maxAtk = 200f,
            minAtk = 200f * 0.8f,
            criticalRate = 30f,
            criticalDamage = 0f,
            accuracy = 800f,
            armourBreak = 0f,

            defense = 100f,
            m_evade = 0f,
            damageReduction = 50f,
            criticalResistance = 0f,

            partialDamage = 0f,

            superArmour = 100f,
            CCImmunity = true
        };
        m_monsterInfo.SetUp(ref data);

        m_DelCurrentLevel = m_monsterInfo.GetCurrentLevel;
        m_DelCurrentHp = m_monsterInfo.GetCurrentHp;
        m_DelCurrentDefense = m_monsterInfo.GetCurrentDefense;
        m_DelCurrentEvade = m_monsterInfo.GetCurrentEvade;
        m_DelCurrentCriticalResistance = m_monsterInfo.GetCurrentCriticalResistance;
    }

    private void FSM_Hit(AttackType type)
    {
        switch (type)
        {
            case AttackType.Normal:
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    m_state = AsphaltGolemState.DMG_L;
                    // 애니메이션 트리거
                    SetTrigerDMGL();
                }
                else
                {
                    m_state = AsphaltGolemState.DMG_R;
                    // 애니메이션 트리거
                    SetTrigerDMGR();
                }
                break;
            case AttackType.Upper:
                m_state = AsphaltGolemState.KD_Upp;
                SetTrigerKDUpp();
                break;
            case AttackType.Break:
                m_state = AsphaltGolemState.KB;
                SetTrigerKB();
                break;
            case AttackType.Down:
                m_state = AsphaltGolemState.KD_Ham;
                SetTrigerKDHam();
                break;
            case AttackType.Strike:
                m_state = AsphaltGolemState.KD_Str;
                SetTrigerKDStr();
                break;
            default:
                break;
        }

        
    }

    private void UpdateUI()
    {
        // UI 갱신
        UIManager.Instance.SetBossHp(m_health, m_startingHealth);
    }
}
