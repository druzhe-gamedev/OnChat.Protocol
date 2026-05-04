using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IPacketHandler
{
    Task Handle(IPacket packet, IConnection caller);
}