using UnityEngine;
using UnityEngine.UI;

public class UITargetBoss : MonoBehaviour
{
    [SerializeField] private Sprite[] m_bossHpBarSprite;            // 보스 체력바 스프라이트
    [SerializeField] private Image m_bossHpBarBack;     // 보스 체력바 뒤쪽
    [SerializeField] private Image m_bossHpBarFront;    // 보스 체력바 앞쪽
    [SerializeField] private Image m_bossHpLostBar;     // 보스 잃은 체력바
    [SerializeField] private Text m_bossNameText;       // 보스 이름 텍스트
    [SerializeField] private GameObject m_bossSuperArmor;           // 보스 슈퍼 아머 상태일 때 아이콘과 텍스트 이미지
    [SerializeField] private Image m_bossSuperArmorBar;             // 보스 슈퍼 아머 바
    [SerializeField] private GameObject m_bossSuperArmorBreakGuard; // 보스 슈퍼 아머 브레이크 가드
    [SerializeField] private Animator m_bossSuperArmorBreakEffect;  // 보스 슈퍼 아머 브레이크 이펙트
    [SerializeField] private GameObject m_bossSuperArmorBreak;      // 보스 슈퍼 아머 브레이크 텍스트 이미지

    private int m_boosHpBarNum = -1;      // 현재 보스 체력 줄


    // 보스 체력
    public void SetBossHp(float currentHp, float maxHp)
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
    public void SetBossName(string name)
    {
        m_bossNameText.text = name;
    }

    // 보스 슈퍼 아머
    public void SetBossSuperArmor(float currentSA, float maxSA)
    {
        m_bossSuperArmorBar.fillAmount = currentSA / maxSA;
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
