using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Animator mAnimator;
    private SpriteRenderer mSprite;

    private bool mIsMoveAnimation;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
        mIsMoveAnimation = false;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 15);
            mSprite.flipX = false;

            if(!mIsMoveAnimation)
            {
                mAnimator.SetTrigger("MoveLeft");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * 15);
            mSprite.flipX = false;

            if (!mIsMoveAnimation)
            {
                mAnimator.SetTrigger("MoveUp");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 15);
            mSprite.flipX = true;

            if (!mIsMoveAnimation)
            {
                mAnimator.SetTrigger("MoveLeft");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 15);            
            mSprite.flipX = false;

            if (!mIsMoveAnimation)
            {
                mAnimator.SetTrigger("MoveDown");
                mIsMoveAnimation = true;
            }            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetBool("IsAttack", true);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) ||
           Input.GetKeyUp(KeyCode.UpArrow) ||
           Input.GetKeyUp(KeyCode.RightArrow) ||
           Input.GetKeyUp(KeyCode.DownArrow))
        {
            mIsMoveAnimation = false;
        }
    }

    public void AttackStop()
    {
        Debug.Log("Attack Stop");
        mAnimator.SetBool("IsAttack", false);
    }
}
