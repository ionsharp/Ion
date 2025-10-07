using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector4M{}"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVector4{}"/>
[Description(IVector4.Description)]
public readonly record struct Vector4<T>
    : IVector4<Vector4<T>, T>, IVectorAlias<Vector4<T>, T>, IVectorImmutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector4<T>, Vector<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public readonly T X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly T Y { get; }

    /// <inheritdoc cref="IVector3.Z"/>
    public readonly T Z { get; }

    /// <inheritdoc cref="IVector4.W"/>
    public readonly T W { get; }

    /// <inheritdoc cref="IVector4{T}.XY"/>
    public readonly (T X, T Y) XY => (X, Y);

    /// <inheritdoc cref="IVector4{T}.XYZ"/>
    public readonly (T X, T Y, T Z) XYZ => (X, Y, Z);

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector4.Format{T}(IEnumerable{T}, VectorType)"/>
    private Vector4(in (T X, T Y, T Z, T W, VectorType Type) i) => (X, Y, Z, W, Type) = i;

    /// <inheritdoc cref="IVector4.Format{T}(in T, VectorType)"/>
    public Vector4(in T xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, VectorType)"/>
    public Vector4(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector4(in (T x, T y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, in T, VectorType)"/>
    public Vector4(in T x, in T y, in T z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector4(in (T x, T y, T z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, in T, in T, VectorType)"/>
    public Vector4(in T x, in T y, in T z, in T w, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, w, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T, T, T}, VectorType)"/>
    public Vector4(in (T x, T y, T z, T w) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector4(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector3{T}, VectorType?)"/>
    public Vector4(in IVector3<T> xyz, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector4{T}, VectorType?)"/>
    public Vector4(in IVector4<T> xyzw, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](Vector4<T> i) => [.. i];

    public static implicit operator Vector<T>(Vector4<T> i) => new(i);

    public static implicit operator VectorM<T>(Vector4<T> i) => new(i);

    ///

    public static implicit operator Vector4<T>(in Vector2<T> i) => new(i);

    public static implicit operator Vector4<T>(in Vector2M<T> i) => new(i);

    public static implicit operator Vector4<T>(in Vector3<T> i) => new(i);

    public static implicit operator Vector4<T>(in Vector3M<T> i) => new(i);

    public static implicit operator Vector4<T>(in Vector4M<T> i) => new(i);

    public static implicit operator Vector4<T>(in (T X, T Y) i) => new(i);

    public static implicit operator Vector4<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector4<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y, Z, W];

    /// <see cref="IArray{,}"/>

    static Vector4<T> IArray<Vector4<T>, T>.Create(Vector4<T> oldSelf, Array newSelf) => new(IVector4.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector4.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector4<T> IVector<Vector4<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector4.Format(value, type));
}