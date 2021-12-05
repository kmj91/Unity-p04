
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillDataScriptableObject", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }


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
                public SkillHitCount this[int index] { get { return hitCount[index]; } }
            }

            public SkillDamageType[] damageType;
            public SkillDamageType this[int index] { get { return damageType[index]; } }
        }

        public SkillLevel[] level;
        public SkillLevel this[int index] { get { return level[index]; } }
    }
    [SerializeField] private SkillType[] m_skillDamage;    // 스킬 데미지
}
