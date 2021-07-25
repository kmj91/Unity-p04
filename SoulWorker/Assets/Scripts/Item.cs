using System.Collections.Generic;
using UnityEngine;

using MyEnum;
using MyStruct;

public class Item : MonoBehaviour
{
    // 기본 공통
    public ItemType type;
    public string itemName;
    public float useLevel;

    // 가변 적인 옵션
    public List<AbilityData> abilityDatas;
}
