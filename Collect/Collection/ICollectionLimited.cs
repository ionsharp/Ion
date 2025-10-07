using Ion.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="ICollection"/> with a <see cref="CollectionLimit"/>.
/// </summary>
/// <remarks>
/// Implements <see cref="ICollectionChanged{T}"/>.
/// </remarks>
public interface ICollectionLimited<T> : ICollection<T>, ICollectionChanged<T>
{
    public CollectionLimit Limit { get; }
}

/// <summary>
/// Extends <see cref="ICollectionLimited{T}"/>.
/// </summary>
[Extend(typeof(ICollectionLimited<>))]
public static class XCollectionLimited
{
    /// <summary>
    /// Assert <see cref="CollectionLimit"/> of <see cref="ICollectionLimited{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Result AssertLimit<T>(this ICollectionLimited<T> i)
    {
        Throw.IfNull(i, nameof(i));
        if (i.Count > i.Limit.Count)
        {
            if (i.Limit.Count > 0)
            {
                switch (i.Limit.Action)
                {
                    case CollectionLimitAction.Clear:
                        i.Clear();
                        break;
                }
            }
            return true;
        }
        return false;
    }
}