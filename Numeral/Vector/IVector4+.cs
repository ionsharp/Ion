using System;

namespace Ion.Numeral;

/// <inheritdoc/>
public interface IVector4<T> : IVector4, IVector3<T>
{
    new public T W { get; }

    new public (T X, T Y) XY { get; }

    public (T X, T Y, T Z) XYZ { get; }

    object IVector4.W => X;
}

/// <inheritdoc/>
public interface IVector4<TSelf, TValue> : IVector3<TSelf, TValue>, IVector4<TValue> where TSelf : IVector4<TSelf, TValue>
{
    Array IArray<TSelf, TValue>.GetArray() => new TValue[Length];
}