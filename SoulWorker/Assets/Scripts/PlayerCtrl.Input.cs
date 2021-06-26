using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;

public partial class PlayerCtrl: MonoBehaviour
{
    public void MoveNone()
    {
        moveInput = new Vector2(0, 0);

        if (CheckState(state, PlayerState.Idle))
            state = PlayerState.Idle;
    }

    public void MoveFF()
    {
        moveInput = new Vector2(0, 1.0f);

        if (CheckState(state, PlayerState.Run))
        {
            state = PlayerState.Run;
        }
    }

    public void MoveFL()
    {
        moveInput = new Vector2(-1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveFR()
    {
        moveInput = new Vector2(1.0f, 1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBB()
    {
        moveInput = new Vector2(0, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBL()
    {
        moveInput = new Vector2(-1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveBR()
    {
        moveInput = new Vector2(1.0f, -1.0f);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveLL()
    {
        moveInput = new Vector2(-1.0f, 0);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void MoveRR()
    {
        moveInput = new Vector2(1.0f, 0);

        if (CheckState(state, PlayerState.Run))
            state = PlayerState.Run;
    }

    public void Dash()
    {
        if (CheckState(state, PlayerState.Dash))
        {
            state = PlayerState.Dash;
            dash = true;
        }
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
