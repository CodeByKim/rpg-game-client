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
        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.LoadScene("Game");        
    }

    private void OnSceneChanged(Scene prevScene, Scene changedScene)
    {        
        if(changedScene.name == "Game")
        {
            NetPacket packet = NetPacket.Alloc();
            short protocol = Protocol.PACKET_CS_CREATE_MY_PLAYER;
            packet.Push(protocol);

            NetworkService.Instance.SendPacket(packet);
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