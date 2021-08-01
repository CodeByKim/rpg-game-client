using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageHandler
{
    void OnConnect();
    void OnDisconnect();
    void OnPacketReceive(NetPacket packet);
}