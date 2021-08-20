using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolObject
{
    void Initialize();
}

public class ObjectPool<T> where T : PoolObject
{
    private Stack<PoolObject> mStack;
    private int mUseCount;
    private GameObject mPrefab;

    public ObjectPool(int capacity, GameObject prefab)
    {
        mStack = new Stack<PoolObject>();
        mUseCount = 0;
        mPrefab = prefab;

        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = GameObject.Instantiate(mPrefab);
            mStack.Push(obj.GetComponent<PoolObject>());
        }
    }

    public T Alloc()
    {
        PoolObject poolObject;        
        if (mStack.Count == 0)
        {
            GameObject newObj = GameObject.Instantiate(mPrefab);
            poolObject = newObj.GetComponent<PoolObject>();
        }
        else
        {
            poolObject = mStack.Pop();
        }

        poolObject.Initialize();
        mUseCount += 1;

        return (T)poolObject;
    }

    public void Free(T poolObject)
    {        
        mStack.Push(poolObject);
        mUseCount -= 1;
    }

    public int GetUseCount()
    {
        return mUseCount;
    }
}
