using System.Reflection;

namespace OnChat.Protocol.Codecs.Impl;

[FactoryCodec]
public class StructCodec(Type structType, PropertyInfo[] properties) : CodecInfo<object>
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
        object instance = Activator.CreateInstance(structType)!;

        foreach (PropertyInfo property in properties)
        {
            object propertyValue = Protocol.GetCodec(property.PropertyType).Decode(reader);
            
            property.SetValue(instance, propertyValue);
        }

        return instance;
    }
}