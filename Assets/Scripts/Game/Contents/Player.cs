using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public float speed;


    public MoveDirection ChrrentDirection
    {
        get
        {
            return mCurrentDirection;
        }
        set
        {
            mCurrentDirection = value;

            switch(mCurrentDirection.GetValue())
            {
                case MoveDirection.MOVE_LEFT:
                    transform.eulerAngles = new Vector3(0, -90, 0);
                    break;

                case MoveDirection.MOVE_UP:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;

                case MoveDirection.MOVE_RIGHT:
                    transform.eulerAngles = new Vector3(0, 90, 0);
                    break;

                case MoveDirection.MOVE_DOWN:
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    break;
            }
        }
    }

    private MoveDirection mCurrentDirection;
    private bool mIsMoving;
    private int mID;
    private bool mIsKeyPress;
    private List<InputButton> mInputButtons;

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;
        mIsKeyPress = false;
        ChrrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);
    }
    
    public void OnPressMoveButton(MoveDirection direction)
    {
        if(!mIsKeyPress)
        {
            ChrrentDirection = direction;
            mIsMoving = true;
            mIsKeyPress = true;

            SendMoveStart(ChrrentDirection.GetValue());
        }        
    }

    public void RemoteMoveStart(byte dir, float x, float z)
    {
        string message = string.Format("[이동 시작] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        ChrrentDirection = new MoveDirection(dir);
        mIsMoving = true;
    }

    public void RemoteMoveEnd(byte dir, float x, float z)
    {
        string message = string.Format("[이동 종료] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        ChrrentDirection = new MoveDirection(dir);
        mIsMoving = false;
    }

    void Start()
    {
        ChrrentDirection = MoveDirection.Down();
        mIsMoving = false;
        mInputButtons = new List<InputButton>();

        mInputButtons.Add(new LeftMoveButton(this));
        mInputButtons.Add(new UpMoveButton(this));
        mInputButtons.Add(new RightMoveButton(this));
        mInputButtons.Add(new DownMoveButton(this));
    }
    
    void Update()
    {        
        if(GameFramework.IsMy(mID))
        {
            ProcessInput();
        }
        
        Move();
    }

    private void ProcessInput()
    {
        foreach (var button in mInputButtons)
        {
            button.Poll();
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.S))
        {
            mIsMoving = false;
            mIsKeyPress = false;

            NetPacket packet = NetPacket.Alloc();
            short protocol = Protocol.PACKET_CS_PLAYER_MOVE_END;                       
            packet.Push(protocol).Push(ChrrentDirection.GetValue()).Push(transform.position.x).Push(transform.position.z);

            NetworkService.Instance.SendPacket(packet);
        }
    }

    private void Move()
    {
        if (mIsMoving)
        {
            Vector3 moveDir = ChrrentDirection.ToVector();
            transform.Translate(moveDir * Time.deltaTime * speed, Space.World);
        }
    }

    private void SendMoveStart(byte dir)
    {       
        NetPacket packet = NetPacket.Alloc();
        short protocol = Protocol.PACKET_CS_PLAYER_MOVE_START;
        packet.Push(protocol).Push(dir).Push(transform.position.x).Push(transform.position.z);

        NetworkService.Instance.SendPacket(packet);
    }
}
