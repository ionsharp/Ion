using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector3M{}"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVector3{}"/>
[Description(IVector3.Description)]
public readonly record struct Vector3<T>
    : IVector3<Vector3<T>, T>, IVectorAlias<Vector3<T>, T>, IVectorImmutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector3<T>, Vector<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public readonly T X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly T Y { get; }

    /// <inheritdoc cref="IVector3.Z"/>
    public readonly T Z { get; }

    /// <inheritdoc cref="IVector3{T}.XY"/>
    public readonly (T X, T Y) XY => (X, Y);

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector3.Format{T}(in IEnumerable{T}, VectorType)"/>
    private Vector3(in (T X, T Y, T Z, VectorType Type) i) => (X, Y, Z, Type) = i;

    /// <inheritdoc cref="IVector3.Format{T}(in T, VectorType)"/>
    public Vector3(in T xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{T}(in T, in T, VectorType)"/>
    public Vector3(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, type);

    /// <inheritdoc cref="IVector3.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector3(in (T x, T y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{T}(in T, in T, in T, VectorType)"/>
    public Vector3(in T x, in T y, in T z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, z, type);

    /// <inheritdoc cref="IVector3.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector3(in (T x, T y, T z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector3(in (T x, T y, T z, T w) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyzw, type);

    /// <inheritdoc cref="IVector3.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector3(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{T}(in IVector3{T}, VectorType?)"/>
    public Vector3(in IVector3<T> xyz, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](Vector3<T> i) => [.. i];

    public static implicit operator Vector<T>(Vector3<T> i) => new(i);

    public static implicit operator VectorM<T>(Vector3<T> i) => new(i);

    ///

    public static implicit operator Vector3<T>(Vector2<T> i) => new(i);

    public static implicit operator Vector3<T>(Vector2M<T> i) => new(i);

    public static implicit operator Vector3<T>(Vector3M<T> i) => new(i);

    public static implicit operator Vector3<T>(Vector4<T> i) => new(i);

    public static implicit operator Vector3<T>(Vector4M<T> i) => new(i);

    public static implicit operator Vector3<T>(in (T X, T Y) i) => new(i);

    public static implicit operator Vector3<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector3<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y, Z];

    /// <see cref="IArray{,}"/>

    static Vector3<T> IArray<Vector3<T>, T>.Create(Vector3<T> oldSelf, Array newSelf) => new(IVector3.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector3.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector3<T> IVector<Vector3<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector3.Format(value, type));
}