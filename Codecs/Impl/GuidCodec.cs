namespace OnChat.Protocol.Codecs.Impl;

public class GuidCodec : CodecInfo<Guid>
{
    public override void Encode(BinaryWriter writer, Guid value)
    {
        byte[] guidBytes = value.ToByteArray();
        
        writer.Write(guidBytes);
    }

    public override Guid Decode(BinaryReader reader)
    {
        byte[] bytes = reader.ReadBytes(16);
        return new (bytes);
    }
}