using UnityEngine;
using UnityEngine.UI;
using MyEnum;

public class UIHotkey : MonoBehaviour
{
    [SerializeField] private Image[] m_hotkeySkillIcon;             // 스킬 아이콘
    [SerializeField] private GameObject[] m_hotkeySkillCooldownBack;// 재사용 대기시간 뒤쪽 이미지
    [SerializeField] private Image[] m_hotkeySkillCooldownFront;    // 재사용 대기시간 앞쪽 이미지, 재사용 시간 경과에 따라 fillAmount 값 조정
    [SerializeField] private Text[] m_hotkeySkillCooldownCount;     // 재사용 대기시간 카운트
    [SerializeField] private Text[] m_hotkeySkillSecondCooldownCount;       // 재사용 이전 스킬 대기시간 카운트
    [SerializeField] private GameObject[] m_hotkeySkillSecondCooldownBar;   // 재사용 대기시간 바


    // 스킬 재사용 대기시간 갱신
    public void UpdateSkillCooldown(int index, float originCooldown, float cooldown)
    {
        m_hotkeySkillCooldownBack[index].gameObject.SetActive(true);
        m_hotkeySkillCooldownFront[index].gameObject.SetActive(true);
        m_hotkeySkillCooldownCount[index].gameObject.SetActive(true);
        m_hotkeySkillCooldownCount[index].text = cooldown.ToString();
        m_hotkeySkillCooldownFront[index].fillAmount = cooldown / originCooldown;
    }

    // 스킬 아이콘 위쪽 재사용 대기시간 갱신
    public void UpdateSkillSecondCooldown(int index, float originCooldown, float cooldown)
    {
        m_hotkeySkillSecondCooldownBar[index].SetActive(true);
        m_hotkeySkillSecondCooldownCount[index].text = cooldown.ToString();
    }

    // 스킬 슬롯 아이콘 변경
    public void ChangeSkillSlotIcon(int index, HaruSkill skill)
    {
        m_hotkeySkillIcon[index].sprite = UIManager.Instance.m_haruSkillIcon[(int)skill];
    }

    // 스킬 재사용 대기시간 비활성화
    public void OffSkillSlotCooldown(int index)
    {
        m_hotkeySkillCooldownBack[index].gameObject.SetActive(false);
        m_hotkeySkillCooldownFront[index].gameObject.SetActive(false);
        m_hotkeySkillCooldownCount[index].gameObject.SetActive(false);
    }

    // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
    public void OffSkillSlotSecondCooldown(int index)
    {
        m_hotkeySkillSecondCooldownBar[index].SetActive(false);
    }
}
