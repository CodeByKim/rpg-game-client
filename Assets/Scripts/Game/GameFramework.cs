using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RegisterMessageHandler();
    }
    
    void Update()
    {
        
    }

    private void RegisterMessageHandler()
    {
        Transform handlerParent = transform.Find("Message Handler");

        for(int i = 0; i < handlerParent.childCount; i++)
        {
            IMessageHandler handler = handlerParent.GetChild(i).GetComponent<IMessageHandler>();            
            NetworkService.Instance.RegisterMessageHandler(handler);
        }
    }
}
