namespace Ion.Numeral;

/// <summary>An <see cref="IVector"/> with a short length.</summary>
/// <remarks>
/// <para><b><see cref="IVector.Length"/> = [1, 4]</b></para>
/// </remarks>
public interface IVectorShort : IVector;

/// <inheritdoc/>
public interface IVectorShort<T> : IVectorShort, IVector<T>;