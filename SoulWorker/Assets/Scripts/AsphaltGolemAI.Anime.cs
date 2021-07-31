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

    private void Ani_Death()
    {

    }

    private void Ani_A_Skill_01()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.A_Skill.B_A_Skill_01"))
        {
            if (isTargetFollow && bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
                isTargetFollow = false;

            if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
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
            if (isTargetFollow && bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
                isTargetFollow = false;

            if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
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
            if (isTargetFollow && bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
                isTargetFollow = false;

            if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                state = AsphaltGolemState.Idle;
                isActionEnd = true;
                isTargetFollow = true;
            }
        }
    }
}
