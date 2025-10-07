using System;

namespace Ion.Reflect;

/// <summary>
/// A <see cref="Cache"/> that stores <see cref="{Instance}"/> by <see cref="Type"/>.
/// </summary>
/// <remarks>
/// Inherited types are considered unique/<see langword="interface"/> supported.
/// </remarks>
public class CacheByType<T>(bool Autoget = true, Func<Type, T> Autogetter = null) : Cache<Type, T>(Autoget, Autogetter)
{
    /// <inheritdoc/>
    public override Func<Type, T> AutogetterDefault => i => i.Create<T>();

    /// <inheritdoc/>
    public X Get<X>() where X : T => (X)this[typeof(X)];

    /// <inheritdoc/>
    public CacheByType(Func<Type, T> Autogetter) : this(true, Autogetter) { }
}

/// <inheritdoc/>
public class CacheByType(bool Autoget = true, Func<Type, object> Autogetter = null) : CacheByType<object>(Autoget, Autogetter)
{
    /// <inheritdoc/>
    public CacheByType(Func<Type, object> Autogetter) : this(true, Autogetter) { }
}