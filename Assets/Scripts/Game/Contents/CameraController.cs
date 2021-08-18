using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform mTarget;

    public static CameraController Instance;
    
    private void Awake()
    {
        Instance = this;    
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    private void LateUpdate()
    {
        if(mTarget != null)
        {
            transform.localPosition = new Vector3(mTarget.position.x,
                                              transform.localPosition.y,
                                              mTarget.position.z);
        }        
    }
}