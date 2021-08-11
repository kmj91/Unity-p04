using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
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

    private int boosHpBarNum = -1;


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

            boosHpBarNum = hpBarNum;
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

    private void Update()
    {
        
    }
}
