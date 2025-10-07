namespace Ion;

/// <summary>
/// An <see cref="IArray"/> with an unfixed length [0, ∞].
/// </summary>
public interface IArrayUnfixed : IArray;

/// <inheritdoc/>
public interface IArrayUnfixed<T> : IArrayUnfixed, IArray<T>;