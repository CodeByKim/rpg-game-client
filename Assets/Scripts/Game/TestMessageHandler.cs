using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessageHandler : MonoBehaviour, IMessageHandler
{
    public void OnConnect()
    {
        Debug.Log("On Connect!");
    }

    public void OnDisconnect()
    {
        
    }

    /*
     * NetworkService의 업데이트에서 계속 호출
     */ 
    public void OnPacketReceive(NetPacket packet)
    {
        int testData1;
        int testData2;
        int testData3;

        packet.Pop(out testData1).Pop(out testData2).Pop(out testData3);
        Debug.Log(String.Format("Packet Receive : {0},{1},{2}", testData1, testData2, testData3));
    }

    private void Start()
    {
        Debug.Log("Start TestMessageHandler");

        NetworkService.Instance.RegisterMessageHandler(this);
        NetworkService.Instance.Connect();
    }
    
    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            NetPacket packet = NetPacket.Alloc();

            int testData1 = 10;
            int testData2 = 20;
            int testData3 = 30;

            packet.Push(testData1).Push(testData2).Push(testData3);            
            NetworkService.Instance.SendPacket(packet);
        }
    }
}
