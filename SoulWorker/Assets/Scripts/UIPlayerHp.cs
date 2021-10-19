using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHp : MonoBehaviour
{
    [SerializeField] private Image m_playerHpBar;       // 플레이어 체력바
    [SerializeField] private Image m_playerHpSignal;    // 플레이어 체력 신호 애니메이션
    [SerializeField] private Text m_playerHpText;       // 플레이어 체력 텍스트
    [SerializeField] private Text m_playerName;         // 플레이어 이름


    // 플레이어 체력
    public void SetPlayerHp(float currentHp, float maxHp)
    {
        m_playerHpText.text = currentHp + "/" + maxHp;
        float amount = currentHp / maxHp;
        m_playerHpBar.fillAmount = amount;
        m_playerHpSignal.fillAmount = amount;
    }

    // 플레이어 이름
    public void SetPlayerName(string name)
    {
        m_playerName.text = name;
    }

    // 플레이어 SG
    public void SetPlayerSG(float sg)
    {

    }
}
