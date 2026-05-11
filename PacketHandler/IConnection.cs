using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IConnection
{
    AuthenticationState AuthenticationState { get; }
    Task Read();
    Task Write(IPacket packet);
}