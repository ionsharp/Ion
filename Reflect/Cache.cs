namespace Ion.Reflect;

/// <summary>
/// A way to access common data.
/// </summary>
public interface ICache;

/// <inheritdoc cref="ICache"/>
/// <summary>
/// Store multiple instances of each unique type.
/// </summary>
/// <remarks>
/// <b>Old instances are replaced when new ones are added.</b>
/// </remarks>
public abstract class Cache : ICache;