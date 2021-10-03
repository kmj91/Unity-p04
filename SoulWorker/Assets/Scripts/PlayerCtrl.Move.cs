using UnityEngine;

public partial class PlayerCtrl : MonoBehaviour
{
    private void Move_Idle()
    {
        m_characterController.Move(Vector3.down);
    }

    private void Move_Run()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_RunEnd()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_Dash()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed * m_dashSpeedGob, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_DashEnd()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed * m_dashSpeedGob, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_Jump()
    {
        // SmoothDamp 사용 안 함
        m_targetSpeed = m_moveSpeed;

        Vector3 moveDir = (m_cameraTransform.forward * m_moveInput.y) + (m_cameraTransform.right * m_moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_DashJump()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime / m_airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_Land()
    {
        m_characterController.Move(Vector3.down);
    }

    private void Move_DashLand()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_Evade()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 15f, ref m_speedSmoothVelocity, m_speedSmoothTime);

        Vector3 moveDir = (m_cameraTransform.forward * m_moveAnimeDir.y) + (m_cameraTransform.right * m_moveAnimeDir.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Ham_F()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime / m_airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Ham_B()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime / m_airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Str()
    {

    }

    private void Move_KD_Upp()
    {
        m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, m_moveSpeed, ref m_speedSmoothVelocity, m_speedSmoothTime / m_airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack1()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 2.7f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 2.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else 
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack2()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 5.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.2f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack3()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 5.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 2.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack4()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 4.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.5f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack5()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 4.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    // 퍼스트 블레이드
    private void Move_FirstBlade()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 6.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.8f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    // 퍼스트 블레이드 추가 공격
    private void Move_FirstBlade02()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 2.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.8f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    // 피어스 스탭
    private void Move_PierceStep()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 10.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }

    // 스핀 커터
    private void Move_SpinCutter()
    {
        if (m_moveAttack)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 5.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else if (m_moveStand)
        {
            m_targetSpeed = Mathf.SmoothDamp(m_currentSpeed, 1.0f, ref m_speedSmoothVelocity, m_speedSmoothTime);
        }
        else
        {
            m_characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = m_moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        m_currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * m_targetSpeed + Vector3.up * m_currentVelocityY;
        m_characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (m_modelTransform.forward != moveDir && Vector3.zero != moveDir)
            m_turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (m_characterController.isGrounded)
        {
            m_currentVelocityY = 0.0f;
        }
    }
}
