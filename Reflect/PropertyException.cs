using System;

namespace Ion.Reflect;

public class PropertyNotMutable<T>(string propertyName)
    : Exception($"Property '{typeof(T).FullName}.{propertyName}' isn't mutable.");