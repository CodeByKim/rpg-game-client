using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogic : MonoBehaviour
{
    public abstract string GetName();
}

public class RPGGameLogic : GameLogic
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject monsterPrefab;

    private Dictionary<int, Player> mPlayers;
    private Dictionary<int, Monster> mMonsters;

    void Start()
    {
        mPlayers = new Dictionary<int, Player>();
        mMonsters = new Dictionary<int, Monster>();
    }

    void Update()
    {
        
    }

    public void Teleport(float x, float z)
    {
        Player player = GetPlayer(GameFramework.MyID);
        player.transform.position = new Vector3(x, 0, z);

        Protocol.SEND_TELEPORT_PLAYER(player.CurrentDirection.GetValue(), 
                                      x, 
                                      z);
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

    public void OtherPlayerAttack(int id, byte dir, float x, float z)
    {
        Player player = GetPlayer(id);
        if (player == null)
        {
            return;
        }

        player.RemoteAttack(dir, x, z);
    }

    public void CreateMonster(int id, byte dir, float x, float z)
    {
        Monster monster = Instantiate(monsterPrefab).GetComponent<Monster>();
        monster.Initialize(id, dir, x, z);
        mMonsters.Add(id, monster);
    }

    public void RemoveMonster(int id)
    {
        Monster monster = GetMonster(id);
        if (monster == null)
        {
            Debug.LogError("monster is null, ID : " + id);
            return;
        }

        mMonsters.Remove(id);
        Destroy(monster.gameObject);
    }

    public void HitMonster(int id, int hp)
    {
        Monster monster = GetMonster(id);
        if (monster == null)
        {
            Debug.LogError("monster is null, ID : " + id);
            return;
        }

        monster.Hit(hp);
    }

    public void DeadMonster(int id)
    {
        Monster monster = GetMonster(id);
        if (monster == null)
        {
            Debug.LogError("monster is null, ID : " + id);
            return;
        }

        mMonsters.Remove(id);
        monster.Dead();
    }

    public void SetPlayerSync(int id, float x, float z)    
    {
        Player player = GetPlayer(id);
        if (player == null)
        {
            return;
        }

        player.SyncPosition(x, z);
        Debug.LogError("SYNC_POSITION : " + id);
    }

    public override string GetName()
    {
        return "RPGGameLogic";
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

    private Monster GetMonster(int id)
    {
        Monster monster;
        if (mMonsters.TryGetValue(id, out monster))
        {
            return monster;
        }

        return null;
    }
}