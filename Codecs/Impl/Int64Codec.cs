namespace OnChat.Protocol.Codecs.Impl;

public class Int64Codec : CodecInfo<long>
{
    public override void Encode(BinaryWriter writer, long value) => writer.Write(value);

    public override long Decode(BinaryReader reader) => reader.ReadInt64();
}