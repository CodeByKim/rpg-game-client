using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer mSprite;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sortingOrder = Mathf.RoundToInt(transform.position.z) * -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
