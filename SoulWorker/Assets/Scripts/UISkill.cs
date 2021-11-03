using UnityEngine;
using UnityEngine.UI;

using MyEnum;

public class UISkill : MonoBehaviour
{
    public HaruInfo m_haruInfo;     // 플레이어 정보

    [SerializeField] private Text[] m_skillLevel;               // 스킬 레벨 텍스트
    [SerializeField] private Sprite[] m_skillButtonSprite;      // 스킬 버튼 스프라이트
    [SerializeField] private Image[] m_skillLevelUpButton;      // 스킬 레벨 업 버튼 이미지
    [SerializeField] private Image[] m_skillLevelDownButton;    // 스킬 레벨 다운 버튼 이미지
    [SerializeField] private Text m_skillPoint;                 // 스킬 포인트 텍스트

    [System.Serializable]
    public class SkillPresetElement
    {
        public Image[] data;
        public Image this[int index] { get { return data[index]; } }
    }
    [SerializeField] private SkillPresetElement[] m_skillPreset;    // 스킬 프리셋 이미지

    private enum SkillButtonSprite
    {
        Gray,
        Yellow,
        Blut
    }


    // 창 닫기
    public void OnClickCloseSkillinfo()
    {
        gameObject.SetActive(false);
    }

    // 스킬 레벨 다운 버튼
    public void OnClickSkillLavelDown(int skill)
    {
        SkillLevelDown((HaruSkill)skill);
    }

    // 스킬 레벨 업 버튼
    public void OnClickSkillLavelUp(int skill)
    {
        SkillLevelUp((HaruSkill)skill);
    }



    // 스킬 정보 창 초기화
    public void InitSkillInfo()
    {
        // sp 정보
        m_skillPoint.text = m_haruInfo.GetSkillPoint() + " / " + m_haruInfo.GetMaxSkillPoint();

        // 프리셋 정보
        HaruSkill[,] skillSlot = m_haruInfo.GetSkillSlot();
        // 스킬 슬롯 전체 순회
        for (int x = 0; x < (int)SkillSlotSize.Column; ++x)
        {
            for (int y = 0; y < (int)SkillSlotSize.Row; ++y)
            {
                // 스킬 프리셋 이미지 초기화
                m_skillPreset[x][y].sprite = UIManager.Instance.m_haruSkillIcon[(int)skillSlot[x, y]];
                m_skillPreset[x][y].enabled = true;
            }
        }
    }

    // 스킬 정보 창 토글
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


    // 스킬 레벨 다운
    private void SkillLevelDown(HaruSkill skill)
    {
        int level = m_haruInfo.GetSkillLevel(skill);
        // 0 보다 작음
        if (level - 1 < 0)
            return;

        // 0 레벨
        if (level - 1 == 0)
        {
            // 기본 스킬 예외 
            if (HaruSkill.FirstBlade == skill || HaruSkill.PierceStep == skill || HaruSkill.SpinCutter == skill)
                return;
            m_skillLevelDownButton[(int)skill].sprite = m_skillButtonSprite[(int)SkillButtonSprite.Gray];
        }

        // 5레벨
        if (level == 5)
            m_skillLevelUpButton[(int)skill].sprite = m_skillButtonSprite[(int)SkillButtonSprite.Yellow];

        // 1레벨 기본 스킬 예외 
        if (level - 1 == 1 && (HaruSkill.FirstBlade == skill || HaruSkill.PierceStep == skill || HaruSkill.SpinCutter == skill))
            m_skillLevelDownButton[(int)skill].sprite = m_skillButtonSprite[(int)SkillButtonSprite.Gray];

        // 텍스트 변경
        m_skillLevel[(int)skill].text = --level + "/5";
        // 감소한 레벨 플레이어 정보로
        m_haruInfo.SetSkillLevel(skill, level);
        // sp 증가
        int sp = m_haruInfo.GetSkillPoint();
        m_haruInfo.SetSkillPoint(++sp);
        m_skillPoint.text = sp + " / " + m_haruInfo.GetMaxSkillPoint();
    }

    // 스킬 레벨 업
    private void SkillLevelUp(HaruSkill skill)
    {
        int sp = m_haruInfo.GetSkillPoint();
        // sp가 충분한지 검사
        if (sp < 1)
            return;

        int level = m_haruInfo.GetSkillLevel(skill);
        // 5 레벨 보다 큼
        if (level + 1 > 5)
            return;

        // 5 레벨
        if (level + 1 == 5)
            m_skillLevelUpButton[(int)skill].sprite = m_skillButtonSprite[(int)SkillButtonSprite.Gray];

        // 0레벨
        if(level == 0)
            m_skillLevelDownButton[(int)skill].sprite = m_skillButtonSprite[(int)SkillButtonSprite.Yellow];

        // 텍스트 변경
        m_skillLevel[(int)skill].text = ++level + "/5";
        // 증가한 레벨 플레이어 정보로
        m_haruInfo.SetSkillLevel(skill, level);
        // sp 감소
        m_haruInfo.SetSkillPoint(--sp);
        m_skillPoint.text = sp + " / " + m_haruInfo.GetMaxSkillPoint();
    }
}
