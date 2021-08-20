using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundProps : MonoBehaviour
{
    private SpriteRenderer mSprite;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sortingOrder = Mathf.RoundToInt(transform.position.z) * -1;
    }
}
