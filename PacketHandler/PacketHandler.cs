using OnChat.Protocol.Packets;

namespace OnChat.Protocol.PacketHandler;

public abstract class PacketHandler<T> : IPacketHandler
{
    protected abstract Task Handle(T packet, IConnection caller);
    public async Task Handle(IPacket packet, IConnection caller) => await Handle((T)packet, caller);
}