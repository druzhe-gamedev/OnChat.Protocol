using System.Collections.Frozen;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OnChat.Protocol.Codecs;
using OnChat.Protocol.Codecs.Impl;
using OnChat.Protocol.PacketHandler;
using OnChat.Protocol.Packets;
using OnChat.Protocol.Types;

namespace OnChat.Protocol;

public class BinaryProtocol
{
    public readonly FrozenDictionary<Type, IPacketHandler> Handlers;
    public readonly FrozenDictionary<PacketId, Type> Packets;
    private readonly FrozenDictionary<Type, ICodec> _codecs;
    private readonly IServiceProvider _serviceProvider;

    public BinaryProtocol(IServiceProvider serviceProvider, params Assembly[] packetsAssemblies)
    {
        _serviceProvider = serviceProvider;
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
            ConstructorInfo? ctor = type.GetConstructors()
                                       .FirstOrDefault(ctor => ctor.GetParameters().Length > 0);
            StructCodec structCodec = new(type, properties, ctor);
            structCodec.Init(this);
            return structCodec;
        }

        return codec;
    }

    private FrozenDictionary<Type, IPacketHandler> GetHandlers() =>
        Assembly.GetEntryAssembly()!.DefinedTypes
                .Where(type =>
                    type.IsConcrete && type.GetInterface(nameof(IPacketHandler)) != null
                )
                .Select(type => (IPacketHandler)ActivatorUtilities.CreateInstance(_serviceProvider, type))
                .ToDictionary(type => type.GetType().BaseType!.GenericTypeArguments
                                          .First(a => a.IsAssignableTo(typeof(IPacket)))
                )
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
                        .Where(type =>
                            type.IsConcrete && type.GetInterface(nameof(IPacket)) != null &&
                            type.GetCustomAttribute<PacketIdAttribute>() != null
                        )
                        .ToDictionary(type => type.GetCustomAttribute<PacketIdAttribute>()!.PacketId)
                        .ToFrozenDictionary();
}