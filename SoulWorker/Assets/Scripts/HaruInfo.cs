﻿using UnityEngine;
using System.Collections;

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

    private HaruSkill[,] m_skillSlot;   // 스킬 슬롯
    private CirculartQueue<HaruSkill>[] skillSlotQueue; // 스킬 슬롯 큐
    // 1 : 스킬 종류
    // 2 : 스킬 레벨
    // 3 : 타격 수
    private float[,,] m_skillDamage;
    private float[] m_skillCooldown;    // 스킬 재사용 대기시간
    private bool[] m_readySkill;        // 스킬 사용 준비


    private enum DEFAULT
    {
        SKILL_SLOT_Y_SIZE = 3,
        SKILL_SLOT_X_SIZE = 6
    }

    // 스킬 슬롯 인덱스의 스킬 상태 얻기
    public bool GetStateOfSkillSlot(int index, out HaruState m_state)
    {
        for (int iCnt = 0; iCnt < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++iCnt)
        {
            // 스킬 사용 가능
            if (m_readySkill[(int)m_skillSlot[index, iCnt]])
            {
                // skill -> m_state 변환
                switch (m_skillSlot[index, iCnt])
                {
                    case HaruSkill.FirstBlade:
                        m_state = HaruState.FirstBlade;
                        return true;
                    case HaruSkill.PierceStep:
                        m_state = HaruState.PierceStep;
                        return true;
                    case HaruSkill.SpinCutter:
                        m_state = HaruState.SpinCutter;
                        return true;
                    default:
                        break;
                }
            }
        }
        m_state = HaruState.End;
        return false;
    }

    // 스킬 사용
    public bool InputSkillSlot(int index)
    {
        for (int iCnt = 0; iCnt < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++iCnt)
        {
            HaruSkill skill = m_skillSlot[index, iCnt];
            // 스킬 사용 가능한지 확인
            if (m_readySkill[(int)skill])
            {
                m_readySkill[(int)skill] = false;
                // 사용 스킬 큐에 입력
                EnqueueUseSkill(skill);
                // 코루틴
                StartCoroutine(CountSkillCooldown(skill));
                return true;
            }
        }

        return false;
    }

    // 스킬 데미지 정보 얻기
    // skill : 스킬 이름
    // cnt : 스킬 타격수
    // damage : 스킬 데미지
    public bool GetSkillDamage(HaruSkill skill, int cnt, ref float damage)
    {
        if (cnt < 0 && 5 <= cnt)
            return false;

        damage = m_skillDamage[(int)skill, 0, cnt];

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
        UIManager.Instance.UpdateEquipment(ref currentPlayerData);

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

    private IEnumerator CountSkillCooldown(HaruSkill skill)
    {
        float cooldown = m_skillCooldown[(int)skill];
        float originCooldown = cooldown;
        // 1초
        WaitForSeconds wait = new WaitForSeconds(1f);

        // UI 재사용 대기시간 처리
        AllSkillSlotCheckCooldown(skill, originCooldown, cooldown);

        while (true)
        {
            yield return wait;

            --cooldown;
            if (cooldown == 0f)
                break;

            // UI 재사용 대기시간 처리
            AllSkillSlotCheckCooldown(skill, originCooldown, cooldown);
        }

        // 스킬 사용 가능
        m_readySkill[(int)skill] = true;
        // UI 재사용 대기시간 완료 처리
        AllSkillSlotCheckReady(skill);
    }

    // 스킬 사용 큐에 입력
    private void EnqueueUseSkill(HaruSkill skill)
    {
        // 스킬 슬롯 전체 순회
        for (int x = 0; x < (int)DEFAULT.SKILL_SLOT_X_SIZE; ++x)
        {
            for (int y = 0; y < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++y)
            {
                // 사용하려는 스킬과 같음
                if (m_skillSlot[x, y] == skill)
                {
                    skillSlotQueue[x].Enqueue(skill);
                    break;
                }
            }
        }
    }

    // 스킬 삭제 큐에서 삭제
    private void EraseSkill(HaruSkill skill, int index)
    {
        for (int y = 0; y < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++y)
        {
            // 사용하려는 스킬과 같음
            if (m_skillSlot[index, y] == skill)
            {
                // 삭제
                skillSlotQueue[index].Erase(skill);
                break;
            }
        }
    }

    // 모든 스킬 슬롯 재사용 대기시간 검사
    private void AllSkillSlotCheckCooldown(HaruSkill skill, float originCooldown, float cooldown)
    {
        // 스킬 슬롯 전체 순회
        for (int x = 0; x < (int)DEFAULT.SKILL_SLOT_X_SIZE; ++x)
        {
            for (int y = 0; y < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++y)
            {
                // 사용하려는 스킬과 같음
                if (m_skillSlot[x, y] == skill)
                {
                    // 해당 스킬 슬롯 재사용 대기시간 처리
                    UpdateUISkillSlotCooldown(skill, x, y, originCooldown, cooldown);
                    break;
                }
            }
        }
    }

    // 스킬 슬롯 재사용 대기시간 처리
    private void UpdateUISkillSlotCooldown(HaruSkill skill, int x, int y, float originCooldown, float cooldown)
    {
        // 다음 사용할 스킬
        HaruSkill nextSkill;

        // 현재 스킬 슬롯 상태
        if (!CheckNextSkill(x, out nextSkill) &&    // false면 다음 사용할 스킬이 없음
            GetLastSkill(x) == skill)               // 마지막으로 사용한 스킬과 비교해서 같으면
        {
            // UI 재사용 대기시간 갱신
            UIManager.Instance.UpdateSkillCooldown(x, originCooldown, cooldown);
            return;
        }
        // 다음 사용할 스킬이 있음
        else if(nextSkill != HaruSkill.None)
        {
            // UI 스킬 슬롯 아이콘 변경
            UIManager.Instance.ChangeSkillSlotIcon(x, nextSkill);
        }

        // 첫번째 스킬
        if (y == 0)
        {
            // 위쪽 스킬들이 하나라도 준비 되있고
            if (m_readySkill[(int)m_skillSlot[x, 1]] || m_readySkill[(int)m_skillSlot[x, 2]])
            {
                // 마지막으로 사용한 스킬과 비교해서 같으면
                if (GetLastSkill(x) == skill)
                {
                    // 스킬 슬롯 위쪽에 재사용 대기시간 표시
                    UIManager.Instance.UpdateSkillSecondCooldown(x, originCooldown, cooldown);
                }
                return;
            }
        }

        // 두번째 스킬
        if (y == 1)
        {
            // 아래나 위쪽 스킬이 하나라도 준비 되있고
            if (m_readySkill[(int)m_skillSlot[x, 0]] || m_readySkill[(int)m_skillSlot[x, 2]])
            {
                // 마지막으로 사용한 스킬과 비교해서 같으면
                if (GetLastSkill(x) == skill)
                {
                    // 스킬 슬롯 위쪽에 재사용 대기시간 표시
                    UIManager.Instance.UpdateSkillSecondCooldown(x, originCooldown, cooldown);
                }
                return;
            }
        }

        // 세번째 스킬
        if (y == 2)
        {
            // 아래쪽 위쪽 스킬이 하나라도 준비 되있고
            if (m_readySkill[(int)m_skillSlot[x, 0]] || m_readySkill[(int)m_skillSlot[x, 1]])
            {
                // 마지막으로 사용한 스킬과 비교해서 같으면
                if (GetLastSkill(x) == skill)
                {
                    // 스킬 슬롯 위쪽에 재사용 대기시간 표시
                    UIManager.Instance.UpdateSkillSecondCooldown(x, originCooldown, cooldown);
                }
                return;
            }
        }

        // 모든 스킬이 준비가 안되있고 두번째 사용한 스킬과 비교해서 같으면
        int second = skillSlotQueue[x].GetEndIndex();
        if (second - 1 < 0)
        {
            second = skillSlotQueue[x].GetQueueSize() - 1;
        }
        else
        {
            --second;
        }

        // 마지막에서 두번째로 사용한 스킬이면
        if (skillSlotQueue[x].GetQueue()[second] == skill ||
            skillSlotQueue[x].GetQueue()[second] == HaruSkill.None)
        {
            // 스킬 슬롯 위쪽에 재사용 대기시간 표시
            UIManager.Instance.UpdateSkillSecondCooldown(x, originCooldown, cooldown);
        }
    }

    // 다음 스킬 확인
    private bool CheckNextSkill(int index, out HaruSkill nextSkill)
    {
        int iCnt = 0;
        while (iCnt < 3)
        {
            if (m_readySkill[(int)m_skillSlot[index, iCnt]])
                break;
            ++iCnt;
        }

        // 사용 가능한 다음 스킬이 없음
        if (iCnt == 3)
        {
            nextSkill = HaruSkill.None;
            return false;
        }

        nextSkill = m_skillSlot[index, iCnt];
        return true;
    }

    // 모든 스킬 슬롯 재사용 대기시간 완료 검사
    private void AllSkillSlotCheckReady(HaruSkill skill)
    {
        // 스킬 슬롯 전체 순회
        for (int x = 0; x < (int)DEFAULT.SKILL_SLOT_X_SIZE; ++x)
        {
            for (int y = 0; y < (int)DEFAULT.SKILL_SLOT_Y_SIZE; ++y)
            {
                // 사용하려는 스킬이 아니면
                if (m_skillSlot[x, y] != skill)
                    continue;

                // 첫번째 스킬
                if (y == 0)
                {
                    // 위에 스킬 사용 준비 안됨
                    if ((!m_readySkill[(int)m_skillSlot[x, 1]] || m_skillSlot[x, 1] == HaruSkill.None) &&
                        (!m_readySkill[(int)m_skillSlot[x, 2]] || m_skillSlot[x, 2] == HaruSkill.None))
                    {
                        // 스킬 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotCooldown(x);
                        // UI 스킬 슬롯 아이콘 변경
                        UIManager.Instance.ChangeSkillSlotIcon(x, skill);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }

                    // 마지막으로 사용한 스킬과 같음
                    if (GetLastSkill(x) == skill)
                    {
                        // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotSecondCooldown(x);
                    }
                    // UI 스킬 슬롯 아이콘 변경
                    UIManager.Instance.ChangeSkillSlotIcon(x, skill);
                    // 큐에서 삭제
                    EraseSkill(skill, x);
                    break;
                }
                // 두번째 스킬
                else if (y == 1)
                {
                    // 아래쪽 준비 안됨 위쪽 준비 안됨
                    if (!m_readySkill[(int)m_skillSlot[x, 0]] &&
                        (!m_readySkill[(int)m_skillSlot[x, 2]] || m_skillSlot[x, 2] == HaruSkill.None))
                    {
                        // 스킬 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotCooldown(x);
                        // UI 스킬 슬롯 아이콘 변경
                        UIManager.Instance.ChangeSkillSlotIcon(x, skill);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }

                    // 아래쪽 준비 안됨 위쪽 준비됨
                    if (!m_readySkill[(int)m_skillSlot[x, 0]] &&
                        m_readySkill[(int)m_skillSlot[x, 2]])
                    {
                        // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotSecondCooldown(x);
                        // UI 스킬 슬롯 아이콘 변경
                        UIManager.Instance.ChangeSkillSlotIcon(x, skill);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }

                    // 아래쪽 준비됨 마지막으로 사용한 스킬과 같음
                    if (m_readySkill[(int)m_skillSlot[x, 0]] &&
                        GetLastSkill(x) == skill)
                    {
                        // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotSecondCooldown(x);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }
                }
                // 세번째 스킬
                else if (y == 2)
                {
                    // 아래쪽 준비 안됨
                    if (!m_readySkill[(int)m_skillSlot[x, 0]] &&
                        !m_readySkill[(int)m_skillSlot[x, 1]])
                    {
                        // 스킬 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotCooldown(x);
                        // UI 스킬 슬롯 아이콘 변경
                        UIManager.Instance.ChangeSkillSlotIcon(x, skill);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }

                    // 아래쪽 준비됨 마지막으로 사용한 스킬과 같음
                    if ((m_readySkill[(int)m_skillSlot[x, 0]] || m_readySkill[(int)m_skillSlot[x, 1]]) &&
                        GetLastSkill(x) == skill)
                    {
                        // 스킬 아이콘 위쪽 재사용 대기시간 비활성화
                        UIManager.Instance.OffSkillSlotSecondCooldown(x);
                        // 큐에서 삭제
                        EraseSkill(skill, x);
                        break;
                    }
                }
            }
        }
    }

    // 마지막으로 사용한 스킬 획득
    private HaruSkill GetLastSkill(int index)
    {
        HaruSkill retSkill = HaruSkill.None;
        var queue = skillSlotQueue[index].GetQueue();
        int iSize = skillSlotQueue[index].GetQueueSize();
        int iRear = skillSlotQueue[index].GetRear();
        int iFront = skillSlotQueue[index].GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // None 제외
            if (queue[iRear] != HaruSkill.None)
            {
                // 마지막으로 사용한 스킬
                retSkill = queue[iRear];
                break;
            }
        }

        return retSkill;
    }
}
