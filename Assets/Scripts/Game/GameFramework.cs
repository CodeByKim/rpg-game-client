using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFramework : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;

    public static GameFramework Instance;

    public int ID { get; set; }
    private Dictionary<int, Player> mPlayers;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {        
        RegisterMessageHandler();
    }
    
    void Update()
    {
        
    }
    
    public bool IsMy(int id)
    {
        return ID == id;
    }

    public void MyCreatePlayer(short id, int x, int z)
    {
        ID = id;

        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Initialize(id, x, z);
        mPlayers.Add(id, player);
    }

    public void OtherCreatePlayer(short id, int x, int z)
    {
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Initialize(id, x, z);
        mPlayers.Add(id, player);
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
