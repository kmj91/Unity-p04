using UnityEngine;

using MyEnum;
using MyStruct;

public partial class HaruInfo : PlayerInfo
{
    public SkinnedMeshRenderer faceRenderer;
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer handsRenderer;
    public SkinnedMeshRenderer pantsRenderer;
    public SkinnedMeshRenderer shoessRenderer;
    public SkinnedMeshRenderer hairRenderer;
    public Texture2D faceMask;
    public Texture2D bodyMask;
    public Texture2D sockMask;
    public Texture2D hairMask;

    public Color skinColor;
    public Color hiarColor;
    public HaruSkill[,] skillSlot { get; private set; }

    // 1 : 스킬 종류
    // 2 : 스킬 레벨
    // 3 : 타격 수
    private float[,,] skillDamage;

    public bool GetSkillDamage(HaruSkill skill, int cnt, ref float damage)
    {
        if (skill == HaruSkill.End)
            return false;

        if (cnt < 0 && 5 <= cnt)
            return false;

        damage = skillDamage[(int)skill, 0, cnt];

        return true;
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
            evade = 0f,
            damageReduction = 50f,
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

        // 스킬 슬롯 생성
        skillSlot = new HaruSkill[3, 6];

        skillSlot[0, 0] = HaruSkill.FirstBlade;
        skillSlot[1, 0] = HaruSkill.PierceStep;
        skillSlot[2, 0] = HaruSkill.SpinCutter;



        skillDamage = new float[(int)HaruSkill.End, 5, 10];

        skillDamage[(int)HaruSkill.NormalAttack1, 0, 0] = 0.5f;
        skillDamage[(int)HaruSkill.NormalAttack1, 1, 0] = 0.625f;
        skillDamage[(int)HaruSkill.NormalAttack1, 2, 0] = 0.75f;
        skillDamage[(int)HaruSkill.NormalAttack1, 3, 0] = 0.875f;
        skillDamage[(int)HaruSkill.NormalAttack1, 4, 0] = 1f;

        skillDamage[(int)HaruSkill.NormalAttack2, 0, 0] = 0.2f;
        skillDamage[(int)HaruSkill.NormalAttack2, 1, 0] = 0.325f;
        skillDamage[(int)HaruSkill.NormalAttack2, 2, 0] = 0.45f;
        skillDamage[(int)HaruSkill.NormalAttack2, 3, 0] = 0.575f;
        skillDamage[(int)HaruSkill.NormalAttack2, 4, 0] = 0.7f;

        skillDamage[(int)HaruSkill.NormalAttack3, 0, 0] = 0.3f;
        skillDamage[(int)HaruSkill.NormalAttack3, 1, 0] = 0.425f;
        skillDamage[(int)HaruSkill.NormalAttack3, 2, 0] = 0.55f;
        skillDamage[(int)HaruSkill.NormalAttack3, 3, 0] = 0.675f;
        skillDamage[(int)HaruSkill.NormalAttack3, 4, 0] = 0.8f;
        skillDamage[(int)HaruSkill.NormalAttack3, 0, 1] = 0.3f;
        skillDamage[(int)HaruSkill.NormalAttack3, 1, 1] = 0.425f;
        skillDamage[(int)HaruSkill.NormalAttack3, 2, 1] = 0.55f;
        skillDamage[(int)HaruSkill.NormalAttack3, 3, 1] = 0.675f;
        skillDamage[(int)HaruSkill.NormalAttack3, 4, 1] = 0.8f;

        skillDamage[(int)HaruSkill.NormalAttack4, 0, 0] = 0.3f;
        skillDamage[(int)HaruSkill.NormalAttack4, 1, 0] = 0.425f;
        skillDamage[(int)HaruSkill.NormalAttack4, 2, 0] = 0.55f;
        skillDamage[(int)HaruSkill.NormalAttack4, 3, 0] = 0.675f;
        skillDamage[(int)HaruSkill.NormalAttack4, 4, 0] = 0.8f;

        skillDamage[(int)HaruSkill.NormalAttack5, 0, 0] = 0.5f;
        skillDamage[(int)HaruSkill.NormalAttack5, 1, 0] = 0.625f;
        skillDamage[(int)HaruSkill.NormalAttack5, 2, 0] = 0.75f;
        skillDamage[(int)HaruSkill.NormalAttack5, 3, 0] = 0.875f;
        skillDamage[(int)HaruSkill.NormalAttack5, 4, 0] = 1f;

        skillDamage[(int)HaruSkill.FirstBlade, 0, 0] = 2.7f;
        skillDamage[(int)HaruSkill.FirstBlade, 0, 1] = 2.7f;
        skillDamage[(int)HaruSkill.FirstBlade, 0, 2] = 1.35f;
        skillDamage[(int)HaruSkill.FirstBlade, 1, 0] = 2.81f;
        skillDamage[(int)HaruSkill.FirstBlade, 1, 1] = 2.81f;
        skillDamage[(int)HaruSkill.FirstBlade, 1, 2] = 1.46f;
        skillDamage[(int)HaruSkill.FirstBlade, 2, 0] = 3.09f;
        skillDamage[(int)HaruSkill.FirstBlade, 2, 1] = 3.09f;
        skillDamage[(int)HaruSkill.FirstBlade, 2, 2] = 1.74f;
        skillDamage[(int)HaruSkill.FirstBlade, 3, 0] = 3.37f;
        skillDamage[(int)HaruSkill.FirstBlade, 3, 1] = 3.37f;
        skillDamage[(int)HaruSkill.FirstBlade, 3, 2] = 2.03f;
        skillDamage[(int)HaruSkill.FirstBlade, 4, 0] = 3.82f;
        skillDamage[(int)HaruSkill.FirstBlade, 4, 1] = 3.82f;
        skillDamage[(int)HaruSkill.FirstBlade, 4, 2] = 2.48f;
    }
}
