using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public float speed;

    public MoveDirection CurrentDirection { get; set; }    
    
    public bool IsAttacking => mAnimator.GetBool("IsAttack");
    
    private Animator mAnimator;
    private SpriteRenderer mSprite;
    private RPGGameLogic mLogic;
    private MoveDirection mCurrentDirection;
    private int mID;
    private bool mIsMoving;
    
    private List<InputButton> mInputButtons;

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;
        mIsMoving = false;
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);

        if(GameFramework.IsMy(mID))
        {
            CameraController.Instance.SetTarget(transform);
        }

        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
    }
    
    public void OnPressMoveButton(MoveDirection direction)
    {
        if(IsAttacking)
        {
            return;
        }

        if(!mIsMoving)
        {
            CurrentDirection = direction;
            mIsMoving = true;

            Protocol.SEND_PLAYER_MOVE_START(CurrentDirection.GetValue(), 
                                            transform.position.x, 
                                            transform.position.z);

            PlayMoveAnimation(CurrentDirection);
        }        
    }

    public void OnPressAttackButton()
    {       
        if (IsAttacking || mIsMoving)
        {
            return;
        }

        Protocol.SEND_PLAYER_ATTACK(CurrentDirection.GetValue(),
                                    transform.position.x,
                                    transform.position.z);

        SoundController.Instance.PlaySoundFx("Attack");
        mAnimator.SetBool("IsAttack", true);        
    }

    public void RemoteMoveStart(byte dir, float x, float z)
    {    
        CurrentDirection = new MoveDirection(dir);
        PlayMoveAnimation(CurrentDirection);
        mIsMoving = true;
    }

    public void RemoteMoveEnd(byte dir, float x, float z)
    {        
        CurrentDirection = new MoveDirection(dir);
        mIsMoving = false;
    }

    public void RemoteAttack(byte dir, float x, float z)
    {
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);

        SoundController.Instance.PlaySoundFx("Attack");
        mAnimator.SetBool("IsAttack", true);
    }

    public void SyncPosition(float x, float z)    
    {        
        transform.position = new Vector3(x, 0, z);
    }

    void Start()
    {        
        mInputButtons = new List<InputButton>();

        mInputButtons.Add(new LeftMoveButton(this));
        mInputButtons.Add(new UpMoveButton(this));
        mInputButtons.Add(new RightMoveButton(this));
        mInputButtons.Add(new DownMoveButton(this));
        mInputButtons.Add(new AttackButton(this));
        
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow))
        {
            mIsMoving = false;

            Protocol.SEND_PLAYER_MOVE_END(CurrentDirection.GetValue(), 
                                          transform.position.x, 
                                          transform.position.z);
        }
    }
    
    private void Move()
    {
        if (mIsMoving)
        {
            Vector3 moveDir = CurrentDirection.ToVector();
            Vector3 offset = moveDir * Time.deltaTime * speed;
            Vector3 result = transform.position + offset;

            if(result.x < 0 || result.x > 2000 || result.z < 0 || result.z > 2000)
            {
                /*
                 * STOP 패킷을 보내려고 했는데 Update에서 계속 패킷을 보내는 문제 발생
                 * 그냥 벽에 걸리는 로직으로 감
                 */                
                return;
            }

            transform.position = result;
        }
    }

    private void PlayMoveAnimation(MoveDirection direction)
    {
        switch (direction.GetValue())
        {
            case MoveDirection.MOVE_LEFT:
                mSprite.flipX = false;
                mAnimator.SetTrigger("MoveLeft");
                break;

            case MoveDirection.MOVE_UP:
                mSprite.flipX = false;
                mAnimator.SetTrigger("MoveUp");
                break;

            case MoveDirection.MOVE_RIGHT:
                mSprite.flipX = true;
                mAnimator.SetTrigger("MoveLeft");
                break;

            case MoveDirection.MOVE_DOWN:
                mSprite.flipX = false;
                mAnimator.SetTrigger("MoveDown");
                break;
        }
    }

    public void AttackStop()
    {        
        mAnimator.SetBool("IsAttack", false);
    }
}
