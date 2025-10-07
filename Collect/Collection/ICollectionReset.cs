using Ion.Core;
using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="ICollection{T}"/> that resets.
/// </summary>
public interface ICollectionReset<T> : ICollection<T>, IReset
{
    /// <summary>
    /// Get items used to reset <see cref="ICollection{T}"/>
    /// </summary>
    public IReadOnlyCollection<T> DefaultItems { get; }
}

/// <summary>
/// Extends <see cref="ICollectionReset{T}"/>.
/// </summary>
[Extend(typeof(ICollectionReset<>))]
public static class XCollectionReset
{
    /// <summary>
    /// Reset by clearing, then adding <see cref="ICollectionReset{T}.DefaultItems"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Reset<T>(this ICollectionReset<T> i)
    {
        Throw.IfNull(i, nameof(i));

        i.Clear();
        i.DefaultItems?.ForEach(i.Add);
    }
}