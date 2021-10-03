﻿using UnityEngine;

using MyStruct;
using MyEnum;

public class PlayerInfo : MonoBehaviour
{
    public PlayerData originPlayerData;
    public PlayerData currentPlayerData;

    public Item headGear;
    public Item shoulderGear;
    public Item bodyGear;
    public Item legGear;
    public Item m_weapon;


    public void SetUp(ref PlayerData data)
    {
        originPlayerData = data;
        currentPlayerData = data;
    }

    public void UpdateInfo()
    {
        currentPlayerData = originPlayerData;

        foreach (var ability in m_weapon.m_abilityDatas)
        {
            switch (ability.type)
            {
                case AbilityType.Attack:
                    currentPlayerData.maxAtk += ability.amount;
                    currentPlayerData.minAtk = currentPlayerData.maxAtk * 0.8f;
                    break;
                case AbilityType.Defense:
                    currentPlayerData.defense += ability.amount;
                    break;
            }

        }
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

    public float GetCurrentEvade()
    {
        return currentPlayerData.m_evade;
    }

    public float GetCurrentCriticalResistance()
    {
        return currentPlayerData.criticalResistance;
    }
}
