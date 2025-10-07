using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="IMatrix2x2"/>
[Description(IMatrix2x2.Description)]
[Using(typeof(IMatrix))]
public readonly record struct Matrix2x2<T>
    : IMatrix2x2<Matrix2x2<T>, T>, IMatrix2D<T>, IMatrix2x2<T>, IMatrixImmutable<T>, IMatrixUnfixedAlias<Matrix<T>, T>
    where T : INumber<T>
{
    /// <see cref="Region.Property"/>

    private readonly T[][] _Value;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => this[y];

    object IArray1D.this[int y] => this[y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public T[] this[int y] { get => [.. _Value[y]]; set => _Value[y] = value; }
#pragma warning restore CA1819

    public T this[int y, int x] { get => _Value[y][x]; set => _Value[y][x] = value; }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IMatrix2x2.Format{T}(in T)"/>
    public Matrix2x2(in T abcd)
        => _Value = IMatrix2x2.Format(abcd);

    /// <inheritdoc cref="IMatrix2x2.Format{T}(in T, in T, in T, in T)"/>
    public Matrix2x2(in T r0c0, in T r0c1, in T r1c0, in T r1c1)
        => _Value = IMatrix2x2.Format(r0c0, r0c1, r1c0, r1c1);

    /// <inheritdoc cref="IMatrix2x2.Format{T}(in IMatrix2x2{T})"/>
    public Matrix2x2(in IMatrix2x2<T> matrix)
        => _Value = IMatrix2x2.Format(matrix);

    /// <inheritdoc cref="IMatrix2x2.Format{T}(in IVector2{T}, in IVector2{T})"/>
    public Matrix2x2(in IVector2<T> row0, in IVector2<T> row1)
        => _Value = IMatrix2x2.Format(row0, row1);

    /// <see cref="Region.Operator"/>
    #region

    public static Matrix2x2<T> operator +(Matrix2x2<T> a, T b) => XNumber.Do(a, Operator.Add, b);

    public static Matrix2x2<T> operator +(Matrix2x2<T> a, IMatrix2x2<T> b) => a.Do(Operator.Add, b);

    public static Vector2<T> operator +(Matrix2x2<T> a, IVector2<T> b) => a.Do(Operator.Add, b);

    public static Matrix2x2<T> operator -(Matrix2x2<T> a, T b) => XNumber.Do(a, Operator.Subtract, b);

    public static Matrix2x2<T> operator -(Matrix2x2<T> a, IMatrix2x2<T> b) => a.Do(Operator.Subtract, b);

    public static Vector2<T> operator -(Matrix2x2<T> a, IVector2<T> b) => a.Do(Operator.Subtract, b);

    public static Matrix2x2<T> operator *(Matrix2x2<T> a, T b) => XNumber.Do(a, Operator.Multiply, b);

    public static Matrix2x2<T> operator *(Matrix2x2<T> a, IMatrix2x2<T> b) => a.Do(Operator.Multiply, b);

    public static Vector2<T> operator *(Matrix2x2<T> a, IVector2<T> b) => a.Do(Operator.Multiply, b);

    public static Matrix2x2<T> operator /(Matrix2x2<T> a, T b) => XNumber.Do(a, Operator.Divide, b);

    public static Matrix2x2<T> operator /(Matrix2x2<T> a, IMatrix2x2<T> b) => a.Do(Operator.Divide, b);

    public static Vector2<T> operator /(Matrix2x2<T> a, IVector2<T> b) => a.Do(Operator.Divide, b);

    public static Matrix2x2<T> operator %(Matrix2x2<T> a, T b) => XNumber.Do(a, Operator.Modulo, b);

    public static Matrix2x2<T> operator %(Matrix2x2<T> a, IMatrix2x2<T> b) => a.Do(Operator.Modulo, b);

    public static Vector2<T> operator %(Matrix2x2<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public T[][] ToArray() => [.. _Value];

    public T[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<Matrix2x2<T>, T>.GetArray() => new T[IMatrix2x2.LengthDimensional][];

    static Matrix2x2<T> IArray<Matrix2x2<T>, T>.Create(Matrix2x2<T> oldSelf, Array newSelf) => newSelf.To<T[][]>().To(i => new Matrix2x2<T>(i[0][0], i[0][1], i[1][0], i[1][1]));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() { foreach (T[] y in _Value) { foreach (T x in y) { yield return x; } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IMatrix.ToString(this, format, provider);
}