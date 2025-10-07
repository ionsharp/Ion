using Ion.Numeral;
using System;

namespace Ion;

/// <summary>
/// A builder to initialize <see cref="Matrix{}"/> from a collection expression.
/// </summary>
public static class MatrixBuilder
{
    public static Matrix<T> Create<T>(ReadOnlySpan<T> i)
    {
        T[][] array = [new T[i.Length]];
        for (int x = 0; x < i.Length; x++)
            array[0][x] = i[x];

        return new(array);
    }
}