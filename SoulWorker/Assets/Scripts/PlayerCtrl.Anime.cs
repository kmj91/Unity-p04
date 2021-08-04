﻿using UnityEngine;

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
                state = HaruState.Land;
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
                state = HaruState.DashLand;
                moveAnimeDir = modelTransform.forward;

                SetJumpFalse();
                SetSpeedZero();
                SetDashFalse();
            }
        }
    }

    private void Ani_Land()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base Action.B_Jump_Land_C") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = HaruState.Idle;
        }
    }

    private void Ani_DashLand()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base Action.B_Dash_Jump_End"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            // 정지
            if (0.35f <= time)
            {
                moveAnimeDir = Vector3.zero;
            }

            // 종료
            if (0.99f <= time)
            {
                state = HaruState.Idle;
            }
        }
    }

    // 일반 공격 1
    private void Ani_NormalAttack1()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_01"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.3f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.3f > time && time >= 0.2f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }

            if (moveAttack && 0.3f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.4f <= time)
            {
                lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    state = HaruState.NormalAttack2;
                    StartNormalAttack(2);
                    return;
                }

            }

            if (!moveStand && 0.7f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                EndNormalAttack();
            }
        }
    }

    // 일반 공격 2
    private void Ani_NormalAttack2()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_02"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.27f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.27f > time && time >= 0.2f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }

            if (moveAttack && 0.23f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.4f <= time)
            {
                lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    state = HaruState.NormalAttack3;
                    StartNormalAttack(3);
                    return;
                }
            }

            if (!moveStand && 0.7f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                EndNormalAttack();
            }
        }
    }

    // 일반 공격 3
    private void Ani_NormalAttack3()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_03"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.3f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.3f > time && time >= 0.21f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }
            else if (isAttacking && 0.21f > time && time >= 0.17f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.17f > time && time >= 0.1f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }

            if (moveAttack && 0.3f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.4f <= time)
            {
                lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    state = HaruState.NormalAttack4;
                    StartNormalAttack(4);
                    return;
                }
            }

            if (!moveStand && 0.75f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                EndNormalAttack();
            }
        }
    }

    // 일반 공격 4
    private void Ani_NormalAttack4()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_04"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.3f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.3f > time && time >= 0.2f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }

            if (moveAttack && 0.28f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.47f <= time)
            {
                lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    state = HaruState.NormalAttack5;
                    StartNormalAttack(5);
                    return;
                }
            }

            if (!moveStand && 0.75f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                EndNormalAttack();
            }
        }
    }

    // 일반 공격 5
    private void Ani_NormalAttack5()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_05"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.33f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.33f > time && time >= 0.24f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
            }

            if (moveAttack && 0.4f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.52f <= time)
            {
                lockInput = false;
            }

            if (!moveStand && 0.8f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                EndNormalAttack();
            }
        }
    }
}
