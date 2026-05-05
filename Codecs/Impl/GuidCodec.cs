namespace OnChat.Protocol.Codecs.Impl;

public class GuidCodec : CodecInfo<Guid>
{
    public override void Encode(BinaryWriter writer, Guid value)
    {
        byte[] guidBytes = value.ToByteArray();
        
        writer.Write(16);
        writer.Write(guidBytes);
    }

    public override Guid Decode(BinaryReader reader)
    {
        Guid guid = Guid.Parse(reader.ReadBytes(16));
        return guid;
    }
}