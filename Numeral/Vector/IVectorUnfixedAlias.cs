namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector"/> that has a related unfixed equivalent (if <see cref="IVectorUnfixed"/>, <see cref="{Alias}"/> is itself).
/// </summary>
public interface IVectorUnfixedAlias<TSelf, TAlias, TValue> 
    : IVector<TValue>, IArrayUnfixedAlias<TSelf, TAlias, TValue> 
    where TSelf : IVector<TSelf, TValue> where TAlias : IVector<TAlias, TValue>, IVectorUnfixed<TValue>;