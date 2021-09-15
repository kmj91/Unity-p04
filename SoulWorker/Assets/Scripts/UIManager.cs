using UnityEngine;
using UnityEngine.UI;

using MyStruct;

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

    [SerializeField] private Image playerHpBar;     // 플레이어 체력바
    [SerializeField] private Image playerHpSignal;  // 플레이어 체력 신호 애니메이션
    [SerializeField] private Text playerHpText;     // 플레이어 체력 텍스트
    [SerializeField] private Text playerName;       // 플레이어 이름

    [SerializeField] private Image bossHpBarBack;   // 보스 체력바 뒤쪽
    [SerializeField] private Image bossHpBarFront;  // 보스 체력바 앞쪽
    [SerializeField] private Image bossHpLostBar;   // 보스 잃은 체력바
    [SerializeField] private Text bossNameText;     // 보스 이름 텍스트
    [SerializeField] private GameObject bossSuperArmor;         // 보스 슈퍼 아머 상태일 때 아이콘과 텍스트 이미지
    [SerializeField] private Image bossSuperArmorBar;           // 보스 슈퍼 아머 바
    [SerializeField] private Image bossSuperArmorBreakGuard;    // 보스 슈퍼 아머 브레이크 가드
    [SerializeField] private Image bossSuperArmorBreakEffect;   // 보스 슈퍼 아머 브레이크 이펙트
    [SerializeField] private Image bossSuperArmorBreak;         // 보스 슈퍼 아머 브레이크 텍스트 이미지

    [SerializeField] private Sprite[] bossHpBarSprite;  // 보스 체력바 스프라이트

    [SerializeField] private Image m_characterinfo;     // 캐릭터 정보 창
    public RectTransform m_equipmentTransform;          // 장비 창 위치
    public Text m_equipmentHP;                  // 캐릭터 정보 HP
    public Text m_equipmentAttack;              // 캐릭터 정보 공격력
    public Text m_equipmentCritical;            // 캐릭터 정보 치명타
    public Text m_equipmentAttackSpeed;         // 캐릭터 정보 공격 속도
    public Text m_equipmentAccuracy;            // 캐릭터 정보 적중도
    public Text m_equipmentArmourBreak;         // 캐릭터 정보 적 방어구 관통
    public Text m_equipmentExtraDmgToEnemy;     // 캐릭터 정보 적 추가 피해 일반
    public Text m_equipmentExtraDmgToBossNamed; // 캐릭터 정보 적 추가 피해 네임드/보스
    public Text m_equipmentStamina;             // 스태미나
    public Text m_equipmentMoveSpeed;           // 이동속도
    public Text m_equipmentDefense;             // 방어도
    public Text m_equipmentEvade;               // 회피도
    public Text m_equipmentDamageReduction;     // 피해 감소
    public Text m_equipmentCriticalResistance;  // 치명타 저항
    public Text m_equipmentShorterCooldown;     // 재사용 대기시간 감소


    private int boosHpBarNum = -1;      // 현재 보스 체력 줄


    // 플레이어 체력
    public void UpdatePlayerHp(float currentHp, float maxHp)
    {
        playerHpText.text = currentHp + "/" + maxHp;
        float amount = currentHp / maxHp;
        playerHpBar.fillAmount = amount;
        playerHpSignal.fillAmount = amount;
    }

    // 플레이어 이름
    public void UpdatePlayerName(string name)
    {
        playerName.text = name;
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
        if (boosHpBarNum != hpBarNum)
        {
            // 0 보다 작으면 예외 처리
            if (hpBarNum < 0)
                hpBarNum = 0;

            // 앞쪽 체력바
            bossHpBarFront.sprite = bossHpBarSprite[hpBarNum % 6];

            // 체력바가 한줄 남았으면 뒤쪽 체력바는 비활성화
            if (hpBarNum != 0)
            {
                // 비활성화 되어있으면 활성화
                if (!bossHpBarBack.enabled)
                    bossHpBarBack.enabled = true;
                bossHpBarBack.sprite = bossHpBarSprite[(hpBarNum - 1) % 6];
            }
            else
            {
                // 비활성화
                bossHpBarBack.enabled = false;
            }

            // 체력줄이 바뀜
            boosHpBarNum = hpBarNum;
            bossHpLostBar.fillAmount = 1f;
        }

        

        float amount = num - hpBarNum;
        bossHpBarFront.fillAmount = amount;
    }

    // 보스 이름
    public void UpdateBossName(string name)
    {
        bossNameText.text = name;
    }

    // 보스 슈퍼 아머
    public void UpdateBossSuperArmor(float currentSA, float maxSA)
    {
        bossSuperArmorBar.fillAmount = currentSA / maxSA;
    }

    // 캐릭터 정보 창
    public void Characterinfo()
    {
        if (m_characterinfo.IsActive())
        {
            m_characterinfo.gameObject.SetActive(false);
        }
        else
        {
            m_characterinfo.gameObject.SetActive(true);
        }
    }

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
    }

    private void Update()
    {
        LostBarUpdate();
    }

    private void LostBarUpdate()
    {
        if (bossHpBarFront.fillAmount >= bossHpLostBar.fillAmount)
            return;

        // 잃은 체력을 따라감
        bossHpLostBar.fillAmount -= 0.0003f;
    }
}
