namespace Ion.Numeral;

/// <summary>An <see cref="IVector"/> with a fixed length.</summary>
/// <remarks>
/// <para><b><see cref="IVector.Length"/> never varies</b></para>
/// </remarks>
public interface IVectorFixed : IVector, IArrayFixed;

/// <inheritdoc/>
public interface IVectorFixed<T> : IVectorFixed, IArrayFixed<T>, IVector<T>;