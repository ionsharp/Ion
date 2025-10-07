using System;

namespace Ion;

public static class Array1DBuilder
{
    public static Array<Value> Create<Value>(ReadOnlySpan<Value> i) => new(i.ToArray());
}