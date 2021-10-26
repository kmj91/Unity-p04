using UnityEngine;
using UnityEngine.UI;

using MyEnum;

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
        int level = m_haruinfo.GetSkillLevel(HaruSkill.FirstBlade);
        // 최대 레벨
        if (level + 1 > 5)
            return;

        // 텍스트 변경
        m_skillLevel[(int)HaruSkill.FirstBlade].text = ++level + "/5";
        // 증가한 레벨 플레이어 정보로
        m_haruinfo.SetSkillLevel(HaruSkill.FirstBlade, level);
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
