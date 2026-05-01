using System.Collections.Frozen;
using System.Reflection;
using OnChat.Protocol.Codecs;
using OnChat.Protocol.Codecs.Impl;
using OnChat.Protocol.PacketHandler;
using OnChat.Protocol.Types;

namespace OnChat.Protocol;

public class BinaryProtocol
{
    public readonly FrozenDictionary<PacketId, IPacketHandler> Handlers;
    public readonly FrozenDictionary<PacketId, Type> Packets;
    private readonly FrozenDictionary<Type, ICodec> _codecs;

    public BinaryProtocol(params Assembly[] packetsAssemblies)
    {
        Handlers = GetHandlers();
        _codecs = GetCodecs();
        Packets = GetPackets(packetsAssemblies);
    }

    public ICodec GetCodec(Type type)
    {
        // ReSharper disable once InvertIf
        if (!_codecs.TryGetValue(type, out ICodec? codec))
        {
            // todo into factory
            PropertyInfo[] properties = type.GetProperties();
            StructCodec structCodec = new(type, properties);
            structCodec.Init(this);
            return structCodec;
        }

        return codec;
    }

    private static FrozenDictionary<PacketId, IPacketHandler> GetHandlers() =>
        // todo check
        Assembly.GetEntryAssembly()!.DefinedTypes
                .Where(type => type.IsConcrete && type.GetInterface(nameof(IPacketHandler)) != null)
                .Select(type => (IPacketHandler)Activator.CreateInstance(type)!)
                .ToDictionary(type => type.PacketId)
                .ToFrozenDictionary();

    private FrozenDictionary<Type, ICodec> GetCodecs() =>
        Assembly.GetExecutingAssembly().DefinedTypes
                .Where(type => type.IsConcrete && type.GetInterfaces().Any(i => i == typeof(ICodec)))
                .Where(type => type.CustomAttributes.All(a => a.AttributeType != typeof(FactoryCodecAttribute)))
                .Select(type =>
                    {
                        ICodec codec = (ICodec)Activator.CreateInstance(type)!;
                        codec.Init(this);
                        return codec;
                    }
                )
                .ToDictionary(codec => codec.HandledType)
                .ToFrozenDictionary();

    private static FrozenDictionary<PacketId, Type> GetPackets(params Assembly[] packetAssemblies) =>
        packetAssemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsConcrete && type.GetCustomAttribute<PacketIdAttribute>() != null)
                .ToDictionary(type => type.GetCustomAttribute<PacketIdAttribute>()!.PacketId)
                .ToFrozenDictionary();
}