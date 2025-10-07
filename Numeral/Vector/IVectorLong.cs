namespace Ion.Numeral;

/// <summary>An <see cref="IVector"/> with a long length.</summary>
/// <remarks>
/// <para><b><see cref="IVector.Length"/> = [5, +∞]</b></para>
/// </remarks>
public interface IVectorLong : IVector;

/// <inheritdoc/>
public interface IVectorLong<T> : IVectorLong, IVector<T>;