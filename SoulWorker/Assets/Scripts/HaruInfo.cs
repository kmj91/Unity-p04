using MyEnum;
using UnityEngine;

public class HaruInfo : PlayerInfo
{
    public SkinnedMeshRenderer faceRenderer;
    public Texture2D faceMask;
    public Color faceColor;
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

    private void Start()
    {
        // 얼굴 텍스처 마스크 색 입히기
        // 텍스처 생성
        Texture2D faceTexture = new Texture2D(faceMask.width, faceMask.height);

        for (int y = 0; y < faceMask.height; ++y)
        {
            for (int x = 0; x < faceMask.width; ++x)
            {
                Color mask = faceMask.GetPixel(x, y);
                // R 값 원본 얼굴 색상
                float fPer = (1 - mask.r) * 1.5f + mask.r;
                Color outColor = faceColor * fPer;
                // G 값 하이라이트 
                fPer = mask.g * 1.5f + (1 - mask.g);
                outColor = outColor * fPer;
                faceTexture.SetPixel(x, y, outColor);
            }
        }
        faceTexture.Apply();

        faceRenderer.materials[0].mainTexture = faceTexture;


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
