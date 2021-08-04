using UnityEngine;
using MyEnum;

public partial class AsphaltGolemAI : MonsterAI
{
    private void SetSpeedZero()
    {
        bodyAnime.SetFloat("Speed", 0);
        armsAnime.SetFloat("Speed", 0);
    }

    private void SetTrigerDMGL()
    {
        bodyAnime.SetTrigger("B_DMG_L");
        armsAnime.SetTrigger("B_DMG_L");
    }

    private void SetTrigerDMGR()
    {
        bodyAnime.SetTrigger("B_DMG_R");
        armsAnime.SetTrigger("B_DMG_R");
    }

    private void SetTrigerKB()
    {
        bodyAnime.SetTrigger("B_KB");
        armsAnime.SetTrigger("B_KB");
    }

    private void SetTrigerKDHam()
    {
        bodyAnime.SetTrigger("B_KD_Ham");
        armsAnime.SetTrigger("B_KD_Ham");
    }

    private void SetTrigerKDStr()
    {
        bodyAnime.SetTrigger("B_KD_Str");
        armsAnime.SetTrigger("B_KD_Str");
    }

    private void SetTrigerKDUpp()
    {
        bodyAnime.SetTrigger("B_KD_Upp");
        armsAnime.SetTrigger("B_KD_Upp");
    }

    private void SetTrigerDeath()
    {
        bodyAnime.SetTrigger("Death");
        armsAnime.SetTrigger("Death");
    }

    private void SetTrigerASkill_01()
    {
        isActionEnd = false;
        bodyAnime.SetTrigger("A_Skill_01");
        armsAnime.SetTrigger("A_Skill_01");
    }

    private void SetTrigerASkill_02()
    {
        isActionEnd = false;
        bodyAnime.SetTrigger("A_Skill_02");
        armsAnime.SetTrigger("A_Skill_02");
    }

    private void SetTrigerASkill_03()
    {
        isActionEnd = false;
        bodyAnime.SetTrigger("A_Skill_03");
        armsAnime.SetTrigger("A_Skill_03");
    }



    private void Ani_Idle()
    {
        SetSpeedZero();
    }

    private void Ani_Run()
    {
        // 타겟에서 근접하면
        if (Vector3.Distance(targetEntity.transform.position, monsterTransform.position) <= meleeDistance - 1f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            return;
        }

        // 이동
        agent.SetDestination(targetEntity.transform.position);
        // 이동 애니메이션 처리
        float velocity = agent.desiredVelocity.magnitude;
        bodyAnime.SetFloat("Speed", velocity);
        armsAnime.SetFloat("Speed", velocity);
    }

    private void Ani_DMG_L()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }

    }

    private void Ani_DMG_R()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }
    }

    private void Ani_KB()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }
    }

    private void Ani_KD_Ham()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }
    }

    private void Ani_KD_Str()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }
    }

    private void Ani_KD_Upp()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = AsphaltGolemState.Idle;
            isActionEnd = true;
            isTargetFollow = true;
        }
    }

    private void Ani_Death()
    {

    }

    private void Ani_A_Skill_01()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_01"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isTargetFollow && 0.1f <= time)
                isTargetFollow = false;

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
                state = AsphaltGolemState.Idle;
                isActionEnd = true;
                isTargetFollow = true;
            }
        }
    }

    private void Ani_A_Skill_02()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_02"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (isTargetFollow &&  0.1f <= time)
                isTargetFollow = false;

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
                state = AsphaltGolemState.Idle;
                isActionEnd = true;
                isTargetFollow = true;
            }
        }
    }

    private void Ani_A_Skill_03()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_03"))
        {
            float time = bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime;

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

            if (isTargetFollow && 0.1f <= time)
                isTargetFollow = false;

            if (0.99f <= time)
            {
                state = AsphaltGolemState.Idle;
                isActionEnd = true;
                isTargetFollow = true;
                OffTriggerLeft();
            }
        }
    }
}
