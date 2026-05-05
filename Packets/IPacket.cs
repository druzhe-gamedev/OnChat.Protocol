namespace OnChat.Protocol.Packets;

public interface IPacket
{
    Guid CorrelationId { get; }
}