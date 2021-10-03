using UnityEngine;
using MyEnum;

public partial class AsphaltGolemAI : MonsterAI
{
    private void SetSpeedZero()
    {
        m_bodyAnime.SetFloat("Speed", 0);
        m_armsAnime.SetFloat("Speed", 0);
    }

    private void SetTrigerDMGL()
    {
        m_bodyAnime.SetTrigger("B_DMG_L");
        m_armsAnime.SetTrigger("B_DMG_L");
    }

    private void SetTrigerDMGR()
    {
        m_bodyAnime.SetTrigger("B_DMG_R");
        m_armsAnime.SetTrigger("B_DMG_R");
    }

    private void SetTrigerKB()
    {
        m_bodyAnime.SetTrigger("B_KB");
        m_armsAnime.SetTrigger("B_KB");
    }

    private void SetTrigerKDHam()
    {
        m_bodyAnime.SetTrigger("B_KD_Ham");
        m_armsAnime.SetTrigger("B_KD_Ham");
    }

    private void SetTrigerKDStr()
    {
        m_bodyAnime.SetTrigger("B_KD_Str");
        m_armsAnime.SetTrigger("B_KD_Str");
    }

    private void SetTrigerKDUpp()
    {
        m_bodyAnime.SetTrigger("B_KD_Upp");
        m_armsAnime.SetTrigger("B_KD_Upp");
    }

    private void SetTrigerDeath()
    {
        m_bodyAnime.SetTrigger("Death");
        m_armsAnime.SetTrigger("Death");
    }

    private void SetTrigerASkill_01()
    {
        m_isActionEnd = false;
        m_bodyAnime.SetTrigger("A_Skill_01");
        m_armsAnime.SetTrigger("A_Skill_01");
    }

    private void SetTrigerASkill_02()
    {
        m_isActionEnd = false;
        m_bodyAnime.SetTrigger("A_Skill_02");
        m_armsAnime.SetTrigger("A_Skill_02");
    }

    private void SetTrigerASkill_03()
    {
        m_isActionEnd = false;
        m_bodyAnime.SetTrigger("A_Skill_03");
        m_armsAnime.SetTrigger("A_Skill_03");
    }



    private void Ani_Idle()
    {
        SetSpeedZero();
    }

    private void Ani_Run()
    {
        // 타겟에서 근접하면
        if (Vector3.Distance(m_targetEntity.transform.position, m_monsterTransform.position) <= m_meleeDistance - 1f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            return;
        }

        // 이동
        m_agent.SetDestination(m_targetEntity.transform.position);
        // 이동 애니메이션 처리
        float velocity = m_agent.desiredVelocity.magnitude;
        m_bodyAnime.SetFloat("Speed", velocity);
        m_armsAnime.SetFloat("Speed", velocity);
    }

    private void Ani_DMG_L()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }

    }

    private void Ani_DMG_R()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }
    }

    private void Ani_KB()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }
    }

    private void Ani_KD_Ham()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }
    }

    private void Ani_KD_Str()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }
    }

    private void Ani_KD_Upp()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            m_state = AsphaltGolemState.Idle;
            m_isActionEnd = true;
            m_isTargetFollow = true;
        }
    }

    private void Ani_Death()
    {

    }

    private void Ani_A_Skill_01()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_01"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isTargetFollow && 0.1f <= time)
                m_isTargetFollow = false;

            if (0.4f <= time)
            {
                // 충돌 트리거 OFF
                OffTriggerRight();
            }
            else if(0.3f <= time && time < 0.4f) 
            {
                // 충돌 트리거 ON
                OnTriggerRight();
            }

            if (0.99f <= time)
            {
                m_state = AsphaltGolemState.Idle;
                m_isActionEnd = true;
                m_isTargetFollow = true;
            }
        }
    }

    private void Ani_A_Skill_02()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_02"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_isTargetFollow &&  0.1f <= time)
                m_isTargetFollow = false;

            if (0.6f <= time)
            {
                // 충돌 트리거 OFF
                OffTriggerRight();
                OffTriggerLeft();
            }
            else if (0.52f <= time && time < 0.6f)
            {
                // 충돌 트리거 ON
                OnTriggerRight();
                OnTriggerLeft();
            }

            if (0.99f <= time)
            {
                m_state = AsphaltGolemState.Idle;
                m_isActionEnd = true;
                m_isTargetFollow = true;
            }
        }
    }

    private void Ani_A_Skill_03()
    {
        if (m_bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_03"))
        {
            float time = m_bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (0.11f <= time)
            {
                // 충돌 트리거 OFF
                OffTriggerLeft();
            }
            else if (0.01f <= time && time < 0.11f)
            {
                // 충돌 트리거 ON
                OnTriggerLeft();
            }

            if (m_isTargetFollow && 0.1f <= time)
                m_isTargetFollow = false;

            if (0.99f <= time)
            {
                m_state = AsphaltGolemState.Idle;
                m_isActionEnd = true;
                m_isTargetFollow = true;
                OffTriggerLeft();
            }
        }
    }
}
