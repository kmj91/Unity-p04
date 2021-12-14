using UnityEngine;

using MyStruct;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private stPlayerData[] m_originPlayerData;
    public stPlayerData[] originPlayerData { get { return m_originPlayerData; } }
}
