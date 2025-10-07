using Ion.Analysis;
using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList{T}"/> with a <see cref="ListLimit"/>.
/// </summary>
public interface IListLimited<T> : IList<T>, IListChanged<T>
{
    public ListLimit Limit { get; }
}

/// <summary>
/// Extends <see cref="IListLimited{T}"/>.
/// </summary>
[Extend(typeof(IListLimited<>))]
public static class XListLimited
{
    /// <summary>
    /// Assert <see cref="ListLimit"/> of <see cref="IListLimited{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Result AssertLimit<T>(this IListLimited<T> i)
    {
        Throw.IfNull(i, nameof(i));
        if (i.Count > i.Limit.Count)
        {
            if (i.Limit.Count > 0)
            {
                switch (i.Limit.Action)
                {
                    case ListLimitAction.Clear:
                        i.Clear();
                        break;
                    case ListLimitAction.RemoveFirst:
                        i.RemoveAt(0);
                        break;
                    case ListLimitAction.RemoveLast:
                        i.RemoveAt(i.Count - 1);
                        break;
                }
            }
            return true;
        }
        return false;
    }
}