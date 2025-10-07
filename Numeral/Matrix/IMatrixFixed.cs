namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> with a fixed length.
/// </summary>
/// <remarks><see cref="IMatrix.Columns"/> and <see cref="IMatrix.Rows"/> are always the same.</remarks>
public interface IMatrixFixed : IMatrix, IArrayFixed;

/// <inheritdoc/>
public interface IMatrixFixed<T> : IMatrixFixed, IArrayFixed<T>, IMatrix<T>;