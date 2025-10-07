namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> that can be changed.
/// </summary>
public interface IMatrixMutable : IMatrix, IMutable;

/// <inheritdoc/>
public interface IMatrixMutable<T> : IMatrixMutable, IMatrix<T>;