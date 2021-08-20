using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolObject
{
    void Initialize();
}

public class ObjectPool<T> where T : PoolObject
{
    private Stack<T> mStack;
    private int mUseCount;
    private GameObject mPrefab;

    public ObjectPool(int capacity, GameObject prefab)
    {
        mStack = new Stack<T>();
        mUseCount = 0;
        mPrefab = prefab;

        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = GameObject.Instantiate(mPrefab);
            mStack.Push(obj.GetComponent<T>());
        }
    }

    public T Alloc()
    {
        T poolObject;        
        if (mStack.Count == 0)
        {            
            GameObject obj = GameObject.Instantiate(mPrefab);
            poolObject = obj.GetComponent<T>();
        }
        else
        {
            poolObject = mStack.Pop();
        }
        
        GameObject go = poolObject as GameObject;
        go.SetActive(true);

        go.GetComponent<PoolObject>().Initialize();
        mUseCount += 1;

        return poolObject;
    }

    public void Free(T poolObject)
    {
        GameObject obj = poolObject as GameObject;
        obj.SetActive(false);

        mStack.Push(poolObject);
        mUseCount -= 1;
    }

    public int GetUseCount()
    {
        return mUseCount;
    }
}
