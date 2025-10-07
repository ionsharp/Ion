using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector3{}"/> for immutable equivalent.</remarks>
/// <inheritdoc cref="IVector3{}"/>
[Description(IVector3.Description)]
public record class Vector3M<T>
    : Model, IVector3<Vector3M<T>, T>, IVectorAlias<Vector3M<T>, T>, IVectorMutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector3M<T>, Vector<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public T X { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector2.Y"/>
    public T Y { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector3.Z"/>
    public T Z { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector3{T}.XY"/>
    public (T X, T Y) XY => (X, Y);

    /// <inheritdoc cref="VectorType"/>
    public VectorType Type { get => Get<VectorType>(); set => Set(value); }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector3.Format{T}(in IEnumerable{T}, VectorType)"/>
    private Vector3M(in (T X, T Y, T Z, VectorType Type) i) => (X, Y, Z, Type) = i;

    /// <inheritdoc cref="IVector3.Format{T}(in T, VectorType)"/>
    public Vector3M(in T xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{T}(in T, in T, VectorType)"/>
    public Vector3M(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, type);

    /// <inheritdoc cref="IVector3.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector3M(in (T X, T Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{T}(in T, in T, in T, VectorType)"/>
    public Vector3M(in T x, in T y, in T z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, z, type);

    /// <inheritdoc cref="IVector3.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector3M(in (T X, T Y, T Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector3M(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{T}(in IVector3{T}, VectorType?)"/>
    public Vector3M(in IVector3<T> xyz, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](in Vector3M<T> i) => [.. i];

    public static implicit operator Vector<T>(in Vector3M<T> i) => new(i.Type, i);

    public static implicit operator VectorM<T>(in Vector3M<T> i) => new(i.Type, i);

    ///

    public static implicit operator Vector3M<T>(in Vector2<T> i) => new(i);

    public static implicit operator Vector3M<T>(in Vector2M<T> i) => new(i);

    public static implicit operator Vector3M<T>(in Vector3<T> i) => new(i);

    public static implicit operator Vector3M<T>(in Vector4<T> i) => new(i);

    public static implicit operator Vector3M<T>(in Vector4M<T> i) => new(i);

    public static implicit operator Vector3M<T>(in (T X, T Y) i) => new(i);

    public static implicit operator Vector3M<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector3M<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y, Z];

    /// <see cref="IArray{,}"/>

    static Vector3M<T> IArray<Vector3M<T>, T>.Create(Vector3M<T> oldSelf, Array newSelf)
    {
        (oldSelf.X, oldSelf.Y, oldSelf.Z) = newSelf.To<T[]>().To(i => (i[0], i[1], i[2]));
        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => IVector3.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector3M<T> IVector<Vector3M<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector3.Format(value, type));
}