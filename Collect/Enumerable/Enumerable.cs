using Ion;
using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ion;

/// <summary>
/// Extends <see cref="IEnumerable"/>.
/// </summary>
/// <remarks>
/// <b><see cref="IEnumerable{T}"/> + <see langword="this"/>.Add(<see langword="params"/> []) = <i>Collection Expression</i></b>
/// <code>
///     <see cref="Type"/> myVar = [x, y, z];
/// </code>
/// </remarks>
[Extend(typeof(IEnumerable))]
public static partial class XEnumerable
{
    private const string _Empty = nameof(Enumerable) + " contains no elements";

    /// <summary>
    /// Get if all elements satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool All(this IEnumerable i, Condition<object> where)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var item in i)
        {
            if (!where(item))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Get if all elements are of type.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool All<T>(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var item in i)
        {
            if (item is not T)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Get if all elements are of type and satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool All<T>(this IEnumerable i, Condition<T> where)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var j in i)
        {
            if (j is not T || j is T k && !where(k))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Get if any elements.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Any(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var _ in i)
            return true;

        return false;
    }

    /// <summary>
    /// Get if any elements are of type.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Any<T>(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var j in i)
        {
            if (j is T)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get if any elements satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Any(this IEnumerable i, Condition<object> where)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var j in i)
        {
            if (where(j))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get if any elements of type satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Any<T>(this IEnumerable i, Condition<T> where)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var j in i)
        {
            if (j is T k)
            {
                if (where(k))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Get if contains given <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotComplete]
    public static bool Contains(this IEnumerable i, object item)
    {
        Throw.IfNull(i, nameof(i));

        foreach (var j in i)
        {
            /// Why is this needed to avoid exception?
            if (j is null) continue;
            if (j.Equals(item))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get count of elements.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int Count(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        var count = 0;
        foreach (var k in i) count++;
        return count;
    }

    /// <summary>
    /// Get count of elements.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int Count<T>(this IEnumerable i, Condition<T> where)
    {
        Throw.IfNull(i, nameof(i));
        var count = 0;
        foreach (var j in i)
        {
            if (j is T k)
            {
                if (where(k))
                    count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Get first element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static object First(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
            return item;

        throw new ArgumentOutOfRangeException(nameof(i));
    }

    /// <summary>
    /// Get first element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T First<T>(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
        {
            if (item is T j)
                return j;
        }
        throw new ArgumentOutOfRangeException(nameof(i));
    }

    /// <summary>
    /// Get first element or <see langword="default"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static object FirstOrDefault(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var j in i)
            return j;

        return default;
    }

    /// <summary>
    /// Get first element or <see langword="default"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T FirstOrDefault<T>(this IEnumerable i, Condition<T> where = null)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var j in i)
        {
            if (j is T k)
            {
                if (where is null || where(k))
                    return k;
            }
        }
        return default;
    }

    /// <summary>
    /// Do given <see cref="Void"/> for each element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach(this IEnumerable i, Void<object> each)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
            each(item);
    }

    /// <summary>
    /// Do given <see cref="Void"/> for each element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this IEnumerable i, Void<T> each)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
        {
            if (item is T j)
                each(j);
        }
    }

    /// <summary>
    /// Get index of element that equals given <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int IndexOf<T>(this IEnumerable i, T j)
    {
        Throw.IfNull(i, nameof(i));
        var index = 0;
        foreach (var x in i)
        {
            if (x is T y)
            {
                if (EqualityComparer<T>.Default.Equals(y, j))
                    return index;
            }
            index++;
        }
        return -1;
    }

    /// <summary>
    /// Get index of element that satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int IndexOf<T>(this IEnumerable i, Condition<T> where)
    {
        Throw.IfNull(i, nameof(i));
        var index = 0;
        foreach (var item in i)
        {
            if (item is T j)
            {
                if (where(j))
                    return index;
            }
            index++;
        }
        return -1;
    }

    /// <summary>
    /// Get last element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static object Last(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        object result = null;
        if (i.Any())
        {
            foreach (var item in i)
                result = item;
        }
        return result ?? throw new ArgumentOutOfRangeException(nameof(i));
    }

    /// <summary>
    /// Get last element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T Last<T>(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        object result = null;
        if (i.Any())
        {
            foreach (var item in i)
            {
                if (item is T j)
                    result = j;
            }
        }
        return (T)result ?? throw new ArgumentOutOfRangeException(nameof(i));
    }

    /// <summary>
    /// Get last element or <see langword="default"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static object LastOrDefault(this IEnumerable i)
    {
        Throw.IfNull(i, nameof(i));
        object result = null;
        foreach (var item in i)
            result = item;

        return result;
    }

    /// <summary>
    /// Get last element or <see langword="default"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T LastOrDefault<T>(this IEnumerable i, Condition<T> where = null)
    {
        Throw.IfNull(i, nameof(i));
        T result = default;
        foreach (var item in i)
        {
            if (item is T j)
            {
                if (where is null || where(j))
                    result = j;
            }
        }
        return result;
    }

    /// <summary>
    /// Get elements of <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<T> Select<T>(this IEnumerable i, Func<object, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        foreach (var j in i)
            yield return select(j);
    }

    /// <summary>
    /// Get elements of <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<T> Select<T>(this IEnumerable i, Func<int, object, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var index = 0;
        foreach (var j in i)
        {
            yield return select(index, j);
            index++;
        }
    }

    /// <summary>
    /// Get elements of <see cref="TNew"/> from elements of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<TNew> Select<TOld, TNew>(this IEnumerable i, Func<TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        foreach (var j in i)
        {
            if (j is TOld x)
                yield return select(x);
        }
    }

    /// <summary>
    /// Get elements of <see cref="TNew"/> from elements of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<TNew> Select<TOld, TNew>(this IEnumerable i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var index = 0;
        foreach (var j in i)
        {
            if (j is TOld x)
                yield return select(index, x);

            index++;
        }
    }

    /// <summary>
    /// Get as <see cref="string"/> with given delimiter and format.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString(this IEnumerable i, string delimit, Func<object, string> format = null)
        => i.ToString(delimit, format is null ? null : (_, j) => format(j));

    /// <summary>
    /// Get as <see cref="string"/> with given delimiter and format.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString(this IEnumerable i, string delimit, Func<int, object, string> format)
        => i.Cast<object>().ToString(delimit, format);

    /// <summary>
    /// Get elements of <see cref="{Value}"/> that satisfy given (optional) <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<T> Where<T>(this IEnumerable i, Condition<T> where = null)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
        {
            if (item is T j)
            {
                if (where is null || where(j))
                    yield return j;
            }
        }
    }
}

[Extend(typeof(IEnumerable<>))]
public static partial class XEnumerable
{
    /// <summary>
    /// Get <see cref="{Value}"/> by aggregating elements.
    /// </summary>
    /// <remarks>
    /// <see langword="var"/> result = <see langword="default"/>;<br/>
    /// <see langword="for"/> (<see cref="int"/> x = 0; x &lt; ... x++)<br/>
    /// ... result = <b>aggregate</b>(x, result, <b>i</b>[x]) <br/>
    /// <see langword="return"/> result;
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static T Aggregate<T>(this IEnumerable<T> i, Func<int, T, T, T> aggregate)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(aggregate, nameof(aggregate));

        var index = -1;
        return i.Aggregate((j, k) => { index++; return aggregate(index, j, k); });
    }

    /// <summary>
    /// Get <see cref="{New}"/> by aggregating elements of <see cref="{Old}"/>.
    /// </summary>
    /// <inheritdoc cref="Aggregate{Value}(IEnumerable{Value}, Func{int, Value, Value, Value})"/>
    public static TNew Aggregate<TOld, TNew>(this IEnumerable<TOld> i, Func<TNew, TOld, TNew> aggregate)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(aggregate, nameof(aggregate));

        TNew result = default;
        i.ForEach(j => result = aggregate(result, j));
        return result;
    }

    /// <summary>
    /// Get <see cref="{New}"/> by aggregating elements of <see cref="{Old}"/>.
    /// </summary>
    /// <inheritdoc cref="Aggregate{Value}(IEnumerable{Value}, Func{int, Value, Value, Value})"/>
    public static TNew Aggregate<TOld, TNew>(this IEnumerable<TOld> i, Func<int, TNew, TOld, TNew> aggregate)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(aggregate, nameof(aggregate));

        var index = 0;

        TNew result = default;
        i.ForEach(j => { result = aggregate(index, result, j); index++; });
        return result;
    }

    /// <summary>
    /// Concatenate with given <b>elements</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> i, params T[] elements)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(elements, nameof(elements));
        return i.Concat((IEnumerable<T>)elements);
    }

    /// <summary>
    /// Do given <see cref="Void"/> for each element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this IEnumerable<T> i, Void<T> each)
    {
        Throw.IfNull(i, nameof(i));
        foreach (var item in i)
            each(item);
    }

    /// <summary>
    /// Do given <see cref="Void"/> for each element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this IEnumerable<T> i, Void<int, T> each)
    {
        Throw.IfNull(i, nameof(i));
        var index = 0;
        foreach (var j in i)
        {
            each(index, j);
            index++;
        }
    }

    /// <summary>
    /// Get index of element that equals given <see cref="object"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int IndexOf<T>(this IEnumerable<T> i, T j)
        => ((IEnumerable)i).IndexOf(j);

    /// <summary>
    /// Get index of element that satisfies given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static int IndexOf<T>(this IEnumerable<T> i, Condition<T> where)
        => ((IEnumerable)i).IndexOf(where);

    /// <summary>
    /// Get least common element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Least<T>(this IEnumerable<T> i)
    {
        Throw.IfNull(i, nameof(i));
        return (from j in i group j by j into k orderby k.Count() ascending select k.Key).FirstOrDefault<T>();
    }

    /// <summary>
    /// Get maximum element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Maximum<T>(this IEnumerable<T> i) where T : IComparable
    {
        Throw.IfNull(i, nameof(i));
        T result = i.First<T>();
        foreach (T j in i)
        {
            if (j.CompareTo(result) > 0)
                result = j;
        }
        return result;
    }

    /// <summary>
    /// Get maximum element based on given projection.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static TValue Maximum<TValue, TKey>(this IEnumerable<TValue> i, Func<TValue, TKey> select)
        => i.Maximum(select, null);

    /// <summary>
    /// Get maximum element based on given projection.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static TValue Maximum<TValue, TKey>(this IEnumerable<TValue> i, Func<TValue, TKey> select, IComparer<TKey> compare)
    {
        ArgumentNullException.ThrowIfNull(i);
        ArgumentNullException.ThrowIfNull(select);
        compare ??= Comparer<TKey>.Default;

        using var iterator = i.GetEnumerator();
        if (!iterator.MoveNext())
            throw new InvalidOperationException(_Empty);

        var max = iterator.Current;
        var maxKey = select(max);
        while (iterator.MoveNext())
        {
            var candidate = iterator.Current;
            var candidateProjected = select(candidate);
            if (compare.Compare(candidateProjected, maxKey) > 0)
            {
                max = candidate;
                maxKey = candidateProjected;
            }
        }
        return max;
    }

    /// <summary>
    /// Get element with longest <see cref="string"/> representation.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Value<T, string> MaximumString<T>(this IEnumerable<T> i, string format = null, IFormatProvider provider = null)
    {
        ArgumentNullException.ThrowIfNull(i, nameof(i));
        return i.Maximum(j => j.IfGet<IFormattable, int>(k => k.ToString(format, provider).Length, k => k.ToString().Length)).To(j => (j, j.IfGet<IFormattable, string>(k => k.ToString(format, provider), k => k.ToString())));
    }

    /// <summary>
    /// Get minimum element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TValue Minimum<TValue>(this IEnumerable<TValue> i) where TValue : IComparable
    {
        ArgumentNullException.ThrowIfNull(i, nameof(i));
        TValue result = i.First<TValue>();
        foreach (TValue j in i)
        {
            if (j.CompareTo(result) < 0)
                result = j;
        }
        return result;
    }

    /// <summary>
    /// Get minimum element based on given projection.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static TValue Minimum<TValue, TKey>(this IEnumerable<TValue> i, Func<TValue, TKey> select)
        => i.Minimum(select, null);

    /// <summary>
    /// Get minimum element based on given projection.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static TValue Minimum<TValue, TKey>(this IEnumerable<TValue> i, Func<TValue, TKey> select, IComparer<TKey> compare)
    {
        ArgumentNullException.ThrowIfNull(i);
        ArgumentNullException.ThrowIfNull(select);
        compare ??= Comparer<TKey>.Default;

        using var iterator = i.GetEnumerator();
        if (!iterator.MoveNext())
            throw new InvalidOperationException(_Empty);

        var min = iterator.Current;
        var minKey = select(min);
        while (iterator.MoveNext())
        {
            var candidate = iterator.Current;
            var candidateProjected = select(candidate);
            if (compare.Compare(candidateProjected, minKey) < 0)
            {
                min = candidate;
                minKey = candidateProjected;
            }
        }
        return min;
    }

    /// <summary>
    /// Get element with shortest <see cref="string"/> representation.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Value<T, string> MinimumString<T>(this IEnumerable<T> i, string format = null, IFormatProvider provider = null)
    {
        ArgumentNullException.ThrowIfNull(i, nameof(i));
        return i.Minimum(j => j.IfGet<IFormattable, int>(k => k.ToString(format, provider).Length, k => k.ToString().Length)).To(j => (j, j.IfGet<IFormattable, string>(k => k.ToString(format, provider), k => k.ToString())));
    }

    /// <summary>
    /// Get most common element.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Most<T>(this IEnumerable<T> i)
    {
        ArgumentNullException.ThrowIfNull(i, nameof(i));
        return (from j in i group j by j into k orderby k.Count() descending select k.Key).FirstOrDefault<T>();
    }

    /// <summary>
    /// Get elements of <see cref="TNew"/> from elements of <see cref="TOld"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<TNew> Select<TOld, TNew>(this IEnumerable<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        var index = 0;
        foreach (var j in i)
        {
            if (j is TOld x)
                yield return select(index, x);
        }
    }

    /// <summary>
    /// Get subarray at given <b>index</b> and of given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T[] Subarray<T>(this IEnumerable<T> i, int index, int length)
        => i.Subarray(index, length, j => j);

    /// <summary>
    /// Get subarray at given <b>index</b> and of given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T[] Subarray<T>(this IEnumerable<T> i, int index, int length, Func<T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.Subarray(index, length, (_, j) => select(j));
    }

    /// <summary>
    /// Get subarray at given <b>index</b> and of given <b>length</b>.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T[] Subarray<T>(this IEnumerable<T> i, int index, int length, Func<int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArgumentOutOfRangeException>(index < 0, nameof(index));
        Throw.If<ArgumentOutOfRangeException>(length < 0, nameof(length));
        Throw.If<ArgumentException>(i.Count() - index < length);

        T[] result = new T[length];
        i.ForEach((j, k) =>
        {
            if (j >= index && j < index + length)
                result[j - index] = select(j, k);
        });
        return result;
    }

    /// <summary>
    /// Get as <see cref="string"/> with given delimiter and format.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(this IEnumerable<T> i, string delimit, Func<T, string> format = null)
        => i.ToString(delimit, format is null ? null : (_, j) => format(j));

    /// <summary>
    /// Get as <see cref="string"/> with given delimiter and format.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(this IEnumerable<T> i, string delimit, Func<int, T, string> format)
    {
        Throw.IfNull(i, nameof(i));
        var result = new StringBuilder();

        var j = 0;
        var k = i.Count() - 1;
        foreach (var item in i)
        {
            _ = result.Append(format?.Invoke(j, item) ?? $"{item}");
            if (j < k)
                _ = result.Append(delimit);

            j++;
        }
        return result.ToString();
    }

    /// <summary>
    /// Get elements that satisfy given <see cref="Condition"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<T> Where<T>(this IEnumerable<T> i, Condition<int, T> where)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(where, nameof(where));

        var index = 0;
        foreach (var j in i)
        {
            if (where(index, j))
                yield return j;

            index++;
        }
    }
}