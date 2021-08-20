using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player�� Monster�� �����Ҹ� ����
 */
public abstract class Entity : MonoBehaviour
{
    protected int mID;
    protected int mHP;
    protected MoveDirection mDirection;

    protected Animator mAnimator;
    protected SpriteRenderer mSprite;

    public virtual void Initialize(int id, byte dir, float x, float z)
    {
        mID = id;
        mDirection = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);        

        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();
    }

    public abstract void OnHit(int hp);

    public abstract void OnDead();

    protected virtual void Update()
    {
        CalcSortingOrderForAxisZ();
    }

    private void CalcSortingOrderForAxisZ()
    {
        mSprite.sortingOrder = Mathf.RoundToInt(transform.position.z) * -1;
    }
}
