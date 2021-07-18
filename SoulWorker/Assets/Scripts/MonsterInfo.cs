using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;

public class MonsterInfo : MonoBehaviour
{
    public MonsterData originMonsterInfo;
    public MonsterData monsterInfo;

    public void SetUp(ref MonsterData info)
    {
        originMonsterInfo = info;
        monsterInfo = info;
    }
}
