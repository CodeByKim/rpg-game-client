using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static readonly int TYPE_A = 0;
    public static readonly int TYPE_B = 1;

    private int mID;
    private int mHP;

    private MoveDirection mCurrentDirection;
    private Animator mAnimator;
    private SpriteRenderer mSprite;

    public MoveDirection CurrentDirection
    {
        get
        {
            return mCurrentDirection;
        }
        set
        {
            mCurrentDirection = value;
        }
    }

    public void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;        
        CurrentDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);
        mHP = 100;

        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();

        PlayIdleAnimation(CurrentDirection);
    }

    public void Hit(int hp)
    {
        mHP = hp;
        
        var logic = GameFramework.GetGameLogic<RPGGameLogic>();
        logic.PlayHitFx(transform.position);
        SoundController.Instance.PlaySoundFx("Hit");
    }

    public void Dead()
    {        
        SoundController.Instance.PlaySoundFx("Dead");

        StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine()
    {        
        mAnimator.SetTrigger("Dead");
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private void PlayIdleAnimation(MoveDirection direction)
    {        
        switch (direction.GetValue())
        {
            case MoveDirection.MOVE_LEFT:
                mSprite.flipX = false;
                mAnimator.SetTrigger("IdleLeft");
                break;

            case MoveDirection.MOVE_UP:
                mSprite.flipX = false;
                mAnimator.SetTrigger("IdleUp");
                break;

            case MoveDirection.MOVE_RIGHT:
                mSprite.flipX = true;
                mAnimator.SetTrigger("IdleLeft");
                break;

            case MoveDirection.MOVE_DOWN:
                mSprite.flipX = false;
                mAnimator.SetTrigger("IdleDown");
                break;
        }
    }

    void Start()
    {        
    }
    
    void Update()
    {
        
    }
}
