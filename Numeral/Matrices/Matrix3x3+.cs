using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="IMatrix3x3"/>
[Description(IMatrix3x3.Description)]
[Using(typeof(IMatrix))]
public readonly record struct Matrix3x3<T>
    : IMatrix3x3<Matrix3x3<T>, T>, IMatrix2D<T>, IMatrix3x3<T>, IMatrixImmutable<T>, IMatrixUnfixedAlias<Matrix<T>, T>
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

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in T)"/>
    public Matrix3x3(in T abc)
        => _Value = IMatrix3x3.Format(abc);

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in T, in T, in T, Matrix3x3Fill)"/>
    public Matrix3x3(in T a, in T b, in T c, Matrix3x3Fill fill = Matrix3x3Fill.Default)
        => _Value = IMatrix3x3.Format(a, b, c, fill);

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in T, in T, in T, in T, in T, in T, in T, in T, in T)"/>
    public Matrix3x3(in T r0c0, in T r0c1, in T r0c2, in T r1c0, in T r1c1, in T r1c2, in T r2c0, in T r2c1, in T r2c2)
        => _Value = IMatrix3x3.Format(r0c0, r0c1, r0c2, r1c0, r1c1, r1c2, r2c0, r2c1, r2c2);

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in IMatrix3x3{T})"/>
    public Matrix3x3(in IMatrix3x3<T> matrix)
        => _Value = IMatrix3x3.Format(matrix);

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in IVector3{T}, Matrix3x3Fill)"/>
    public Matrix3x3(in IVector3<T> abc, Matrix3x3Fill fill = Matrix3x3Fill.Default)
        => _Value = IMatrix3x3.Format(abc, fill);

    /// <inheritdoc cref="IMatrix3x3.Format{T}(in IVector3{T}, in IVector3{T}, in IVector3{T})"/>
    public Matrix3x3(in IVector3<T> r0, in IVector3<T> r1, in IVector3<T> r2)
        => _Value = IMatrix3x3.Format(r0, r1, r2);

    /// <see cref="Region.Operator"/>
    #region

    public static Matrix3x3<T> operator +(Matrix3x3<T> a, T b) => XMatrix.Do(a, Operator.Add, b);

    public static Matrix3x3<T> operator +(Matrix3x3<T> a, IMatrix3x3<T> b) => a.Do(Operator.Add, b);

    public static Vector3<T> operator +(Matrix3x3<T> a, IVector3<T> b) => a.Do(Operator.Add, b);

    public static Matrix3x3<T> operator -(Matrix3x3<T> a, T b) => XMatrix.Do(a, Operator.Subtract, b);

    public static Matrix3x3<T> operator -(Matrix3x3<T> a, IMatrix3x3<T> b) => a.Do(Operator.Subtract, b);

    public static Vector3<T> operator -(Matrix3x3<T> a, IVector3<T> b) => a.Do(Operator.Subtract, b);

    public static Matrix3x3<T> operator *(Matrix3x3<T> a, T b) => XMatrix.Do(a, Operator.Multiply, b);

    public static Matrix3x3<T> operator *(Matrix3x3<T> a, IMatrix3x3<T> b) => a.Do(Operator.Multiply, b);

    public static Vector3<T> operator *(Matrix3x3<T> a, IVector3<T> b) => a.Do(Operator.Multiply, b);

    public static Matrix3x3<T> operator /(Matrix3x3<T> a, T b) => XMatrix.Do(a, Operator.Divide, b);

    public static Matrix3x3<T> operator /(Matrix3x3<T> a, IMatrix3x3<T> b) => a.Do(Operator.Divide, b);

    public static Vector3<T> operator /(Matrix3x3<T> a, IVector3<T> b) => a.Do(Operator.Divide, b);

    public static Matrix3x3<T> operator %(Matrix3x3<T> a, T b) => XMatrix.Do(a, Operator.Modulo, b);

    public static Matrix3x3<T> operator %(Matrix3x3<T> a, IMatrix3x3<T> b) => a.Do(Operator.Modulo, b);

    public static Vector3<T> operator %(Matrix3x3<T> a, IVector3<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public T[][] ToArray() => [.. _Value];

    public T[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<Matrix3x3<T>, T>.GetArray() => new T[IMatrix3x3.LengthDimensional][];

    static Matrix3x3<T> IArray<Matrix3x3<T>, T>.Create(Matrix3x3<T> oldSelf, Array newSelf) => newSelf.To<T[][]>().To(i => new Matrix3x3<T>(i[0][0], i[0][1], i[0][2], i[1][0], i[1][1], i[1][2], i[2][0], i[2][1], i[2][2]));

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