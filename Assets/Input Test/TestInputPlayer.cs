using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputPlayer : MonoBehaviour
{    
    void Start()
    {
        
    }

    float xPos = 0;

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * Time.deltaTime * 20);
            //transform.position += new Vector3();
            //Debug.Log(transform.position.x);

            xPos += Time.deltaTime * 20;
            Debug.Log(xPos);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //transform.position = Vector3.zero;
            xPos = 0;
        }
    }
}
