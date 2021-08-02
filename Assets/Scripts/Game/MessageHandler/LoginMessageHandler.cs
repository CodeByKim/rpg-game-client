using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMessageHandler : MonoBehaviour, IMessageHandler
{
    bool isConnect;

    public void OnConnect()
    {        
        isConnect = true;
        SceneManager.LoadScene("Game");
    }

    public void OnDisconnect()
    {
        
    }

    public void OnPacketReceive(NetPacket packet)
    {
        
    }

    private void Update()
    {
        
    }
}