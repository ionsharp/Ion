using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector2{}"/> for immutable equivalent.</remarks>
/// <inheritdoc cref="IVector2{}"/>
[Description(IVector2.Description)]
public record class Vector2M<T>
    : Model, IVector2<Vector2M<T>, T>, IVectorAlias<Vector2M<T>, T>, IVectorMutable<T>, IVectorShort<T>, IVectorUnfixedAlias<Vector2M<T>, VectorM<T>, T>
{
    /// <inheritdoc cref="IVector2.X"/>
    public T X { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="IVector2.Y"/>
    public T Y { get => Get<T>(); set => Set(value); }

    /// <inheritdoc cref="VectorType"/>
    public VectorType Type { get => Get<VectorType>(); set => Set(value); }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector2.Format{T}(in IEnumerable{T}, VectorType)"/>
    private Vector2M(in (T X, T Y, VectorType Type) i) => (X, Y, Type) = i;

    /// <inheritdoc cref="IVector2.Format{T}(in T, VectorType)"/>
    public Vector2M(in T xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{T}(in T, in T, VectorType)"/>
    public Vector2M(in T x, in T y, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(x, y, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T}, VectorType)"/>
    public Vector2M(in (T X, T Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T, T}, VectorType)"/>
    public Vector2M(in (T X, T Y, T Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyz, type);

    /// <inheritdoc cref="IVector2.Format{T}(in ValueTuple{T, T, T, T}, VectorType)"/>
    public Vector2M(in (T X, T Y, T Z, T W) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyzw, type);

    /// <inheritdoc cref="IVector2.Format{T}(in IVector2{T}, VectorType?)"/>
    public Vector2M(in IVector2<T> xy, VectorType? type = null)
        => (X, Y, Type) = IVector2.Format(xy, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator T[](in Vector2M<T> i) => [.. i];

    public static implicit operator Vector<T>(in Vector2M<T> i) => new(i.Type, i);

    public static implicit operator VectorM<T>(in Vector2M<T> i) => new(i.Type, i);

    ///

    public static implicit operator Vector2M<T>(Vector2<T> i) => new(i);

    public static implicit operator Vector2M<T>(Vector3<T> i) => new(i);

    public static implicit operator Vector2M<T>(Vector3M<T> i) => new(i);

    public static implicit operator Vector2M<T>(Vector4<T> i) => new(i);

    public static implicit operator Vector2M<T>(Vector4M<T> i) => new(i);

    public static implicit operator Vector2M<T>(in (T X, T Y) i) => new(i.X, i.Y);

    public static implicit operator Vector2M<T>(in (T X, T Y, T Z) i) => new(i);

    public static implicit operator Vector2M<T>(in (T X, T Y, T Z, T W) i) => new(i);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public T[] ToArray() => [X, Y];

    /// <see cref="IArray{,}"/>

    static Vector2M<T> IArray<Vector2M<T>, T>.Create(Vector2M<T> oldSelf, Array newSelf)
    {
        (oldSelf.X, oldSelf.Y) = newSelf.To<T[]>().To(i => (i[0], i[1]));
        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (ToArray() as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => IVector2.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector2M<T> IVector<Vector2M<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(IVector2.Format(value, type));
}