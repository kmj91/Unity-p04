using UnityEngine;

using MyEnum;
using System.Resources;

public partial class PlayerCtrl : MonoBehaviour
{
    private void SetSpeedZero()
    {
        m_hairAnime.SetFloat("Speed", 0);
        m_faceAnime.SetFloat("Speed", 0);
        m_bodyAnime.SetFloat("Speed", 0);
        m_pantsAnime.SetFloat("Speed", 0);
        m_handsAnime.SetFloat("Speed", 0);
        m_footAnime.SetFloat("Speed", 0);
    }

    private void SetDashTrue()
    {
        m_hairAnime.SetBool("Dash", true);
        m_faceAnime.SetBool("Dash", true);
        m_bodyAnime.SetBool("Dash", true);
        m_pantsAnime.SetBool("Dash", true);
        m_handsAnime.SetBool("Dash", true);
        m_footAnime.SetBool("Dash", true);
    }

    private void SetDashFalse()
    {
        m_hairAnime.SetBool("Dash", false);
        m_faceAnime.SetBool("Dash", false);
        m_bodyAnime.SetBool("Dash", false);
        m_pantsAnime.SetBool("Dash", false);
        m_handsAnime.SetBool("Dash", false);
        m_footAnime.SetBool("Dash", false);
    }

    private void SetJumpTrue()
    {
        m_hairAnime.SetBool("Jump", true);
        m_faceAnime.SetBool("Jump", true);
        m_bodyAnime.SetBool("Jump", true);
        m_pantsAnime.SetBool("Jump", true);
        m_handsAnime.SetBool("Jump", true);
        m_footAnime.SetBool("Jump", true);
    }

    private void SetJumpFalse()
    {
        m_hairAnime.SetBool("Jump", false);
        m_faceAnime.SetBool("Jump", false);
        m_bodyAnime.SetBool("Jump", false);
        m_pantsAnime.SetBool("Jump", false);
        m_handsAnime.SetBool("Jump", false);
        m_footAnime.SetBool("Jump", false);
    }

    private void SetUppTrue()
    {
        m_hairAnime.SetBool("Upp", true);
        m_faceAnime.SetBool("Upp", true);
        m_bodyAnime.SetBool("Upp", true);
        m_pantsAnime.SetBool("Upp", true);
        m_handsAnime.SetBool("Upp", true);
        m_footAnime.SetBool("Upp", true);
    }

    private void SetUppFalse()
    {
        m_hairAnime.SetBool("Upp", false);
        m_faceAnime.SetBool("Upp", false);
        m_bodyAnime.SetBool("Upp", false);
        m_pantsAnime.SetBool("Upp", false);
        m_handsAnime.SetBool("Upp", false);
        m_footAnime.SetBool("Upp", false);
    }

    private void SetDownTrue()
    {
        m_hairAnime.SetBool("Down", true);
        m_faceAnime.SetBool("Down", true);
        m_bodyAnime.SetBool("Down", true);
        m_pantsAnime.SetBool("Down", true);
        m_handsAnime.SetBool("Down", true);
        m_footAnime.SetBool("Down", true);
    }

    private void SetDownFalse()
    {
        m_hairAnime.SetBool("Down", false);
        m_faceAnime.SetBool("Down", false);
        m_bodyAnime.SetBool("Down", false);
        m_pantsAnime.SetBool("Down", false);
        m_handsAnime.SetBool("Down", false);
        m_footAnime.SetBool("Down", false);
    }

    private void SetNormalAttackCnt(int cnt)
    {
        m_hairAnime.SetInteger("NormalAttackCnt", cnt);
        m_faceAnime.SetInteger("NormalAttackCnt", cnt);
        m_bodyAnime.SetInteger("NormalAttackCnt", cnt);
        m_pantsAnime.SetInteger("NormalAttackCnt", cnt);
        m_handsAnime.SetInteger("NormalAttackCnt", cnt);
        m_footAnime.SetInteger("NormalAttackCnt", cnt);
    }

    private void SetTrigerNormalAttackStart()
    {
        m_hairAnime.SetTrigger("NormalAttackStart");
        m_faceAnime.SetTrigger("NormalAttackStart");
        m_bodyAnime.SetTrigger("NormalAttackStart");
        m_pantsAnime.SetTrigger("NormalAttackStart");
        m_handsAnime.SetTrigger("NormalAttackStart");
        m_footAnime.SetTrigger("NormalAttackStart");
    }

    private void SetTrigerNormalAttackEnd()
    {
        m_hairAnime.SetTrigger("NormalAttackEnd");
        m_faceAnime.SetTrigger("NormalAttackEnd");
        m_bodyAnime.SetTrigger("NormalAttackEnd");
        m_pantsAnime.SetTrigger("NormalAttackEnd");
        m_handsAnime.SetTrigger("NormalAttackEnd");
        m_footAnime.SetTrigger("NormalAttackEnd");
    }

    private void SetTrigerEvade()
    {
        m_hairAnime.SetTrigger("B_Evade");
        m_faceAnime.SetTrigger("B_Evade");
        m_bodyAnime.SetTrigger("B_Evade");
        m_pantsAnime.SetTrigger("B_Evade");
        m_handsAnime.SetTrigger("B_Evade");
        m_footAnime.SetTrigger("B_Evade");
    }

    private void SetTrigerDMGL()
    {
        m_hairAnime.SetTrigger("B_DMG_L");
        m_faceAnime.SetTrigger("B_DMG_L");
        m_bodyAnime.SetTrigger("B_DMG_L");
        m_pantsAnime.SetTrigger("B_DMG_L");
        m_handsAnime.SetTrigger("B_DMG_L");
        m_footAnime.SetTrigger("B_DMG_L");
    }

    private void SetTrigerDMGR()
    {
        m_hairAnime.SetTrigger("B_DMG_R");
        m_faceAnime.SetTrigger("B_DMG_R");
        m_bodyAnime.SetTrigger("B_DMG_R");
        m_pantsAnime.SetTrigger("B_DMG_R");
        m_handsAnime.SetTrigger("B_DMG_R");
        m_footAnime.SetTrigger("B_DMG_R");
    }

    private void SetTrigerKB()
    {
        m_hairAnime.SetTrigger("B_KB");
        m_faceAnime.SetTrigger("B_KB");
        m_bodyAnime.SetTrigger("B_KB");
        m_pantsAnime.SetTrigger("B_KB");
        m_handsAnime.SetTrigger("B_KB");
        m_footAnime.SetTrigger("B_KB");
    }

    private void SetTrigerKDHamF()
    {
        m_hairAnime.SetTrigger("B_KD_Ham_F");
        m_faceAnime.SetTrigger("B_KD_Ham_F");
        m_bodyAnime.SetTrigger("B_KD_Ham_F");
        m_pantsAnime.SetTrigger("B_KD_Ham_F");
        m_handsAnime.SetTrigger("B_KD_Ham_F");
        m_footAnime.SetTrigger("B_KD_Ham_F");
    }

    private void SetTrigerKDHamB()
    {
        m_hairAnime.SetTrigger("B_KD_Ham_B");
        m_faceAnime.SetTrigger("B_KD_Ham_B");
        m_bodyAnime.SetTrigger("B_KD_Ham_B");
        m_pantsAnime.SetTrigger("B_KD_Ham_B");
        m_handsAnime.SetTrigger("B_KD_Ham_B");
        m_footAnime.SetTrigger("B_KD_Ham_B");
    }

    private void SetTrigerKDStr()
    {
        m_hairAnime.SetTrigger("B_KD_Str");
        m_faceAnime.SetTrigger("B_KD_Str");
        m_bodyAnime.SetTrigger("B_KD_Str");
        m_pantsAnime.SetTrigger("B_KD_Str");
        m_handsAnime.SetTrigger("B_KD_Str");
        m_footAnime.SetTrigger("B_KD_Str");
    }

    private void SetTrigerKDUpp()
    {
        m_hairAnime.SetTrigger("B_KD_Upp");
        m_faceAnime.SetTrigger("B_KD_Upp");
        m_bodyAnime.SetTrigger("B_KD_Upp");
        m_pantsAnime.SetTrigger("B_KD_Upp");
        m_handsAnime.SetTrigger("B_KD_Upp");
        m_footAnime.SetTrigger("B_KD_Upp");
    }

    private void SetTrigerKDUppEnd()
    {
        m_hairAnime.SetTrigger("B_KD_Upp_End");
        m_faceAnime.SetTrigger("B_KD_Upp_End");
        m_bodyAnime.SetTrigger("B_KD_Upp_End");
        m_pantsAnime.SetTrigger("B_KD_Upp_End");
        m_handsAnime.SetTrigger("B_KD_Upp_End");
        m_footAnime.SetTrigger("B_KD_Upp_End");
    }

    private void SetTrigerKDUppRaise()
    {
        m_hairAnime.SetTrigger("B_KD_Upp_Raise");
        m_faceAnime.SetTrigger("B_KD_Upp_Raise");
        m_bodyAnime.SetTrigger("B_KD_Upp_Raise");
        m_pantsAnime.SetTrigger("B_KD_Upp_Raise");
        m_handsAnime.SetTrigger("B_KD_Upp_Raise");
        m_footAnime.SetTrigger("B_KD_Upp_Raise");
    }

    private void SetTrigerKDUppAirHit()
    {
        m_hairAnime.SetTrigger("B_KD_Upp_Air_Hit");
        m_faceAnime.SetTrigger("B_KD_Upp_Air_Hit");
        m_bodyAnime.SetTrigger("B_KD_Upp_Air_Hit");
        m_pantsAnime.SetTrigger("B_KD_Upp_Air_Hit");
        m_handsAnime.SetTrigger("B_KD_Upp_Air_Hit");
        m_footAnime.SetTrigger("B_KD_Upp_Air_Hit");
    }

    private void SetTrigerKDUppDownHit()
    {
        m_hairAnime.SetTrigger("B_KD_Upp_Down_Hit");
        m_faceAnime.SetTrigger("B_KD_Upp_Down_Hit");
        m_bodyAnime.SetTrigger("B_KD_Upp_Down_Hit");
        m_pantsAnime.SetTrigger("B_KD_Upp_Down_Hit");
        m_handsAnime.SetTrigger("B_KD_Upp_Down_Hit");
        m_footAnime.SetTrigger("B_KD_Upp_Down_Hit");
    }

    private void SetTrigerSkillFirstBlade()
    {
        m_hairAnime.SetTrigger("B_Skill_First_Blade");
        m_faceAnime.SetTrigger("B_Skill_First_Blade");
        m_bodyAnime.SetTrigger("B_Skill_First_Blade");
        m_pantsAnime.SetTrigger("B_Skill_First_Blade");
        m_handsAnime.SetTrigger("B_Skill_First_Blade");
        m_footAnime.SetTrigger("B_Skill_First_Blade");
    }

    private void SetTrigerSkillFirstBlade02()
    {
        m_hairAnime.SetTrigger("B_Skill_First_Blade02");
        m_faceAnime.SetTrigger("B_Skill_First_Blade02");
        m_bodyAnime.SetTrigger("B_Skill_First_Blade02");
        m_pantsAnime.SetTrigger("B_Skill_First_Blade02");
        m_handsAnime.SetTrigger("B_Skill_First_Blade02");
        m_footAnime.SetTrigger("B_Skill_First_Blade02");
    }

    private void SetTrigerSkillPierceStep()
    {
        m_hairAnime.SetTrigger("B_Skill_Pierce_Step");
        m_faceAnime.SetTrigger("B_Skill_Pierce_Step");
        m_bodyAnime.SetTrigger("B_Skill_Pierce_Step");
        m_pantsAnime.SetTrigger("B_Skill_Pierce_Step");
        m_handsAnime.SetTrigger("B_Skill_Pierce_Step");
        m_footAnime.SetTrigger("B_Skill_Pierce_Step");
    }

    private void SetTrigerSkillSpinCutter()
    {
        m_hairAnime.SetTrigger("B_Skill_Spin_Cutter");
        m_faceAnime.SetTrigger("B_Skill_Spin_Cutter");
        m_bodyAnime.SetTrigger("B_Skill_Spin_Cutter");
        m_pantsAnime.SetTrigger("B_Skill_Spin_Cutter");
        m_handsAnime.SetTrigger("B_Skill_Spin_Cutter");
        m_footAnime.SetTrigger("B_Skill_Spin_Cutter");
    }

    private void SetTrigerNormalStand()
    {
        m_hairAnime.SetTrigger("N_Stand");
        m_faceAnime.SetTrigger("N_Stand");
        m_bodyAnime.SetTrigger("N_Stand");
        m_pantsAnime.SetTrigger("N_Stand");
        m_handsAnime.SetTrigger("N_Stand");
        m_footAnime.SetTrigger("N_Stand");
    }

    private void SetTrigerBattleStand()
    {
        m_hairAnime.SetTrigger("B_Stand");
        m_faceAnime.SetTrigger("B_Stand");
        m_bodyAnime.SetTrigger("B_Stand");
        m_pantsAnime.SetTrigger("B_Stand");
        m_handsAnime.SetTrigger("B_Stand");
        m_footAnime.SetTrigger("B_Stand");
    }

    private void SetTrigerBattleStandDuration()
    {
        m_hairAnime.SetTrigger("B_Stand_Duration");
        m_faceAnime.SetTrigger("B_Stand_Duration");
        m_bodyAnime.SetTrigger("B_Stand_Duration");
        m_pantsAnime.SetTrigger("B_Stand_Duration");
        m_handsAnime.SetTrigger("B_Stand_Duration");
        m_footAnime.SetTrigger("B_Stand_Duration");
    }

    private void SetTrigerBattleRun()
    {
        m_hairAnime.SetTrigger("B_Run");
        m_faceAnime.SetTrigger("B_Run");
        m_bodyAnime.SetTrigger("B_Run");
        m_pantsAnime.SetTrigger("B_Run");
        m_handsAnime.SetTrigger("B_Run");
        m_footAnime.SetTrigger("B_Run");
    }

    private void SetTrigerBattleDash()
    {
        m_hairAnime.SetTrigger("B_Dash_Start");
        m_faceAnime.SetTrigger("B_Dash_Start");
        m_bodyAnime.SetTrigger("B_Dash_Start");
        m_pantsAnime.SetTrigger("B_Dash_Start");
        m_handsAnime.SetTrigger("B_Dash_Start");
        m_footAnime.SetTrigger("B_Dash_Start");
    }

    private void SetTrigerBattleJump()
    {
        m_hairAnime.SetTrigger("B_Jump_Start_F");
        m_faceAnime.SetTrigger("B_Jump_Start_F");
        m_bodyAnime.SetTrigger("B_Jump_Start_F");
        m_pantsAnime.SetTrigger("B_Jump_Start_F");
        m_handsAnime.SetTrigger("B_Jump_Start_F");
        m_footAnime.SetTrigger("B_Jump_Start_F");
    }

    private void SetTrigerBattleDashJump()
    {
        m_hairAnime.SetTrigger("B_Dash_Jump_Start");
        m_faceAnime.SetTrigger("B_Dash_Jump_Start");
        m_bodyAnime.SetTrigger("B_Dash_Jump_Start");
        m_pantsAnime.SetTrigger("B_Dash_Jump_Start");
        m_handsAnime.SetTrigger("B_Dash_Jump_Start");
        m_footAnime.SetTrigger("B_Dash_Jump_Start");
    }



    private void Ani_Idle()
    {

    }

    private void Ani_Run()
    {
        float speedPer = m_targetSpeed / m_moveSpeed;

        m_hairAnime.SetFloat("Speed", speedPer);
        m_faceAnime.SetFloat("Speed", speedPer);
        m_bodyAnime.SetFloat("Speed", speedPer);
        m_pantsAnime.SetFloat("Speed", speedPer);
        m_handsAnime.SetFloat("Speed", speedPer);
        m_footAnime.SetFloat("Speed", speedPer);
    }

    private void Ani_RunEnd()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Run_F_End") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            lastIdleChangeTime = Time.time;
            m_state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_Dash()
    {

    }

    private void Ani_DashEnd()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Dash_End") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            lastIdleChangeTime = Time.time;
            m_state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_Jump()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.Land;
            ChangeFlagTrue();
        }
    }

    private void Ani_DashJump()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.DashLand;
            ChangeFlagTrue();

            m_moveAnimeDir = m_modelTransform.forward;
        }
    }

    private void Ani_Land()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Jump_Land_C"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_lockInput && 0.3f <= time)
            {
                m_lockInput = false;
            }

            if (0.95f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_DashLand()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Dash_Jump_End"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_lockInput && 0.1f <= time)
            {
                m_lockInput = false;
            }

            // 정지
            if (0.35f <= time)
            {
                m_moveAnimeDir = Vector3.zero;
            }

            // 종료
            if (0.9f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_Evade()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BattleBase.B_Evade_F"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            // 정지
            if (0.5f <= time)
            {
                m_moveAnimeDir = Vector3.zero;
            }

            if (m_lockInput && 0.9f <= time)
            {
                m_lockInput = false;
            }

            // 종료
            if (0.95f <= time)
            {
                ChangeFlagFalse();
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    private void Ani_DMG_L()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_DMG_L") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            lastIdleChangeTime = Time.time;
            m_state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_DMG_R()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_DMG_R") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            lastIdleChangeTime = Time.time;
            m_state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    private void Ani_KB()
    {

    }

    private void Ani_KD_Ham_F()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Ham_B()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Str()
    {

    }

    private void Ani_KD_Upp()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_End()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_End") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_Down;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Down()
    {

    }

    private void Ani_KD_Upp_Air_Hit()
    {
        // 착지
        if (m_characterController.isGrounded)
        {
            // 점프 한지 얼마 안됬으면 무시
            if (Time.time - m_lastJumpTime <= 0.5f)
                return;

            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_End;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Down_Hit()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_Down_Hit") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            m_state = HaruState.KD_Upp_Down;
            ChangeFlagTrue();
        }
    }

    private void Ani_KD_Upp_Raise()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit.B_KD_Upp_Raise") &&
            m_hairAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            ChangeFlagFalse();
            lastIdleChangeTime = Time.time;
            m_state = HaruState.Idle;
            ChangeFlagTrue();
        }
    }

    // 일반 공격 1
    private void Ani_NormalAttack1()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_01"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.3f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.3f > time && time >= 0.2f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack1, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.3f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.4f <= time)
            {
                m_lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (m_gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    m_state = HaruState.NormalAttack2;
                    ChangeFlagTrue();
                    return;
                }

            }

            if (!m_moveStand && 0.7f <= time)
            {
                m_moveStand = true;
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
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_02"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.27f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.27f > time && time >= 0.2f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack2, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.23f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.4f <= time)
            {
                m_lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (m_gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    m_state = HaruState.NormalAttack3;
                    ChangeFlagTrue();
                    return;
                }
            }

            if (!m_moveStand && 0.7f <= time)
            {
                m_moveStand = true;
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
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_03"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.3f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.3f > time && time >= 0.21f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack3, 1, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }
            else if (m_isAttacking && 0.21f > time && time >= 0.17f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.17f > time && time >= 0.1f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack3, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.3f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.4f <= time)
            {
                m_lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (m_gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    m_state = HaruState.NormalAttack4;
                    ChangeFlagTrue();
                    return;
                }
            }

            if (!m_moveStand && 0.75f <= time)
            {
                m_moveStand = true;
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
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_04"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.3f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.3f > time && time >= 0.2f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Down;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack4, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.28f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.47f <= time)
            {
                m_lockInput = false;

                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (m_gameManager.KeyInputCheck(KeyCode.Mouse0, 0.5f))
                {
                    m_state = HaruState.NormalAttack5;
                    ChangeFlagTrue();
                    return;
                }
            }

            if (!m_moveStand && 0.75f <= time)
            {
                m_moveStand = true;
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
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.NormalAttack.B_N_Attack_05"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.33f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.33f > time && time >= 0.24f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Strike;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.NormalAttack5, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.4f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.52f <= time)
            {
                m_lockInput = false;
            }

            if (!m_moveStand && 0.8f <= time)
            {
                m_moveStand = true;
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
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_First_Blade"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isAttacking && time >= 0.67f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.67f > time && time >= 0.43f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Strike;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 2, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }
            else if (m_isAttacking && 0.43f > time && time >= 0.3f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.3f > time && time >= 0.2f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 1, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }
            else if (m_isAttacking && 0.2f > time && time >= 0.17f)
            {
                m_isAttacking = false;
                // 무기 충돌 트리거 OFF
                m_weapon.OffTrigger();
            }
            else if (!m_isAttacking && 0.17f > time && time >= 0.1f)
            {
                m_isAttacking = true;
                // 무기 충돌 트리거 ON
                m_weapon.OnTrigger();
                m_weapon.m_attackType = AttackType.Normal;
                float damage = 0f;
                if (!m_playerInfo.GetSkillDamage(HaruSkill.FirstBlade, 0, ref damage))
                    return;
                m_weapon.m_attackDamage = Random.Range(m_playerInfo.currentPlayerData.minAtk, m_playerInfo.currentPlayerData.maxAtk) * damage;
            }

            if (m_moveAttack && 0.5f <= time)
            {
                m_moveAttack = false;
            }

            if (0.43f > time && time >= 0.3f)
            {
                // 키 입력 판정보다 약간 이전에 입력 받았어도 허용
                if (m_gameManager.KeyInputCheck(KeyCode.Mouse1, 0.5f))
                {
                    ChangeFlagFalse();
                    m_state = HaruState.FirstBlade02;
                    ChangeFlagTrue();
                    return;
                }
            }

            if (m_lockInput && 0.7f <= time)
            {
                m_lockInput = false;
            }

            if (!m_moveStand && 0.8f <= time)
            {
                m_moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 퍼스트 블레이드 추가 공격
    private void Ani_FirstBlade02()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_First_Blade02"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_moveAttack && 0.4f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.6f <= time)
            {
                m_lockInput = false;
            }

            if (!m_moveStand && 0.7f <= time)
            {
                m_moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 피어스 스탭
    private void Ani_PierceStep()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_Pierce_Step"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_moveAttack && 0.4f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.7f <= time)
            {
                m_lockInput = false;
            }

            if (!m_moveStand && 0.72f <= time)
            {
                m_moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }

    // 스핀 커터
    private void Ani_SpinCutter()
    {
        if (m_hairAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Skill.B_Skill_Spin_Cutter"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_moveAttack && 0.5f <= time)
            {
                m_moveAttack = false;
            }

            if (m_lockInput && 0.7f <= time)
            {
                m_lockInput = false;
            }

            if (!m_moveStand && 0.85f <= time)
            {
                m_moveStand = true;
            }

            if (0.99f <= time)
            {
                ChangeFlagFalse();
                lastIdleChangeTime = Time.time;
                m_state = HaruState.Idle;
                ChangeFlagTrue();
            }
        }
    }
}
