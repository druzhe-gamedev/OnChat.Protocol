using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IConnection
{
    Task Read();
    Task Write(IPacket packet);
}