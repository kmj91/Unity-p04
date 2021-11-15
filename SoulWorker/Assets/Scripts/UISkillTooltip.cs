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

    }
}
