using UnityEngine;

using MyStruct;
using MyEnum;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] protected PlayerData m_playerData;
    protected stPlayerData m_currentPlayerData;
    public stPlayerData currentPlayerData { get { return m_currentPlayerData; } }
    public int m_level = 1;         // 레벨

    public Item m_headGear;
    public Item m_shoulderGear;
    public Item m_bodyGear;
    public Item m_legGear;
    public Item m_weapon;



    public void UpdateInfo()
    {
        m_currentPlayerData = m_playerData.originPlayerData[m_level];

        foreach (var ability in m_weapon.m_abilityDatas)
        {
            switch (ability.type)
            {
                case AbilityType.Attack:
                    m_currentPlayerData.maxAtk += ability.amount;
                    m_currentPlayerData.minAtk = m_currentPlayerData.maxAtk * 0.8f;
                    break;
                case AbilityType.Defense:
                    m_currentPlayerData.defense += ability.amount;
                    break;
            }

        }
    }



    public float GetCurrentHp()
    {
        return m_currentPlayerData.hp;
    }

    public float GetCurrentLevel()
    {
        return m_level;
    }

    public float GetCurrentDefense()
    {
        return m_currentPlayerData.defense;
    }

    public float GetCurrentEvade()
    {
        return m_currentPlayerData.evade;
    }

    public float GetCurrentCriticalResistance()
    {
        return m_currentPlayerData.criticalResistance;
    }
}
