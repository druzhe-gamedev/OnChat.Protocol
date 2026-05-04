using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public abstract class PacketHandler<T> : IPacketHandler
{
    protected abstract Task Handle(T packet);
    public async Task Handle(IPacket packet) => await Handle((T)packet);
}