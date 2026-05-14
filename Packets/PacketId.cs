namespace OnChat.Protocol.Packets;

public enum PacketId : byte
{
    SuccessfulPacket,
    AuthenticationPacket,
    RegistrationPacket,
    SendMessagePacket,
    TokensPacket,
    RegistrationSuccess,
    RegistrationFailure,
    AuthenticationSuccessfulPacket,
    WrongLoginPacket,
    WrongPasswordPacket,
    WrongMailPacket,
    TokensRotationPacket,
    BadRefreshTokenPacket,
    UnauthorizedPacket,
    ReceiveMessagePacket,
    WrongIdPacket,
    PublicKeyPacket,
    GetUsersModelsPacket,
    ReceiveUsersModelsPacket
}