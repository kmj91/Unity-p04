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
                jumpTime = Time.realtimeSinceStartup;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);

                hairAnime.SetFloat("Speed", 0);
                faceAnime.SetFloat("Speed", 0);
                bodyAnime.SetFloat("Speed", 0);
                pantsAnime.SetFloat("Speed", 0);
                handsAnime.SetFloat("Speed", 0);
                footAnime.SetFloat("Speed", 0);
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
                moveAnimeDir = modelTransform.forward;

                hairAnime.SetBool("Jump", false);
                faceAnime.SetBool("Jump", false);
                bodyAnime.SetBool("Jump", false);
                pantsAnime.SetBool("Jump", false);
                handsAnime.SetBool("Jump", false);
                footAnime.SetBool("Jump", false);

                hairAnime.SetFloat("Speed", 0);
                faceAnime.SetFloat("Speed", 0);
                bodyAnime.SetFloat("Speed", 0);
                pantsAnime.SetFloat("Speed", 0);
                handsAnime.SetFloat("Speed", 0);
                footAnime.SetFloat("Speed", 0);

                hairAnime.SetBool("Dash", false);
                faceAnime.SetBool("Dash", false);
                bodyAnime.SetBool("Dash", false);
                pantsAnime.SetBool("Dash", false);
                handsAnime.SetBool("Dash", false);
                footAnime.SetBool("Dash", false);
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
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.B_Dash_Jump_End"))
        {
            // 정지
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
            {
                moveAnimeDir = Vector3.zero;
            }

            // 종료
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
            }
        }
    }

    // 일반 공격 1
    private void Ani_NormalAttack1()
    {
        hairAnime.SetInteger("NormalAttack", 1);
        faceAnime.SetInteger("NormalAttack", 1);
        bodyAnime.SetInteger("NormalAttack", 1);
        pantsAnime.SetInteger("NormalAttack", 1);
        handsAnime.SetInteger("NormalAttack", 1);
        footAnime.SetInteger("NormalAttack", 1);

        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_01"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                lockInput = false;
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAttack = 0;

                hairAnime.SetInteger("NormalAttack", 0);
                faceAnime.SetInteger("NormalAttack", 0);
                bodyAnime.SetInteger("NormalAttack", 0);
                pantsAnime.SetInteger("NormalAttack", 0);
                handsAnime.SetInteger("NormalAttack", 0);
                footAnime.SetInteger("NormalAttack", 0);
            }
        }
    }

    // 일반 공격 2
    private void Ani_NormalAttack2()
    {
        hairAnime.SetInteger("NormalAttack", 2);
        faceAnime.SetInteger("NormalAttack", 2);
        bodyAnime.SetInteger("NormalAttack", 2);
        pantsAnime.SetInteger("NormalAttack", 2);
        handsAnime.SetInteger("NormalAttack", 2);
        footAnime.SetInteger("NormalAttack", 2);

        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_02"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.23f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
            {
                lockInput = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAttack = 0;

                hairAnime.SetInteger("NormalAttack", 0);
                faceAnime.SetInteger("NormalAttack", 0);
                bodyAnime.SetInteger("NormalAttack", 0);
                pantsAnime.SetInteger("NormalAttack", 0);
                handsAnime.SetInteger("NormalAttack", 0);
                footAnime.SetInteger("NormalAttack", 0);
            }
        }
    }

    // 일반 공격 3
    private void Ani_NormalAttack3()
    {
        hairAnime.SetInteger("NormalAttack", 3);
        faceAnime.SetInteger("NormalAttack", 3);
        bodyAnime.SetInteger("NormalAttack", 3);
        pantsAnime.SetInteger("NormalAttack", 3);
        handsAnime.SetInteger("NormalAttack", 3);
        footAnime.SetInteger("NormalAttack", 3);

        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_03"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                lockInput = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAttack = 0;

                hairAnime.SetInteger("NormalAttack", 0);
                faceAnime.SetInteger("NormalAttack", 0);
                bodyAnime.SetInteger("NormalAttack", 0);
                pantsAnime.SetInteger("NormalAttack", 0);
                handsAnime.SetInteger("NormalAttack", 0);
                footAnime.SetInteger("NormalAttack", 0);
            }
        }
    }

    // 일반 공격 4
    private void Ani_NormalAttack4()
    {
        hairAnime.SetInteger("NormalAttack", 4);
        faceAnime.SetInteger("NormalAttack", 4);
        bodyAnime.SetInteger("NormalAttack", 4);
        pantsAnime.SetInteger("NormalAttack", 4);
        handsAnime.SetInteger("NormalAttack", 4);
        footAnime.SetInteger("NormalAttack", 4);

        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_04"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.28f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                lockInput = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAttack = 0;

                hairAnime.SetInteger("NormalAttack", 0);
                faceAnime.SetInteger("NormalAttack", 0);
                bodyAnime.SetInteger("NormalAttack", 0);
                pantsAnime.SetInteger("NormalAttack", 0);
                handsAnime.SetInteger("NormalAttack", 0);
                footAnime.SetInteger("NormalAttack", 0);
            }
        }
    }

    // 일반 공격 5
    private void Ani_NormalAttack5()
    {
        hairAnime.SetInteger("NormalAttack", 5);
        faceAnime.SetInteger("NormalAttack", 5);
        bodyAnime.SetInteger("NormalAttack", 5);
        pantsAnime.SetInteger("NormalAttack", 5);
        handsAnime.SetInteger("NormalAttack", 5);
        footAnime.SetInteger("NormalAttack", 5);

        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_05"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                lockInput = false;
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAttack = 0;

                hairAnime.SetInteger("NormalAttack", 0);
                faceAnime.SetInteger("NormalAttack", 0);
                bodyAnime.SetInteger("NormalAttack", 0);
                pantsAnime.SetInteger("NormalAttack", 0);
                handsAnime.SetInteger("NormalAttack", 0);
                footAnime.SetInteger("NormalAttack", 0);
            }
        }
    }
}
