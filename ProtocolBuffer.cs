using OnChat.Protocol.Exceptions;

namespace OnChat.Protocol;

public class ProtocolBuffer(MemoryStream stream) : IDisposable
{
    public BinaryReader Reader { get; } = new(stream);
    public BinaryWriter Writer { get; } = new(stream);
    public MemoryStream Stream => stream;
    private bool _disposed;

    public async Task WrapPacket(BinaryWriter writer)
    {
        writer.Write(PacketConstants.Signature);
        writer.Write((int)stream.Length);

        Stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(writer.BaseStream);
    }
    
    public static async Task<ProtocolBuffer> CreateFromReader(BinaryReader reader)
    {
        (byte first, byte second) = (reader.ReadByte(), reader.ReadByte());
        
        if (first != PacketConstants.Signature[0] || second != PacketConstants.Signature[1])
            throw new BadPacketHeaderSignatureException("Bad packet header signature");

        int length = reader.ReadInt32();
        byte[] payload = new byte[length];
        await reader.BaseStream.ReadExactlyAsync(payload, 0, length);
        
        MemoryStream ms = new(payload);
        ms.Seek(0, SeekOrigin.Begin);
        
        ProtocolBuffer buffer = new(ms);

        return buffer;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing || _disposed) return;
        
        stream.Dispose();
        Reader.Dispose();
        Writer.Dispose();
        _disposed = true;
    }
}