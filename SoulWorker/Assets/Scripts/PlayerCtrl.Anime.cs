using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;

public partial class PlayerCtrl : MonoBehaviour
{
    private void Ani_Idle()
    {
        hairAnime.SetFloat("Speed", 0);
        faceAnime.SetFloat("Speed", 0);
        bodyAnime.SetFloat("Speed", 0);
        pantsAnime.SetFloat("Speed", 0);
        handsAnime.SetFloat("Speed", 0);
        footAnime.SetFloat("Speed", 0);

        if (dash)
        {
            dash = false;

            hairAnime.SetBool("Dash", false);
            faceAnime.SetBool("Dash", false);
            bodyAnime.SetBool("Dash", false);
            pantsAnime.SetBool("Dash", false);
            handsAnime.SetBool("Dash", false);
            footAnime.SetBool("Dash", false);
        }
    }

    private void Ani_Run()
    {
        float speedPer = targetSpeed / moveSpeed;

        hairAnime.SetFloat("Speed", speedPer);
        faceAnime.SetFloat("Speed", speedPer);
        bodyAnime.SetFloat("Speed", speedPer);
        pantsAnime.SetFloat("Speed", speedPer);
        handsAnime.SetFloat("Speed", speedPer);
        footAnime.SetFloat("Speed", speedPer);
    }

    private void Ani_Dash()
    {
        hairAnime.SetBool("Dash", true);
        faceAnime.SetBool("Dash", true);
        bodyAnime.SetBool("Dash", true);
        pantsAnime.SetBool("Dash", true);
        handsAnime.SetBool("Dash", true);
        footAnime.SetBool("Dash", true);
    }

    private void Ani_Jump()
    {
        if (!jump)
        {
            jump = true;
            hairAnime.SetBool("Jump", true);
            faceAnime.SetBool("Jump", true);
            bodyAnime.SetBool("Jump", true);
            pantsAnime.SetBool("Jump", true);
            handsAnime.SetBool("Jump", true);
            footAnime.SetBool("Jump", true);
        }
        else
        {
            // 착지
            if (characterController.isGrounded)
            {
                // 점프 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - jumpTime <= 0.5f)
                    return;

                jump = false;
                state = PlayerState.Land;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);
            }
        }
    }

    private void Ani_DashJump()
    {
        if (!jump)
        {
            jump = true;
            hairAnime.SetBool("Jump", true);
            faceAnime.SetBool("Jump", true);
            bodyAnime.SetBool("Jump", true);
            pantsAnime.SetBool("Jump", true);
            handsAnime.SetBool("Jump", true);
            footAnime.SetBool("Jump", true);
        }
        else
        {
            // 착지
            if (characterController.isGrounded)
            {
                // 점프 한지 얼마 안됬으면 무시
                if (Time.realtimeSinceStartup - jumpTime <= 0.5f)
                    return;

                jump = false;
                state = PlayerState.DashLand;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);
            }
        }
    }

    private void Ani_Land()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.B_Jump_Land_C") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = PlayerState.Idle;
        }
    }

    private void Ani_DashLand()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.B_Dash_Jump_End") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = PlayerState.Idle;
        }
    }
}
