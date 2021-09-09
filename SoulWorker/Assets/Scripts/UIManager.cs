using UnityEngine;
using UnityEngine.UI;

using MyEnum;
using System.Dynamic;

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
