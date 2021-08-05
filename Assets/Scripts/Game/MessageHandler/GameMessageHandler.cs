using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageHandler : MonoBehaviour, IMessageHandler
{    
    private GameFramework mGameFramework;
    private RPGGameLogic mLogic;

    public void OnInit(GameFramework game)
    {
        mGameFramework = game;
        mLogic = mGameFramework.GetGameLogic();
    }

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
                PacketCreateMyPlayer(packet);
                break;

            case Protocol.PACKET_SC_CREATE_OTHER_PLAYER:
                PacketCreateOtherPlayer(packet);
                break;

            case Protocol.PACKET_SC_DELETE_MY_PLAYER:
                PacketDeleteMyPlayer(packet);
                break;

            case Protocol.PACKET_SC_DELETE_OTHER_PLAYER:
                PacketDeleteOtherPlayer(packet);
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_START:                
                PacketPlayerMoveStart(packet);
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_END:                
                PacketPlayerMoveEnd(packet);
                break; 
        }
    }
     
    private void Update()
    {
        
    }

    #region Packet Func
    private void PacketCreateMyPlayer(NetPacket packet)
    {
        short id;
        float x;
        float z;
        packet.Pop(out id).Pop(out x).Pop(out z);

        Debug.Log(x);
        Debug.Log(z);

        mGameFramework.ID = id;
        mLogic.MyCreatePlayer(id, x, z);
    }

    private void PacketCreateOtherPlayer(NetPacket packet)
    {

    }

    private void PacketDeleteMyPlayer(NetPacket packet)
    {

    }

    private void PacketDeleteOtherPlayer(NetPacket packet)
    {

    }

    private void PacketPlayerMoveStart(NetPacket packet)
    {

    }

    private void PacketPlayerMoveEnd(NetPacket packet)
    {
        
    }
    #endregion
}