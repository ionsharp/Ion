namespace Ion;

/// <inheritdoc/>
public interface IArray1D<T> : IArray1D, IArray<T>
{
    new public T this[int x] { get; }

    new public T[] ToArray();

    object[] IArray1D.ToArray() => [.. ToArray()];
}

/// <inheritdoc/>
public interface IArray1D<TSelf, TValue> : IArray1D<TValue>, IArray<TSelf, TValue> where TSelf : IArray1D<TSelf, TValue>;