using UnityEngine;

using MyEnum;
using System.Resources;

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

    private void SetUppTrue()
    {
        hairAnime.SetBool("Upp", true);
        faceAnime.SetBool("Upp", true);
        bodyAnime.SetBool("Upp", true);
        pantsAnime.SetBool("Upp", true);
        handsAnime.SetBool("Upp", true);
        footAnime.SetBool("Upp", true);
    }

    private void SetUppFalse()
    {
        hairAnime.SetBool("Upp", false);
        faceAnime.SetBool("Upp", false);
        bodyAnime.SetBool("Upp", false);
        pantsAnime.SetBool("Upp", false);
        handsAnime.SetBool("Upp", false);
        footAnime.SetBool("Upp", false);
    }

    private void SetDownTrue()
    {
        hairAnime.SetBool("Down", true);
        faceAnime.SetBool("Down", true);
        bodyAnime.SetBool("Down", true);
        pantsAnime.SetBool("Down", true);
        handsAnime.SetBool("Down", true);
        footAnime.SetBool("Down", true);
    }

    private void SetDownFalse()
    {
        hairAnime.SetBool("Down", false);
        faceAnime.SetBool("Down", false);
        bodyAnime.SetBool("Down", false);
        pantsAnime.SetBool("Down", false);
        handsAnime.SetBool("Down", false);
        footAnime.SetBool("Down", false);
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

    private void SetTrigerNormalAttackStart()
    {
        hairAnime.SetTrigger("NormalAttackStart");
        faceAnime.SetTrigger("NormalAttackStart");
        bodyAnime.SetTrigger("NormalAttackStart");
        pantsAnime.SetTrigger("NormalAttackStart");
        handsAnime.SetTrigger("NormalAttackStart");
        footAnime.SetTrigger("NormalAttackStart");
    }

    private void SetTrigerNormalAttackEnd()
    {
        hairAnime.SetTrigger("NormalAttackEnd");
        faceAnime.SetTrigger("NormalAttackEnd");
        bodyAnime.SetTrigger("NormalAttackEnd");
        pantsAnime.SetTrigger("NormalAttackEnd");
        handsAnime.SetTrigger("NormalAttackEnd");
        footAnime.SetTrigger("NormalAttackEnd");
    }

    private void SetTrigerEvade()
    {
        hairAnime.SetTrigger("B_Evade");
        faceAnime.SetTrigger("B_Evade");
        bodyAnime.SetTrigger("B_Evade");
        pantsAnime.SetTrigger("B_Evade");
        handsAnime.SetTrigger("B_Evade");
        footAnime.SetTrigger("B_Evade");
    }

    private void SetTrigerDMGL()
    {
        hairAnime.SetTrigger("B_DMG_L");
        faceAnime.SetTrigger("B_DMG_L");
        bodyAnime.SetTrigger("B_DMG_L");
        pantsAnime.SetTrigger("B_DMG_L");
        handsAnime.SetTrigger("B_DMG_L");
        footAnime.SetTrigger("B_DMG_L");
    }

    private void SetTrigerDMGR()
    {
        hairAnime.SetTrigger("B_DMG_R");
        faceAnime.SetTrigger("B_DMG_R");
        bodyAnime.SetTrigger("B_DMG_R");
        pantsAnime.SetTrigger("B_DMG_R");
        handsAnime.SetTrigger("B_DMG_R");
        footAnime.SetTrigger("B_DMG_R");
    }

    private void SetTrigerKB()
    {
        hairAnime.SetTrigger("B_KB");
        faceAnime.SetTrigger("B_KB");
        bodyAnime.SetTrigger("B_KB");
        pantsAnime.SetTrigger("B_KB");
        handsAnime.SetTrigger("B_KB");
        footAnime.SetTrigger("B_KB");
    }

    private void SetTrigerKDHamF()
    {
        hairAnime.SetTrigger("B_KD_Ham_F");
        faceAnime.SetTrigger("B_KD_Ham_F");
        bodyAnime.SetTrigger("B_KD_Ham_F");
        pantsAnime.SetTrigger("B_KD_Ham_F");
        handsAnime.SetTrigger("B_KD_Ham_F");
        footAnime.SetTrigger("B_KD_Ham_F");
    }

    private void SetTrigerKDHamB()
    {
        hairAnime.SetTrigger("B_KD_Ham_B");
        faceAnime.SetTrigger("B_KD_Ham_B");
        bodyAnime.SetTrigger("B_KD_Ham_B");
        pantsAnime.SetTrigger("B_KD_Ham_B");
        handsAnime.SetTrigger("B_KD_Ham_B");
        footAnime.SetTrigger("B_KD_Ham_B");
    }

    private void SetTrigerKDStr()
    {
        hairAnime.SetTrigger("B_KD_Str");
        faceAnime.SetTrigger("B_KD_Str");
        bodyAnime.SetTrigger("B_KD_Str");
        pantsAnime.SetTrigger("B_KD_Str");
        handsAnime.SetTrigger("B_KD_Str");
        footAnime.SetTrigger("B_KD_Str");
    }

    private void SetTrigerKDUpp()
    {
        hairAnime.SetTrigger("B_KD_Upp");
        faceAnime.SetTrigger("B_KD_Upp");
        bodyAnime.SetTrigger("B_KD_Upp");
        pantsAnime.SetTrigger("B_KD_Upp");
        handsAnime.SetTrigger("B_KD_Upp");
        footAnime.SetTrigger("B_KD_Upp");
    }

    private void SetTrigerKDUppEnd()
    {
        hairAnime.SetTrigger("B_KD_Upp_End");
        faceAnime.SetTrigger("B_KD_Upp_End");
        bodyAnime.SetTrigger("B_KD_Upp_End");
        pantsAnime.SetTrigger("B_KD_Upp_End");
        handsAnime.SetTrigger("B_KD_Upp_End");
        footAnime.SetTrigger("B_KD_Upp_End");
    }

    private void SetTrigerKDUppRaise()
    {
        hairAnime.SetTrigger("B_KD_Upp_Raise");
        faceAnime.SetTrigger("B_KD_Upp_Raise");
        bodyAnime.SetTrigger("B_KD_Upp_Raise");
        pantsAnime.SetTrigger("B_KD_Upp_Raise");
        handsAnime.SetTrigger("B_KD_Upp_Raise");
        footAnime.SetTrigger("B_KD_Upp_Raise");
    }

    private void SetTrigerKDUppAirHit()
    {
        hairAnime.SetTrigger("B_KD_Upp_Air_Hit");
        faceAnime.SetTrigger("B_KD_Upp_Air_Hit");
        bodyAnime.SetTrigger("B_KD_Upp_Air_Hit");
        pantsAnime.SetTrigger("B_KD_Upp_Air_Hit");
        handsAnime.SetTrigger("B_KD_Upp_Air_Hit");
        footAnime.SetTrigger("B_KD_Upp_Air_Hit");
    }

    private void SetTrigerKDUppDownHit()
    {
        hairAnime.SetTrigger("B_KD_Upp_Down_Hit");
        faceAnime.SetTrigger("B_KD_Upp_Down_Hit");
        bodyAnime.SetTrigger("B_KD_Upp_Down_Hit");
        pantsAnime.SetTrigger("B_KD_Upp_Down_Hit");
        handsAnime.SetTrigger("B_KD_Upp_Down_Hit");
        footAnime.SetTrigger("B_KD_Upp_Down_Hit");
    }

    private void SetTrigerSkillFirstBlade()
    {
        hairAnime.SetTrigger("B_Skill_First_Blade");
        faceAnime.SetTrigger("B_Skill_First_Blade");
        bodyAnime.SetTrigger("B_Skill_First_Blade");
        pantsAnime.SetTrigger("B_Skill_First_Blade");
        handsAnime.SetTrigger("B_Skill_First_Blade");
        footAnime.SetTrigger("B_Skill_First_Blade");
    }

    private void SetTrigerSkillFirstBlade02()
    {
        hairAnime.SetTrigger("B_Skill_First_Blade02");
        faceAnime.SetTrigger("B_Skill_First_Blade02");
        bodyAnime.SetTrigger("B_Skill_First_Blade02");
        pantsAnime.SetTrigger("B_Skill_First_Blade02");
        handsAnime.SetTrigger("B_Skill_First_Blade02");
        footAnime.SetTrigger("B_Skill_First_Blade02");
    }

    private void SetTrigerSkillPierceStep()
    {
        hairAnime.SetTrigger("B_Skill_Pierce_Step");
        faceAnime.SetTrigger("B_Skill_Pierce_Step");
        bodyAnime.SetTrigger("B_Skill_Pierce_Step");
        pantsAnime.SetTrigger("B_Skill_Pierce_Step");
        handsAnime.SetTrigger("B_Skill_Pierce_Step");
        footAnime.SetTrigger("B_Skill_Pierce_Step");
    }

    private void SetTrigerSkillSpinCutter()
    {
        hairAnime.SetTrigger("B_Skill_Spin_Cutter");
        faceAnime.SetTrigger("B_Skill_Spin_Cutter");
        bodyAnime.SetTrigger("B_Skill_Spin_Cutter");
        pantsAnime.SetTrigger("B_Skill_Spin_Cutter");
        handsAnime.SetTrigger("B_Skill_Spin_Cutter");
        footAnime.SetTrigger("B_Skill_Spin_Cutter");
    }

    private void SetTrigerNormalStand()
    {
        hairAnime.SetTrigger("N_Stand");
        faceAnime.SetTrigger("N_Stand");
        bodyAnime.SetTrigger("N_Stand");
        pantsAnime.SetTrigger("N_Stand");
        handsAnime.SetTrigger("N_Stand");
        footAnime.SetTrigger("N_Stand");
    }

    private void SetTrigerBattleStand()
    {
        hairAnime.SetTrigger("B_Stand");
        faceAnime.SetTrigger("B_Stand");
        bodyAnime.SetTrigger("B_Stand");
        pantsAnime.SetTrigger("B_Stand");
        handsAnime.SetTrigger("B_Stand");
        footAnime.SetTrigger("B_Stand");
    }



    private void Ani_Idle()
    {

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

    }

    private void Ani_Jump()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.Land;
            ChangeFlagTrue();
        }
    }

    private void Ani_DashJump()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.DashLand;
            ChangeFlagTrue();

            moveAnimeDir = modelTransform.forward;
        }
    }

    private void Ani_Land()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Jump_Land_C"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (lockInput && 0.3f <= time)
            {
                lockInput = false;
            }

            if (0.95f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_DashLand()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Dash_Jump_End"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (lockInput && 0.1f <= time)
            {
                lockInput = false;
            }

            // 정지
            if (0.35f <= time)
            {
                moveAnimeDir = Vector3.zero;
            }

            // 종료
            if (0.9f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_Evade()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Evade_F"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            // 정지
            if (0.5f <= time)
            {
                moveAnimeDir = Vector3.zero;
            }

            if (lockInput && 0.9f <= time)
            {
                lockInput = false;
            }

            // 종료
            if (0.95f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_DMG_L()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_DMG_L") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_DMG_R()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_DMG_R") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_KB()
    {

    }

    private void Ani_KD_Ham_F()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Ham_B()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Str()
    {

    }

    private void Ani_KD_Upp()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_End()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_End") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            state = HaruState.KD_Upp_Down;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Down()
    {

    }

    private void Ani_KD_Upp_Air_Hit()
    {
        // 착지
        if (characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.realtimeSinceStartup - fsmChangeTime <= 0.5f)
                return;

            ChangeFlagFalse();
            state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Down_Hit()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_Down_Hit") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            state = HaruState.KD_Upp_Down;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Raise()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_Raise") &&
            hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            state = HaruState.Idle;
            ChangeFlagTrue();
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack1, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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
                    ChangeFlagTrue();
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack2, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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
                    ChangeFlagTrue();
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack3, 1, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack3, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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
                    ChangeFlagTrue();
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
                weapon.attackType = AttackType.Down;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack4, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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
                    ChangeFlagTrue();
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
                weapon.attackType = AttackType.Strike;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.NormalAttack5, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
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

    // 퍼스트 블레이드
    private void Ani_FirstBlade()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_First_Blade"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isAttacking && time >= 0.67f)
            {
                isAttacking = false;
                // 무기 충돌 트리거 OFF
                weapon.OffTrigger();
            }
            else if (!isAttacking && 0.67f > time && time >= 0.43f)
            {
                isAttacking = true;
                // 무기 충돌 트리거 ON
                weapon.OnTrigger();
                weapon.attackType = AttackType.Strike;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 2, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
            }
            else if (isAttacking && 0.43f > time && time >= 0.3f)
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 1, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
            }
            else if (isAttacking && 0.2f > time && time >= 0.17f)
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
                weapon.attackType = AttackType.Normal;
                float damage = 0f;
                if (!playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 0, ref damage))
                    return;
                weapon.attackDamage = Random.Range(playerInfo.currentPlayerData.minAtk, playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (moveAttack && 0.5f <= time)
            {
                moveAttack = false;
            }

            if (0.43f > time && time >= 0.3f)
            {
                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (gameManager.KeyInputCheck(KeyCode.Mouse1, 0.5f))
                {
                    ChangeFlagFalse();
                    state = HaruState.FirstBlade02;
                    ChangeFlagTrue();
                    return;
                }
            }

            if (lockInput && 0.7f <= time)
            {
                lockInput = false;
            }

            if (!moveStand && 0.8f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 퍼스트 블레이드 추가 공격
    private void Ani_FirstBlade02()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_First_Blade02"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (moveAttack && 0.4f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.6f <= time)
            {
                lockInput = false;
            }

            if (!moveStand && 0.7f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 피어스 스탭
    private void Ani_PierceStep()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_Pierce_Step"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (moveAttack && 0.4f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.7f <= time)
            {
                lockInput = false;
            }

            if (!moveStand && 0.72f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 스핀 커터
    private void Ani_SpinCutter()
    {
        if (hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_Spin_Cutter"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (moveAttack && 0.5f <= time)
            {
                moveAttack = false;
            }

            if (lockInput && 0.7f <= time)
            {
                lockInput = false;
            }

            if (!moveStand && 0.85f <= time)
            {
                moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }
}
