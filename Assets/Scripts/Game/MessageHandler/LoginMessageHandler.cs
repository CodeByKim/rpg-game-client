using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMessageHandler : MonoBehaviour, IMessageHandler
{    
    public void OnConnect()
    {                
        SceneManager.LoadScene("Game");
    }

    public void OnDisconnect()
    {
        
    }

    public void OnPacketReceive(NetPacket packet)
    {
        short protocol;
        packet.Pop(out protocol);

        switch(protocol)
        {
            case Protocol.PACKET_SC_CREATE_MY_PLAYER:
                SC_CREATE_MY_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_CREATE_OTHER_PLAYER:
                SC_CREATE_OTHER_PLAYER(packet);
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_START:
                break;

            case Protocol.PACKET_SC_PLAYER_MOVE_END:
                break;
        }
    }

    private void Update()
    {
        
    }

    private void SC_CREATE_MY_PLAYER(NetPacket packet)
    {

    }

    private void SC_CREATE_OTHER_PLAYER(NetPacket packet)
    {

    }
}