using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// Extends <see cref="IList"/>, <see cref="IList{T}"/>.
/// </summary>
[Extend(typeof(IList))]
public static partial class XList
{
    /// <summary>
    /// Insert given <b>item</b> above (or before) given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="NullReferenceException"/>
    public static void InsertAbove(this IList i, int index, object item)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<InvalidOperationException>(i is Array);

        index = index < 0
            ? 0 : index;

        if (index > -1 && index < i.Count)
            i.Insert(index, item);
    }

    /// <summary>
    /// Insert given <b>item</b> below (or after) given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="NullReferenceException"/>
    public static void InsertBelow(this IList i, int index, object item)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<InvalidOperationException>(i is Array);

        index = index < 0
            ? i.Count - 1 : index + 1;

        if (index > -1 && index < i.Count)
            i.Insert(index, item);
    }

    /// <summary>
    /// Add given <see cref="IEnumerable"/> at given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsGreater"/>
    /// <exception cref="ArgumentIsLess"/>
    /// <exception cref="InvalidOperationException"/>
    public static void InsertRange(this IList i, int index, IEnumerable items)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(items, nameof(items));

        Throw.If<InvalidOperationException>(i is Array);

        Throw.IfGreater(index, i.Count, nameof(index));
        Throw.IfLess(index, 0, nameof(index));

        items.ForEach(j => i.Insert(index, j));
    }

    /// <summary>
    /// Move item at given <b>index</b> to <b>index</b> + 1.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void MoveDown(this IList i, int index, bool wrap)
    {
        Throw.IfNull(i, nameof(i));
        var j = i[index];
        if (index + 1 <= i.Count - 2)
        {
            i.RemoveAt(index);
            i.Insert(index + 1, j);
        }
        else if (index + 1 <= i.Count - 1)
        {
            i.RemoveAt(index);
            _ = i.Add(j);
        }
        else if (wrap)
        {
            i.RemoveAt(index);
            i.Insert(0, j);
        }
    }

    /// <summary>
    /// Move item at given <b>index</b> to <b>index</b> - 1.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void MoveUp(this IList i, int index, bool wrap)
    {
        var a = new List<int>();

        Throw.IfNull(i, nameof(i));
        if (index - 1 >= 0)
        {
            var m = i[index - 1];
            i.RemoveAt(index - 1);
            i.Insert(index, m);
        }
        else if (wrap)
        {
            var n = i[index];
            i.RemoveAt(index);
            _ = i.Add(n);
        }
    }

    /// <summary>
    /// Remove items based on given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static void Remove(this IList i, Condition<object> where)
    {
        Throw.IfNull(where, nameof(where));
        i.Remove((_, j) => where(j));
    }

    /// <summary>
    /// Remove items based on given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static void Remove(this IList i, Condition<int, object> where)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(where, nameof(where));
        Throw.If<InvalidOperationException>(i is Array);

        for (var j = i.Count - 1; j >= 0; j--)
        {
            if (where(j, i[j]))
                i.RemoveAt(j);
        }
    }

    /// <summary>
    /// Remove (<b>n</b><i>th</i> to) last item.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void RemoveLast(this IList i, int n = 0)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentOutOfRangeException>(n < 0, nameof(n));
        Throw.If<ArgumentOutOfRangeException>(i.Count - 1 - n < 0, nameof(n));
        Throw.If<InvalidOperationException>(i is Array);
        i.RemoveAt(i.Count - 1 - n);
    }
}

[Extend(typeof(IList<>))]
public static partial class XList
{
    /// <inheritdoc cref="InsertAbove(IList, int, object)"/>
    public static void InsertAbove<T>(this IList<T> i, int index, T item)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<InvalidOperationException>(i is Array);

        index = index < 0
            ? 0 : index;

        if (index > -1 && index < i.Count)
            i.Insert(index, item);
    }

    /// <inheritdoc cref="InsertBelow(IList, int, object)"/>
    public static void InsertBelow<T>(this IList<T> i, int index, T item)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<InvalidOperationException>(i is Array);

        index = index < 0
            ? i.Count - 1 : index + 1;

        if (index > -1 && index < i.Count)
            i.Insert(index, item);
    }

    /// <inheritdoc cref="InsertRange(IList, int, IEnumerable)"/>
    public static void InsertRange<T>(this IList<T> i, int index, IEnumerable<T> items)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(items, nameof(items));

        Throw.If<InvalidOperationException>(i is Array);

        Throw.IfGreater(index, i.Count, nameof(index));
        Throw.IfLess(index, 0, nameof(index));

        items.ForEach(j => i.Insert(index, j));
    }

    /// <inheritdoc cref="MoveDown(IList, int, bool)"/>
    public static void MoveDown<T>(this IList<T> i, int index, bool wrap)
    {
        Throw.IfNull(i, nameof(i));
        var j = i[index];
        if (index + 1 <= i.Count - 2)
        {
            i.RemoveAt(index);
            i.Insert(index + 1, j);
        }
        else if (index + 1 <= i.Count - 1)
        {
            i.RemoveAt(index);
            i.Add(j);
        }
        else if (wrap)
        {
            i.RemoveAt(index);
            i.Insert(0, j);
        }
    }

    /// <inheritdoc cref="MoveUp(IList, int, bool)"/>
    public static void MoveUp<T>(this IList<T> i, int index, bool wrap)
    {
        var a = new List<int>();

        Throw.IfNull(i, nameof(i));
        if (index - 1 >= 0)
        {
            var m = i[index - 1];
            i.RemoveAt(index - 1);
            i.Insert(index, m);
        }
        else if (wrap)
        {
            var n = i[index];
            i.RemoveAt(index);
            i.Add(n);
        }
    }

    /// <inheritdoc cref="Remove(IList, Condition{object})"/>
    public static bool Remove<T>(this IList<T> i, Condition<T> where)
    {
        Throw.IfNull(where, nameof(where));
        return i.Remove((_, j) => where(j));
    }

    /// <inheritdoc cref="Remove(IList, Condition{object})"/>
    public static bool Remove<T>(this IList<T> i, Condition<int, T> where)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(where, nameof(where));
        Throw.If<InvalidOperationException>(i is Array);

        var result = true;
        for (var j = i.Count - 1; j >= 0; j--)
        {
            if (where(j, i[j]))
                result = result && i.Remove(i[j]);
        }
        return result;
    }

    /// <inheritdoc cref="RemoveLast(IList, int)"/>
    public static void RemoveLast<T>(this IList<T> i, int n = 0)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentOutOfRangeException>(n < 0, nameof(n));
        Throw.If<ArgumentOutOfRangeException>(i.Count - 1 - n < 0, nameof(n));
        Throw.If<InvalidOperationException>(i is Array);
        i.RemoveAt(i.Count - 1 - n);
    }
}