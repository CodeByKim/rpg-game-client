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

    private MoveDirection mCurrentDirection;
    private bool mIsMoving;

    void Start()
    {
        mCurrentDirection = MoveDirection.Up;
        mIsMoving = false;
    }
    
    void Update()
    {
        ProcessInput();
        Move();
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
        }
    }

    private void Move()
    {
        if(mIsMoving)
        {
            if(mCurrentDirection == MoveDirection.Left)
            {
                transform.Translate(Vector3.left * Time.deltaTime * 20);
            }

            if (mCurrentDirection == MoveDirection.Up)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 20);
            }

            if (mCurrentDirection == MoveDirection.Right)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 20);
            }

            if (mCurrentDirection == MoveDirection.Down)
            {
                transform.Translate(Vector3.back * Time.deltaTime * 20);
            }
        }
    }
}
