using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    public static NetworkService Instance;

    private Connector mConnector;
    private IMessageHandler mHandler;
    private Queue<NetPacket> mRecvPacketQueue;
    private Queue<NetPacket> mDispatchPacketQueue;
    private Object mLock;

    public void RegisterMessageHandler(IMessageHandler handler)
    {
        mHandler = handler;
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
    }

    private void Start()
    {
        mRecvPacketQueue = new Queue<NetPacket>();
        mDispatchPacketQueue = new Queue<NetPacket>();
        mLock = new Object();
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
            mHandler.OnPacketReceive(packet);
            NetPacket.Free(packet);
        }
    }

    private void RegisterCallback()
    {
        mConnector.RegisterOnConnect(() =>
        {
            mHandler.OnConnect();            
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