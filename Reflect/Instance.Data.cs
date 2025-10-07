using Ion.Collect;
using System;

namespace Ion.Reflect;

public static partial class Instance
{
    public sealed class Data(Type Type) : ObjectDictionary()
    {
        public Type Type { get; } = Type;
    }
}