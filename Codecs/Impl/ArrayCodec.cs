namespace OnChat.Protocol.Codecs.Impl;

[FactoryCodec]
public class ArrayCodec<T>(ICodec elementCodec) : CodecInfo<T[]>
{
    public override void Encode(BinaryWriter writer, T[] value)
    {
        writer.Write(value.Length);

        foreach (T element in value)
            elementCodec.Encode(writer, element!);
        
    }

    public override T[] Decode(BinaryReader reader)
    {
        int length = reader.ReadInt32();
        T[] array = new T[length];

        for (int i = 0; i < length; i++)
            array[i] = (T)elementCodec.Decode(reader);

        return array;
    }
}