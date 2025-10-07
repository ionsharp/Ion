using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector2M{}"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVector2{}"/>
[Description(IVector2.Description)]
public readonly record struct Vector2<T>
    : IVector2<Vector2<T>, T>, IVectorAlias<Vector2<T>, T>, IVectorImmutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector2<T>, Vector<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public readonly T X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly T Y { get; }

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector2.Format{T}(in IEnumerable{T}, VectorType)"/>
    private Vector2(in (T X, T Y, VectorType Type) i) => (X, Y, Type) = i;

    /// <inheritdoc cref="IVector2.Format{T}(in T, VectorType)"/>
    public Vector2(in T xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{T}(in T, in T, VectorType)"/>
    public Vector2(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(x, y, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector2(in (T X, T Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector2(in (T X, T Y, T Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyz, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T, T, T}, VectorType)"/>
    public Vector2(in (T X, T Y, T Z, T W) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyzw, type);

    /// <inheritdoc cref="IVector2.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector2(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Type) = IVector2.Format(xy, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](Vector2<T> i) => [.. i];

    public static implicit operator Vector<T>(Vector2<T> i) => new(i);

    public static implicit operator VectorM<T>(Vector2<T> i) => new(i);

    ///

    public static implicit operator Vector2<T>(Vector2M<T> i) => new(i);

    public static implicit operator Vector2<T>(Vector3<T> i) => new(i);

    public static implicit operator Vector2<T>(Vector3M<T> i) => new(i);

    public static implicit operator Vector2<T>(Vector4<T> i) => new(i);

    public static implicit operator Vector2<T>(Vector4M<T> i) => new(i);

    public static implicit operator Vector2<T>(in (T X, T Y) i) => new(i.X, i.Y);

    public static implicit operator Vector2<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector2<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y];

    /// <see cref="IArray{,}"/>

    static Vector2<T> IArray<Vector2<T>, T>.Create(Vector2<T> oldSelf, Array newSelf) => new(IVector2.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector2.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector2<T> IVector<Vector2<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector2.Format(value, type));
}