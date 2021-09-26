using UnityEngine;

using MyEnum;

public partial class PlayerCtrl: MonoBehaviour
{
    public void MoveNone()
    {
        moveInput = Vector2.zero;

        if (CheckState(state, HaruState.Idle))
        {
            IdleBranch();
        }
    }

    public void Move(Vector2 dir)
    {
        moveInput = dir;

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void Dash()
    {
        if (CheckState(state, HaruState.Dash))
        {
            ChangeFlagFalse();
            state = HaruState.Dash;
            ChangeFlagTrue();
        }
    }

    public void MouseLeft()
    {
        if (CheckState(state, HaruState.NormalAttack1) && !lockInput)
        {
            ChangeFlagFalse();
            state = HaruState.NormalAttack1;
            ChangeFlagTrue();
        }
        else if (CheckState(state, HaruState.NormalAttack2) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack2;
            ChangeFlagTrue();
        }
        else if (CheckState(state, HaruState.NormalAttack3) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack3;
            ChangeFlagTrue();
        }
        else if (CheckState(state, HaruState.NormalAttack4) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack4;
            ChangeFlagTrue();
        }
        else if (CheckState(state, HaruState.NormalAttack5) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack5;
            ChangeFlagTrue();
        }
    }

    public void MouseRight()
    {

    }

    public void Jump()
    {
        if (lockInput)
            return;

        if (dash)
        {
            if (CheckState(state, HaruState.DashJump) && characterController.isGrounded)
            {
                ChangeFlagFalse();
                state = HaruState.DashJump;
                ChangeFlagTrue();

                lastJumpTime = Time.time;
                currentVelocityY = jumpVelocity * 0.6f;
                moveAnimeDir = modelTransform.forward;
                oldInput = moveInput;
                // 대쉬 점프 속도가 높지 않으면 강제로 맞춰줌
                if (targetSpeed != moveSpeed * dashSpeedGob)
                {
                    targetSpeed = moveSpeed * dashSpeedGob;
                }
            }
        }
        else
        {
            if (CheckState(state, HaruState.Jump) && characterController.isGrounded)
            {
                ChangeFlagFalse();
                state = HaruState.Jump;
                ChangeFlagTrue();

                lastJumpTime = Time.time;
                currentVelocityY = jumpVelocity;
                moveInput = new Vector2(0, 0);
            }
        }
    }

    public void Evade(Vector2 dir)
    {
        return;

        if (CheckState(state, HaruState.Evade))
        {
            ChangeFlagFalse();
            state = HaruState.Evade;
            ChangeFlagTrue();
            moveAnimeDir = dir;
        }
    }

    public void SkillSlot1()
    {
        HaruState retState;

        // 해당 스킬슬롯의 스킬 상태 얻어오기
        if (!playerInfo.GetStateOfSkillSlot(0, out retState))
            return;

        if (CheckState(state, retState))
        {
            if (playerInfo.CheckSkillCooldown(0))
            {
                ChangeFlagFalse();
                state = retState;
                ChangeFlagTrue();
            }
        }
    }

    public void SkillSlot2()
    {
        HaruState retState;

        // 해당 스킬슬롯의 스킬 상태 얻어오기
        if (!playerInfo.GetStateOfSkillSlot(1, out retState))
            return;

        if (CheckState(state, retState))
        {
            ChangeFlagFalse();
            state = retState;
            ChangeFlagTrue();
        }
    }

    public void SkillSlot3()
    {
        HaruState retState;

        // 해당 스킬슬롯의 스킬 상태 얻어오기
        if (!playerInfo.GetStateOfSkillSlot(2, out retState))
            return;

        if (CheckState(state, retState))
        {
            ChangeFlagFalse();
            state = retState;
            ChangeFlagTrue();
        }
    }

    public void SkillSlot4()
    {

    }

    public void SkillSlot5()
    {

    }

    public void SkillSlot6()
    {

    }





    private void IdleBranch()
    {
        switch (state)
        {
            case HaruState.Run:
                ChangeFlagFalse();
                state = HaruState.RunEnd;
                ChangeFlagTrue();
                return;
            case HaruState.Dash:
                ChangeFlagFalse();
                state = HaruState.DashEnd;
                ChangeFlagTrue();
                return;
            default:
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                state = HaruState.Idle;
                ChangeFlagTrue();
                return;
        }
    }

    private void MoveBranch()
    {
        // 다운 상태
        if (state == HaruState.KD_Upp_Down)
        {
            ChangeFlagFalse();
            state = HaruState.KD_Upp_Raise;
            ChangeFlagTrue();
            return;
        }

        // 대쉬 착지중이면
        // 입력키 상태에따라 Dash or Run 으로 변경됨
        if (state == HaruState.DashLand)
        {
            if (moveInput == oldInput && dash)
            {
                ChangeFlagFalse();
                state = HaruState.Dash;
                ChangeFlagTrue();
            }
            else
            {
                ChangeFlagFalse();
                state = HaruState.Run;
                ChangeFlagTrue();
            }
        }
        // 일반 공격중
        else if (normalAtk && !moveStand)
        {
            return;
        }
        // 그외 상태
        else
        {
            ChangeFlagFalse();
            state = HaruState.Run;
            ChangeFlagTrue();
        }
    }

    private void StartNormalAttack(int attackCnt)
    {
        lockInput = true;
        moveAttack = true;
        moveStand = false;
        normalAtk = true;
        isAttacking = false;
        SetNormalAttackCnt(attackCnt);
        SetAttackDir();
    }

    private void EndNormalAttack()
    {
        ChangeFlagFalse();
        lastIdleChangeTime = Time.time;
        state = HaruState.Idle;
        ChangeFlagTrue();
    }


    // 공격 방향
    private void SetAttackDir()
    {
        if (cameraDirAtk)
        {
            // 카메라 방향
            moveAnimeDir = cameraTransform.forward;
            return;
        }

        // cameraDirAtk가 false일 경우 공격 방향
        if (moveInput == Vector2.zero)
            moveAnimeDir = modelTransform.forward;
        else
            moveAnimeDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
    }
}
