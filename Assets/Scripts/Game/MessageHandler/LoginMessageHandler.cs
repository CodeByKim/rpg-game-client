using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMessageHandler : MonoBehaviour, IMessageHandler
{
    public void OnInit()
    {
        
    }

    public void OnConnect()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.LoadScene("Game");        
    }

    //TODO : �� �κе� �� ó���ϰ� �ʹ�..
    private void OnSceneChanged(Scene prevScene, Scene changedScene)
    {        
        if(changedScene.name == "Game")
        {
            Protocol.SEND_CREATE_MY_PLAYER();
        }
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