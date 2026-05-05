using System.Reflection;

namespace OnChat.Protocol.Codecs.Impl;

[FactoryCodec]
public class StructCodec(Type structType, PropertyInfo[] properties, ConstructorInfo? ctor) : CodecInfo<object>
{
    public override void Encode(BinaryWriter writer, object value)
    {
        if(ctor != null)
        {
            ParameterInfo[] parameters = ctor.GetParameters();
            properties = properties
                         .OrderBy(p =>
                             {
                                 int index = Array.FindIndex(
                                     parameters,
                                     param => string.Equals(param.Name, p.Name, StringComparison.OrdinalIgnoreCase)
                                 );
                                 return index >= 0 ? index : int.MaxValue;
                             }
                         )
                         .ToArray();
        }
        foreach (PropertyInfo property in properties)
        {
            object propertyValue = property.GetValue(value)!;
            
            Protocol.GetCodec(property.PropertyType).Encode(writer, propertyValue);
        }
    }

    public override object Decode(BinaryReader reader)
    {
        List<object> args = [];

        if (ctor != null)
        {
            args.AddRange(ctor.GetParameters().Select(param => Protocol.GetCodec(param.ParameterType).Decode(reader)));

            object[] argsArray = args.ToArray();
            return Activator.CreateInstance(structType, argsArray)!;
        }
        
        object instance = Activator.CreateInstance(structType)!;
        foreach (PropertyInfo property in properties)
        {
            object propertyValue = Protocol.GetCodec(property.PropertyType).Decode(reader);

            property.SetValue(instance, propertyValue);
        }

        return instance;
    }
}