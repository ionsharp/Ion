using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector4{}"/> for immutable equivalent.</remarks>
/// <inheritdoc cref="IVector4{}"/>
[Description(IVector4.Description)]
public record class Vector4M<T>
    : Model, IVector4<Vector4M<T>, T>, IVectorAlias<Vector4M<T>, T>, IVectorMutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector4M<T>, Vector<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public T X { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector2.Y"/>
    public T Y { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector3.Z"/>
    public T Z { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector4.W"/>
    public T W { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector4{T}.XY"/>
    public (T X, T Y) XY => (X, Y);

    /// <inheritdoc cref="IVector4{T}.XYZ"/>
    public (T X, T Y, T Z) XYZ => (X, Y, Z);

    /// <inheritdoc cref="VectorType"/>
    public VectorType Type { get => Get<VectorType>(); set => Set(value); }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector4.Format{T}(IEnumerable{T}, VectorType)"/>
    private Vector4M(in (T X, T Y, T Z, T W, VectorType Type) i) => (X, Y, Z, W, Type) = i;

    /// <inheritdoc cref="IVector4.Format{T}(in T, VectorType)"/>
    public Vector4M(in T xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, VectorType)"/>
    public Vector4M(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector4M(in (T X, T Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, in T, VectorType)"/>
    public Vector4M(in T x, in T y, in T z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector4M(in (T X, T Y, T Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{T}(in T, in T, in T, in T, VectorType)"/>
    public Vector4M(in T x, in T y, in T z, in T w, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, w, type);

    /// <inheritdoc cref="IVector4.Format{T}(in ValueTuple{T, T, T, T}, VectorType)"/>
    public Vector4M(in (T X, T Y, T Z, T W) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector4M(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector3{T}, VectorType?)"/>
    public Vector4M(in IVector3<T> xyz, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{T}(in IVector4{T}, VectorType?)"/>
    public Vector4M(in IVector4<T> xyzw, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](in Vector4M<T> i) => [.. i];

    public static implicit operator Vector<T>(in Vector4M<T> i) => new(i.Type, i);

    public static implicit operator VectorM<T>(in Vector4M<T> i) => new(i.Type, i);

    ///

    public static implicit operator Vector4M<T>(in Vector2<T> i) => new(i);

    public static implicit operator Vector4M<T>(in Vector2M<T> i) => new(i);

    public static implicit operator Vector4M<T>(in Vector3<T> i) => new(i);

    public static implicit operator Vector4M<T>(in Vector3M<T> i) => new(i);

    public static implicit operator Vector4M<T>(in Vector4<T> i) => new(i);

    public static implicit operator Vector4M<T>(in (T X, T Y) i) => new(i);

    public static implicit operator Vector4M<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector4M<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y, Z, W];

    /// <see cref="IArray{,}"/>

    static Vector4M<T> IArray<Vector4M<T>, T>.Create(Vector4M<T> oldSelf, Array newSelf)
    {
        (oldSelf.X, oldSelf.Y, oldSelf.Z, oldSelf.W) = newSelf.To<T[]>().To(i => (i[0], i[1], i[2], i[3]));
        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => IVector4.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector4M<T> IVector<Vector4M<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector4.Format(value, type));
}