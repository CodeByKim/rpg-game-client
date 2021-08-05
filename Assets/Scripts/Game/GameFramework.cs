using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{    
    [Header("Game Logic")]
    [SerializeField] private RPGGameLogic logic;

    public int ID { get; set; }    

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
    
    public RPGGameLogic GetGameLogic()
    {
        return logic;
    }

    public bool IsMy(int id)
    {
        return ID == id;
    }

    private void RegisterMessageHandler()
    {
        Transform handlerParent = transform.Find("Message Handler");

        for(int i = 0; i < handlerParent.childCount; i++)
        {
            IMessageHandler handler = handlerParent.GetChild(i).GetComponent<IMessageHandler>();
            handler.OnInit(this);

            NetworkService.Instance.RegisterMessageHandler(handler);
        }
    }
}
