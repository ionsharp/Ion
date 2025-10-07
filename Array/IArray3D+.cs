using System;

namespace Ion;

/// <inheritdoc/>
public interface IArray3D<T> : IArray3D, IArray2D<T>
{
    new public T this[int z, int y, int x] { get; }

#pragma warning disable CA1819 /// Properties should not return arrays
    new public T[] this[int z, int y] { get; }

    new public T[][] this[int z] { get; }
#pragma warning restore CA1819

    public T[] ToArray(int z, int y);

    new public T[][] ToArray(int z);

    new public T[][][] ToArray();

    int IArray.Length => XLength * YLength * ZLength;

    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray<T>.this[int x] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray1D<T>.this[int x] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T IArray2D<T>.this[int y, int x] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray2D<T>.this[int y] => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    object[] IArray1D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray1D<T>.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    object[][] IArray2D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray2D<T>.ToArray(int y) => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[][] IArray2D<T>.ToArray() => throw new NotSupportedException();

    (int Z, int Y, int X) IArray3D.Length => (ZLength, YLength, XLength);
}

/// <inheritdoc/>
public interface IArray3D<TSelf, TValue> : IArray3D<TValue>, IArray2D<TSelf, TValue> where TSelf : IArray3D<TSelf, TValue>;