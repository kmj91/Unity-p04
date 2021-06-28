﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;

public partial class PlayerCtrl: MonoBehaviour
{
    public void MoveNone()
    {
        moveInput = Vector2.zero;

        if (CheckState(state, PlayerState.Idle))
            state = PlayerState.Idle;

        // 대쉬 착지중이면
        if (state == PlayerState.DashLand)
            dash = false;
    }

    public void MoveFF()
    {
        moveInput = new Vector2(0, 1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else 
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveFL()
    {
        moveInput = new Vector2(-1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveFR()
    {
        moveInput = new Vector2(1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveBB()
    {
        moveInput = new Vector2(0, -1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveBL()
    {
        moveInput = new Vector2(-1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveBR()
    {
        moveInput = new Vector2(1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveLL()
    {
        moveInput = new Vector2(-1.0f, 0);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void MoveRR()
    {
        moveInput = new Vector2(1.0f, 0);

        if (CheckState(state, PlayerState.Run))
        {
            // 대쉬 착지중이면
            // 입력키 상태에따라 Dash or Run 으로 변경됨
            if (state == PlayerState.DashLand)
            {
                if (moveInput == oldInput && dash)
                {
                    state = PlayerState.Dash;
                }
                else
                {
                    state = PlayerState.Run;
                    dash = false;
                }
            }
            else
            {
                state = PlayerState.Run;
            }
        }
    }

    public void Dash()
    {
        if (CheckState(state, PlayerState.Dash))
        {
            state = PlayerState.Dash;
            dash = true;
        }
    }

    public void MouseLeft()
    {
        if (normalAttack == 0)
        {
            if (CheckState(state, PlayerState.NormalAttack1) && !lockInput)
            {
                state = PlayerState.NormalAttack1;
                lockInput = true;
                ++normalAttack;
            }
        }
        else if (normalAttack == 1)
        {
            if (CheckState(state, PlayerState.NormalAttack2) && !lockInput)
            {
                state = PlayerState.NormalAttack2;
                lockInput = true;
                ++normalAttack;
            }
        }
    }

    public void MouseRight()
    {

    }

    public void Jump()
    {
        if (dash)
        {
            if (CheckState(state, PlayerState.DashJump) && characterController.isGrounded)
            {
                state = PlayerState.DashJump;
                currentVelocityY = jumpVelocity * 0.6f;
                jumpTime = Time.realtimeSinceStartup;
                moveAnimeDir = modelTransform.forward;
                oldInput = moveInput;
            }
        }
        else
        {
            if (CheckState(state, PlayerState.Jump) && characterController.isGrounded)
            {
                // 착지 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - jumpTime <= 0.2f)
                    return;

                state = PlayerState.Jump;
                currentVelocityY = jumpVelocity;
                jumpTime = Time.realtimeSinceStartup;
                moveInput = new Vector2(0, 0);
            }
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
}
