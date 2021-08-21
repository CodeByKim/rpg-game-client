using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public static readonly int TYPE_A = 0;
    public static readonly int TYPE_B = 1;

    private RPGGameLogic mLogic;

    public override void Initialize(int id, byte dir, float x, float z)
    {
        base.Initialize(id, dir, x, z);

        mHP = 100;
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
        PlayIdleAnimation(mDirection);
    }

    public override void OnHit(int hp)
    {
        mHP = hp;

        GameFramework.GetController<SoundController>().Play("Hit");
        GameFramework.GetController<VFXController>().Play("Hit", transform.position);
    }

    public override void OnDead()
    {        
        GameFramework.GetController<SoundController>().Play("Dead");

        StartCoroutine(DeadRoutine());        
    }

    private IEnumerator DeadRoutine()
    {        
        //mAnimator.SetTrigger("Dead");
        yield return new WaitForSeconds(.1f);

        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
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
}
