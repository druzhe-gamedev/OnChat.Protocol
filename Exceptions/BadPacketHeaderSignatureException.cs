namespace OnChat.Protocol.Exceptions;

internal class BadPacketHeaderSignatureException(string? message) : Exception(message);