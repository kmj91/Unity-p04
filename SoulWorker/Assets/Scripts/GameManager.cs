using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyStruct;
using MyEnum;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerCtrl m_playerCtrl;                 // 플레이어 컨트롤 스크립트
    public bool m_useMouse = false;                 // 마우스 사용 여부


    private CirculartQueue<KeyInfo> m_keyQueue;     // 키 입력 큐
    private Dictionary<KeyCode, Action> m_keyDown;  // 키 누름
    private Dictionary<KeyCode, Action> m_keyUp;    // 키 뗌

    private void Awake()
    {
        // UI 불러오기
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Start()
    {
        // UI 캐릭터 모습
        var uiMgr = UIManager.Instance;
        var uiPlayer = Instantiate(m_playerCtrl.gameObject);

        // 부모 지정
        uiPlayer.transform.parent = uiMgr.GetEquipmentTransform();
        uiPlayer.transform.localPosition = new Vector3(0f, -150f, -100f);
        uiPlayer.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        uiPlayer.transform.localScale = new Vector3(180f, 180f, 180f);
        uiPlayer.layer = 5;
        uiPlayer.GetComponent<PlayerCtrl>().SetUIObject();

        KeyInit();
    }

    private void Update()
    {
        KeyProcess();
    }


    // (현재시간 - time) 사이에 키값을 입력 받았으면 true
    public bool KeyInputCheck(KeyCode key, float time)
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 찾음
            if (queue[iRear].backupKey == key &&
                queue[iRear].time >= Time.time - time)
            {
                return true;
            }
        }

        return false;
    }


    // 키 초기화
    private void KeyInit()
    {
        m_keyQueue = new CirculartQueue<KeyInfo>();

        // 키 누름
        m_keyDown = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyDown_Forward },     // 앞으로 이동
            { KeyCode.S, KeyDown_Back },        // 뒤로 이동
            { KeyCode.A, KeyDown_Left },        // 왼쪽으로 이동
            { KeyCode.D, KeyDown_Right },       // 오른쪽으로 이동
            { KeyCode.Mouse0, KeyDown_Mouse0 }, // 마우스 왼쪽
            { KeyCode.Mouse1, KeyDown_Mouse1 }, // 마우스 오른쪽
            { KeyCode.Space, KeyDown_Jump },    // 점프
            { KeyCode.LeftShift, KeyDown_Dvade },   // 회피
            { KeyCode.Alpha1 , KeyDown_Alpha1 },    // 스킬 슬롯 1
            { KeyCode.Alpha2 , KeyDown_Alpha2 },    // 스킬 슬롯 2
            { KeyCode.Alpha3 , KeyDown_Alpha3 },    // 스킬 슬롯 3
            { KeyCode.Alpha4 , KeyDown_Alpha4 },    // 스킬 슬롯 4
            { KeyCode.Alpha5 , KeyDown_Alpha5 },    // 스킬 슬롯 5
            { KeyCode.Alpha6 , KeyDown_Alpha6 }     // 스킬 슬롯 6
        };

        // 키 뗌
        m_keyUp = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyUp_Forward },       // 앞으로 이동
            { KeyCode.S, KeyUp_Back },          // 뒤로 이동
            { KeyCode.A, KeyUp_Left },          // 왼쪽으로 이동
            { KeyCode.D, KeyUp_Right },         // 오른쪽으로 이동
            { KeyCode.Mouse0, KeyUp_Mouse0 },   // 마우스 왼쪽
            { KeyCode.Mouse1, KeyUp_Mouse1 },   // 마우스 오른쪽
            { KeyCode.Space, KeyUp_Jump },      // 점프
            { KeyCode.LeftShift, KeyUp_Dvade }, // 회피
            { KeyCode.Alpha1 , KeyUp_Alpha1 },  // 스킬 슬롯 1
            { KeyCode.Alpha2 , KeyUp_Alpha2 },  // 스킬 슬롯 2
            { KeyCode.Alpha3 , KeyUp_Alpha3 },  // 스킬 슬롯 3
            { KeyCode.Alpha4 , KeyUp_Alpha4 },  // 스킬 슬롯 4
            { KeyCode.Alpha5 , KeyUp_Alpha5 },  // 스킬 슬롯 5
            { KeyCode.Alpha6 , KeyUp_Alpha6 },  // 스킬 슬롯 6
            { KeyCode.I, KeyUp_Inventory },     // 인벤토리
            { KeyCode.P, KeyUp_Characterinfo }, // 캐릭터 상태창
            { KeyCode.K, KeyUp_Skillinfo }      // 스킬 정보창
        };
    }

    // 키처리
    private void KeyProcess() 
    {
        // 키 뗌
        // UI 관련
        foreach (var dic in m_keyUp)
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
            foreach (var dic in m_keyDown)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }

        // 2. 스킬 슬롯
        SkillCheck();
        // 3. 회피
        EvadeCheck();
        // 4. 점프
        JumpCheck();
        // 5. 마우스
        MouseCheck();
        // 6. 방향키
        MoveCheck();
    }


    // 스킬
    private void SkillCheck()
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 스킬 슬롯 1
            if (queue[iRear].key == KeyCode.Alpha1)
            {
                m_playerCtrl.SkillSlot1();
            }
            // 스킬 슬롯 2
            else if (queue[iRear].key == KeyCode.Alpha2)
            {
                m_playerCtrl.SkillSlot2();
            }
            // 스킬 슬롯 3
            else if (queue[iRear].key == KeyCode.Alpha3)
            {
                m_playerCtrl.SkillSlot3();
            }
            // 스킬 슬롯 4
            else if (queue[iRear].key == KeyCode.Alpha4)
            {
                m_playerCtrl.SkillSlot4();
            }
            // 스킬 슬롯 5
            else if (queue[iRear].key == KeyCode.Alpha5)
            {
                m_playerCtrl.SkillSlot5();
            }
            // 스킬 슬롯 6
            else if (queue[iRear].key == KeyCode.Alpha6)
            {
                m_playerCtrl.SkillSlot6();
            }
        }
    }

    // 회피
    private void EvadeCheck()
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 회피
            if (queue[iRear].key == KeyCode.LeftShift)
            {
                iRear = m_keyQueue.GetRear();
                iFront = m_keyQueue.GetFront();

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

                            // 앞 왼쪽으로 회피
                            if (queue[iRear].key == KeyCode.A)
                            {
                                m_playerCtrl.Evade(new Vector2(-1f, 1f));
                                return;
                            }
                            // 앞 오른쪽으로 회피
                            else if (queue[iRear].key == KeyCode.D)
                            {
                                m_playerCtrl.Evade(new Vector2(1f, 1f));
                                return;
                            }
                        }

                        // 앞으로 회피
                        m_playerCtrl.Evade(new Vector2(0f, 1f));
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

                            // 뒤 왼쪽으로 회피
                            if (queue[iRear].key == KeyCode.A)
                            {
                                m_playerCtrl.Evade(new Vector2(-1f, -1f));
                                return;
                            }
                            // 뒤 오른쪽으로 회피
                            else if (queue[iRear].key == KeyCode.D)
                            {
                                m_playerCtrl.Evade(new Vector2(1f, -1f));
                                return;
                            }
                        }

                        // 뒤로 회피
                        m_playerCtrl.Evade(new Vector2(0f, -1f));
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

                            // 앞 왼쪽으로 회피
                            if (queue[iRear].key == KeyCode.W)
                            {
                                m_playerCtrl.Evade(new Vector2(-1f, 1f));
                                return;
                            }
                            // 뒤 왼쪽으로 회피
                            else if (queue[iRear].key == KeyCode.S)
                            {
                                m_playerCtrl.Evade(new Vector2(-1f, -1f));
                                return;
                            }
                        }

                        // 왼쪽으로 회피
                        m_playerCtrl.Evade(new Vector2(-1f, 0f));
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

                            // 앞 오른쪽으로 회피
                            if (queue[iRear].key == KeyCode.W)
                            {
                                m_playerCtrl.Evade(new Vector2(1f, 1f));
                                return;
                            }
                            // 뒤 오른쪽으로 회피
                            else if (queue[iRear].key == KeyCode.S)
                            {
                                m_playerCtrl.Evade(new Vector2(1f, -1f));
                                return;
                            }
                        }

                        // 오른쪽으로 회피
                        m_playerCtrl.Evade(new Vector2(1f, 0f));
                        return;
                    }
                }
            }// if (queue[iRear].key == KeyCode.LeftShift)
        }// while (iRear != iFront)
    }

    // 점프
    private void JumpCheck()
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

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
                m_playerCtrl.Jump();
                return;
            }
        }
    }

    // 마우스
    private void MouseCheck()
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 마우스 왼쪽
            if (queue[iRear].key == KeyCode.Mouse0)
            {
                m_playerCtrl.MouseLeft();
                return;
            }
            // 마우스 오른쪽
            else if (queue[iRear].key == KeyCode.Mouse1)
            {
                m_playerCtrl.MouseRight();
                return;
            }
        }
    }

    // 이동
    private void MoveCheck()
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

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
                // 대쉬 입력 확인
                if (DashCheck(KeyCode.W, queue[iRear].time, iRear))
                {
                    m_playerCtrl.Dash();
                }

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
                        m_playerCtrl.Move(new Vector2(-1f, 1f));
                        return;
                    }
                    // 앞 오른쪽으로 이동
                    else if(queue[iRear].key == KeyCode.D)
                    {
                        m_playerCtrl.Move(new Vector2(1f, 1f));
                        return;
                    }
                }

                // 앞으로 이동
                m_playerCtrl.Move(new Vector2(0f, 1f));
                return;
            }
            // 뒤로 이동
            else if (queue[iRear].key == KeyCode.S)
            {
                // 대쉬 입력 확인
                if (DashCheck(KeyCode.S, queue[iRear].time, iRear))
                {
                    m_playerCtrl.Dash();
                }

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
                        m_playerCtrl.Move(new Vector2(-1f, -1f));
                        return;
                    }
                    // 뒤 오른쪽으로 이동
                    else if (queue[iRear].key == KeyCode.D)
                    {
                        m_playerCtrl.Move(new Vector2(1f, -1f));
                        return;
                    }
                }

                // 뒤로 이동
                m_playerCtrl.Move(new Vector2(0f, -1f));
                return;
            }
            // 왼쪽으로 이동
            else if (queue[iRear].key == KeyCode.A)
            {
                // 대쉬 입력 확인
                if (DashCheck(KeyCode.A, queue[iRear].time, iRear))
                {
                    m_playerCtrl.Dash();
                }

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
                        m_playerCtrl.Move(new Vector2(-1f, 1f));
                        return;
                    }
                    // 뒤 왼쪽으로 이동
                    else if (queue[iRear].key == KeyCode.S)
                    {
                        m_playerCtrl.Move(new Vector2(-1f, -1f));
                        return;
                    }
                }

                // 왼쪽으로 이동
                m_playerCtrl.Move(new Vector2(-1f, 0f));
                return;
            }
            // 오른쪽으로 이동
            else if (queue[iRear].key == KeyCode.D)
            {
                // 대쉬 입력 확인
                if (DashCheck(KeyCode.D, queue[iRear].time, iRear))
                {
                    m_playerCtrl.Dash();
                }

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
                        m_playerCtrl.Move(new Vector2(1f, 1f));
                        return;
                    }
                    // 뒤 오른쪽으로 이동
                    else if (queue[iRear].key == KeyCode.S)
                    {
                        m_playerCtrl.Move(new Vector2(1f, -1f));
                        return;
                    }
                }

                // 오른쪽으로 이동
                m_playerCtrl.Move(new Vector2(1f, 0f));
                return;
            }
        }

        m_playerCtrl.MoveNone();
    }

    // 대쉬 입력 확인
    private bool DashCheck(KeyCode key, float time, int iRear)
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iFront = m_keyQueue.GetFront();

        while (iRear != iFront)
        {
            --iRear;
            if (iRear < 0)
            {
                iRear = iSize - 1;
            }

            // 같은 키 값 찾기
            if (queue[iRear].backupKey == key)
            {
                // 짧은 시간안에 눌렀을 경우 true
                if (time - queue[iRear].time <= 0.5f)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }

    // 키값 지우기
    private void EraseKey(KeyCode key)
    {
        var queue = m_keyQueue.GetQueue();
        int iSize = m_keyQueue.GetQueueSize();
        int iRear = m_keyQueue.GetRear();
        int iFront = m_keyQueue.GetFront();

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
                return;
            }
        }
    }


    //***************************
    // Key Down
    //***************************

    // 앞으로 이동
    private void KeyDown_Forward()
    {
        KeyInfo key = new KeyInfo(KeyCode.W, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 뒤로 이동
    private void KeyDown_Back()
    {
        KeyInfo key = new KeyInfo(KeyCode.S, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 왼쪽으로 이동
    private void KeyDown_Left()
    {
        KeyInfo key = new KeyInfo(KeyCode.A, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 오른쪽으로 이동
    private void KeyDown_Right()
    {
        KeyInfo key = new KeyInfo(KeyCode.D, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 마우스 왼쪽
    private void KeyDown_Mouse0()
    {
        KeyInfo key = new KeyInfo(KeyCode.Mouse0, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 마우스 오른쪽
    private void KeyDown_Mouse1()
    {
        KeyInfo key = new KeyInfo(KeyCode.Mouse1, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 점프
    private void KeyDown_Jump()
    {
        KeyInfo key = new KeyInfo(KeyCode.Space, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 회피
    private void KeyDown_Dvade()
    {
        KeyInfo key = new KeyInfo(KeyCode.LeftShift, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 1
    private void KeyDown_Alpha1()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha1, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 2
    private void KeyDown_Alpha2()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha2, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 3
    private void KeyDown_Alpha3()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha3, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 4
    private void KeyDown_Alpha4()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha4, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 5
    private void KeyDown_Alpha5()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha5, Time.time);
        m_keyQueue.Enqueue(in key);
    }

    // 스킬 슬롯 6
    private void KeyDown_Alpha6()
    {
        KeyInfo key = new KeyInfo(KeyCode.Alpha6, Time.time);
        m_keyQueue.Enqueue(in key);
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

    // 마우스 왼쪽
    private void KeyUp_Mouse0()
    {
        EraseKey(KeyCode.Mouse0);
    }

    // 마우스 오른쪽
    private void KeyUp_Mouse1()
    {
        EraseKey(KeyCode.Mouse1);
    }

    // 점프
    private void KeyUp_Jump()
    {
        EraseKey(KeyCode.Space);
    }

    // 회피
    private void KeyUp_Dvade()
    {
        EraseKey(KeyCode.LeftShift);
    }

    // 스킬 슬롯 1
    private void KeyUp_Alpha1()
    {
        EraseKey(KeyCode.Alpha1);
    }

    // 스킬 슬롯 2
    private void KeyUp_Alpha2()
    {
        EraseKey(KeyCode.Alpha2);
    }

    // 스킬 슬롯 3
    private void KeyUp_Alpha3()
    {
        EraseKey(KeyCode.Alpha3);
    }

    // 스킬 슬롯 4
    private void KeyUp_Alpha4()
    {
        EraseKey(KeyCode.Alpha4);
    }

    // 스킬 슬롯 5
    private void KeyUp_Alpha5()
    {
        EraseKey(KeyCode.Alpha5);
    }

    // 스킬 슬롯 6
    private void KeyUp_Alpha6()
    {
        EraseKey(KeyCode.Alpha6);
    }

    // 인벤토리
    private void KeyUp_Inventory()
    {
        // 마우스 사용
        m_useMouse = true;
        // 조작키 큐? 배열 초기화 해야함
    }

    // 캐릭터 상태창
    private void KeyUp_Characterinfo()
    {
        // 마우스 사용
        m_useMouse = true;
        UIManager.Instance.ToggleCharacterinfo();
    }

    private void KeyUp_Skillinfo()
    {
        // 마우스 사용
        m_useMouse = true;
        UIManager.Instance.ToggleSkillinfo();
    }
}
