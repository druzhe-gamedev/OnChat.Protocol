using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IPacketHandler
{
    PacketId PacketId { get; }
    Task Handle(IPacket packet);
}