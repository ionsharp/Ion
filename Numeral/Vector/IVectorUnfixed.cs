namespace Ion.Numeral;

/// <summary>An <see cref="IVector"/> with an unfixed length.</summary>
/// <remarks>
/// <para><b><see cref="IVector.Length"/> may vary</b></para>
/// </remarks>
public interface IVectorUnfixed : IVector, IArrayUnfixed;

/// <inheritdoc/>
public interface IVectorUnfixed<T> : IVectorUnfixed, IArrayUnfixed<T>, IVector<T>;