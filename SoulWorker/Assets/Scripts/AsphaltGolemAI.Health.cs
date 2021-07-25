﻿using UnityEngine;

using MyStruct;
using MyEnum;

public partial class AsphaltGolemAI : LivingEntity
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        if (targetEntity == null)
        {
            targetEntity = damageMessage.damager.GetComponent<LivingEntity>();
        }

        if (!superArmourBreak)
            return true;

        //if (state == AsphaltGolemState.DMG_L)
        //{
        //    state = AsphaltGolemState.DMG_R;
        //}
        //else if (state == AsphaltGolemState.DMG_R)
        //{
        //    state = AsphaltGolemState.DMG_L;
        //}
        //else
        //{

        //}


        // 애니메이션 트리거
        SetTrigerDMGL();

        return true;
    }

    public override void Die()
    {
        base.Die();

        // 컬라이더 비활성화
        GetComponent<Collider>().enabled = false;
        // 에이전트 비활성화
        agent.enabled = false;

        // 애니메이션 처리...
    }

    public void SetUp(float health, float damage, float runSpeed, float patrolSpeed, Color skinColor)
    {
        startingHealth = health;
        this.health = health;
        this.damage = damage;
        moveSpeed = runSpeed;
        this.patrolSpeed = patrolSpeed;

        agent.speed = patrolSpeed;
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
        //public float evade;             // 회피도
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
            hp = 1500f,

            maxAtk = 200f,
            minAtk = 200f * 0.8f,
            criticalRate = 30f,
            criticalDamage = 0f,
            accuracy = 0f,
            armourBreak = 0f,

            defense = 100f,
            evade = 0f,
            damageReduction = 50f,
            criticalResistance = 0f,

            partialDamage = 0f,

            superArmour = 100f,
            CCImmunity = true
        };
        monsterInfo.SetUp(ref data);

        DelCurrentLevel = monsterInfo.GetCurrentLevel;
        DelCurrentHp = monsterInfo.GetCurrentHp;
        DelCurrentDefense = monsterInfo.GetCurrentDefense;
        DelCurrentEvade = monsterInfo.GetCurrentEvade;
        DelCurrentCriticalResistance = monsterInfo.GetCurrentCriticalResistance;
    }
}
