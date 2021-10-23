using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    [SerializeField] private Text[] m_skillLevel;     // 스킬 레벨 텍스트


    // 스킬 정보 창
    public void ToggleSkillinfo()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // 스킬 포인트
    public void SetSkillPoint(int MaxSP, int SP)
    {

    }

    // 스킬 레벨
    public void SetSkillLevel()
    {

    }
}
