namespace OnChat.Protocol.PacketHandler;

public record AuthenticationState;

public record NotAuthenticated : AuthenticationState;

public record Authenticated(Guid UserId, string Username, IConnection Connection) : AuthenticationState;