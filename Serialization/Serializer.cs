using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Serialization;

/// <inheritdoc cref="ISerializer"/>
/// <remarks>Implements <see cref="ISerializer"/>.</remarks>
public abstract class Serializer : ISerializer
{
    public abstract T Deserialize<T>(string data);

    public abstract string Serialize<T>(T data);

    public static Serializer GetBy(SerializationType type)
    {
        return type switch
        {
            SerializationType.Binary 
                => new SerializeBinary(),
            SerializationType.JSON
                => new SerializeJSON(),
            SerializationType.None
                => null,
            SerializationType.XML 
                => new SerializeXML(),
            _ 
                => throw new NotSupportedException($"Serialization type '{type}' is not supported."),
        };
    }
}
