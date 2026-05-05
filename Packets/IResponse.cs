namespace OnChat.Protocol.Packets;

public interface IResponse : IPacket
{
    bool IsSuccessful { get; }
    string Description { get; }
}

public interface ISuccessfulResponse : IResponse
{
    bool IResponse.IsSuccessful => true;
}

public interface IFailureResponse : IResponse
{
    bool IResponse.IsSuccessful => false;
}