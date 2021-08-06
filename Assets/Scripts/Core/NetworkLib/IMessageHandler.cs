using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageHandler
{
    void OnInit(GameFramework game);
    void OnConnect();
    void OnDisconnect();
    void OnPacketReceive(short protocol, NetPacket packet);
}