using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{
    private static ResourcesLoader mLoader;
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

    public static GameObject GetPrefab(string name)
    {
        return mLoader.GetPrefab(name);
    }

    public static AudioClip GetSound(string name)
    {
        return mLoader.GetSound(name);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NetworkService.Instance.Initialize();

        LoadResources();

        RegisterGameLogic();

        RegisterMessageHandler();

        Debug.Log("Start Game !");        
    }

    private void LoadResources()
    {
        mLoader = new ResourcesLoader();

        mLoader.Load(ResourcesLoader.ResourceType.Prefab, "Entity");
        mLoader.Load(ResourcesLoader.ResourceType.Prefab, "Fx");
        mLoader.Load(ResourcesLoader.ResourceType.Sound);
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
            logic.OnInitialzie();

            mLogics.Add(logic.GetType(), logic);            
        }        
    }
}
