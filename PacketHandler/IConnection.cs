using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public interface IConnection
{
    void Authenticate(Guid userId, string username);
    AuthenticationState AuthenticationState { get; }
    Task Read();
    Task Write(IPacket packet);
}