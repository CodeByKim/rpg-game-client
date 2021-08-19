using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{    
    private static Dictionary<System.Type, GameLogic> mLogics;

    public static int MyID;
    
    public static T GetGameLogic<T>() where T : GameLogic
    {
        System.Type type = typeof(T);
        return mLogics[type] as T;
    }

    public static bool IsMy(int id)
    {
        return MyID == id;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NetworkService.Instance.Initialize();

        RegisterGameLogic();

        RegisterMessageHandler();

        Debug.Log("Start Game");
    }

    private void RegisterMessageHandler()
    {
        Transform handlerParent = transform.Find("Message Handler");

        for(int i = 0; i < handlerParent.childCount; i++)
        {
            IMessageHandler handler = handlerParent.GetChild(i).GetComponent<IMessageHandler>();
            handler.OnInit();

            NetworkService.Instance.RegisterMessageHandler(handler);
        }
    }

    private void RegisterGameLogic()
    {
        mLogics = new Dictionary<System.Type, GameLogic>();
        Transform logicParent = transform.Find("Game Logics");

        for (int i = 0; i < logicParent.childCount; i++)
        {
            GameLogic logic = logicParent.GetChild(i).GetComponent<GameLogic>();

            mLogics.Add(logic.GetType(), logic);            
        }        
    }
}
