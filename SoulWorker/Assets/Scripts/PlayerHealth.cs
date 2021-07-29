using MyStruct;

public class PlayerHealth : LivingEntity
{
    public PlayerInfo playerInfo;         // 플레이어 정보

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        // UI 갱신
        UpdateUI();
    }

    public override bool ApplyDamage(ref DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(ref damageMessage)) return false;

        // 공격 받음

        // UI 갱신
        UpdateUI();

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
        #region
        //public float level;             // 레벨
        //public float hp;                // 체력
        //public float stamina;           // 스태미나
        //public float staminaRegen;      // 스태미나 회복
        //public float sg;                // sg
        //public float moveSpeed;         // 이동 속도[%]

        //public float maxAtk;            // 최대 공격력
        //public float minAtk;            // 최소 공격력  = 최대 공격력 * 0.8
        //public float criticalRate;      // 치명타 확률[%]
        //public float criticalDamage;    // 치명타 피해
        //public float atkSpeed;          // 공격 속도
        //public float accuracy;          // 적중도
        //public float armourBreak;       // 적 방어도 관통[%]
        //public float extraDmgToEnemy;   // 적 추가 피해 일반[%]
        //public float extraDmgToBossNamed;   // 적 추가 피해 보스 / 네임드[%]

        //public float defense;           // 방어도
        //public float evade;             // 회피도
        //public float damageReduction;   // 피해 감소[%]
        //public float criticalResistance;// 치명타 저항[%]
        //public float shorterCooldown;   // 재사용 대기시간 감소

        //public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)
        //public float superArmourBreak;  // 슈퍼아머 파괴력[%]
        #endregion
        // 몬스터 정보 초기화
        PlayerData data = new PlayerData
        {
            level = 1f,
            hp = 1150f,
            stamina = 100f,
            staminaRegen = 10f,
            sg = 200f,
            moveSpeed = 1f,

            maxAtk = 54f,
            minAtk = 54f * 0.8f,
            criticalRate = 1f,
            criticalDamage = 43f,
            atkSpeed = 1f,
            accuracy = 804f,
            armourBreak = 0f,
            extraDmgToEnemy = 0f,
            extraDmgToBossNamed = 0f,

            defense = 7f,
            evade = 0f,
            damageReduction = 50f,
            criticalResistance = 0f,
            shorterCooldown = 0f,

            partialDamage = 50f,
            superArmourBreak = 0f
        };
        playerInfo.SetUp(ref data);

        DelCurrentLevel = playerInfo.GetCurrentLevel;
        DelCurrentHp = playerInfo.GetCurrentHp;
        DelCurrentDefense = playerInfo.GetCurrentDefense;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // UI 갱신
        UIManager.Instance.UpdatePlayerHp(health, startingHealth);
    }
}
