using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion.Numeral;

/// <remarks>See <see cref="Matrix{}"/> for the immutable equivalent.</remarks>
/// <inheritdoc cref="IMatrixMutable"/>
[CollectionBuilder(typeof(MatrixMutableBuilder), nameof(MatrixMutableBuilder.Create))]
[Description(IMatrix.Description)]
[Using(typeof(IMatrix))]
public record class MatrixMutable<T>
    : IMatrix<MatrixMutable<T>, T>, IMatrix2D<T>, IMatrixMutable, IMatrixUnfixed<T>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IMatrix.Columns"/>
    public int Columns => _Value[0].Length;

    /// <inheritdoc cref="IMatrix.Rows"/>
    public int Rows => _Value.Length;

    private T[][] _Value;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => this[y];

    object IArray1D.this[int y] => this[y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public T[] this[int row] { get => [.. _Value[row]]; set => _Value[row] = value; }
#pragma warning restore CA1819

    public T this[int row, int column] { get => _Value[row][column]; set => _Value[row][column] = value; }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IMatrix.Format{T}(int, in T)"/>
    public MatrixMutable(int length, in T value = default)
        => _Value = IMatrix.Format(length, value);

    /// <inheritdoc cref="IMatrix.Format{T}(int, in T, in T, MatrixFill)"/>
    public MatrixMutable(int length, in T a, in T b, MatrixFill fill)
        => _Value = IMatrix.Format(length, a, b, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(int, int, in T)"/>
    public MatrixMutable(int rows, int columns, in T value)
        => _Value = IMatrix.Format(rows, columns, value);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[], Matrix2DFromArray1D)"/>
    public MatrixMutable(in T[] array, Matrix2DFromArray1D fill = default)
        => _Value = IMatrix.Format(array, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][])"/>
    public MatrixMutable(in T[][] array)
        => _Value = IMatrix.Format(array);

    /// <inheritdoc cref="IMatrix.Format{T}(in T[][][])"/>
    public MatrixMutable(in T[][][] array)
        => _Value = IMatrix.Format(array);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray1D{T}, Matrix2DFromArray1D)"/>
    public MatrixMutable(in IArray1D<T> array, Matrix2DFromArray1D fill = default)
        => _Value = IMatrix.Format(array, fill);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray2D{T})"/>
    public MatrixMutable(in IArray2D<T> array)
        => _Value = IMatrix.Format(array);

    /// <inheritdoc cref="IMatrix.Format{T}(in IArray3D{T})"/>
    public MatrixMutable(in IArray3D<T> array)
        => _Value = IMatrix.Format(array);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix2D{T})"/>
    public MatrixMutable(in IMatrix2D<T> matrix)
        => _Value = IMatrix.Format(matrix);

    /// <inheritdoc cref="IMatrix.Format{T}(in IMatrix3D{T})"/>
    public MatrixMutable(in IMatrix3D<T> matrix)
        => _Value = IMatrix.Format(matrix);

    /// <inheritdoc cref="IMatrix.Format{T}(in IVector{T}, int)"/>
    public MatrixMutable(in IVector<T> vector, int repeat = 1)
        => _Value = IMatrix.Format(vector);

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public T[][] ToArray() => [.. _Value];

    public T[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<MatrixMutable<T>, T>.GetArray() => _Value;

    static MatrixMutable<T> IArray<MatrixMutable<T>, T>.Create(MatrixMutable<T> oldSelf, Array newSelf) 
    {
        if (newSelf is T[][] result)
            oldSelf._Value = result;

        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() { foreach (T[] y in _Value) { foreach (T x in y) { yield return x; } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public sealed override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public virtual string ToString(string format, IFormatProvider provider) => IMatrix.ToString(this, format, provider);
}