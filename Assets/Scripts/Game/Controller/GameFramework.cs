using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{
    private static ResourcesLoader mLoader;
    private static Dictionary<System.Type, GameLogic> mLogics;
    private static Dictionary<System.Type, IFxController> mResourceControllers;

    public static int MyID;
    
    public static T GetGameLogic<T>() where T : GameLogic
    {
        System.Type type = typeof(T);
        return mLogics[type] as T;
    }

    public static T GetController<T>() where T : class, IFxController
    {
        System.Type type = typeof(T);
        return mResourceControllers[type] as T;
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

    public static void LoadResources(ResourcesLoader.ResourceType type, string path)
    {        
        mLoader.Load(type, path);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NetworkService.Instance.Initialize();

        RegisterResourceController();        

        RegisterGameLogic();

        RegisterMessageHandler();

        Debug.Log("Start Game !");        
    }

    private void RegisterResourceController()
    {
        mLoader = new ResourcesLoader();
        mResourceControllers = new Dictionary<System.Type, IFxController>();

        Transform controllerParent = transform.Find("Resource Controllers");

        for (int i = 0; i < controllerParent.childCount; i++)
        {
            IFxController controller = controllerParent.GetChild(i).GetComponent<IFxController>();
            controller.OnInitialize(mLoader);

            mResourceControllers.Add(controller.GetType(), controller);
        }
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
