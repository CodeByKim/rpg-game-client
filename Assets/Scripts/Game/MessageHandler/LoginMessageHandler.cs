using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMessageHandler : MonoBehaviour, IMessageHandler
{
    private LoginSceneLogic mLogic;

    public void OnInit()
    {
        mLogic = GameFramework.GetGameLogic<LoginSceneLogic>();
    }

    public void OnConnect()
    {
        mLogic.StartGame();
    }

    public void OnDisconnect()
    {
        
    }

    public void OnPacketReceive(short protocol, NetPacket packet)
    {
        
    }
}