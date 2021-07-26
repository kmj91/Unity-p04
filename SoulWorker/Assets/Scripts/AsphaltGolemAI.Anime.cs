using MyEnum;

public partial class AsphaltGolemAI : LivingEntity
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



    private void Ani_Idle()
    {
        SetSpeedZero();
    }

    private void Ani_Run()
    {
        // 이동 애니메이션 처리
        float velocity = agent.desiredVelocity.magnitude;
        bodyAnime.SetFloat("Speed", velocity);
        armsAnime.SetFloat("Speed", velocity);
    }

    private void Ani_DMG_L()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            state = AsphaltGolemState.Idle;

    }

    private void Ani_DMG_R()
    {
        if (bodyAnime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            state = AsphaltGolemState.Idle;
    }

    private void Ani_Death()
    {

    }

    private void Ani_A_Skill_01()
    {

    }

    private void Ani_A_Skill_02()
    {

    }

    private void Ani_A_Skill_03()
    {

    }
}
