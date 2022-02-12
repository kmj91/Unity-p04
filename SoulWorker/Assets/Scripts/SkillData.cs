using UnityEngine;

using MyEnum;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    [System.Serializable]
    public class SkillType
    {
        [System.Serializable]
        public class SkillLevel
        {
            [System.Serializable]
            public class SkillDamageType
            {
                [System.Serializable]
                public class SkillHitCount
                {
                    public float damage;
                }

                public SkillHitCount[] hitCount;
                public float this[int index] { get { return hitCount[index].damage; } }
            }

            public SkillDamageType[] damageType;
            public SkillDamageType this[int index] { get { return damageType[index]; } }
        }

        public SkillLevel[] level;
        public SkillLevel this[int index] { get { return level[index]; } }
    }
    // 스킬 데미지
    // 1 : 스킬 종류
    // 2 : 스킬 레벨
    // 3 : 데미지 타입
    // 4 : 타격 수
    [SerializeField] private SkillType[] m_skillDamage;
    public SkillType[] SkillDamage { get { return m_skillDamage; } }

    // 스킬 재사용 대기시간
    [SerializeField] private float[] m_skillCooldown;
    public float[] SkillCooldown { get { return m_skillCooldown; } }


    // 스킬 공격 정보
    // 스킬 종류
    [System.Serializable]
    public class SkillAttackInfo
    {
        // 타격 수
        [System.Serializable]
        public class SkillHitCount
        {
            public float filst;                     // 공격 타이밍 시작
            public float second;                    // 공격 타이밍 끝
            [SerializeField] public AttackType attackType; // 공격 타입
        }

        public SkillHitCount[] hitCount;
        public SkillHitCount this[int index] { get { return hitCount[index]; } }
    }
    // 스킬 공격 정보
    // 1 : 스킬 종류
    // 2 : 타격 수
    [SerializeField] private SkillAttackInfo[] m_skillAttackTime;
    public SkillAttackInfo[] SkillAtkInfo { get { return m_skillAttackTime; } }
}
