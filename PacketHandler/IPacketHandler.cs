namespace OnChat.Protocol.PacketHandler;

public interface IPacketHandler
{
    PacketId PacketId { get; }
    void Handle(object packet);
}