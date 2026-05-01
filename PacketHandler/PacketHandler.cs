namespace OnChat.Protocol.PacketHandler;

public abstract class PacketHandler<T> : IPacketHandler
{
    public abstract PacketId PacketId { get; }

    protected abstract void Handle(T packet);
    public void Handle(object packet) => Handle((T)packet);
}