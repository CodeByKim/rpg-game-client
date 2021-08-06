using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum MoveDirection
    {
        Left,
        Up,
        Right,
        Down
    }

    public float speed;

    private MoveDirection mCurrentDirection;
    private bool mIsMoving;

    private int mID;
    private bool mIsKeyPress;

    private MoveDirection[] mMoveDirOffset =
    {
        MoveDirection.Left,
        MoveDirection.Up,
        MoveDirection.Right,
        MoveDirection.Down
    };

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;
        mIsKeyPress = false;
        SetDirection(dir);
        transform.position = new Vector3(x, 0, z);
    }

    public void MoveStart(byte dir, float x, float z)
    {
        string message = string.Format("[이동 시작] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        SetDirection(dir);
        mIsMoving = true;
    }

    public void MoveEnd(byte dir, float x, float z)
    {
        string message = string.Format("[이동 종료] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        SetDirection(dir);
        mIsMoving = false;
    }

    void Start()
    {
        mCurrentDirection = MoveDirection.Up;
        mIsMoving = false;
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
        if(!mIsKeyPress)
        {
            if (Input.GetKey(KeyCode.A))
            {
                mCurrentDirection = MoveDirection.Left;
                mIsMoving = true;
                mIsKeyPress = true;

                SendMoveStart(0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                mCurrentDirection = MoveDirection.Up;
                mIsMoving = true;
                mIsKeyPress = true;

                SendMoveStart(1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                mCurrentDirection = MoveDirection.Right;
                mIsMoving = true;
                mIsKeyPress = true;

                SendMoveStart(2);
            }

            if (Input.GetKey(KeyCode.S))
            {
                mCurrentDirection = MoveDirection.Down;
                mIsMoving = true;
                mIsKeyPress = true;

                SendMoveStart(3);
            }
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
            byte tempDir = 0;
            //TODO : 나중엔 제대로 멈췄을 때 Dir 값을 넣어줘야 함
            packet.Push(protocol).Push(tempDir).Push(transform.position.x).Push(transform.position.z);

            NetworkService.Instance.SendPacket(packet);
        }
    }

    private void Move()
    {
        if (mIsMoving)
        {
            if (mCurrentDirection == MoveDirection.Left)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (mCurrentDirection == MoveDirection.Up)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (mCurrentDirection == MoveDirection.Right)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (mCurrentDirection == MoveDirection.Down)
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }
    }

    private void SetDirection(byte dir)
    {
        mCurrentDirection = mMoveDirOffset[dir];
    }

    private void SendMoveStart(byte dir)
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = Protocol.PACKET_CS_PLAYER_MOVE_START;
        packet.Push(protocol).Push(dir).Push(transform.position.x).Push(transform.position.z);

        NetworkService.Instance.SendPacket(packet);
    }
}
