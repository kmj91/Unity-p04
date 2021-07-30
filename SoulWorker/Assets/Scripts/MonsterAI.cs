using UnityEngine;
using MyStruct;

public partial class MonsterAI : LivingEntity
{
    protected void SkillCondition(ref MonsterSkillInfo[,] skillGroup, int groupNum, out int useSkillNum)
    {
        float rand = Random.Range(0f, 99f);

        for (int cnt = 0; cnt < 3; ++cnt)
        {
            if (skillGroup[groupNum, cnt].skillNum != 0 &&
                rand < skillGroup[groupNum, cnt].random)
            {
                useSkillNum = skillGroup[groupNum, cnt].skillNum;
                return;
            }
        }

        useSkillNum = 0;
    }
}