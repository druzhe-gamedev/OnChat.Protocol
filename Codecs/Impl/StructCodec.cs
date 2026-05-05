using System.Reflection;

namespace OnChat.Protocol.Codecs.Impl;

[FactoryCodec]
public class StructCodec(Type structType, PropertyInfo[] properties, ConstructorInfo? ctor) : CodecInfo<object>
{
    public override void Encode(BinaryWriter writer, object value)
    {
        foreach (PropertyInfo property in properties)
        {
            object propertyValue = property.GetValue(value)!;
            
            Protocol.GetCodec(property.PropertyType).Encode(writer, propertyValue);
        }
    }

    public override object Decode(BinaryReader reader)
    {
        List<object> args = [];
        args.AddRange(
            ctor != null
                ? ctor.GetParameters().Select(parameter => Protocol.GetCodec(parameter.ParameterType).Decode(reader))
                : properties.Select(property => Protocol.GetCodec(property.PropertyType).Decode(reader))
        );

        object instance = Activator.CreateInstance(structType, args)!;
        return instance;
    }
}