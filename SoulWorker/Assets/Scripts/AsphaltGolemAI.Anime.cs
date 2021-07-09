using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AsphaltGolemAI : LivingEntity
{
    private void SetSpeedZero()
    {
        bodyAnime.SetFloat("Speed", 0);
        armsAnime.SetFloat("Speed", 0);
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
