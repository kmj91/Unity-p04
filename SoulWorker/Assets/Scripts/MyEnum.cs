namespace MyEnum
{
    public enum KeyState
    {
        Up,
        Down,
        Hold
    }

    public enum ItemType
    {
        HaruWeapon,
        HeadGear,
        ShoulderGear,
        BodyGear,
        LegGear,
        End
    }

    public enum UsePlayer
    {
        Public,
        Haru,
        End
    }

    public enum AbilityType
    {
        Attack,
        Defense,
        End
    }

    public enum AttackType
    {
        None,
        Normal,
        Upper,
        Down,
        Break,
        Strike,
        End
    }

    public enum FashionType
    {
        HairAccessory,      // 머리 장신구
        Glasses,            // 안경
        Mask,               // 마스크
        Hat,                // 모자
        Headgear,           // 모자 + 머리 장신구
        Helmet,             // 모자 + 머리 장신구 + 안경 + 마스크
        Top,                // 상의
        Bottom,             // 하의
        TopBottom,          // 상의 + 하의
        Gloves,             // 장갑
        Shoes,              // 신발
        Socks,              // 양말
        Underwear,          // 속옷
        Tail,               // 꼬리
        Back,               // 등
        BackTail,           // 등 + 꼬리
        Weapon,             // 무기
        End
    }

    public enum HaruState
    {
        Idle,
        Run,
        RunEnd,
        Dash,
        DashEnd,
        Jump,
        DashJump,
        Land,
        DashLand,
        Evade,
        DMG_L,
        DMG_R,
        KB,
        KD_Ham_F,
        KD_Ham_B,
        KD_Str,
        KD_Upp,
        KD_Upp_End,
        KD_Upp_Down,
        KD_Upp_Air_Hit,
        KD_Upp_Down_Hit,
        KD_Upp_Raise,
        NormalAttack1,
        NormalAttack2,
        NormalAttack3,
        NormalAttack4,
        NormalAttack5,
        FirstBlade,
        FirstBlade02,
        PierceStep,
        SpinCutter,
        End
    }

    // 하루 스킬 종류
    public enum HaruSkill
    {
        FirstBlade,
        PierceStep,
        SpinCutter,
        NormalAttack,
        None,
        End
    }

    // 스킬과 일반 공격들의 데미지를 정해주기 위한 enum
    public enum HaruSkillDamage
    {
        FirstBlade,
        PierceStep,
        SpinCutter,
        NormalAttack1,
        NormalAttack2,
        NormalAttack3,
        NormalAttack4,
        NormalAttack5,
        End
    }

    public enum AsphaltGolemState
    {
        Idle,
        Run,
        DMG_L,
        DMG_R,
        KB,
        KD_Ham,
        KD_Str,
        KD_Upp,
        Death,
        A_Skill_01,
        A_Skill_02,
        A_Skill_03,
        End
    }
}