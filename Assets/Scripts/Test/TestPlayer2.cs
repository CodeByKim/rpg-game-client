using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2 : MonoBehaviour
{
    private Animator mAnimator;
    private SpriteRenderer mSprite;

    private bool mIsMoveAnimation;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();

        mAnimator.SetBool("IsIdle", true);
        mAnimator.SetTrigger("Down");

        mIsMoveAnimation = false;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 15);

            if (!mIsMoveAnimation)
            {
                mAnimator.SetBool("IsMove", true);                
                mAnimator.SetTrigger("Left");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * 15);

            if (!mIsMoveAnimation)
            {
                mAnimator.SetBool("IsMove", true);                
                mAnimator.SetTrigger("Up");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 15);

            if (!mIsMoveAnimation)
            {
                mAnimator.SetBool("IsMove", true);                
                mAnimator.SetTrigger("Right");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 15);

            if (!mIsMoveAnimation)
            {
                mAnimator.SetBool("IsMove", true);                
                mAnimator.SetTrigger("Down");
                mIsMoveAnimation = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
           Input.GetKeyUp(KeyCode.UpArrow) ||
           Input.GetKeyUp(KeyCode.RightArrow) ||
           Input.GetKeyUp(KeyCode.DownArrow))
        {
            mIsMoveAnimation = false;
            mAnimator.SetBool("IsMove", false);            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetBool("IsAttack", true);
        }

    }

    public void AttackStop()
    {
        Debug.Log("Attack Stop");
        mAnimator.SetBool("IsAttack", false);
    }
}
