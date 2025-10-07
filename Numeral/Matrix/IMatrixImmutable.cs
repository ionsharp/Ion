namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> that can't be changed.
/// </summary>
public interface IMatrixImmutable : IMatrix, IImmutable;

/// <inheritdoc/>
public interface IMatrixImmutable<T> : IMatrixImmutable, IMatrix<T>;