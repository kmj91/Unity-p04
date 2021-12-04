
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillDataScriptableObject", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }


    [System.Serializable]
    public class SkillType
    {
        public class SkillLevel
        {
            public class SkillHitCount
            {

            }

            public SkillHitCount[] HitCount;
            public SkillHitCount this[int index] { get { return HitCount[index]; } }
        }

        public SkillLevel[] level;
        public SkillLevel this[int index] { get { return level[index]; } }
    }
    [SerializeField] private SkillType[] m_skillDamage;    // 스킬 데미지
}
