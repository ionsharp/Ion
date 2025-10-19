using Ion.Analysis;
using Ion.Core;
using Ion.Reflect;
using Ion.Serialization;
using Ion.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Storage;

/// <summary>
/// Serializes and deserializes <see cref="object"/> to and from file.
/// </summary>
/// <remarks>Uses <see cref="Serializer"/>.</remarks>
[Using<Serializer>]
[Using<SerializationType>]
public static class FileSerializer
{
    /// <inheritdoc cref="Deserialize{T}(string, out T, ISerializer, Text.Encoding)"/>
    /// <remarks>Initializes <see cref="Serializer"/> based on <see cref="SerializationType"/>.</remarks>
    public static Result Deserialize<T>(string filePath, out T data, SerializationType type = SerializationType.JSON, Text.Encoding encode = Text.Encoding.UTF8)
        => Deserialize(filePath, out data, Serializer.GetBy(type), encode);

    /// <inheritdoc cref="Serialize{T}(string, T, ISerializer, Text.Encoding)"/>
    /// <remarks>Initializes <see cref="Serializer"/> based on <see cref="SerializationType"/>.</remarks>
    public static Result Serialize<T>(string filePath, T data, SerializationType type = SerializationType.JSON, Text.Encoding encode = Text.Encoding.UTF8)
        => Serialize(filePath, data, Serializer.GetBy(type), encode);

    /// <summary>
    /// Deserialize data from given file path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    /// <param name="serializer"></param>
    /// <param name="encode"></param>
    /// <exception cref="FileNotDeserialized"/>
    /// <exception cref="InvalidCastException"/>
    /// <returns><see cref="Result"/></returns>
    public static Result Deserialize<T>(string filePath, out T data, ISerializer serializer, Text.Encoding encode = Text.Encoding.UTF8)
    {
        data = default;
        Result result = null;
        try
        {
            string text = File.ReadAllText(filePath, encode.New());
            if (serializer is null)
            {
                data = text is T t ? t : throw new InvalidCastException();
            }
            else
            {
                data = serializer.Deserialize<T>(text);
            }

            result = new Success();
        }
        catch (Exception e)
        {
            result = new FileNotDeserialized(filePath, e.InnerException);
            Log.Write(result);
        }
        return result;
    }

    /// <summary>
    /// Serialize given data to given file path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    /// <param name="type"></param>
    /// <exception cref="FileNotSerialized"/>
    /// <returns><see cref="Result"/></returns>
    public static Result Serialize<T>(string filePath, T data, ISerializer serializer, Text.Encoding encode = Text.Encoding.UTF8)
    {
        Result result = null;
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            string text = serializer?.Serialize(data) ?? data.ToString();

            File.WriteAllText(filePath, text, encode.New());
            result = new Success();
        }
        catch (Exception e)
        {
            result = new FileNotSerialized(filePath, e);
            Log.Write(result);
        }
        return result;
    }
}