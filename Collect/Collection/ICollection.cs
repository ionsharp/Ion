using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// Extends <see cref="ICollection"/>, <see cref="ICollection{T}"/>.
/// </summary>
[Extend(typeof(ICollection))]
public static partial class XCollection;

[Extend(typeof(ICollection<>))]
public static partial class XCollection
{
    /// <summary>
    /// Add given <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static void AddRange<T>(this ICollection<T> i, IEnumerable<T> items)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(items, nameof(items));
        Throw.If<InvalidOperationException>(i is Array);

        using IEnumerator<T> e = items.GetEnumerator();
        while (e.MoveNext())
            i.Add(e.Current);
    }

    /// <summary>
    /// Remove given <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static void RemoveRange<T>(this ICollection<T> i, IEnumerable<T> items)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(items, nameof(items));
        Throw.If<InvalidOperationException>(i is Array);

        using IEnumerator<T> e = items.GetEnumerator();
        while (e.MoveNext())
            i.Remove(e.Current);
    }
}