using Ion;
using Ion.Serialization;
using Ion.Storage;
using System;

namespace Ion.Serialization;

public enum SerializationType 
{
    None,
    [Obsolete]
    Binary,
    [Obsolete]
    Image, 
    JSON, 
    XML 
}