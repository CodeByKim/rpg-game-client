using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int mID;
    private int mHP;

    private MoveDirection mCurrentDirection;
    private Animation mAnimation;

    public MoveDirection CurrentDirection
    {
        get
        {
            return mCurrentDirection;
        }
        set
        {
            mCurrentDirection = value;

            switch (mCurrentDirection.GetValue())
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

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;        
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);
        mHP = 100;
    }

    public void Hit(int hp)
    {
        mHP = hp;
        mAnimation.CrossFade("MonsterHit");
    }

    void Start()
    {
        mAnimation = GetComponent<Animation>();
    }
    
    void Update()
    {
        
    }
}
