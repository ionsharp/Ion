namespace Ion.Numeral;

/// <summary>
/// An <see cref="IArray"/> that has a related unfixed equivalent (if <see cref="IArrayUnfixed"/>, <see cref="{Alias}"/> is itself).
/// </summary>
public interface IArrayUnfixedAlias<TAlias, TValue> : IArray<TValue>
    where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>;

/// <inheritdoc/>
public interface IArrayUnfixedAlias<TSelf, TAlias, TValue> : IArrayUnfixedAlias<TAlias, TValue>
    where TSelf : IArray<TSelf, TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>;