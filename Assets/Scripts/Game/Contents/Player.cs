using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public float speed;

    public MoveDirection CurrentDirection
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

    public bool IsAttacking => mAnimation.IsPlaying("PlayerAttack");
    
    private Animation mAnimation;
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
        }        
    }

    public void RemoteMoveStart(byte dir, float x, float z)
    {
        string message = string.Format("[이동 시작] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        CurrentDirection = new MoveDirection(dir);
        mIsMoving = true;
    }

    public void RemoteMoveEnd(byte dir, float x, float z)
    {
        string message = string.Format("[이동 종료] 위치 차이 : X({0}), Z({1})", transform.position.x - x, transform.position.z - z);
        Debug.Log(message);

        CurrentDirection = new MoveDirection(dir);
        mIsMoving = false;
    }

    public void RemoteAttack(byte dir, float x, float z)
    {
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);

        mAnimation.CrossFade("PlayerAttack");
    }

    public void SyncPosition(byte dir, float x, float z)
    {
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);
    }

    void Start()
    {        
        mInputButtons = new List<InputButton>();

        mInputButtons.Add(new LeftMoveButton(this));
        mInputButtons.Add(new UpMoveButton(this));
        mInputButtons.Add(new RightMoveButton(this));
        mInputButtons.Add(new DownMoveButton(this));

        mAnimation = GetComponent<Animation>();
    }

    void Update()
    {        
        if(GameFramework.IsMy(mID))
        {
            ProcessInput();
        }

        Move();        
    }

    private void LateUpdate()
    {
        if (GameFramework.IsMy(mID))
        {
            Transform cameraTransform = Camera.main.transform;
            
            cameraTransform.localPosition = new Vector3(transform.position.x,
                                                        cameraTransform.localPosition.y,
                                                        transform.position.z);
        }            
    }

    private void ProcessInput()
    {
        foreach (var button in mInputButtons)
        {
            button.Poll();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();   
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
    
    private void Attack()
    {
        if (IsAttacking || mIsMoving)
        {
            return;
        }

        Protocol.SEND_PLAYER_ATTACK(CurrentDirection.GetValue(), 
                                    transform.position.x, 
                                    transform.position.z);

        mAnimation.CrossFade("PlayerAttack");
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
}
