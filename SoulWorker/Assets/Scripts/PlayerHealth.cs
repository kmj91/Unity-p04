using UnityEngine;

using MyStruct;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private PlayerInfo m_playerInfo;     // 플레이어 정보
    [SerializeField] private PlayerCtrl m_playerCtrl;     // 플레이어 상태 조작

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        // UI 갱신
        UpdateUI();
    }

    public override bool ApplyDamage(ref DamageMessage damageMessage)
    {
        // 회피
        if (m_playerCtrl.m_evade)
            return false;

        if (!base.ApplyDamage(ref damageMessage)) return false;

        // UI 갱신
        UpdateUI();

        // 공격 타입에 따른 상태 변환
        m_playerCtrl.FSM_Hit(ref damageMessage);

        return true;
    }

    public override void Die()
    {
        base.Die();

        // 사망

        // UI 갱신
        UpdateUI();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // UI 갱신
        //UpdateUI();
    }

    private void Awake()
    {
        m_DelCurrentLevel = m_playerInfo.GetCurrentLevel;
        m_DelCurrentHp = m_playerInfo.GetCurrentHp;
        m_DelCurrentDefense = m_playerInfo.GetCurrentDefense;
        m_DelCurrentEvade = m_playerInfo.GetCurrentEvade;
        m_DelCurrentCriticalResistance = m_playerInfo.GetCurrentCriticalResistance;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // UI 갱신
        UIManager.Instance.SetPlayerHp(m_health, m_startingHealth);
    }
}
