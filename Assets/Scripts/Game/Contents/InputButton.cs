using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputButton
{
    protected Player mPlayer;
    protected Animator mAnimator;
    protected SpriteRenderer mSprite;

    public InputButton(Player player)
    {
        mPlayer = player;
        mAnimator = mPlayer.GetComponent<Animator>();
        mSprite = mPlayer.GetComponent<SpriteRenderer>();
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkLeft");

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkUp");

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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mSprite.flipX = true;
            mAnimator.SetTrigger("IsWalkLeft");

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
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkDown");

            mPlayer.OnPressMoveButton(MoveDirection.Down());
        }
    }
}

public class AttackButton : InputButton
{
    public AttackButton(Player player) : base(player)
    {
    }

    public override void Poll()
    {
        if (Input.GetKey(KeyCode.Space))
        {            
            mPlayer.OnPressAttackButton();
        }
    }
}

