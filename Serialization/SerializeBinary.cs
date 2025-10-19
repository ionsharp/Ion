using Ion.Analysis;
using Ion.Reflect;
using Ion.Storage;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Serialization;

[Obsolete]
public class SerializeBinary : Serializer
{
    public override T Deserialize<T>(string data)
    {
        byte[] bytes = Convert.FromBase64String(data);
        using (var stream = new MemoryStream(bytes))
            return (T)new BinaryFormatter().Deserialize(stream);
    }

    public override string Serialize<T>(T data)
    {
        using (var stream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(stream, data);
            return Convert.ToBase64String(stream.ToArray());
        }
    }
}