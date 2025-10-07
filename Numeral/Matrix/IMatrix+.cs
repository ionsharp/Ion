using System;

namespace Ion.Numeral;

/// <inheritdoc/>
public interface IMatrix<T> : IMatrix, IArray2D<T>
{
    /// <remarks><b>Needed for terminology.</b></remarks>
    new T this[int row, int column] { get; }

    /// <remarks><b>Needed for terminology.</b></remarks>
    new public T[] ToArray(int row);

    /// <remarks><b>Illogical to support!</b></remarks>
    object[] IArray1D.ToArray() => throw new NotSupportedException();

    T[] IArray2D<T>.ToArray(int row) => ToArray(row);
}

/// <inheritdoc/>
public interface IMatrix<TSelf, TValue> : IMatrix<TValue>, IArray2D<TSelf, TValue> where TSelf : IMatrix<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Columns * Rows;
}