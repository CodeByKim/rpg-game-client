using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageHandler : MonoBehaviour, IMessageHandler
{
    public Player player;

    public void OnConnect()
    {
        
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
            case Protocol.PACKET_SC_CREATE_MY_PLAYER:
                SC_CREATE_MY_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_CREATE_OTHER_PLAYER:
                SC_CREATE_OTHER_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_DELETE_MY_PLAYER:
                SC_DELETE_MY_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_DELETE_OTHER_PLAYER:
                SC_DELETE_OTHER_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_START:
                SC_PLAYER_MOVE_START(packet);
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_END:
                SC_PLAYER_MOVE_END(packet);
                break;

        }
    }

    private void Update()
    {
        
    }

    #region Packet Func

    private void SC_CREATE_MY_PLAYER(NetPacket packet)
    {

    }

    private void SC_CREATE_OTHER_PLAYER(NetPacket packet)
    {

    }

    private void SC_DELETE_MY_PLAYER(NetPacket packet)
    {

    }

    private void SC_DELETE_OTHER_PLAYER(NetPacket packet)
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

    #endregion
}