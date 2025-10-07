using Ion.Numeral;
using System;

namespace Ion;

/// <summary>
/// A builder to initialize <see cref="Matrix3D{}"/> from a collection expression.
/// </summary>
public static class Matrix3DBuilder
{
    public static Matrix3D<T> Create<T>(ReadOnlySpan<T> i)
    {
        T[][][] array = [[new T[i.Length]]];
        for (int x = 0; x < i.Length; x++)
            array[0][0][x] = i[x];

        return new(array);
    }
}