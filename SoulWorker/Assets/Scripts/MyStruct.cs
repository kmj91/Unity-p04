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
        public GameObject damager;
        public float amount;
        public Vector3 hitPoint;
        public Vector3 hitNormal;
    }

    // 몬스터 정보
    [Serializable]
    public struct MonsterData
    {
        public float level;             // 레벨
        public float hp;                // 체력

        public float moveSpeed;         // 이동 속도
        public float jumpPower;         // 점프력
        public float jumpVelocity;      // 점프 속도
        public float rotationSpeed;     // 회전 속도

        public float maxAtk;            // 최대 공격력
        public float minAtk;            // 최소 공격력 = 최대 공격력 * 0.8
        public float criticalRate;      // 치명타 확률[%]
        public float criticalDamage;    // 치명타 피해
        public float accuracy;          // 적중도
        public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)
        public float armourBreak;       // 적 방어도 관통[%]

        public float defense;           // 방어도
        public float evade;             // 회피도
        public float damageReduction;   // 피해 감소[%]
        public float criticalResistance;// 치명타 저항[%]

        public float superArmor;        // 슈퍼아머
        public bool CCImmunity;         // CC 면역
    }

    // 플레이어 정보
    [Serializable]
    public struct PlayerData
    {
        public float hp;             // 체력
        public float staminae;       // 스태미나
        public float staminaRegen;   // 스태미나 회복
        public float minAtk;         // 최소 공격력
        public float maxAtk;         // 최대 공격력
        public float criticalPer;    // 치명타 확률 퍼센트
        public float atkSpeed;       // 공격 속도
        public float atkSpeedPer;    // 공격 속도 퍼센트 ( * 100.f 이 실제 표시)
        public float moveSpeed;      // 이동 속도
        public float moveSpeedPer;   // 이동 속도 퍼센트 ( * 100.f 이 실제 표시)
        public float reduceMoveSpeed;// 이동속도 감속
        public float dashPer;        // 대쉬할 때 속도에 곱할 값, (m_fSpeed * m_fDash) = 대쉬 속도
        public float jumpPower;      // 점프력
        public float jumpVelocity;   // 점프 속도
        public float def;            // 방어력
        public float atkPer;         // 공격력 증가 퍼센트
        public float hpPer;          // 체력 증가 퍼센트
        public float bossAtkPer;     // 보스 추가 피해
        public float skillCoolPer;   // 재사용 대기시간 감소
    }
}