namespace Ion;

/// <summary>
/// An <see cref="IArray"/> with a fixed length [0, <i>n</i>].
/// </summary>
public interface IArrayFixed : IArray;

/// <inheritdoc/>
public interface IArrayFixed<T> : IArrayFixed, IArray<T>;