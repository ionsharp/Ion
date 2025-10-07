using Ion.Numeral;
using System;

namespace Ion;

public static class VectorMutableBuilder
{
    public static VectorM<T> Create<T>(ReadOnlySpan<T> i) => new(i.ToArray());
}