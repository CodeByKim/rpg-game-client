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

    //TODO : 이 부분도 좀 처리하고 싶다..
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