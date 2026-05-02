using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public abstract class PacketHandler<T> : IPacketHandler
{
    public abstract PacketId PacketId { get; }

    protected abstract Task Handle(T packet);
    public async Task Handle(IPacket packet) => await Handle((T)packet);
}