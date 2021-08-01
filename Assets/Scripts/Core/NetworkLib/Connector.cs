using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Connector
{
    private Socket mSocket;
    private string mIP;
    private int mPort;

    private SocketAsyncEventArgs mRecvArgs;
    private SocketAsyncEventArgs mSendArgs;

    private byte[] mRecvBuffer;
    private RingBuffer mRecvPacketRingBuffer;

    private Action mOnConnectComplete;
    private Action<NetPacket> mOnReceiveComplete;    

    private Queue<NetPacket> mSendPacketQueue;
    private Queue<NetPacket> mSentPacketQueue;
    private List<ArraySegment<byte>> mSendDataList;

    private int mIsSending;

    public Connector(string ip, int port)
    {
        mIP = ip;
        mPort = port;

        mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        mRecvBuffer = new byte[1024];
        mRecvArgs = new SocketAsyncEventArgs();        
        mRecvArgs.SetBuffer(mRecvBuffer, 0, 1024);
        mRecvArgs.Completed += OnReceive;
        
        mSendArgs = new SocketAsyncEventArgs();        
        mSendArgs.Completed += OnSent;

        mIsSending = 0;

        mRecvPacketRingBuffer = new RingBuffer(1500);
        mSendPacketQueue = new Queue<NetPacket>();
        mSentPacketQueue = new Queue<NetPacket>();
        mSendDataList = new List<ArraySegment<byte>>();
    }

    public void RegisterOnConnect(Action onComplete)
    {
        mOnConnectComplete = onComplete;
    }

    public void RegisterOnReceive(Action<NetPacket> onComplete)
    {
        mOnReceiveComplete = onComplete;
    }

    public void Connect()
    {
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(mIP), mPort);
        args.Completed += OnConnect;
        mSocket.ConnectAsync(args);        
    }

    public void SendPacket(NetPacket packet)
    {
        short length = (short)packet.GetSize();
        PacketHeader header = new PacketHeader(length);
        packet.SetHeader(header);

        mSendPacketQueue.Enqueue(packet);
        
        /*
         * ��ȯ���� 1�� ���Դٴ� ����
         * ���� Send���ε� �� Send�� �Ϸ��� �ϴ� ��
         * return �ؾ� ��
         */
        if(Interlocked.Exchange(ref mIsSending, 1) == 1)
        {
            return;
        }

        PostSend();
    }

    public void Close()
    {
        mSocket.Close();
    }

    public string GetIP()
    {
        return mIP;
    }

    private void PostReceive()
    {
        Array.Clear(mRecvBuffer, 0, 1024);
        mRecvArgs.SetBuffer(mRecvBuffer, 0, 1024);
        mSocket.ReceiveAsync(mRecvArgs);
    }

    private void PostSend()
    {                
        for (int i = 0; i < mSendPacketQueue.Count; i++)
        {
            NetPacket sendPacket = mSendPacketQueue.Dequeue();            
            mSendDataList.Add(new ArraySegment<byte>(sendPacket.GetBuffer(), 
                                                     0, 
                                                     sendPacket.GetSize() + PacketHeader.GetHeaderSize()));

            mSentPacketQueue.Enqueue(sendPacket);
        }

        mSendArgs.BufferList = mSendDataList;
        mSocket.SendAsync(mSendArgs);
    }

    private void OnConnect(object sender, SocketAsyncEventArgs e)
    {
        mOnConnectComplete();

        PostReceive();
    }

    private void OnSent(object sender, SocketAsyncEventArgs e)
    {
        mSendDataList.Clear();

        int count = mSentPacketQueue.Count;

        for (int i = 0; i < count; i++)
        {
            NetPacket packet = mSentPacketQueue.Dequeue();            
            NetPacket.Free(packet);
        }

        Interlocked.Exchange(ref mIsSending, 0);

        if (mSendPacketQueue.Count > 0)
        {
            if (Interlocked.Exchange(ref mIsSending, 1) == 1)
            {
                return;
            }

            PostSend();
        }
    }

    private void OnReceive(object sender, SocketAsyncEventArgs e)
    {
        int recvBytes = e.BytesTransferred;        
        mRecvPacketRingBuffer.Enqueue(mRecvBuffer, recvBytes);

        while (mRecvPacketRingBuffer.GetUseSize() > 0)
        {            
            /*
             * �켱 header�� ���� �� �ִ��� üũ
             * header���� payload�� ����� �������
             */
            byte[] headerBytes = mRecvPacketRingBuffer.Peek(PacketHeader.GetHeaderSize());
            if (headerBytes == null)
            {                
                break;
            }

            PacketHeader header = new PacketHeader();
            header.Parse(headerBytes);

            /*
             * header + payload, �� ������ ��Ŷ�� ���� �� �ִ��� üũ
             */            
            if(mRecvPacketRingBuffer.GetUseSize() < PacketHeader.GetHeaderSize() + header.length)
            {                
                break;
            }
            
            byte[] packetBytes = mRecvPacketRingBuffer.Dequeue(PacketHeader.GetHeaderSize() + header.length);            
            NetPacket packet = NetPacket.Alloc(packetBytes);

            mOnReceiveComplete(packet);
        }

        PostReceive();
    }
}
