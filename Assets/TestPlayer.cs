using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Animator mAnimator;
    private SpriteRenderer mSprite;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkUp");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkDown");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            mSprite.flipX = false;
            mAnimator.SetTrigger("IsWalkLeft");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            mSprite.flipX = true;
            mAnimator.SetTrigger("IsWalkLeft");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mAnimator.GetBool("IsAttack") == false)
            {
                mAnimator.SetBool("IsAttack", true);
            }                
        }
    }

    public void AttackStop()
    {
        Debug.Log("Attack Stop");
        mAnimator.SetBool("IsAttack", false);
    }
}
