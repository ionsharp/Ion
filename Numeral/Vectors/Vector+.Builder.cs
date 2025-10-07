using Ion.Numeral;
using System;

namespace Ion;

public static class VectorBuilder
{
    public static Vector<T> Create<T>(ReadOnlySpan<T> i) => new(i.ToArray());
}