using UnityEngine;

using MyEnum;

public partial class PlayerCtrl: MonoBehaviour
{
    public void MoveNone()
    {
        moveInput = Vector2.zero;

        if (CheckState(state, HaruState.Idle))
            state = HaruState.Idle;

        // 대쉬 착지중이면
        if (state == HaruState.DashLand)
            dash = false;
    }

    public void MoveFF()
    {
        moveInput = new Vector2(0, 1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveFL()
    {
        moveInput = new Vector2(-1.0f, 1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveFR()
    {
        moveInput = new Vector2(1.0f, 1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveBB()
    {
        moveInput = new Vector2(0, -1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveBL()
    {
        moveInput = new Vector2(-1.0f, -1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveBR()
    {
        moveInput = new Vector2(1.0f, -1.0f);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveLL()
    {
        moveInput = new Vector2(-1.0f, 0);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void MoveRR()
    {
        moveInput = new Vector2(1.0f, 0);

        if (CheckState(state, HaruState.Run) && !lockInput)
        {
            MoveBranch();
        }
    }

    public void Dash()
    {
        if (CheckState(state, HaruState.Dash))
        {
            state = HaruState.Dash;
            dash = true;
        }
    }

    public void MouseLeft()
    {
        if (CheckState(state, HaruState.NormalAttack1) && !lockInput)
        {
            state = HaruState.NormalAttack1;
            SetNormalAttackTrue();
            StartNormalAttack(1);
        }
        else if (CheckState(state, HaruState.NormalAttack2) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack2;
            StartNormalAttack(2);
        }
        else if (CheckState(state, HaruState.NormalAttack3) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack3;
            StartNormalAttack(3);
        }
        else if (CheckState(state, HaruState.NormalAttack4) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack4;
            StartNormalAttack(4);
        }
        else if (CheckState(state, HaruState.NormalAttack5) && !lockInput && normalAtk)
        {
            state = HaruState.NormalAttack5;
            StartNormalAttack(5);
        }
    }

    public void MouseRight()
    {

    }

    public void Jump()
    {
        if (dash)
        {
            if (CheckState(state, HaruState.DashJump) && characterController.isGrounded)
            {
                state = HaruState.DashJump;
                currentVelocityY = jumpVelocity * 0.6f;
                moveAnimeDir = modelTransform.forward;
                oldInput = moveInput;
            }
        }
        else
        {
            if (CheckState(state, HaruState.Jump) && characterController.isGrounded)
            {
                // 착지 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - fsmChangeTime <= 0.2f)
                    return;

                state = HaruState.Jump;
                currentVelocityY = jumpVelocity;
                moveInput = new Vector2(0, 0);
            }
        }
    }

    public void Evade()
    {
        if (CheckState(state, HaruState.Evade))
        {
            state = HaruState.Evade;
            dash = true;
        }
    }

    public void SkillSlot1()
    {

    }

    public void SkillSlot2()
    {

    }

    public void SkillSlot3()
    {

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





    private void MoveBranch()
    {
        // 다운 상태
        if (state == HaruState.KD_Upp_Down)
        {
            state = HaruState.KD_Upp_Raise;
            down = false;
            SetTrigerKDUppRaise();
            SetDownFalse();
            return;
        }

        // 대쉬 착지중이면
        // 입력키 상태에따라 Dash or Run 으로 변경됨
        if (state == HaruState.DashLand)
        {
            if (moveInput == oldInput && dash)
            {
                state = HaruState.Dash;
            }
            else
            {
                state = HaruState.Run;
                dash = false;
            }
        }
        else
        {
            state = HaruState.Run;
        }

        // 공격 종료
        if (normalAtk)
        {
            normalAtk = false;
            SetNormalAttackFalse();
            SetNormalAttackCnt(-1);
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
        state = HaruState.Idle;
        normalAtk = false;
        moveStand = false;
        isAttacking = false;
        SetNormalAttackFalse();
        SetNormalAttackCnt(0);
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
