namespace OnChat.Protocol.Codecs.Impl;

public class Int16Codec : CodecInfo<short>
{
    public override void Encode(BinaryWriter writer, short value) => writer.Write(value);

    public override short Decode(BinaryReader reader) => reader.ReadInt16();
}