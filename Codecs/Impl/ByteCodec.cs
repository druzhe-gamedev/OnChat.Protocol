namespace OnChat.Protocol.Codecs.Impl;

public class ByteCodec : CodecInfo<byte>
{
    public override void Encode(BinaryWriter writer, byte value) => writer.Write(value);

    public override byte Decode(BinaryReader reader) => reader.ReadByte();
}