using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion.Numeral;

/// <inheritdoc cref="IMatrix3D"/>
[CollectionBuilder(typeof(Matrix3DBuilder), nameof(Matrix3DBuilder.Create))]
[Description(IMatrix3D.Description)]
[Using(typeof(IMatrix3D))]
[NotImplemented(typeof(IMatrixUnfixedAlias<,>), IMatrix3D.NotImplemented)]
public readonly record struct Matrix3D<T>
    : IMatrix3D<Matrix3D<T>, T>, IMatrixImmutable<T>, IMatrixUnfixed<T>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IMatrix.Columns"/>
    public int Columns => _Value[0][0].Length;

    /// <inheritdoc cref="IMatrix.Rows"/>
    public int Rows => _Value[0].Length;

    /// <inheritdoc cref="IMatrix3D.Slices"/>
    public int Slices => _Value.Length;

    private readonly T[][][] _Value;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int x] => this[x];

    object IArray1D.this[int z] => this[z];

    object IArray2D.this[int slice, int row] => this[slice, row];

    object IMatrix3D.this[int slice, int row, int column] => this[slice, row, column];

    public T this[int slice, int row, int column] { get => _Value[slice][row][column]; set => _Value[slice][row][column] = value; }

#pragma warning disable CA1819 /// Properties should not return arrays
    public T[] this[int slice, int row] { get => _Value[slice][row]; set => _Value[slice][row] = value; }

    public T[][] this[int slice] { get => _Value[slice]; set => _Value[slice] = value; }
#pragma warning restore CA1819

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IMatrix3D.Format{T}(int, in T)"/>
    public Matrix3D(int length, in T value = default)
        => _Value = IMatrix3D.Format(length, value);

    /// <inheritdoc cref="IMatrix3D.Format{T}(int, in T, in T, MatrixFill, Matrix3DFillSide)"/>
    public Matrix3D(int length, in T a, in T b, MatrixFill fill, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(length, a, b, fill, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(int, int, in T, Matrix3DFillSide)"/>
    public Matrix3D(int rows, int columns, in T value, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(rows, columns, value, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(int, int, int, in T)"/>
    public Matrix3D(int slices, int rows, int columns, in T value)
        => _Value = IMatrix3D.Format(slices, rows, columns, value);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in T[], Matrix3DFillSide)"/>
    public Matrix3D(in T[] array1D, Matrix2DFromArray1D fill = default, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(array1D, fill, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in T[][], Matrix3DFillSide)"/>
    public Matrix3D(in T[][] array2D, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(array2D, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in T[][][])"/>
    public Matrix3D(in T[][][] array3D)
        => _Value = IMatrix3D.Format(array3D);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IArray1D{T}, Matrix3DFillSide)"/>
    public Matrix3D(in IArray1D<T> array1D, Matrix2DFromArray1D fill = default, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(array1D, fill, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IArray2D{T}, Matrix3DFillSide)"/>
    public Matrix3D(in IArray2D<T> array2D, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(array2D, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IArray3D{T})"/>
    public Matrix3D(in IArray3D<T> array3D)
        => _Value = IMatrix3D.Format(array3D);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IMatrix2D{T}, Matrix3DFillSide)"/>
    public Matrix3D(in IMatrix2D<T> matrix2D, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(matrix2D, slices, side);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IMatrix3D{T})"/>
    public Matrix3D(in IMatrix3D<T> matrix3D)
        => _Value = IMatrix3D.Format(matrix3D);

    /// <inheritdoc cref="IMatrix3D.Format{T}(in IVector{T}, Matrix3DFillSide)"/>
    public Matrix3D(in IVector<T> vector, int repeat = 1, int slices = 1, Matrix3DFillSide side = default)
        => _Value = IMatrix3D.Format(vector, repeat, slices, side);

    /// <see cref="IArray3D"/>

    object[][][] IArray3D.ToArray() => XArray3D.ToArray(_Value, i => (object)i);

    public T[][][] ToArray() => [.. _Value];

    public T[][] ToArray(int slice) => this[slice];

    public T[] ToArray(int slice, int row) => this[slice, row];

    /// <see cref="IArray{,}"/>

    Array IArray<Matrix3D<T>, T>.GetArray() => new T[Slices][][];

    static Matrix3D<T> IArray<Matrix3D<T>, T>.Create(Matrix3D<T> oldSelf, Array newSelf) => new((T[][][])newSelf);

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() { foreach (T[][] z in _Value) { foreach (T[] y in z) { foreach (T x in y) { yield return x; } } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix3D.ToString{}(IMatrix3D{}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix3D.ToString{}(IMatrix3D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix3D.ToString{}(IMatrix3D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IMatrix3D.ToString(this, format, provider);

    /// <see cref="IMatrix3D{,}"/>

    public static Matrix3D<T> Create(T[][][] value) => new(value);
}