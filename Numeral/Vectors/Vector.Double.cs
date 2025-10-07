using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <remarks>See <see cref="VectorMutable"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVector"/>
[Description(IVector.Description)]
public readonly record struct Vector
    : IVector<Vector, double>, IVectorAlias<Vector<double>, double>, IVectorImmutable<double>, IVectorLong<double>, IVectorUnfixed<double>, IVectorUnfixedAlias<Vector, Vector, double>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector.Length"/>
    public readonly int Length => _Value.Length;

    /// <inheritdoc cref="IVector.Type"/>
    public readonly VectorType Type { get; }

    private readonly double[] _Value { get; }

    /// <see cref="Region.Property.Indexor"/>

    public readonly double this[int index] => _Value[index];

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector.Format{T}(VectorType, in IEnumerable{T})"/>
    public Vector(VectorType type, in IEnumerable<double> i)
        => (_Value, Type) = IVector.Format(type, i);

    /// <inheritdoc cref="IVector.Format{T}(in IEnumerable{T}, VectorType)"/>
    public Vector(in IEnumerable<double> i, VectorType type)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public Vector(params double[] i)
        => (_Value, Type) = IVector.Format(i);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public Vector(in IArray1D<double> i, VectorType type = default)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IMatrix{T}, VectorType?)"/>
    public Vector(in IMatrix<double> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IVector{T}, VectorType?)"/>
    public Vector(in IVector<double> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Vector(double[] i) => new(i);

    public static implicit operator Vector<double>(Vector i) => new(i);

    public static implicit operator Vector(Vector<double> i) => new(i);

    public static implicit operator VectorM<double>(Vector i) => new(i);

    public static implicit operator Vector(VectorM<double> i) => new(i);

    ///

    public static Vector operator +(Vector a, double b) => a.Do(Operator.Add, b);

    public static Vector operator -(Vector a, double b) => a.Do(Operator.Subtract, b);

    public static Vector operator *(Vector a, double b) => a.Do(Operator.Multiply, b);

    public static Vector operator /(Vector a, double b) => a.Do(Operator.Divide, b);

    public static Vector operator %(Vector a, double b) => a.Do(Operator.Modulo, b);

    ///

    public static Vector operator +(Vector i) => i;

    public static Vector operator +(Vector a, IVector<double> b) => a.Do(Operator.Add, b);

    public static Vector operator ++(Vector i) => i.Do(Operator.Add, 1);

    public static Vector operator -(Vector i) => i.Do(Operator.Multiply, -1);

    public static Vector operator -(Vector a, IVector<double> b) => a.Do(Operator.Subtract, b);

    public static Vector operator --(Vector i) => i.Do(Operator.Subtract, 1);

    public static Vector operator *(Vector a, IVector<double> b) => a.Do(Operator.Multiply, b);

    public static Vector operator /(Vector a, IVector<double> b) => a.Do(Operator.Divide, b);

    public static Vector operator %(Vector a, IVector<double> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public double[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<Vector, double>.GetArray() => new double[Length];

    static Vector IArray<Vector, double>.Create(Vector oldSelf, Array newSelf) => new((double[])newSelf);

    /// <see cref="IEnumerable{}"/>

    public IEnumerator<double> GetEnumerator() => (_Value as IEnumerable<double>).GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IVector.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector IVector<Vector, double>.Create(VectorType type, IEnumerable<double> value) => new(type, value);
}