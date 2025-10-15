using System;

namespace Ion.Numeral;

/// <inheritdoc/>
public interface IVector2<T> : IVector2, IVectorFixed<T>
{
    new public T X { get; }

    new public T Y { get; }

    object IVector2.X => X;

    object IVector2.Y => Y;

    [NotComplete]
    T IArray<T>.this[int i] => default;

    [NotComplete]
    T IArray1D<T>.this[int i] => default;
}

/// <inheritdoc/>
public interface IVector2<TSelf, TValue> : IVector<TSelf, TValue>, IVector2<TValue> where TSelf : IVector2<TSelf, TValue>
{
    Array IArray<TSelf, TValue>.GetArray() => new TValue[Length];
}