using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogic : MonoBehaviour
{
    public abstract string GetName();

    public abstract void OnInitialzie();
}

public class RPGGameLogic : GameLogic
{    
    private Dictionary<int, Player> mPlayers;
    private Dictionary<int, Monster> mMonsters;

    private PrefabController mPrefabController;

    public override void OnInitialzie()
    {
        mPlayers = new Dictionary<int, Player>();
        mMonsters = new Dictionary<int, Monster>();

        mPrefabController = GameFramework.GetController<PrefabController>();
    }

    public void CreateMyPlayer(int id, byte dir, float x, float z, bool isMy)
    {        
        Player player = mPrefabController.Create("Player").GetComponent<Player>();
        player.Initialize(id, dir, x, z);

        if(isMy)
        {
            player.name = "MyPlayer";
        }
        else
        {
            player.name = "RemotePlayer";
        }

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

    public void CreateMonster(int id, byte dir, byte type, float x, float z)
    {
        Monster monster;

        if (type == Monster.TYPE_A)
            monster = mPrefabController.Create("Monster A").GetComponent<Monster>();        
        else
            monster = mPrefabController.Create("Monster B").GetComponent<Monster>();

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

        monster.OnHit(hp);
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
        monster.OnDead();
    }

    public void SetPlayerSync(int id, float x, float z)    
    {
        Player player = GetPlayer(id);
        if (player == null)
        {
            return;
        }

        player.SyncPosition(x, z);

        if(id == GameFramework.MyID)
        {
            Debug.LogError("SYNC_POSITION");
        }
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