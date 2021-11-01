using UnityEngine;
using UnityEngine.UI;

using MyStruct;
using MyEnum;

public partial class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();

            return instance;
        }
    }

    public Sprite[] m_haruSkillIcon;          // 하루 스킬 아이콘 스프라이트

    [SerializeField] private UIPlayerHp m_UIPlayerHP;           // 플레이어 체력 바
    [SerializeField] private UITargetBoss m_UITargetBoss;       // 보스 체력 바
    [SerializeField] private UICharacterinfo m_UICharacterinfo; // 캐릭터 정보 창
    [SerializeField] private UIHotkey m_UIHotkey;               // 스킬 단축키
    [SerializeField] private UISkill m_UISkillinfo;             // 스킬 정보 창


    //-------------------------------
    // 보스 체력 바
    //-------------------------------
    // 플레이어 체력
    public void SetPlayerHp(float currentHp, float maxHp)
    {
        m_UIPlayerHP.SetPlayerHp(currentHp, maxHp);
    }

    // 플레이어 이름
    public void SetPlayerName(string name)
    {
        m_UIPlayerHP.SetPlayerName(name);
    }

    // 플레이어 SG
    public void SetPlayerSG(float sg)
    {
        m_UIPlayerHP.SetPlayerSG(sg);
    }


    //-------------------------------
    // 보스 체력 바
    //-------------------------------
    // 보스 체력
    public void SetBossHp(float currentHp, float maxHp)
    {
        m_UITargetBoss.SetBossHp(currentHp, maxHp);
    }

    // 보스 이름
    public void SetBossName(string name)
    {
        m_UITargetBoss.SetBossName(name);
    }

    // 보스 슈퍼 아머
    public void SetBossSuperArmor(float currentSA, float maxSA)
    {
        m_UITargetBoss.SetBossSuperArmor(currentSA, maxSA);
    }

    //-------------------------------
    // 캐릭터 정보 창
    //-------------------------------
    // GET 장비창 트랜스폼
    public Transform GetEquipmentTransform()
    {
        return m_UICharacterinfo.GetEquipmentTransform();
    }

    // 캐릭터 정보 창
    public void ToggleCharacterinfo()
    {
        m_UICharacterinfo.ToggleCharacterinfo();
    }

    // 캐릭터 스텟 셋
    public void SetEquipmentStat(ref PlayerData data)
    {
        m_UICharacterinfo.SetEquipmentStat(ref data);
    }

    //-------------------------------
    // 스킬 단축키
    //-------------------------------
    // 스킬 재사용 대기시간 갱신
    public void UpdateSkillCooldown(int index, float originCooldown, float cooldown)
    {
        m_UIHotkey.UpdateSkillCooldown(index, originCooldown, cooldown);
    }

    // 스킬 아이콘 위쪽 재사용 대기시간 갱신
    public void UpdateSkillSecondCooldown(int index, float originCooldown, float cooldown)
    {
        m_UIHotkey.UpdateSkillSecondCooldown(index, originCooldown, cooldown);
    }

    // 스킬 슬롯 아이콘 변경
    public void ChangeSkillSlotIcon(int index, HaruSkill skill)
    {
        m_UIHotkey.ChangeSkillSlotIcon(index, skill);
    }

    // 스킬 재사용 대기시간 비활성화
    public void OffSkillSlotCooldown(int index)
    {
        m_UIHotkey.OffSkillSlotCooldown(index);
    }

    // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
    public void OffSkillSlotSecondCooldown(int index)
    {
        m_UIHotkey.OffSkillSlotSecondCooldown(index);
    }

    //-------------------------------
    // 스킬 정보 창
    //-------------------------------
    // 스킬 정보 창 초기화
    public void InitSkillInfo(HaruInfo playerinfo)
    {
        m_UISkillinfo.InitSkillInfo(playerinfo);
    }

    // 스킬 정보 창
    public void ToggleSkillinfo()
    {
        m_UISkillinfo.ToggleSkillinfo();
    }
}
