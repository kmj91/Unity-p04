using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStruct;
using MyEnum;

public class GameManager : MonoBehaviour
{
    public PlayerCtrl playerCtrl;                   // 플레이어 컨트롤 스크립트
    public bool useMouse = false;                   // 마우스 사용 여부


    private KeyInputQueue<KeyInfo> keyQueue;        // 키 입력 큐
    private Dictionary<KeyCode, Action> keyDown;    // 키 누름
    private Dictionary<KeyCode, Action> keyUp;      // 키 뗌

    private void Start()
    {
        KeyInit();
    }

    private void Update()
    {
        KeyProcess();
    }

    // 키 초기화
    private void KeyInit()
    {
        keyQueue = new KeyInputQueue<KeyInfo>();

        // 키 누름
        keyDown = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyDown_Forward },     // 앞으로 이동
            { KeyCode.S, KeyDown_Back },        // 뒤로 이동
            { KeyCode.A, KeyDown_Left },        // 왼쪽으로 이동
            { KeyCode.D, KeyDown_Right },       // 오른쪽으로 이동
            { KeyCode.Space, KeyDown_Jump }     // 점프
        };

        // 키 뗌
        keyUp = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyUp_Forward },       // 앞으로 이동
            { KeyCode.S, KeyUp_Back },          // 뒤로 이동
            { KeyCode.A, KeyUp_Left },          // 왼쪽으로 이동
            { KeyCode.D, KeyUp_Right },         // 오른쪽으로 이동
            { KeyCode.Space, KeyUp_Jump },      // 점프
            { KeyCode.I, KeyUp_Inventory }      // 인벤토리
        };
    }

    // 키처리
    private void KeyProcess() 
    {
        // 키 뗌
        // UI 관련
        foreach (var dic in keyUp)
        {
            if (Input.GetKeyUp(dic.Key))
            {
                dic.Value();
            }
        }

        // 키 누름
        // 조작 관련
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDown)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }

        JumpCheck();
        MoveCheck();
    }


    private void JumpCheck()
    {
        var queue = keyQueue.GetQueue();
        int iSize = keyQueue.GetQueueSize();
        int iRear = keyQueue.GetRear();
        int iFront = keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 점프
            if (queue[iRear].key == KeyCode.Space)
            {
                playerCtrl.Jump();
            }
        }
    }

    private void MoveCheck()
    {
        var queue = keyQueue.GetQueue();
        int iSize = keyQueue.GetQueueSize();
        int iRear = keyQueue.GetRear();
        int iFront = keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 앞으로 이동
            if (queue[iRear].key == KeyCode.W)
            {
                while (iRear != iFront)
                {
                    --iRear;
                    if (iRear < 0)
                    {
                        iRear = iSize - 1;
                    }

                    // 앞 왼쪽으로 이동
                    if (queue[iRear].key == KeyCode.A)
                    {
                        playerCtrl.MoveFL();
                        return;
                    }
                    // 앞 오른쪽으로 이동
                    else if(queue[iRear].key == KeyCode.D)
                    {
                        playerCtrl.MoveFR();
                        return;
                    }
                }

                // 앞으로 이동
                playerCtrl.MoveFF();
                return;
            }
            // 뒤로 이동
            else if (queue[iRear].key == KeyCode.S)
            {
                while (iRear != iFront)
                {
                    --iRear;
                    if (iRear < 0)
                    {
                        iRear = iSize - 1;
                    }

                    // 뒤 왼쪽으로 이동
                    if (queue[iRear].key == KeyCode.A)
                    {
                        playerCtrl.MoveBL();
                        return;
                    }
                    // 뒤 오른쪽으로 이동
                    else if (queue[iRear].key == KeyCode.D)
                    {
                        playerCtrl.MoveBR();
                        return;
                    }
                }

                // 뒤로 이동
                playerCtrl.MoveBB();
                return;
            }
            // 왼쪽으로 이동
            else if (queue[iRear].key == KeyCode.A)
            {
                while (iRear != iFront)
                {
                    --iRear;
                    if (iRear < 0)
                    {
                        iRear = iSize - 1;
                    }

                    // 앞 왼쪽으로 이동
                    if (queue[iRear].key == KeyCode.W)
                    {
                        playerCtrl.MoveFL();
                        return;
                    }
                    // 뒤 왼쪽으로 이동
                    else if (queue[iRear].key == KeyCode.S)
                    {
                        playerCtrl.MoveBL();
                        return;
                    }
                }

                // 왼쪽으로 이동
                playerCtrl.MoveLL();
                return;
            }
            // 오른쪽으로 이동
            else if (queue[iRear].key == KeyCode.D)
            {
                while (iRear != iFront)
                {
                    --iRear;
                    if (iRear < 0)
                    {
                        iRear = iSize - 1;
                    }

                    // 앞 오른쪽으로 이동
                    if (queue[iRear].key == KeyCode.W)
                    {
                        playerCtrl.MoveFR();
                        return;
                    }
                    // 뒤 오른쪽으로 이동
                    else if (queue[iRear].key == KeyCode.S)
                    {
                        playerCtrl.MoveBR();
                        return;
                    }
                }

                // 왼쪽으로 이동
                playerCtrl.MoveRR();
                return;
            }
        }

        playerCtrl.MoveNone();
    }

    private void EraseKey(KeyCode key)
    {
        var queue = keyQueue.GetQueue();
        int iSize = keyQueue.GetQueueSize();
        int iRear = keyQueue.GetRear();
        int iFront = keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 지움
            if (queue[iRear].key == key)
            {
                queue[iRear].key = KeyCode.None;
            }
        }
    }


    //***************************
    // Key Down
    //***************************

    // 앞으로 이동
    private void KeyDown_Forward()
    {
        KeyInfo key = new KeyInfo(KeyState.Down, KeyCode.W, Time.realtimeSinceStartup);
        keyQueue.Enqueue(in key);
    }

    // 뒤로 이동
    private void KeyDown_Back()
    {
        KeyInfo key = new KeyInfo(KeyState.Down, KeyCode.S, Time.realtimeSinceStartup);
        keyQueue.Enqueue(in key);
    }

    // 왼쪽으로 이동
    private void KeyDown_Left()
    {
        KeyInfo key = new KeyInfo(KeyState.Down, KeyCode.A, Time.realtimeSinceStartup);
        keyQueue.Enqueue(in key);
    }

    // 오른쪽으로 이동
    private void KeyDown_Right()
    {
        KeyInfo key = new KeyInfo(KeyState.Down, KeyCode.D, Time.realtimeSinceStartup);
        keyQueue.Enqueue(in key);
    }

    // 점프
    private void KeyDown_Jump()
    {
        KeyInfo key = new KeyInfo(KeyState.Down, KeyCode.Space, Time.realtimeSinceStartup);
        keyQueue.Enqueue(in key);
    }



    //***************************
    // Key Up
    //***************************

    private void KeyUp_Forward()
    {
        EraseKey(KeyCode.W);
    }

    private void KeyUp_Back()
    {
        EraseKey(KeyCode.S);
    }

    private void KeyUp_Left()
    {
        EraseKey(KeyCode.A);
    }

    private void KeyUp_Right()
    {
        EraseKey(KeyCode.D);
    }

    private void KeyUp_Jump()
    {
        EraseKey(KeyCode.Space);
    }

    // 인벤토리
    private void KeyUp_Inventory()
    {
        // 마우스 사용
        useMouse = true;
        // 조작키 큐? 배열 초기화 해야함
    }
}
