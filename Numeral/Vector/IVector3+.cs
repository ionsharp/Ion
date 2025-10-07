using System;

namespace Ion.Numeral;

/// <inheritdoc/>
public interface IVector3<T> : IVector3, IVector2<T>
{
    new public T Z { get; }

    public (T X, T Y) XY { get; }

    object IVector3.Z => X;
}

/// <inheritdoc/>
public interface IVector3<TSelf, TValue> : IVector2<TSelf, TValue>, IVector3<TValue> where TSelf : IVector3<TSelf, TValue>
{
    Array IArray<TSelf, TValue>.GetArray() => new TValue[Length];
}