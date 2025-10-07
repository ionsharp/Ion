using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion.Numeral;

/// <remarks>See <see cref="VectorM{}"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVector"/>
[CollectionBuilder(typeof(VectorBuilder), nameof(VectorBuilder.Create))]
[Description(IVector.Description)]
public readonly record struct Vector<T>
    : IVector<Vector<T>, T>, IVectorAlias<Vector<T>, T>, IVectorImmutable<T>, IVectorLong<T>, IVectorUnfixed<T>, IVectorUnfixedAlias<Vector<T>, Vector<T>, T>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector.Length"/>
    public readonly int Length => _Value.Length;

    /// <inheritdoc cref="IVector.Type"/>
    public readonly VectorType Type { get; }

    private readonly T[] _Value { get; }

    /// <see cref="Region.Property.Indexor"/>

    public T this[int i] => _Value[i];

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector.Format{T}(VectorType, in IEnumerable{T})"/>
    public Vector(VectorType type, in IEnumerable<T> i)
        => (_Value, Type) = IVector.Format(type, i);

    /// <inheritdoc cref="IVector.Format{T}(in IEnumerable{T}, VectorType)"/>
    public Vector(in IEnumerable<T> i, VectorType type)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public Vector(params T[] i)
        => (_Value, Type) = IVector.Format(i);

    /// <inheritdoc cref="IVector.Format{T}(in IArray1D{T}, VectorType)"/>
    public Vector(in IArray1D<T> i, VectorType type = default)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IMatrix{T}, VectorType?)"/>
    public Vector(in IMatrix<T> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IVector{T}, VectorType?)"/>
    public Vector(in IVector<T> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>

    public static implicit operator Vector<T>(T[] i) => new(i);

    public static implicit operator Vector<T>(VectorM<T> i) => new(i);

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public T[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<Vector<T>, T>.GetArray() => new T[Length];

    static Vector<T> IArray<Vector<T>, T>.Create(Vector<T> oldSelf, Array newSelf) => new((T[])newSelf);

    /// <see cref="IEnumerable{}"/>

    public IEnumerator<T> GetEnumerator() => (_Value as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IVector.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector<T> IVector<Vector<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(type, value);
}