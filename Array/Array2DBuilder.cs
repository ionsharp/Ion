using System;

namespace Ion;

public static class Array2DBuilder
{
    public static Array2D<T> Create<T>(ReadOnlySpan<T> i)
    {
        T[][] array = [new T[i.Length]];
        for (int x = 0; x < i.Length; x++)
            array[0][x] = i[x];

        return new(array);
    }
}