using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    public static NetworkService Instance;

    private Connector mConnector;    
    private List<IMessageHandler> mHandlers;
    private Queue<NetPacket> mRecvPacketQueue;
    private Queue<NetPacket> mDispatchPacketQueue;
    private Object mLock;

    public void RegisterMessageHandler(IMessageHandler handler)
    {
        mHandlers.Add(handler);
        RegisterCallback();
    }

    public void Connect()
    {        
        mConnector.Connect();
    }

    public void SendPacket(NetPacket packet)
    {
        mConnector.SendPacket(packet);
    }

    private void Awake()
    {
        Instance = this;

        mConnector = new Connector("127.0.0.1", 6000);
        mHandlers = new List<IMessageHandler>();
        mRecvPacketQueue = new Queue<NetPacket>();
        mDispatchPacketQueue = new Queue<NetPacket>();
        mLock = new Object();
    }

    private void Start()
    {        
        
    }

    private void Update()
    {
        lock (mLock)
        {            
            if (mRecvPacketQueue.Count > 0)
            {
                Util.Swap(ref mRecvPacketQueue, ref mDispatchPacketQueue);
            }
            else
            {
                return;
            }
        }

        while (mDispatchPacketQueue.Count != 0)
        {
            NetPacket packet = mDispatchPacketQueue.Dequeue();

            switch(packet.Type)
            {
                case NetPacket.PacketType.Connect:
                    foreach (var handler in mHandlers)
                        handler.OnConnect();

                    break;

                case NetPacket.PacketType.Disconnect:
                    break;

                case NetPacket.PacketType.Receive:
                    foreach (var handler in mHandlers)
                        handler.OnPacketReceive(packet);

                    break;
            }

            NetPacket.Free(packet);
        }
    }

    private void RegisterCallback()
    {
        mConnector.RegisterOnConnect(() =>
        {            
            NetPacket packet = NetPacket.Alloc(null);
            packet.Type = NetPacket.PacketType.Connect;

            lock (mLock)
            {
                mRecvPacketQueue.Enqueue(packet);
            }
        });

        mConnector.RegisterOnReceive((NetPacket packet) =>
        {
            lock(mLock)
            {
                mRecvPacketQueue.Enqueue(packet);
            }
        });
    }

    private void OnApplicationQuit()
    {
        mConnector.Close();
    }
}