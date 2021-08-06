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

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;
        SetDirection(dir);
        transform.position = new Vector3(x, 0, z);
    }

    void Start()
    {
        mCurrentDirection = MoveDirection.Up;
        mIsMoving = false;
    }
    
    void Update()
    {
        //if(GameFramework.Instance.IsMy(mID))
        //{
        //    ProcessInput();
        //}
        
        Move();
    }

    private void SetDirection(byte dir)
    {
        switch(dir)
        {
            case 0:
                mCurrentDirection = MoveDirection.Left;
                break;

            case 1:
                mCurrentDirection = MoveDirection.Up;
                break;

            case 2:
                mCurrentDirection = MoveDirection.Right;
                break;

            case 3:
                mCurrentDirection = MoveDirection.Down;
                break;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            mCurrentDirection = MoveDirection.Left;
            mIsMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mCurrentDirection = MoveDirection.Up;
            mIsMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            mCurrentDirection = MoveDirection.Right;
            mIsMoving = true;

            NetPacket packet = NetPacket.Alloc();
            short protocol = Protocol.PACKET_CS_PLAYER_MOVE_START;
            packet.Push(protocol).Push(transform.position.x).Push(transform.position.z);

            NetworkService.Instance.SendPacket(packet);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            mCurrentDirection = MoveDirection.Down;
            mIsMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.S))
        {
            mIsMoving = false;

            NetPacket packet = NetPacket.Alloc();
            short protocol = Protocol.PACKET_CS_PLAYER_MOVE_END;
            packet.Push(protocol).Push(transform.position.x).Push(transform.position.z);

            NetworkService.Instance.SendPacket(packet);
        }
    }

    private void Move()
    {
        if(mIsMoving)
        {
            if(mCurrentDirection == MoveDirection.Left)
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

            //Debug.Log("X : " + transform.position.x + ", Z : " + transform.position.z);
        }
    }
}
