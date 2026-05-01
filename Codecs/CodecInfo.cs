namespace OnChat.Protocol.Codecs;

public abstract class CodecInfo<T> : ICodec
{
    public BinaryProtocol Protocol { get; private set; }
    public Type HandledType => typeof(T);

    public void Init(BinaryProtocol protocol) => Protocol = protocol;
    public abstract void Encode(BinaryWriter writer, T value);
    public abstract T Decode(BinaryReader reader);
    
    void ICodec.Encode(BinaryWriter writer, object value) => Encode(writer, (T)value);
    object ICodec.Decode(BinaryReader reader) => Decode(reader)!;
}