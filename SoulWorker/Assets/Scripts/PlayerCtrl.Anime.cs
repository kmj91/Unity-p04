using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;

public partial class PlayerCtrl : MonoBehaviour
{
    private void SetSpeedZero()
    {
        hairAnime.SetFloat("Speed", 0);
        faceAnime.SetFloat("Speed", 0);
        bodyAnime.SetFloat("Speed", 0);
        pantsAnime.SetFloat("Speed", 0);
        handsAnime.SetFloat("Speed", 0);
        footAnime.SetFloat("Speed", 0);
    }

    private void SetDashTrue()
    {
        hairAnime.SetBool("Dash", true);
        faceAnime.SetBool("Dash", true);
        bodyAnime.SetBool("Dash", true);
        pantsAnime.SetBool("Dash", true);
        handsAnime.SetBool("Dash", true);
        footAnime.SetBool("Dash", true);
    }

    private void SetDashFalse()
    {
        hairAnime.SetBool("Dash", false);
        faceAnime.SetBool("Dash", false);
        bodyAnime.SetBool("Dash", false);
        pantsAnime.SetBool("Dash", false);
        handsAnime.SetBool("Dash", false);
        footAnime.SetBool("Dash", false);
    }

    private void SetJumpTrue()
    {
        hairAnime.SetBool("Jump", true);
        faceAnime.SetBool("Jump", true);
        bodyAnime.SetBool("Jump", true);
        pantsAnime.SetBool("Jump", true);
        handsAnime.SetBool("Jump", true);
        footAnime.SetBool("Jump", true);
    }

    private void SetJumpFalse()
    {
        hairAnime.SetBool("Jump", false);
        faceAnime.SetBool("Jump", false);
        bodyAnime.SetBool("Jump", false);
        pantsAnime.SetBool("Jump", false);
        handsAnime.SetBool("Jump", false);
        footAnime.SetBool("Jump", false);
    }

    private void SetNormalAttackTrue()
    {
        hairAnime.SetBool("NormalAttack", true);
        faceAnime.SetBool("NormalAttack", true);
        bodyAnime.SetBool("NormalAttack", true);
        pantsAnime.SetBool("NormalAttack", true);
        handsAnime.SetBool("NormalAttack", true);
        footAnime.SetBool("NormalAttack", true);
    }

    private void SetNormalAttackFalse()
    {
        hairAnime.SetBool("NormalAttack", false);
        faceAnime.SetBool("NormalAttack", false);
        bodyAnime.SetBool("NormalAttack", false);
        pantsAnime.SetBool("NormalAttack", false);
        handsAnime.SetBool("NormalAttack", false);
        footAnime.SetBool("NormalAttack", false);
    }

    private void SetNormalAttackCnt(int cnt)
    {
        hairAnime.SetInteger("NormalAttackCnt", cnt);
        faceAnime.SetInteger("NormalAttackCnt", cnt);
        bodyAnime.SetInteger("NormalAttackCnt", cnt);
        pantsAnime.SetInteger("NormalAttackCnt", cnt);
        handsAnime.SetInteger("NormalAttackCnt", cnt);
        footAnime.SetInteger("NormalAttackCnt", cnt);
    }



    private void Ani_Idle()
    {
        SetSpeedZero();

        if (dash)
        {
            dash = false;
            SetDashFalse();
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
        SetDashTrue();
    }

    private void Ani_Jump()
    {
        if (!jump)
        {
            jump = true;
            SetJumpTrue();
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

                SetJumpFalse();
                SetSpeedZero();
            }
        }
    }

    private void Ani_DashJump()
    {
        if (!jump)
        {
            jump = true;
            SetJumpTrue();
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

                SetJumpFalse();
                SetSpeedZero();
                SetDashFalse();
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
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_01"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
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
                normalAtk = false;
                SetNormalAttackFalse();
                SetNormalAttackCnt(0);
            }
        }
    }

    // 일반 공격 2
    private void Ani_NormalAttack2()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_02"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.23f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
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
                normalAtk = false;
                SetNormalAttackFalse();
                SetNormalAttackCnt(0);
            }
        }
    }

    // 일반 공격 3
    private void Ani_NormalAttack3()
    {
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
                normalAtk = false;
                SetNormalAttackFalse();
                SetNormalAttackCnt(0);
            }
        }
    }

    // 일반 공격 4
    private void Ani_NormalAttack4()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_04"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.28f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.47f)
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
                normalAtk = false;
                SetNormalAttackFalse();
                SetNormalAttackCnt(0);
            }
        }
    }

    // 일반 공격 5
    private void Ani_NormalAttack5()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_05"))
        {
            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                moveAttack = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.52f)
            {
                lockInput = false;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                moveStand = true;
            }

            if (hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = PlayerState.Idle;
                normalAtk = false;
                SetNormalAttackFalse();
                SetNormalAttackCnt(0);
            }
        }
    }
}
