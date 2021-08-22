using MyEnum;

public class HaruInfo : PlayerInfo
{
    public HaruSkill[,] skillSlot { get; private set; }


    private void Start()
    {
        // 스킬 슬롯 생성
        skillSlot = new HaruSkill[3, 6];

        skillSlot[0, 0] = HaruSkill.FirstBlade;
        skillSlot[1, 0] = HaruSkill.PierceStep;
        skillSlot[2, 0] = HaruSkill.SpinCutter;
    }
}
