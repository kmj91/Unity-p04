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
    [SerializeField] private Text m_skillPoint;                 // 스킬 포인트 텍스트


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

    // 퍼스트 블레이드 스킬 레벨 다운
    public void OnClickFirstBladeLavelDown()
    {
        SkillLevelDown(HaruSkill.FirstBlade);
    }

    // 퍼스트 블레이드 스킬 레벨 업
    public void OnClickFirstBladeLavelUp()
    {
        SkillLevelUp(HaruSkill.FirstBlade);
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


    // 스킬 레벨 업
    private void SkillLevelDown(HaruSkill skill)
    {
        int level = m_haruinfo.GetSkillLevel(skill);
        // 최소 레벨
        if (level - 1 < 0)
            return;

        // 텍스트 변경
        m_skillLevel[(int)skill].text = --level + "/5";
        // 감소한 레벨 플레이어 정보로
        m_haruinfo.SetSkillLevel(skill, level);
        // sp 증가
        int sp = m_haruinfo.GetSkillPoint();
        m_haruinfo.SetSkillPoint(++sp);
        m_skillPoint.text = sp + " / " + m_haruinfo.GetMaxSkillPoint();
    }

    // 스킬 레벨 다운
    private void SkillLevelUp(HaruSkill skill)
    {
        int sp = m_haruinfo.GetSkillPoint();
        // sp가 충분한지 검사
        if (sp < 1)
            return;

        int level = m_haruinfo.GetSkillLevel(skill);
        // 최대 레벨
        if (level + 1 > 5)
            return;

        // 텍스트 변경
        m_skillLevel[(int)skill].text = ++level + "/5";
        // 증가한 레벨 플레이어 정보로
        m_haruinfo.SetSkillLevel(skill, level);
        // sp 감소
        m_haruinfo.SetSkillPoint(--sp);
        m_skillPoint.text = sp + " / " + m_haruinfo.GetMaxSkillPoint();
    }
}
