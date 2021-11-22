using UnityEngine;
using UnityEngine.UI;

using MyEnum;

public class UISkillTooltip : MonoBehaviour
{
    // 스킬 타입 스프라이트 번호
    public enum SkillTypeSprite
    {
        Active,
        Passive,
        End
    }

    [SerializeField] private Sprite[] m_skillTypeSprite;        // 스킬 타입 스프라이트

    // 스킬 타이틀
    [SerializeField] private Image m_skillIcon;                 // 스킬 아이콘
    [SerializeField] private Image m_skillType;                 // 스킬 타입
    [SerializeField] private Text m_skillName;                  // 스킬 이름
    [SerializeField] private Text m_skillLevel;                 // 스킬 레벨
    [SerializeField] private GameObject m_skillMaster;          // 스킬 마스터 (마스터면 표시)
    [SerializeField] private GameObject m_skillUnacquired;      // 스킬 미습득 (미습득이면 표시)

    // 스킬 정보
    [System.Serializable]
    private class SkillInfo
    {
        public Text title;              // 타이틀
        public Text damage;             // 데미지
        public Text superAmourBreak;    // 슈퍼아머 파괴량
        public GameObject layout;       // 스킬 정보 레이아웃 오브젝트
    }
    [SerializeField] private SkillInfo[] m_skillInfo;           // 스킬 정보

    // 스킬 일반 정보
    [SerializeField] private GameObject m_skillNormalLayout;    // 스킬 정보 노말 레이아웃 오브젝트
    [SerializeField] private Text m_normalTitle;                // 노말 타이틀
    [SerializeField] private Text m_skillSG;                    // SG 소모량
    [SerializeField] private Text m_skillCooldown;              // 재사용 대기 시간

    // 코멘트
    [SerializeField] private Text m_skillComment;               // 스킬 코멘트

    // 스킬 메뉴얼
    [System.Serializable]
    private class SkillManual
    {
        public Text title;              // 타이틀
        public Text text;               // 텍스트
        public GameObject layout;       // 메뉴얼 레이아웃 오브젝트
    }
    [SerializeField] private SkillManual[] m_skillManual;       // 스킬 메뉴얼

    private UISkill m_uiSkillInfo;                              // 스킬 정보 창


    private void Start()
    {
        // 스킬 정보 창
        m_uiSkillInfo = UIManager.Instance.GetSkillInfo();
    }

    // 스킬 툴팁 켜기
    public void ShowSkillTooltip(HaruSkill skill)
    {
        gameObject.SetActive(true);
        // 스킬 아이콘 변경
        m_skillIcon.sprite = UIManager.Instance.m_haruSkillIcon[(int)skill];

        // 툴팁 갱신
        UpdateTooltip(skill);
    }


    // 툴팁 갱신
    private void UpdateTooltip(HaruSkill skill)
    {
        // 스킬 레벨
        int skillLevel = m_uiSkillInfo.m_haruInfo.GetSkillLevel(skill);

        switch (skill)
        {
            case HaruSkill.FirstBlade:
                SetTitle(SkillTypeSprite.Active, "퍼스트 블레이드", skillLevel);
                SetSkillInfo(HaruSkillDamage.FirstBlade, skill, "[스킬 정보]");
                SetSkillInfoSecond(HaruSkillDamage.FirstBlade02, skill, "[스킬 정보 : 추가 공격]");
                SetSkillInfoNormal(skill, "[스킬 정보 : 일반]", 35);
                SetSkillComment("빠르게 전진하며 검을 세번 휘둘러 마무리로 적을 밀쳐냅니다.\n추가 조작시 밀쳐내는 대신 검을 올려칩니다.");
                SetManual("[조작법]", "마우스 : 스킬 퀵슬롯에 등록하여 해당 키 입력");
                SetManualSecond("[조작법 : 추가 공격]", "마우스 : 두번째 공격 이후 (우클릭)");
                break;
            case HaruSkill.PierceStep:
                SetTitle(SkillTypeSprite.Active, "피어스 스텝", skillLevel);
                SetSkillInfo(HaruSkillDamage.PierceStep, skill, "[스킬 정보]");
                m_skillInfo[1].layout.SetActive(false);
                SetSkillInfoNormal(skill, "[스킬 정보 : 일반]", 15);
                SetSkillComment("전방으로 빠르게 돌진하며 적을 미쳐냅니다.\n스킬을 캔슬하여 사용할 수 있습니다.");
                SetManual("[조작법]", "마우스 : 스킬 퀵슬롯에 등록하여 해당 키 입력");
                m_skillManual[1].layout.SetActive(false);
                break;
            case HaruSkill.SpinCutter:
                break;
            default:
                break;
        }
    }

    // 툴팁 타이틀
    private void SetTitle(SkillTypeSprite skillType, string skillName, int skillLevel)
    {
        // 스킬 타입 스프라이트
        m_skillType.sprite = m_skillTypeSprite[(int)skillType];

        // 스킬 레벨 로마자
        string roma = "";
        switch (skillLevel)
        {
            case 1:
                roma = "I";
                break;
            case 2:
                roma = "II";
                break;
            case 3:
                roma = "III";
                break;
            case 4:
                roma = "IV";
                break;
            case 5:
                roma = "V";
                break;
            default:
                break;
        }
        // 스킬 이름 + 로마자
        m_skillName.text = skillName + " " + roma;
        // 스킬 레벨
        m_skillLevel.text = "레벨 " + skillLevel;
    }

    // 스킬 정보
    private void SetSkillInfo(HaruSkillDamage skillDamage, HaruSkill skill, string infoTitle)
    {
        // 스킬 정보 1 타이틀
        m_skillInfo[0].title.text = infoTitle;
        m_skillInfo[0].damage.text = "피해량 : [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.Damage) * 100) + "%]";
        m_skillInfo[0].superAmourBreak.text = "슈퍼아머 파괴량 : [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.SuperArmour) * 100) + "%]";
    }

    // 스킬 정보 두번째
    private void SetSkillInfoSecond(HaruSkillDamage skillDamage, HaruSkill skill, string infoTitle)
    {
        // 스킬 정보 2 타이틀
        m_skillInfo[1].title.text = infoTitle;
        m_skillInfo[1].damage.text = "피해량 : [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.Damage) * 100) + "%]";
        m_skillInfo[1].superAmourBreak.text = "슈퍼아머 파괴량 : [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.SuperArmour) * 100) + "%]";
    }

    // 스킬 정보 일반
    private void SetSkillInfoNormal(HaruSkill skill, string infoTitle, int sg)
    {
        // 스킬 정보 2 타이틀
        m_normalTitle.text = infoTitle;
        m_skillSG.text = "SG 소모량 : [" + sg + "]";
        m_skillCooldown.text = "재사용 대기 시간 : [" + (int)m_uiSkillInfo.m_haruInfo.GetSkillCooldown(skill) + "초]";
    }

    // 스킬 코멘트
    private void SetSkillComment(string comment)
    {
        m_skillComment.text = comment;
    }

    // 메뉴얼
    private void SetManual(string title, string text)
    {
        m_skillManual[0].title.text = title;
        m_skillManual[0].text.text = text;
    }

    // 메뉴얼 두번째
    private void SetManualSecond(string title, string text)
    {
        m_skillManual[1].title.text = title;
        m_skillManual[1].text.text = text;
    }
}
