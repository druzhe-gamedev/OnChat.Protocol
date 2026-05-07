namespace OnChat.Protocol.Codecs.Impl;

public class DatetimeOffsetCodec : CodecInfo<DateTimeOffset>
{
    public override void Encode(BinaryWriter writer, DateTimeOffset value) => writer.Write(value.ToUnixTimeSeconds());

    public override DateTimeOffset Decode(BinaryReader reader) => DateTimeOffset.FromUnixTimeSeconds(reader.ReadInt64());
}