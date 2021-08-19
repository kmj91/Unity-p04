using UnityEngine;

public partial class PlayerCtrl : MonoBehaviour
{
    private void Move_Idle()
    {
        characterController.Move(Vector3.down);
    }

    private void Move_Run()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_Dash()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed * dashSpeedGob, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_Jump()
    {
        // SmoothDamp 사용 안 함
        targetSpeed = moveSpeed;

        Vector3 moveDir = (cameraTransform.forward * moveInput.y) + (cameraTransform.right * moveInput.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_DashJump()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime / airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_Land()
    {
        characterController.Move(Vector3.down);
    }

    private void Move_DashLand()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_Evade()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, 15f, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 moveDir = (cameraTransform.forward * moveAnimeDir.y) + (cameraTransform.right * moveAnimeDir.x);
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Ham_F()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime / airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Ham_B()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime / airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_KD_Str()
    {

    }

    private void Move_KD_Upp()
    {
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime / airControlPercent);

        // 대쉬 점프는 방향 변경 불가
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack1()
    {
        if (moveAttack)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 2.7f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else if (moveStand)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 2.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else 
        {
            characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack2()
    {
        if (moveAttack)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 5.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else if (moveStand)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 1.2f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else
        {
            characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack3()
    {
        if (moveAttack)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 5.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else if (moveStand)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 2.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else
        {
            characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack4()
    {
        if (moveAttack)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 4.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else if (moveStand)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 1.5f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else
        {
            characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    private void Move_NormalAttack5()
    {
        if (moveAttack)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 4.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else if (moveStand)
        {
            targetSpeed = Mathf.SmoothDamp(currentSpeed, 1.0f, ref speedSmoothVelocity, speedSmoothTime);
        }
        else
        {
            characterController.Move(Vector3.down);
            return;
        }

        // 카메라 방향 혹은 캐릭터 방향
        Vector3 moveDir = moveAnimeDir;
        moveDir.y = 0.0f;
        moveDir = moveDir.normalized;

        currentVelocityY += Time.deltaTime * Physics.gravity.y * 3.0f;
        Vector3 velocity = moveDir * targetSpeed + Vector3.up * currentVelocityY;
        characterController.Move(velocity * Time.deltaTime);

        // 현제 캐릭터가 바라보는 곳과 이동 방향이 같지 않으면 이동 방향값 셋팅
        // 단 캐릭터 조작을 하지 않아서 가만히 있으면 X
        if (modelTransform.forward != moveDir && Vector3.zero != moveDir)
            turnDir = moveDir;

        // 땅에 닿으면 초기화
        if (characterController.isGrounded)
        {
            currentVelocityY = 0.0f;
        }
    }

    // 퍼스트 블레이드
    private void Move_FirstBlade()
    {

    }

    // 피어스 스탭
    private void Move_PierceStep()
    {

    }

    // 스핀 커터
    private void Move_SpinCutter()
    {

    }
}
