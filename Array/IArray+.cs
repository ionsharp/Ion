using System;
using System.Collections.Generic;

namespace Ion;

/// <inheritdoc/>
public interface IArray<T> : IArray, IEnumerable<T>
{
    new public T this[int x] { get; }
}

/// <summary>
/// An <see cref="IArray{}"/> that can create a new instance of itself from an <see cref="Array"/>.
/// </summary>
public interface IArray<TSelf, TValue> : IArray<TValue>, ISelf<TSelf> where TSelf : IArray<TSelf, TValue>
{
    public Array GetArray();

    public static abstract TSelf Create(TSelf oldSelf, Array newSelf);
}