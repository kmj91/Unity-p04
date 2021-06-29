using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyEnum;

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
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed * 2.0f, ref speedSmoothVelocity, speedSmoothTime);

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
        targetSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime / airControlPercent);

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

    private void Move_NormalAttack1()
    {

    }

    private void Move_NormalAttack2()
    {

    }

    private void Move_NormalAttack3()
    {

    }

    private void Move_NormalAttack4()
    {

    }

    private void Move_NormalAttack5()
    {

    }
}
