using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Serialization;

[NotImplemented]
public class SerializeXML : Serializer
{
    public override T Deserialize<T>(string data) => default;

    public override string Serialize<T>(T data) => data.ToString();
}
