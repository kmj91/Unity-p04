using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    private HaruInfo m_haruinfo;     // 플레이어 정보

    [SerializeField] private Text[] m_skillLevel;               // 스킬 레벨 텍스트
    [SerializeField] private Sprite[] m_skillButtonSprite;      // 스킬 버튼 스프라이트
    [SerializeField] private Image[] m_skillLevelUpButton;      // 스킬 레벨 업 버튼 이미지
    [SerializeField] private Image[] m_skillLevelDownButton;    // 스킬 레벨 다운 버튼 이미지


    // 플레이어 정보
    public void SetPlayerInfo(HaruInfo playerinfo)
    {
        m_haruinfo = playerinfo;
    }

    // 창 닫기
    public void OnClickCloseSkillinfo()
    {
        gameObject.SetActive(false);
    }

    // 퍼스트 블레이드 스킬 레벨 업
    public void OnClickFirstBladeLavelUp()
    {
        
    }

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
