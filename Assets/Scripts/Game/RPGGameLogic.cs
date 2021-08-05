using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameLogic : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;

    private Dictionary<int, Player> mPlayers;

    void Start()
    {
        mPlayers = new Dictionary<int, Player>();
    }

    void Update()
    {
        
    }

    public void MyCreatePlayer(short id, float x, float z)
    {        
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Initialize(id, x, z);
        mPlayers.Add(id, player);        
    }

    public void OtherCreatePlayer(short id, int x, int z)
    {
        //Player player = Instantiate(playerPrefab).GetComponent<Player>();
        //player.Initialize(id, x, z);
        //mPlayers.Add(id, player);
    }
}
