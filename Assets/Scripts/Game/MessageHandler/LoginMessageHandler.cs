using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMessageHandler : MonoBehaviour, IMessageHandler
{
    private GameFramework mGame;

    public void OnInit(GameFramework game)
    {
        mGame = game;
    }

    public void OnConnect()
    {        
        //SceneManager.LoadScene("Game");
    }

    public void OnDisconnect()
    {
        
    }

    public void OnPacketReceive(NetPacket packet)
    {
        //short protocol;
        //packet.Pop(out protocol);
    }

    private void Update()
    {
        
    }
}