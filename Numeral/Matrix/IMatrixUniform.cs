namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> with a uniform length (same number of columns and rows).
/// </summary>
public interface IMatrixUniform : IMatrix;

/// <inheritdoc/>
public interface IMatrixUniform<T> : IMatrixUniform, IMatrix<T>;