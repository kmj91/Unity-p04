using UnityEngine;
using UnityEngine.UI;

using MyStruct;
using MyEnum;

public partial class UIManager : Singleton<UIManager>
{
    public Sprite[] m_haruSkillIcon;    // 하루 스킬 아이콘 스프라이트
    private HaruInfo m_haruInfo;        // 플레이어 정보

    [SerializeField] private UIPlayerHp m_UIPlayerHP;           // 플레이어 체력 바
    [SerializeField] private UITargetBoss m_UITargetBoss;       // 보스 체력 바
    [SerializeField] private UICharacterinfo m_UICharacterinfo; // 캐릭터 정보 창
    [SerializeField] private UIHotkey m_UIHotkey;               // 스킬 단축키
    [SerializeField] private UISkill m_UISkillInfo;             // 스킬 정보 창


    // 플레이어 정보 초기화
    public void InitPlayerInfo(HaruInfo playerInfo)
    {
        m_haruInfo = playerInfo;
        m_UICharacterinfo.m_haruInfo = playerInfo;
        m_UISkillInfo.m_haruInfo = playerInfo;
    }

    // 플레이어 정보 스크립트 얻어오기
    public HaruInfo GetPlayerInfo()
    {
        return m_haruInfo;
    }

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
    // 캐릭터 스텟 정보 초기화
    public void InitEquipmentStat()
    {
        m_UICharacterinfo.InitEquipmentStat();
    }

    // 장비창 트랜스폼 정보 얻어오기
    public Transform GetEquipmentTransform()
    {
        return m_UICharacterinfo.GetEquipmentTransform();
    }

    // 캐릭터 정보 창 토글
    public void ToggleCharacterinfo()
    {
        m_UICharacterinfo.ToggleCharacterinfo();
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
    public void InitSkillInfo()
    {
        m_UISkillInfo.InitSkillInfo();
    }

    // 스킬 정보 창 토글
    public void ToggleSkillInfo()
    {
        m_UISkillInfo.ToggleSkillInfo();
    }

    // 스킬 정보 창 스크립트 얻어오기
    public UISkill GetSkillInfo()
    {
        return m_UISkillInfo;
    }
}
