using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameLogic : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;

    public static RPGGameLogic Instance;

    private Dictionary<int, Player> mPlayers;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mPlayers = new Dictionary<int, Player>();
    }

    void Update()
    {
        
    }

    public void Teleport(float x, float z)
    {
        Player player = GetPlayer(GameFramework.MyID);

        NetPacket packet = NetPacket.Alloc();
        short protocol = Protocol.PACKET_CS_TELEPORT_PLAYER;
        byte dir = player.CurrentDirection.GetValue();

        packet.Push(protocol).Push(dir).Push(x).Push(z);        
        NetworkService.Instance.SendPacket(packet);     
        
        player.transform.position = new Vector3(x, 0, z);
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

    public void SetPlayerSync(int id, byte dir, float x, float z)
    {
        Player player = GetPlayer(id);
        if (player == null)
        {
            return;
        }

        player.SyncPosition(dir, x, z);
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