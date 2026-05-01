namespace OnChat.Protocol.Codecs;

public interface ICodec
{
    BinaryProtocol Protocol { get; }
    Type HandledType { get; }
    void Init(BinaryProtocol protocol);
    void Encode(BinaryWriter writer, object value);
    object Decode(BinaryReader reader);
}