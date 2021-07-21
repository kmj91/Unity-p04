using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;

public class MonsterInfo : MonoBehaviour
{
    public MonsterData originMonsterData;
    public MonsterData currentMonsterData;

    public void SetUp(ref MonsterData data)
    {
        originMonsterData = data;
        currentMonsterData = data;
    }



    public float GetCurrentHp()
    {
        return currentMonsterData.hp;
    }

    public float GetCurrentLevel()
    {
        return currentMonsterData.level;
    }

    public float GetCurrentDefense()
    {
        return currentMonsterData.defense;
    }
}
