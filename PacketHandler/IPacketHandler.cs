using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IPacketHandler
{
    Task<IResponse> Handle(IPacket packet, IConnection caller);
}