using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion;

[Extend(typeof(IArray<,>))]
[Extend(typeof(IArray1DRank<>))]
public static partial class XArray
{
    /// <summary>Get new instance by flipping order.</summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [1, 2, 3] ⇒ [3, 2, 1]
    /// </remarks>
    public static TSelf Flip<TSelf, TValue>(this IArray<TSelf, TValue> i)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> => i.New((x, y) => i[i.Length - 1 - x]);

    /// <summary>
    /// Get new instance where each <see cref="TValue"/> equal to <b>Replace</b> is replaced with given <see cref="TValue"/> <b>With</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Replace<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue Replace, TValue With)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue>
    {
        Throw.IfNull(i, nameof(i));
        return i.New((x, y) => EqualityComparer<TValue>.Default.Equals(y, Replace) ? With : y);
    }

    /// <summary>
    /// Get new instance where each <see cref="TValue"/> that satisfies given condition <b>Replace</b> is replaced with given <see cref="TValue"/> <b>With</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Replace<TSelf, TValue>(this IArray<TSelf, TValue> i, Condition<TValue> Replace, TValue With)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue>
    {
        Throw.IfNull(i, nameof(i));
        return i.New((x, y) => Replace(y) ? With : y);
    }

    /// <summary>
    /// Get new instance with each <see cref="TValue"/> in both <b>i</b> and <b>Replace</b> replaced with given <see cref="TValue"/> <b>With</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Replace<TSelf, TValue>(this IArray<TSelf, TValue> i, IEnumerable<TValue> Replace, TValue With)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(Replace, nameof(Replace));
        return i.New((x, y) => Replace.Contains(y) ? With : y);
    }
}

[Extend(typeof(IArray<,>))]
[Extend(typeof(IArray1DRank<>))]
[Extend(typeof(IArrayUnfixed<>))]
public static partial class XArray
{
    /// <summary>
    /// Set <see cref="TValue"/> at <b>aIndex</b> with <see cref="TValue"/> at <b>bIndex</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorInvalidIndex"/>
    public static TSelf Swap<TSelf, TValue>(this IArray<TSelf, TValue> i, int aIndex, int bIndex)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue>, IArrayUnfixed<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<VectorInvalidIndex>(!IVector.IsValidIndex(aIndex, i.Length), nameof(aIndex));
        Throw.If<VectorInvalidIndex>(!IVector.IsValidIndex(aIndex, i.Length), nameof(bIndex));

        TValue x = i[aIndex], y = i[bIndex];
        return i.New((j, k) => j == aIndex ? y : j == bIndex ? x : k);
    }
}

[Extend(typeof(IArray1DRank<>))]
[Extend(typeof(IArrayUnfixedAlias<,,>))]
public static partial class XArray
{
    /// <summary>
    /// Insert given <see cref="TValue"/> <b>b</b> to end.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Insert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> a, TValue b)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
        => a.Insert([b]);

    /// <summary>
    /// Insert each <see cref="TValue"/> in <see cref="IEnumerable"/> <b>b</b> to end.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Insert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> a, IEnumerable<TValue> b)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
        => a.InsertAt(a.Length, b);

    /// <summary>
    /// Insert given <see cref="TValue"/> <b>b</b> at given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsNotOfRange"/>
    public static TAlias InsertAt<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> a, int index, TValue b)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
        => a.InsertAt(index, [b]);

    /// <summary>
    /// Insert each <see cref="TValue"/> in <see cref="IEnumerable"/> <b>b</b> at given <b>index</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsNotOfRange"/>
    [NotOptimized]
    public static TAlias InsertAt<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> a, int index, IEnumerable<TValue> b)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
    {
        Throw.IfNull(a, nameof(a));
        Throw.IfNull(b, nameof(b));
        Throw.IfNotOfRange(index, 0, a.Length, nameof(index));

        TValue[] result = [];
        Array1D.Do(a.Length, i => result = i == index ? [.. result, .. b] : [.. result, (TValue)a[i]]);
        return TAlias.Create((TAlias)a, result);
    }

    /// <summary>
    /// Desert given <see cref="TValue"/> <b>j</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, TValue j)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue> => i.Desert([j]);

    /// <summary>
    /// Desert each <see cref="TValue"/> that satisfies given <see cref="Condition"/> <b>Where</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    [NotOptimized]
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, Condition<TValue> Where)
           where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(Where, nameof(Where));
        return TAlias.Create((TAlias)i, i.Where(j => !Where(j)).ToArray());
    }

    /// <summary>
    /// Desert each <see cref="TValue"/> that satisfies given <see cref="Condition"/> <b>Where</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, Condition<int, TValue> Where)
           where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
    {
        var index = -1;
        return i.Desert(j => { index++; return !Where(index, j); });
    }

    /// <summary>
    /// Desert each <see cref="TValue"/> in given <see cref="IEnumerable"/> <b>j</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, IEnumerable<TValue> j)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue> => i.Desert(x => j.Contains(x));

    /// <summary>
    /// Desert each <see cref="TValue"/> in given <see cref="IEnumerable"/> <b>Where</b> that also satisfies given <see cref="Condition"/> <b>And</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, IEnumerable<TValue> Where, Condition<TValue> And)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue> => i.Desert(i => Where.Contains(i) && And(i));

    /// <summary>
    /// Desert each <see cref="TValue"/> in given <see cref="IEnumerable"/> <b>Where</b> that also satisfies given <see cref="Condition"/> <b>And</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias Desert<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, IEnumerable<TValue> Where, Condition<int, TValue> And)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue> => i.Desert((j, k) => Where.Contains(k) && And(j, k));

    /// <summary>
    /// Desert each <see cref="TValue"/> with index of range [<b>index</b>, <b>index</b> + <b>length</b>].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentIsNegativeOrZero"/>
    /// <exception cref="ArgumentIsNotOfRange"/>
    public static TAlias DesertAt<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i, int index, int length = 1)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue>
    {
        Throw.IfNotOfRange(index, 0, i.Length - 1, nameof(index));
        Throw.IfNegativeOrZero(length, nameof(length));
        return i.Desert((x, _) => x < index && x >= index + length);
    }

    /// <summary>
    /// Desert last <see cref="TValue"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TAlias DesertLast<TSelf, TAlias, TValue>(this IArrayUnfixedAlias<TSelf, TAlias, TValue> i)
        where TSelf : IArray<TSelf, TValue>, IArray1DRank<TValue> where TAlias : IArray<TAlias, TValue>, IArrayUnfixed<TValue> => i.DesertAt(i.Length - 1);
}