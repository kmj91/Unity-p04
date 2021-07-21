using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;

public class PlayerInfo : MonoBehaviour
{
    public PlayerData originPlayerData;
    public PlayerData currentPlayerData;

    public void SetUp(ref PlayerData data)
    {
        originPlayerData = data;
        currentPlayerData = data;
    }



    public float GetCurrentHp()
    {
        return currentPlayerData.hp;
    }

    public float GetCurrentLevel()
    {
        return currentPlayerData.level;
    }

    public float GetCurrentDefense()
    {
        return currentPlayerData.defense;
    }
}
