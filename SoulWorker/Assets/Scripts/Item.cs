using System.Collections.Generic;
using UnityEngine;

using MyEnum;
using MyStruct;

public class Item : MonoBehaviour
{
    // 기본 공통
    public ItemType m_type;
    public UsePlayer m_usePlayer;
    public string m_itemName;
    public float m_useLevel;

    // 가변 적인 옵션
    public List<AbilityData> m_abilityDatas;
}
