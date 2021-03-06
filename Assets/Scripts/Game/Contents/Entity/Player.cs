using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{    
    public float speed;
    
    private RPGGameLogic mLogic;
    private List<InputButton> mInputButtons;
    private bool IsAttacking => mAnimator.GetBool("IsAttack");
    private bool mIsMoving;

    public override MoveDirection Direction 
    { 
        get => base.Direction; 
        set
        {
            mDirection = value;

            switch (mDirection.GetValue())
            {
                case MoveDirection.MOVE_LEFT:
                    mAnimator.SetTrigger("Left");
                    break;

                case MoveDirection.MOVE_UP:
                    mAnimator.SetTrigger("Up");
                    break;

                case MoveDirection.MOVE_RIGHT:
                    mAnimator.SetTrigger("Right");
                    break;

                case MoveDirection.MOVE_DOWN:
                    mAnimator.SetTrigger("Down");
                    break;
            }
        }
    }

    public override void Initialize(int id, byte dir, float x, float z)
    {
        base.Initialize(id, dir, x, z);
        
        mHP = 1000;        
        mIsMoving = false;
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
        
        mInputButtons = new List<InputButton>();

        mInputButtons.Add(new LeftMoveButton(this));
        mInputButtons.Add(new UpMoveButton(this));
        mInputButtons.Add(new RightMoveButton(this));
        mInputButtons.Add(new DownMoveButton(this));
        mInputButtons.Add(new AttackButton(this));

        if (GameFramework.IsMy(mID))
        {
            CameraController.Instance.SetTarget(transform);
        }
    }
    
    public void OnPressMoveButton(MoveDirection direction)
    {
        if(IsAttacking)
        {
            return;
        }

        if(!mIsMoving)
        {
            Direction = direction;
            mIsMoving = true;

            Protocol.SEND_PLAYER_MOVE_START(Direction.GetValue(), 
                                            transform.position.x, 
                                            transform.position.z);

            PlayMoveAnimation(Direction);
        }        
    }

    public void OnPressAttackButton()
    {       
        if (IsAttacking || mIsMoving)
        {
            return;
        }

        Protocol.SEND_PLAYER_ATTACK(Direction.GetValue(),
                                    transform.position.x,
                                    transform.position.z);

        GameFramework.GetController<SoundController>().Play("Attack");        
        mAnimator.SetBool("IsAttack", true);        
    }

    public void RemoteMoveStart(byte dir, float x, float z)
    {
        Direction = new MoveDirection(dir);
        PlayMoveAnimation(Direction);
        mIsMoving = true;
    }

    public void RemoteMoveEnd(byte dir, float x, float z)
    {
        Direction = new MoveDirection(dir);
        mIsMoving = false;
        mAnimator.SetBool("IsMove", false);
    }

    public void RemoteAttack(byte dir, float x, float z)
    {
        Direction = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);

        GameFramework.GetController<SoundController>().Play("Attack");        
        mAnimator.SetBool("IsAttack", true);
    }

    public void SyncPosition(float x, float z)    
    {        
        transform.position = new Vector3(x, 0, z);
    }

    protected override void Update()
    {
        base.Update();

        if (GameFramework.IsMy(mID))
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

        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow))
        {
            mIsMoving = false;
            mAnimator.SetBool("IsMove", false);

            Protocol.SEND_PLAYER_MOVE_END(Direction.GetValue(), 
                                          transform.position.x, 
                                          transform.position.z);
        }
    }
    
    private void Move()
    {
        if (mIsMoving)
        {
            Vector3 moveDir = mDirection.ToVector();
            Vector3 offset = moveDir * Time.deltaTime * speed;
            Vector3 result = transform.position + offset;

            if(result.x < 0 || result.x > 2000 || result.z < 0 || result.z > 2000)
            {
                /*
                 * STOP ?????? ???????? ?????? Update???? ???? ?????? ?????? ???? ????
                 * ???? ???? ?????? ???????? ??
                 */                
                return;
            }

            transform.position = result;
        }
    }

    private void PlayMoveAnimation(MoveDirection direction)
    {
        mAnimator.SetBool("IsMove", true);

        switch (direction.GetValue())
        {
            case MoveDirection.MOVE_LEFT:                
                mAnimator.SetTrigger("Left");
                break;

            case MoveDirection.MOVE_UP:                
                mAnimator.SetTrigger("Up");
                break;

            case MoveDirection.MOVE_RIGHT:
                mAnimator.SetTrigger("Right");
                break;

            case MoveDirection.MOVE_DOWN:
                mAnimator.SetTrigger("Down");
                break;
        }
    }

    public void AttackStop()
    {        
        mAnimator.SetBool("IsAttack", false);
    }

    public override void OnHit(int hp)
    {
        // ???? ???? ????
    }

    public override void OnDead()
    {
        // ???? ???? ????
    }
}
