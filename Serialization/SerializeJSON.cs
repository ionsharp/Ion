using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ion.Serialization;

public class SerializeJSON : Serializer
{
    public JsonSerializerOptions Options { get; set; } = new();

    public override T Deserialize<T>(string data) => (T)JsonSerializer.Deserialize(data, typeof(T), Options ?? new());

    public override string Serialize<T>(T data) => JsonSerializer.Serialize(data, Options ?? new());
}