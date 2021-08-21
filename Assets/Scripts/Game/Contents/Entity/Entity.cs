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

    public virtual MoveDirection Direction
    {
        get { return mDirection; }
        set { mDirection = value; }        
    }

    public virtual void Initialize(int id, byte dir, float x, float z)
    {       
        mAnimator = GetComponent<Animator>();
        mSprite = GetComponent<SpriteRenderer>();

        mID = id;
        Direction = new MoveDirection(dir);
        transform.position = new Vector3(x, 0, z);
    }

    public int GetID()
    {
        return mID;
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
