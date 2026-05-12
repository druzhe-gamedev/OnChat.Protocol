namespace OnChat.Protocol.Packets;

public enum PacketId : byte
{
    AuthenticationPacket,
    RegistrationPacket,
    SendMessagePacket,
    TokensPacket,
    RegistrationSuccess,
    RegistrationFailure,
    AuthenticationSuccessfulPacket,
    WrongLoginPacket,
    WrongPasswordPacket,
    TokensRotationPacket,
    BadRefreshTokenPacket,
    UnauthorizedPacket,
    ReceiveMessagePacket,
    WrongIdPacket,
    PublicKeyPacket,
    GetUsersModelsPacket,
    ReceiveUsersModelsPacket
}