
public static class Protocol
{
    public const short PACKET_CS_CREATE_MY_PLAYER = 0;
    public const short PACKET_SC_CREATE_MY_PLAYER = 1;
    public const short PACKET_SC_CREATE_OTHER_PLAYER = 2;
    
    public const short PACKET_SC_DELETE_OTHER_PLAYER = 3;

    public const short PACKET_CS_PLAYER_MOVE_START = 4;
    public const short PACKET_CS_PLAYER_MOVE_END = 5;
    public const short PACKET_SC_PLAYER_MOVE_START = 6;
    public const short PACKET_SC_PLAYER_MOVE_END = 7;

    public const short PACKET_SC_CREATE_MONSTER = 8;
    public const short PACKET_SC_REMOVE_MONSTER = 9;

    public const short PACKET_CS_PLAYER_ATTACK = 10;
    public const short PACKET_SC_PLAYER_ATTACK = 11;

    public const short PACKET_SC_MONSTER_HIT = 12;
    public const short PACKET_SC_MONSTER_DEAD = 13;

    public const short PACKET_SC_SYNC_POSITION = 99;    
    public const short PACKET_CS_TELEPORT_PLAYER = 100;

    public static void SEND_CREATE_MY_PLAYER()
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = PACKET_CS_CREATE_MY_PLAYER;
        packet.Push(protocol);

        NetworkService.Instance.SendPacket(packet);
    }

    public static void SEND_PLAYER_MOVE_START(byte dir, float x, float z)
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = PACKET_CS_PLAYER_MOVE_START;
        packet.Push(protocol).Push(dir).Push(x).Push(z);

        NetworkService.Instance.SendPacket(packet);
    }

    public static void SEND_PLAYER_MOVE_END(byte dir, float x, float z)
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = PACKET_CS_PLAYER_MOVE_END;
        packet.Push(protocol).Push(dir).Push(x).Push(z);

        NetworkService.Instance.SendPacket(packet);
    }

    public static void SEND_PLAYER_ATTACK(byte dir, float x, float z)
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = PACKET_CS_PLAYER_ATTACK;
        packet.Push(protocol).Push(dir).Push(x).Push(z);

        NetworkService.Instance.SendPacket(packet);
    }

    public static void SEND_TELEPORT_PLAYER(byte dir, float x, float z)
    {
        NetPacket packet = NetPacket.Alloc();
        short protocol = Protocol.PACKET_CS_TELEPORT_PLAYER;        
        packet.Push(protocol).Push(dir).Push(x).Push(z);

        NetworkService.Instance.SendPacket(packet);
    }
}
