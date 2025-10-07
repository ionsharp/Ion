namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> that has a related unfixed equivalent (if <see cref="IMatrixUnfixed{Value}"/>, <see cref="{Alias}"/> is itself).
/// </summary>
public interface IMatrixUnfixedAlias<TAlias, TValue>
    : IMatrix<TValue>, IArrayUnfixedAlias<TAlias, TValue> where TAlias : IMatrix<TAlias, TValue>, IMatrixUnfixed<TValue>;