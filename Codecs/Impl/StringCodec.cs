namespace OnChat.Protocol.Codecs.Impl;

public class StringCodec : CodecInfo<string>
{
    public override void Encode(BinaryWriter writer, string value) => writer.Write(value);

    public override string Decode(BinaryReader reader) => reader.ReadString();
}