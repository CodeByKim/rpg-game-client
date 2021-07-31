using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PROTOCOL
{
    public const short CS_PLAYER_MOVE_START = 0;
    public const short CS_PLAYER_MOVE_END = 1;
    public const short SC_PLAYER_MOVE_START = 2;
    public const short SC_PLAYER_MOVE_END = 3;
}

public class TestMessageHandler : MonoBehaviour, IMessageHandler
{
    public Player player;

    public void OnConnect()
    {
        Debug.Log("On Connect!");
    }

    public void OnDisconnect()
    {
        
    }
    
    public void OnPacketReceive(NetPacket packet)
    {
        short protocol;
        packet.Pop(out protocol);

        switch (protocol)
        {
            case PROTOCOL.SC_PLAYER_MOVE_START:
                SC_PLAYER_MOVE_START(packet);
                break;

            case PROTOCOL.SC_PLAYER_MOVE_END:
                SC_PLAYER_MOVE_END(packet);
                break;
        }
    }

    private void Start()
    {
        Debug.Log("Start TestMessageHandler");

        NetworkService.Instance.RegisterMessageHandler(this);
        NetworkService.Instance.Connect();
    }
    
    private void Update()
    {
        
    }

    private void SC_PLAYER_MOVE_START(NetPacket packet)
    {

    }

    private void SC_PLAYER_MOVE_END(NetPacket packet)
    {
        float x;
        float z;

        packet.Pop(out x).Pop(out z);

        Debug.Log(string.Format("server x : {0}, local x : {1}", x, player.transform.position.x));
    }
}