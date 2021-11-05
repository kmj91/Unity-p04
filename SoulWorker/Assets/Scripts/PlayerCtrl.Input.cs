using UnityEngine;

using MyEnum;

public partial class PlayerCtrl: MonoBehaviour
{
    public void MoveNone()
    {
        m_moveInput = Vector2.zero;

        if (CheckState(m_state, HaruState.Idle))
        {
            IdleBranch();
        }
    }

    public void Move(Vector2 dir)
    {
        m_moveInput = dir;

        if (CheckState(m_state, HaruState.Run) && !m_lockInput)
        {
            MoveBranch();
        }
    }

    public void Dash()
    {
        if (CheckState(m_state, HaruState.Dash))
        {
            ChangeFlagFalse();
            m_state = HaruState.Dash;
            ChangeFlagTrue();
        }
    }

    public void MouseLeft()
    {
        if (CheckState(m_state, HaruState.NormalAttack1) && !m_lockInput)
        {
            ChangeFlagFalse();
            m_state = HaruState.NormalAttack1;
            ChangeFlagTrue();
        }
        else if (CheckState(m_state, HaruState.NormalAttack2) && !m_lockInput && m_normalAtk)
        {
            m_state = HaruState.NormalAttack2;
            ChangeFlagTrue();
        }
        else if (CheckState(m_state, HaruState.NormalAttack3) && !m_lockInput && m_normalAtk)
        {
            m_state = HaruState.NormalAttack3;
            ChangeFlagTrue();
        }
        else if (CheckState(m_state, HaruState.NormalAttack4) && !m_lockInput && m_normalAtk)
        {
            m_state = HaruState.NormalAttack4;
            ChangeFlagTrue();
        }
        else if (CheckState(m_state, HaruState.NormalAttack5) && !m_lockInput && m_normalAtk)
        {
            m_state = HaruState.NormalAttack5;
            ChangeFlagTrue();
        }
    }

    public void MouseRight()
    {

    }

    public void Jump()
    {
        if (m_lockInput)
            return;

        if (m_dash)
        {
            if (CheckState(m_state, HaruState.DashJump) && m_characterController.isGrounded)
            {
                ChangeFlagFalse();
                m_state = HaruState.DashJump;
                ChangeFlagTrue();

                m_lastJumpTime = Time.time;
                m_currentVelocityY = m_jumpVelocity * 0.6f;
                m_moveAnimeDir = m_modelTransform.forward;
                m_oldInput = m_moveInput;
                // 대쉬 점프 속도가 높지 않으면 강제로 맞춰줌
                if (m_targetSpeed != m_moveSpeed * m_dashSpeedGob)
                {
                    m_targetSpeed = m_moveSpeed * m_dashSpeedGob;
                }
            }
        }
        else
        {
            if (CheckState(m_state, HaruState.Jump) && m_characterController.isGrounded)
            {
                ChangeFlagFalse();
                m_state = HaruState.Jump;
                ChangeFlagTrue();

                m_lastJumpTime = Time.time;
                m_currentVelocityY = m_jumpVelocity;
                m_moveInput = new Vector2(0, 0);
            }
        }
    }

    public void Evade(Vector2 dir)
    {
        return;

        if (CheckState(m_state, HaruState.Evade))
        {
            ChangeFlagFalse();
            m_state = HaruState.Evade;
            ChangeFlagTrue();
            m_moveAnimeDir = dir;
        }
    }

    // 스킬 단축키
    public void SkillHotkey(int index)
    {
        HaruState retState;

        // 해당 스킬슬롯의 스킬 상태 얻어오기
        if (!m_playerInfo.GetStateOfSkillSlot(index, out retState))
            return;

        if (CheckState(m_state, retState))
        {
            ChangeFlagFalse();
            m_state = retState;
            ChangeFlagTrue();
            m_playerInfo.PressSkillHotkey(index);
        }
    }





    private void IdleBranch()
    {
        switch (m_state)
        {
            case HaruState.Run:
                ChangeFlagFalse();
                m_state = HaruState.RunEnd;
                ChangeFlagTrue();
                return;
            case HaruState.Dash:
                ChangeFlagFalse();
                m_state = HaruState.DashEnd;
                ChangeFlagTrue();
                return;
            default:
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
                return;
        }
    }

    private void MoveBranch()
    {
        // 다운 상태
        if (m_state == HaruState.KD_Upp_Down)
        {
            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_Raise;
            ChangeFlagTrue();
            return;
        }

        // 대쉬 착지중이면
        // 입력키 상태에따라 Dash or Run 으로 변경됨
        if (m_state == HaruState.DashLand)
        {
            if (m_moveInput == m_oldInput && m_dash)
            {
                ChangeFlagFalse();
                m_state = HaruState.Dash;
                ChangeFlagTrue();
            }
            else
            {
                ChangeFlagFalse();
                m_state = HaruState.Run;
                ChangeFlagTrue();
            }
        }
        // 일반 공격중
        else if (m_normalAtk && !m_moveStand)
        {
            return;
        }
        // 그외 상태
        else
        {
            ChangeFlagFalse();
            m_state = HaruState.Run;
            ChangeFlagTrue();
        }
    }

    private void StartNormalAttack(int attackCnt)
    {
        m_lockInput = true;
        m_moveAttack = true;
        m_moveStand = false;
        m_normalAtk = true;
        m_isAttacking = false;
        SetNormalAttackCnt(attackCnt);
        SetAttackDir();
    }

    private void EndNormalAttack()
    {
        ChangeFlagFalse();
        lastIdleChangeTime = Time.time;
        m_state = HaruState.Idle;
        ChangeFlagTrue();
    }


    // 공격 방향
    private void SetAttackDir()
    {
        if (m_cameraDirAtk)
        {
            // 카메라 방향
            m_moveAnimeDir = m_cameraTransform.forward;
            return;
        }

        // m_cameraDirAtk가 false일 경우 공격 방향
        if (m_moveInput == Vector2.zero)
            m_moveAnimeDir = m_modelTransform.forward;
        else
            m_moveAnimeDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
    }
}
