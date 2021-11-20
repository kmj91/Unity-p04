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
    [SerializeField] private Text m_skillCommnet;               // 스킬 코멘트

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
                SetSkillInfo(HaruSkillDamage.FirstBlade, skill, "[기본 정보]");
                break;
            case HaruSkill.PierceStep:
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

    private void SetSkillInfo(HaruSkillDamage skillDamage, HaruSkill skill, string infoTitle)
    {
        // 스킬 정보 1 타이틀
        m_skillInfo[0].title.text = infoTitle;
        m_skillInfo[0].damage.text = "피해량 [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.Damage) * 100) + "%]";
        m_skillInfo[0].superAmourBreak.text = "슈퍼아머 파괴량 [" + (m_uiSkillInfo.m_haruInfo.GetSkillTooltipDamage(skillDamage, skill, SkillDamageType.SuperArmour) * 100) + "%]";
    }
}
