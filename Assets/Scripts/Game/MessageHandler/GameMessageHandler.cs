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

            case Protocol.PACKET_SC_PLAYER_ATTACK:
                PacketPlayerAttack(packet);
                break;

            case Protocol.PACKET_SC_CREATE_MONSTER:
                PacketCreateMonster(packet);
                break;

            case Protocol.PACKET_SC_REMOVE_MONSTER:
                PacketRemoveMonster(packet);
                break;

            case Protocol.PACKET_SC_MONSTER_HIT:
                PacketHitMonster(packet);
                break;

            case Protocol.PACKET_SC_MONSTER_DEAD:
                PacketDeadMonster(packet);
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
        mLogic.CreatePlayer(id, dir, x, z, true);
    }

    private void PacketCreateOtherPlayer(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);
        
        mLogic.CreatePlayer(id, dir, x, z, false);
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

    private void PacketPlayerAttack(NetPacket packet)
    {
        int id;
        byte dir;
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);

        mLogic.OtherPlayerAttack(id, dir, x, z);
    }

    private void PacketCreateMonster(NetPacket packet)
    {
        int id;
        byte dir;        
        float x;
        float z;
        packet.Pop(out id).Pop(out dir).Pop(out x).Pop(out z);

        mLogic.CreateMonster(id, dir, x, z);
    }

    private void PacketRemoveMonster(NetPacket packet)
    {
        int id;        
        packet.Pop(out id);

        mLogic.RemoveMonster(id);
    }

    private void PacketHitMonster(NetPacket packet)
    {
        int id;
        int hp;
        packet.Pop(out id).Pop(out hp);

        mLogic.HitMonster(id, hp);
    }

    private void PacketDeadMonster(NetPacket packet)
    {
        int id;        
        packet.Pop(out id);

        mLogic.DeadMonster(id);
    }

    private void PacketSyncPosition(NetPacket packet)
    {
        int id;        
        float x;
        float z;
        packet.Pop(out id).Pop(out x).Pop(out z);
        
        mLogic.SetPlayerSync(id, x, z);
    }    
    #endregion
}