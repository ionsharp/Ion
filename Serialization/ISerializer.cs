using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Serialization;

/// <summary>
/// Serializes and deserializes <see cref="object"/> to and from <see cref="string"/>.
/// </summary>
public interface ISerializer
{
    T Deserialize<T>(string data);

    string Serialize<T>(T data);
}

/// <inheritdoc/>
public interface ISerializer<T> : ISerializer
{
    T Deserialize(string data);

    string Serialize(T data);
}