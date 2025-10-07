namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector"/> that can't be changed.
/// </summary>
public interface IVectorImmutable : IVector, IImmutable;

/// <inheritdoc/>
public interface IVectorImmutable<T> : IVectorImmutable, IVector<T>;