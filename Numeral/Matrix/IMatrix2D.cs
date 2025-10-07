using System;

namespace Ion.Numeral;

/// <summary>
/// A 2-dimensional <see cref="IMatrix"/>.
/// </summary>
public interface IMatrix2D : IMatrix, IArray2D, IArray2DRank;

/// <inheritdoc/>
public interface IMatrix2D<T> : IMatrix2D, IMatrix<T>, IArray2D<T>, IArray2DRank<T>
{
    /// <remarks><b>Illogical to support!</b></remarks>
    object[] IArray1D.ToArray() => throw new NotSupportedException();

    /// <remarks><b>Illogical to support!</b></remarks>
    T[] IArray1D<T>.ToArray() => throw new NotSupportedException();
}

/// <inheritdoc/>
public interface IMatrix2D<TSelf, TValue> : IMatrix2D<TValue>, IArray2D<TSelf, TValue> where TSelf : IMatrix2D<TSelf, TValue>;