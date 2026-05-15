namespace OnChat.Protocol.Packets;

public enum PacketId : byte
{
    SuccessfulPacket,
    AuthenticationPacket,
    RegistrationPacket,
    SendMessagePacket,
    LoadMessagesPacket,
    TokensPacket,
    RegistrationSuccess,
    RegistrationFailure,
    AuthenticationSuccessfulPacket,
    WrongLoginPacket,
    WrongPasswordPacket,
    WrongMailPacket,
    WrongAgePacket,
    TokensRotationPacket,
    BadRefreshTokenPacket,
    UnauthorizedPacket,
    ReceiveMessagePacket,
    WrongIdPacket,
    PublicKeyPacket,
    GetUsersModelsPacket,
    ReceiveUsersModelsPacket,
    ValidationFailurePacket,
    WrongLimitsPacket
}