
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillDataScriptableObject", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }
}
