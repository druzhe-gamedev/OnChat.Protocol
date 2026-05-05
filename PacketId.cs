namespace OnChat.Protocol;

public enum PacketId : byte
{
    AuthenticationPacket,
    RegistrationPacket,
    SendMessagePacket,
    TokensPacket,
    RegistrationSuccess
}