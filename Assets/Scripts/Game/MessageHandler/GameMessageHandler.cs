using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageHandler : MonoBehaviour, IMessageHandler
{        
    private RPGGameLogic mLogic;

    public void OnInit()
    {        
        mLogic = GameFramework.GetGameLogic<RPGGameLogic>();
    }

    public void OnConnect()
    {
        
    }

    public void OnDisconnect()
    {
        
    }
    
    public void OnPacketReceive(short protocol, NetPacket packet)
    {        
        switch (protocol)
        {
            case Protocol.PACKET_SC_CREATE_MY_PLAYER:
                PacketCreateMyPlayer(packet);
                break;

            case Protocol.PACKET_SC_CREATE_OTHER_PLAYER:
                PacketCreateOtherPlayer(packet);
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

            case Protocol.PACKET_SC_SYNC_POSITION:
                PacketSyncPosition(packet);
                break;            
        }
    }
     
    private void Update()
    {
        
    }

    #region Packet Func
    private void PacketCreateMyPlayer(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);
        
        GameFramework.MyID = id;
        mLogic.CreateMyPlayer(id, dir, x, z);
    }

    private void PacketCreateOtherPlayer(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);
        
        mLogic.CreateOtherPlayer(id, dir, x, z);
    }

    private void PacketDeleteOtherPlayer(NetPacket packet)
    {
        int id;
        packet.Pop(out id);        
        mLogic.DeleteOtherPlayer(id);
    }

    private void PacketPlayerMoveStart(NetPacket packet)
    {        
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);
        
        mLogic.OtherPlayerMoveStart(id, dir, x, z);
    }

    private void PacketPlayerMoveEnd(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);

        mLogic.OtherPlayerMoveEnd(id, dir, x, z);
    }

    private void PacketSyncPosition(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);
        
        mLogic.SetPlayerSync(id, dir, x, z);
    }    
    #endregion
}