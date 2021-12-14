using UnityEngine;
using MyEnum;
using System;

namespace MyStruct
{
    // 키 입력 정보
    public struct KeyInfo
    {
        public KeyInfo(KeyCode key, float time)
        {
            this.key = key;
            backupKey = key;
            this.time = time;
        }

        public KeyCode key;
        public KeyCode backupKey;
        public float time;
    }

    // 데미지 정보를 전달하는 메시지
    public struct DamageMessage
    {
        public GameObject damager;      // 공격자
        public float damage;            // 데미지
        public float criticalRate;      // 치명타 확률[%]
        public float criticalDamage;    // 치명타 피해
        public float accuracy;          // 적중도
        public float partialDamage;     // 빗맞힘 시 피해[%]
        public float power;             // 충격력
        public AttackType attackType;   // 공격 타입
        public Vector3 hitDir;          // 공격 방향
        public Vector3 hitPoint;
        public Vector3 hitNormal;
    }

    public struct AttackInfo
    {
        public float power;
        public float damage;
        public AttackType type;
    }

    // 몬스터 정보
    [Serializable]
    public struct MonsterData
    {
        public float level;             // 레벨
        public float hp;                // 체력

        public float maxAtk;            // 최대 공격력
        public float minAtk;            // 최소 공격력 = 최대 공격력 * 0.8
        public float criticalRate;      // 치명타 확률[%]
        public float criticalDamage;    // 치명타 피해
        public float accuracy;          // 적중도
        public float armourBreak;       // 적 방어도 관통[%]

        public float defense;           // 방어도
        public float m_evade;             // 회피도
        public float damageReduction;   // 피해 감소[%]
        public float criticalResistance;// 치명타 저항[%]

        public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)
        
        public float superArmour;        // 슈퍼아머
        public bool CCImmunity;         // CC 면역
    }

    // 플레이어 정보
    [Serializable]
    public struct stPlayerData
    {
        public float hp;                // 체력
        public float stamina;           // 스태미나
        public float staminaRegen;      // 스태미나 회복
        public float sg;                // sg
        public float moveSpeed;         // 이동 속도[%]

        public float maxAtk;            // 최대 공격력
        public float minAtk;            // 최소 공격력  = 최대 공격력 * 0.8
        public float criticalRate;      // 치명타 확률[%]
        public float criticalDamage;    // 치명타 피해
        public float atkSpeed;          // 공격 속도
        public float accuracy;          // 적중도
        public float armourBreak;       // 적 방어도 관통[%]
        public float extraDmgToEnemy;   // 적 추가 피해 일반[%]
        public float extraDmgToBossNamed;   // 적 추가 피해 보스 / 네임드[%]

        public float defense;           // 방어도
        public float evade;             // 회피도
        public float damageReduction;   // 피해 감소[%]
        public float criticalResistance;// 치명타 저항[%]
        public float shorterCooldown;   // 재사용 대기시간 감소

        public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)
        public float superArmourBreak;  // 슈퍼아머 파괴력[%]
    }

    public struct AbilityData
    {
        public AbilityType type;
        public float amount;
    }

    public struct MonsterSkillInfo
    {
        public MonsterSkillInfo(int skillNum, float random, float cooltime)
        {
            this.skillNum = skillNum;
            this.random = random;
            this.cooltime = cooltime;
        }

        public int skillNum;        // 스킬 번호
        public float random;        // 확률
        public float cooltime;      // 스킬 쿨타임
    }
}