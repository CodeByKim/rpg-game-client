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
        //TODO : Scene �Ѿ�� RequestCreatePlayer ��Ŷ�� ������ Player ID�� �޴°ɷ� ����.
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