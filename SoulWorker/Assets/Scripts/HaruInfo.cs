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
        m_currentPlayerData = m_playerData.originPlayerData[m_level];
    }

    private void Start()
    {
        // 텍스처 마스크 검사
        CheckTextureMask();

        // 스킬 레벨 초기화
        m_skillLevel = new int[(int)HaruSkill.End];

        m_skillLevel[(int)HaruSkill.FirstBlade] = 1;
        m_skillLevel[(int)HaruSkill.PierceStep] = 1;
        m_skillLevel[(int)HaruSkill.SpinCutter] = 1;
        m_skillLevel[(int)HaruSkill.NormalAttack] = 1;


        // 스킬 슬롯 생성
        m_skillSlot = new HaruSkill[(int)SkillSlotSize.Column, (int)SkillSlotSize.Row];

        for (int x = 0; x < (int)SkillSlotSize.Column; ++x)
        {
            for (int y = 0; y < (int)SkillSlotSize.Row; ++y)
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
        skillSlotQueue = new CirculartQueue<HaruSkill>[(int)SkillSlotSize.Column];

        for (int iCnt = 0; iCnt < (int)SkillSlotSize.Column; ++iCnt)
        {
            skillSlotQueue[iCnt] = new CirculartQueue<HaruSkill>(4);
        }


        m_readySkill = new bool[(int)HaruSkill.End];

        m_readySkill[(int)HaruSkill.FirstBlade] = true;
        m_readySkill[(int)HaruSkill.PierceStep] = true;
        m_readySkill[(int)HaruSkill.SpinCutter] = true;
    }

    
}
