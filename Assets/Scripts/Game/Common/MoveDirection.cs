using System;
using UnityEngine;

public struct MoveDirection
{
    public const byte MOVE_LEFT = 0;
    public const byte MOVE_UP = 1;
    public const byte MOVE_RIGHT = 2;
    public const byte MOVE_DOWN = 3;

    private byte mDirection;

    public MoveDirection(byte dir)
    {
        mDirection = dir;
    }

    public byte GetValue()
    {
        return mDirection;
    }

    public Vector3 ToVector()
    {
        switch(mDirection)
        {
            case MOVE_LEFT:
                return Vector3.left;

            case MOVE_UP:
                return Vector3.forward;

            case MOVE_RIGHT:
                return Vector3.right;

            case MOVE_DOWN:
                return Vector3.back;
        }

        return Vector3.zero;
    }

    public static MoveDirection Left()
    {
        return new MoveDirection(0);
    }

    public static MoveDirection Up()
    {
        return new MoveDirection(1);
    }

    public static MoveDirection Right()
    {
        return new MoveDirection(2);
    }

    public static MoveDirection Down()
    {
        return new MoveDirection(3);
    }
}
