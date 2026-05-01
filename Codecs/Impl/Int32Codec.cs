namespace OnChat.Protocol.Codecs.Impl;

public class Int32Codec : CodecInfo<int>
{
    public override void Encode(BinaryWriter writer, int value) => writer.Write(value);

    public override int Decode(BinaryReader reader) => reader.ReadInt32();
}