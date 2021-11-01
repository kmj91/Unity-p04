using UnityEngine;
using System.Collections;

using MyEnum;

public partial class HaruInfo : PlayerInfo
{
    private int m_maxSkillPoint = 10;        // 최대 스킬 포인트
    private int m_skillPoint = 10;           // 사용하고 남은 스킬 포인트
    private int[] m_skillLevel;         // 스킬 레벨

    private HaruSkill[,] m_skillSlot;   // 스킬 슬롯
    private CirculartQueue<HaruSkill>[] skillSlotQueue; // 스킬 슬롯 큐
    // 1 : 스킬 종류
    // 2 : 스킬 레벨
    // 3 : 타격 수
    private float[,,] m_skillDamage;
    private float[] m_skillCooldown;    // 스킬 재사용 대기시간
    private bool[] m_readySkill;        // 스킬 사용 준비


    // 최대 스킬 포인트 얻어오기
    public int GetMaxSkillPoint()
    {
        return m_maxSkillPoint;
    }

    // 최대 스킬 포인트 값 셋
    public void SetMaxSkillPoint(int sp)
    {
        m_maxSkillPoint = sp;
    }

    // 스킬 포인트 얻어오기
    public int GetSkillPoint()
    {
        return m_skillPoint;
    }

    // 스킬 포인트 값 셋
    public void SetSkillPoint(int sp)
    {
        m_skillPoint = sp;
    }

    // 스킬 레벨 얻어오기
    public int GetSkillLevel(HaruSkill skill)
    {
        return m_skillLevel[(int)skill];
    }

    // 스킬 레벨 셋
    public void SetSkillLevel(HaruSkill skill, int level)
    {
        m_skillLevel[(int)skill] = level;
    }

    // 스킬 슬롯 얻어오기
    public HaruSkill[,] GetSkillSlot()
    {
        return m_skillSlot;
    }

    // 스킬 슬롯 인덱스의 스킬 상태 얻기
    public bool GetStateOfSkillSlot(int index, out HaruState m_state)
    {
        for (int iCnt = 0; iCnt < (int)SkillSlotSize.y; ++iCnt)
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
        for (int iCnt = 0; iCnt < (int)SkillSlotSize.y; ++iCnt)
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
    public bool GetSkillDamage(HaruSkillDamage skill, int cnt, ref float damage)
    {
        if (cnt < 0 && 5 <= cnt)
            return false;

        HaruSkill skillLevel;
        switch (skill)
        {
            case HaruSkillDamage.FirstBlade:
                skillLevel = HaruSkill.FirstBlade;
                break;
            case HaruSkillDamage.PierceStep:
                skillLevel = HaruSkill.PierceStep;
                break;
            case HaruSkillDamage.SpinCutter:
                skillLevel = HaruSkill.SpinCutter;
                break;
            case HaruSkillDamage.NormalAttack1:
                skillLevel = HaruSkill.NormalAttack;
                break;
            case HaruSkillDamage.NormalAttack2:
                skillLevel = HaruSkill.NormalAttack;
                break;
            case HaruSkillDamage.NormalAttack3:
                skillLevel = HaruSkill.NormalAttack;
                break;
            case HaruSkillDamage.NormalAttack4:
                skillLevel = HaruSkill.NormalAttack;
                break;
            case HaruSkillDamage.NormalAttack5:
                skillLevel = HaruSkill.NormalAttack;
                break;
            default:
                skillLevel = HaruSkill.None;
                break;
        }

        damage = m_skillDamage[(int)skill, m_skillLevel[(int)skillLevel] - 1, cnt];

        return true;
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
        for (int x = 0; x < (int)SkillSlotSize.x; ++x)
        {
            for (int y = 0; y < (int)SkillSlotSize.y; ++y)
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
        for (int y = 0; y < (int)SkillSlotSize.y; ++y)
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
        for (int x = 0; x < (int)SkillSlotSize.x; ++x)
        {
            for (int y = 0; y < (int)SkillSlotSize.y; ++y)
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
        else if (nextSkill != HaruSkill.None)
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
        for (int x = 0; x < (int)SkillSlotSize.x; ++x)
        {
            for (int y = 0; y < (int)SkillSlotSize.y; ++y)
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
