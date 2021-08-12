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

    public void CreateMyPlayer(int id, byte dir, float x, float z)
    {        
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Initialize(id, dir, x, z);
        mPlayers.Add(id, player);
    }

    public void CreateOtherPlayer(int id, byte dir, float x, float z)
    {
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Initialize(id, dir, x, z);
        mPlayers.Add(id, player);
    }

    public void DeleteOtherPlayer(int id)
    {
        Player player = GetPlayer(id);
        if(player == null)
        {
            Debug.LogError("player is null, ID : " + id);
            return;
        }

        mPlayers.Remove(id);
        Destroy(player.gameObject);
    }

    public void OtherPlayerMoveStart(int id, byte dir, float x, float z)
    {
        Player player = GetPlayer(id);
        if(player == null)
        {
            return;
        }

        player.RemoteMoveStart(dir, x, z);
    }

    public void OtherPlayerMoveEnd(int id, byte dir, float x, float z)
    {
        Player player = GetPlayer(id);
        if (player == null)
        {
            return;
        }

        player.RemoteMoveEnd(dir, x, z);
    }

    private Player GetPlayer(int id)
    {
        Player player;
        if(mPlayers.TryGetValue(id, out player))
        {
            return player;
        }

        return null;
    }
}