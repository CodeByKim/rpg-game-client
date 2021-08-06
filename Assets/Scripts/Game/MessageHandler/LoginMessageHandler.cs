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
        //TODO : Scene 넘어가고 RequestCreatePlayer 패킷을 던져서 Player ID를 받는걸로 하자.
        //SceneManager.LoadScene("Game");
    }

    public void OnDisconnect()
    {
        
    }

    public void OnPacketReceive(short protocol, NetPacket packet)
    {
        
    }

    private void Update()
    {
        
    }
}