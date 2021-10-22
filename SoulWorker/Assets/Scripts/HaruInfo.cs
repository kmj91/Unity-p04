using UnityEngine;

using MyEnum;
using MyStruct;

public partial class HaruInfo : PlayerInfo
{
    public SkinnedMeshRenderer m_faceRenderer;
    public SkinnedMeshRenderer m_bodyRenderer;
    public SkinnedMeshRenderer m_handsRenderer;
    public SkinnedMeshRenderer m_pantsRenderer;
    public SkinnedMeshRenderer m_shoessRenderer;
    public SkinnedMeshRenderer m_hairRenderer;
    public Texture2D m_faceMask;
    public Texture2D m_bodyMask;
    public Texture2D m_sockMask;
    public Texture2D m_hairMask;

    public Color m_skinColor;
    public Color m_hiarColor;
    

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
        //public float m_evade;             // 회피도
        //public float damageReduction;   // 피해 감소[%]
        //public float criticalResistance;// 치명타 저항[%]
        //public float shorterCooldown;   // 재사용 대기시간 감소

        //public float partialDamage;     // 빗맞힘 시 피해[%] (기본 수치 50%)
        //public float superArmourBreak;  // 슈퍼아머 파괴력[%]
        #endregion
        // 플레이어 정보 초기화
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
            m_evade = 0f,
            damageReduction = 0f,
            criticalResistance = 0f,
            shorterCooldown = 0f,

            partialDamage = 50f,
            superArmourBreak = 0f
        };
        SetUp(ref data);
    }

    private void Start()
    {
        // 텍스처 마스크 검사
        CheckTextureMask();
        // 장비창 갱신
        UIManager.Instance.SetEquipmentStat(ref currentPlayerData);

        // 스킬 레벨 초기화
        m_skillLevel = new int[(int)HaruSkill.End];

        m_skillLevel[(int)HaruSkill.FirstBlade] = 1;
        m_skillLevel[(int)HaruSkill.PierceStep] = 1;
        m_skillLevel[(int)HaruSkill.SpinCutter] = 1;


        // 스킬 슬롯 생성
        m_skillSlot = new HaruSkill[(int)DEFAULT.SKILL_SLOT_X_SIZE, (int)DEFAULT.SKILL_SLOT_Y_SIZE];

        for (int x = 0; x < (int)DEFAULT.SKILL_SLOT_X_SIZE; ++x)
        {
            for (int y = 0; y < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++y)
            {
                m_skillSlot[x, y] = HaruSkill.None;
            }
        }

        m_skillSlot[0, 0] = HaruSkill.FirstBlade;
        m_skillSlot[0, 1] = HaruSkill.PierceStep;
        m_skillSlot[0, 2] = HaruSkill.SpinCutter;

        m_skillSlot[1, 0] = HaruSkill.PierceStep;
        m_skillSlot[1, 1] = HaruSkill.FirstBlade;

        m_skillSlot[2, 0] = HaruSkill.SpinCutter;


        // 스킬 슬롯 큐 초기화
        skillSlotQueue = new CirculartQueue<HaruSkill>[(int)DEFAULT.SKILL_SLOT_X_SIZE];

        for (int iCnt = 0; iCnt < (int)DEFAULT.SKILL_SLOT_X_SIZE; ++iCnt)
        {
            skillSlotQueue[iCnt] = new CirculartQueue<HaruSkill>(4);
        }


        m_skillDamage = new float[(int)HaruSkill.End, 5, 10];

        m_skillDamage[(int)HaruSkill.NormalAttack1, 0, 0] = 0.5f;
        m_skillDamage[(int)HaruSkill.NormalAttack1, 1, 0] = 0.625f;
        m_skillDamage[(int)HaruSkill.NormalAttack1, 2, 0] = 0.75f;
        m_skillDamage[(int)HaruSkill.NormalAttack1, 3, 0] = 0.875f;
        m_skillDamage[(int)HaruSkill.NormalAttack1, 4, 0] = 1f;

        m_skillDamage[(int)HaruSkill.NormalAttack2, 0, 0] = 0.2f;
        m_skillDamage[(int)HaruSkill.NormalAttack2, 1, 0] = 0.325f;
        m_skillDamage[(int)HaruSkill.NormalAttack2, 2, 0] = 0.45f;
        m_skillDamage[(int)HaruSkill.NormalAttack2, 3, 0] = 0.575f;
        m_skillDamage[(int)HaruSkill.NormalAttack2, 4, 0] = 0.7f;

        m_skillDamage[(int)HaruSkill.NormalAttack3, 0, 0] = 0.3f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 1, 0] = 0.425f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 2, 0] = 0.55f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 3, 0] = 0.675f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 4, 0] = 0.8f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 0, 1] = 0.3f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 1, 1] = 0.425f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 2, 1] = 0.55f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 3, 1] = 0.675f;
        m_skillDamage[(int)HaruSkill.NormalAttack3, 4, 1] = 0.8f;

        m_skillDamage[(int)HaruSkill.NormalAttack4, 0, 0] = 0.3f;
        m_skillDamage[(int)HaruSkill.NormalAttack4, 1, 0] = 0.425f;
        m_skillDamage[(int)HaruSkill.NormalAttack4, 2, 0] = 0.55f;
        m_skillDamage[(int)HaruSkill.NormalAttack4, 3, 0] = 0.675f;
        m_skillDamage[(int)HaruSkill.NormalAttack4, 4, 0] = 0.8f;

        m_skillDamage[(int)HaruSkill.NormalAttack5, 0, 0] = 0.5f;
        m_skillDamage[(int)HaruSkill.NormalAttack5, 1, 0] = 0.625f;
        m_skillDamage[(int)HaruSkill.NormalAttack5, 2, 0] = 0.75f;
        m_skillDamage[(int)HaruSkill.NormalAttack5, 3, 0] = 0.875f;
        m_skillDamage[(int)HaruSkill.NormalAttack5, 4, 0] = 1f;

        m_skillDamage[(int)HaruSkill.FirstBlade, 0, 0] = 2.7f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 0, 1] = 2.7f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 0, 2] = 1.35f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 1, 0] = 2.81f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 1, 1] = 2.81f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 1, 2] = 1.46f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 2, 0] = 3.09f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 2, 1] = 3.09f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 2, 2] = 1.74f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 3, 0] = 3.37f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 3, 1] = 3.37f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 3, 2] = 2.03f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 4, 0] = 3.82f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 4, 1] = 3.82f;
        m_skillDamage[(int)HaruSkill.FirstBlade, 4, 2] = 2.48f;


        m_skillCooldown = new float[(int)HaruSkill.End];

        m_skillCooldown[(int)HaruSkill.FirstBlade] = 10f;
        m_skillCooldown[(int)HaruSkill.PierceStep] = 8f;
        m_skillCooldown[(int)HaruSkill.SpinCutter] = 5f;


        m_readySkill = new bool[(int)HaruSkill.End];

        m_readySkill[(int)HaruSkill.FirstBlade] = true;
        m_readySkill[(int)HaruSkill.PierceStep] = true;
        m_readySkill[(int)HaruSkill.SpinCutter] = true;
    }

    
}
