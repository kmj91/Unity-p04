using MyEnum;

public class HaruInfo : PlayerInfo
{
    private HaruSkill[,] skillSlot;


    private void Start()
    {
        // 스킬 슬롯 생성
        skillSlot = new HaruSkill[3, 6];

        skillSlot[0, 0] = HaruSkill.FirstBlade;
        skillSlot[0, 1] = HaruSkill.PierceStep;
        skillSlot[0, 2] = HaruSkill.SpinCutter;
    }
}
