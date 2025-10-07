using System.Collections.Generic;

namespace Ion.Numeral;

/// <inheritdoc/>
public interface IVector<T> : IVector, IArray1D<T>, IArray1DRank<T>;

/// <inheritdoc/>
public interface IVector<TSelf, TValue> : IVector<TValue>, IArray1D<TSelf, TValue> where TSelf : IVector<TSelf, TValue>
{
    public static abstract TSelf Create(VectorType type, IEnumerable<TValue> value);
}