using System;

namespace Ion;

/// <inheritdoc/>
public interface IArray2D<T> : IArray2D, IArray1D<T>
{
    new public T this[int y, int x] { get; }

#pragma warning disable CA1819 /// Properties should not return arrays
    new public T[] this[int y] { get; }
#pragma warning restore CA1819

    public T[] ToArray(int y);

    new public T[][] ToArray();

    int IArray.Length => XLength * YLength;

    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray<T>.this[int x] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray1D<T>.this[int x] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    object[] IArray1D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray1D<T>.ToArray() => throw new NotSupportedException();
}

/// <inheritdoc/>
public interface IArray2D<TSelf, TValue> : IArray2D<TValue>, IArray1D<TSelf, TValue> where TSelf : IArray2D<TSelf, TValue>;