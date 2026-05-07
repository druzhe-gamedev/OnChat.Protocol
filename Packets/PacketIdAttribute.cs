namespace OnChat.Protocol.Packets;

[AttributeUsage(AttributeTargets.Class)]
public class PacketIdAttribute(PacketId packetId) : Attribute
{
    public PacketId PacketId => packetId;
}