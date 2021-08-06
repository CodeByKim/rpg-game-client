using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputButton
{
    protected Player mPlayer;

    public InputButton(Player player)
    {
        mPlayer = player;
    }

    public abstract void Poll();
}

public class LeftMoveButton : InputButton
{
    public LeftMoveButton(Player player) : base(player)
    {
    }

    public override void Poll()
    {
        if (Input.GetKey(KeyCode.A))
        {
            mPlayer.OnPressMoveButton(MoveDirection.Left());
        }
    }
}

public class UpMoveButton : InputButton
{
    public UpMoveButton(Player player) : base(player)
    {
    }

    public override void Poll()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mPlayer.OnPressMoveButton(MoveDirection.Up());
        }
    }
}

public class RightMoveButton : InputButton
{
    public RightMoveButton(Player player) : base(player)
    {
    }

    public override void Poll()
    {
        if (Input.GetKey(KeyCode.D))
        {
            mPlayer.OnPressMoveButton(MoveDirection.Right());
        }
    }
}

public class DownMoveButton : InputButton
{
    public DownMoveButton(Player player) : base(player)
    {
    }

    public override void Poll()
    {
        if (Input.GetKey(KeyCode.S))
        {
            mPlayer.OnPressMoveButton(MoveDirection.Down());
        }
    }
}

