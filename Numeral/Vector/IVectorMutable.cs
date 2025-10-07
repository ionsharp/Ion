namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector"/> that can be changed.
/// </summary>
public interface IVectorMutable : IVector, IMutable;

/// <inheritdoc/>
public interface IVectorMutable<T> : IVectorMutable, IVector<T>;