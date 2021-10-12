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

    [SerializeField] private Image m_playerHpBar;       // 플레이어 체력바
    [SerializeField] private Image m_playerHpSignal;    // 플레이어 체력 신호 애니메이션
    [SerializeField] private Text m_playerHpText;       // 플레이어 체력 텍스트
    [SerializeField] private Text m_playerName;         // 플레이어 이름

    [SerializeField] private Image m_bossHpBarBack;     // 보스 체력바 뒤쪽
    [SerializeField] private Image m_bossHpBarFront;    // 보스 체력바 앞쪽
    [SerializeField] private Image m_bossHpLostBar;     // 보스 잃은 체력바
    [SerializeField] private Text m_bossNameText;       // 보스 이름 텍스트
    [SerializeField] private GameObject m_bossSuperArmor;           // 보스 슈퍼 아머 상태일 때 아이콘과 텍스트 이미지
    [SerializeField] private Image m_bossSuperArmorBar;             // 보스 슈퍼 아머 바
    [SerializeField] private GameObject m_bossSuperArmorBreakGuard; // 보스 슈퍼 아머 브레이크 가드
    [SerializeField] private Animator m_bossSuperArmorBreakEffect;  // 보스 슈퍼 아머 브레이크 이펙트
    [SerializeField] private GameObject m_bossSuperArmorBreak;      // 보스 슈퍼 아머 브레이크 텍스트 이미지

    [SerializeField] private Sprite[] m_bossHpBarSprite;            // 보스 체력바 스프라이트

    [SerializeField] private GameObject m_characterinfo;            // 캐릭터 정보 창
    [SerializeField] private RectTransform m_equipmentTransform;    // 장비 창 위치
    [SerializeField] private Text m_equipmentHP;                    // 캐릭터 정보 HP
    [SerializeField] private Text m_equipmentAttack;                // 캐릭터 정보 공격력
    [SerializeField] private Text m_equipmentCritical;              // 캐릭터 정보 치명타
    [SerializeField] private Text m_equipmentAttackSpeed;           // 캐릭터 정보 공격 속도
    [SerializeField] private Text m_equipmentAccuracy;              // 캐릭터 정보 적중도
    [SerializeField] private Text m_equipmentArmourBreak;           // 캐릭터 정보 적 방어구 관통
    [SerializeField] private Text m_equipmentExtraDmgToEnemy;       // 캐릭터 정보 적 추가 피해 일반
    [SerializeField] private Text m_equipmentExtraDmgToBossNamed;   // 캐릭터 정보 적 추가 피해 네임드/보스
    [SerializeField] private Text m_equipmentStamina;               // 스태미나
    [SerializeField] private Text m_equipmentMoveSpeed;             // 이동속도
    [SerializeField] private Text m_equipmentDefense;               // 방어도
    [SerializeField] private Text m_equipmentEvade;                 // 회피도
    [SerializeField] private Text m_equipmentDamageReduction;       // 피해 감소
    [SerializeField] private Text m_equipmentCriticalResistance;    // 치명타 저항
    [SerializeField] private Text m_equipmentShorterCooldown;       // 재사용 대기시간 감소

    [SerializeField] private Sprite[] m_haruSkillIcon;              // 하루 스킬 아이콘 스프라이트
    [SerializeField] private Image[] m_hotkeySkillIcon;             // 스킬 아이콘
    [SerializeField] private GameObject[] m_hotkeySkillCooldownBack;// 재사용 대기시간 뒤쪽 이미지
    [SerializeField] private Image[] m_hotkeySkillCooldownFront;    // 재사용 대기시간 앞쪽 이미지, 재사용 시간 경과에 따라 fillAmount 값 조정
    [SerializeField] private Text[] m_hotkeySkillCooldownCount;     // 재사용 대기시간 카운트
    [SerializeField] private Text[] m_hotkeySkillSecondCooldownCount;       // 재사용 이전 스킬 대기시간 카운트
    [SerializeField] private GameObject[] m_hotkeySkillSecondCooldownBar;   // 재사용 대기시간 바

    private int m_boosHpBarNum = -1;      // 현재 보스 체력 줄


    // 플레이어 체력
    public void UpdatePlayerHp(float currentHp, float maxHp)
    {
        m_playerHpText.text = currentHp + "/" + maxHp;
        float amount = currentHp / maxHp;
        m_playerHpBar.fillAmount = amount;
        m_playerHpSignal.fillAmount = amount;
    }

    // 플레이어 이름
    public void UpdatePlayerName(string name)
    {
        m_playerName.text = name;
    }

    // 플레이어 SG
    public void UpdatePlayerSG(float sg)
    {

    }


    // 보스 체력
    public void UpdateBossHp(float currentHp, float maxHp)
    {
        // 9줄로 나눴을 때의 현제 체력 줄
        float num = currentHp / (maxHp / 9f);
        // 소수점 버림
        int hpBarNum = (int)num;

        // 보스 체력줄이 다름
        if (m_boosHpBarNum != hpBarNum)
        {
            // 0 보다 작으면 예외 처리
            if (hpBarNum < 0)
                hpBarNum = 0;

            // 앞쪽 체력바
            m_bossHpBarFront.sprite = m_bossHpBarSprite[hpBarNum % 6];

            // 체력바가 한줄 남았으면 뒤쪽 체력바는 비활성화
            if (hpBarNum != 0)
            {
                // 비활성화 되어있으면 활성화
                if (!m_bossHpBarBack.enabled)
                    m_bossHpBarBack.enabled = true;
                m_bossHpBarBack.sprite = m_bossHpBarSprite[(hpBarNum - 1) % 6];
            }
            else
            {
                // 비활성화
                m_bossHpBarBack.enabled = false;
            }

            // 체력줄이 바뀜
            m_boosHpBarNum = hpBarNum;
            m_bossHpLostBar.fillAmount = 1f;
        }

        

        float amount = num - hpBarNum;
        m_bossHpBarFront.fillAmount = amount;
    }

    // 보스 이름
    public void UpdateBossName(string name)
    {
        m_bossNameText.text = name;
    }

    // 보스 슈퍼 아머
    public void UpdateBossSuperArmor(float currentSA, float maxSA)
    {
        m_bossSuperArmorBar.fillAmount = currentSA / maxSA;
    }

    // GET 장비창 트랜스폼
    public Transform GetEquipmentTransform()
    {
        return m_equipmentTransform;
    }

    // 캐릭터 정보 창
    public void Characterinfo()
    {
        if (m_characterinfo.activeSelf)
        {
            m_characterinfo.SetActive(false);
        }
        else
        {
            m_characterinfo.SetActive(true);
        }
    }

    // 캐릭터 스텟 셋
    public void UpdateEquipment(ref PlayerData data)
    {
        m_equipmentHP.text = data.hp.ToString();
        m_equipmentAttack.text = data.minAtk + " - " + data.maxAtk;
        m_equipmentCritical.text = data.criticalRate.ToString("F1") + "% (+" + data.criticalDamage + ")";
        m_equipmentAttackSpeed.text = (data.atkSpeed * 100f).ToString("F1") + "%";
        m_equipmentAccuracy.text = data.accuracy.ToString();
        m_equipmentArmourBreak.text = data.armourBreak.ToString("F1") + "%";
        m_equipmentExtraDmgToEnemy.text = data.extraDmgToEnemy.ToString("F1") + "%";
        m_equipmentExtraDmgToBossNamed.text = data.extraDmgToBossNamed.ToString("F1") + "%";
        m_equipmentStamina.text = data.stamina.ToString();
        m_equipmentMoveSpeed.text = data.moveSpeed.ToString("F1") + "%";
        m_equipmentDefense.text = data.defense.ToString();
        m_equipmentEvade.text = data.m_evade.ToString();
        m_equipmentDamageReduction.text = data.damageReduction.ToString("F1") + "%";
        m_equipmentCriticalResistance.text = data.criticalResistance.ToString("F1") + "%";
        m_equipmentShorterCooldown.text = data.shorterCooldown.ToString("F1") + "%";
    }

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
        m_hotkeySkillIcon[index].sprite = m_haruSkillIcon[(int)skill];
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

    private void Update()
    {
        LostBarUpdate();
    }

    private void LostBarUpdate()
    {
        if (m_bossHpBarFront.fillAmount >= m_bossHpLostBar.fillAmount)
            return;

        // 잃은 체력을 따라감
        m_bossHpLostBar.fillAmount -= 0.0003f;
    }
}
