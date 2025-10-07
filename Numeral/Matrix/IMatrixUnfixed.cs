namespace Ion.Numeral;

/// <summary>
/// An <see cref="IMatrix"/> with an unfixed length.
/// </summary>
/// <remarks><see cref="IMatrix.Columns"/> and <see cref="IMatrix.Rows"/> are always the same.</remarks>
public interface IMatrixUnfixed : IMatrix, IArrayUnfixed;

/// <inheritdoc/>
public interface IMatrixUnfixed<T> : IMatrixUnfixed, IArrayUnfixed<T>, IMatrix<T>;