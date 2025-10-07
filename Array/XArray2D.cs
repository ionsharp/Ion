using System;
using System.Collections.Generic;

namespace Ion;

[Extend(typeof(object[,]))]
[Using(typeof(Array2D))]
public static partial class XArray2D
{
#pragma warning disable CA1814
    /// Prefer jagged arrays
    /// <summary>
    /// Get new instance of 2-dimensional <see cref="object"/>[,] from given jagged <see cref="object"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[,] As<T>(this T[][] i)
    {
        Throw.IfNull(i, nameof(i));

        var length = i.GetLength();

        var result = new T[length.Y, length.X];
        Array2D.Do(length.Y, length.X, (y, x) => result[y, x] = i[y][x]);
        return result;
    }

    /// <summary>
    /// Get new instance of jagged <see cref="object"/>[][] from given 2-dimensional <see cref="object"/>[,].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] As<T>(this T[,] i)
    {
        Throw.IfNull(i, nameof(i));
        return Array2D.Get(i.GetLength(0), i.GetLength(1), (y, x) => i[y, x]);
    }
#pragma warning restore CA1814
}

[Extend(typeof(object[][]))]
[Using(typeof(Array2D))]
public static partial class XArray2D
{
    /// <summary>
    /// Get if contains given <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains<T>(this T[][] i, T j) => i.IndexOf(j) != (-1, -1);

    /// <summary>
    /// Do given <b>action</b> for each value.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this T[][] i, Void<int, int, T> action)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(action, nameof(action));

        var length = i.GetLength();
        Array2D.Do(length.Y, length.X, (y, x) => action(y, x, i[y][x]));
    }

    /// <summary>
    /// Get 2-dimensional length.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (int Y, int X) GetLength<T>(this T[][] i)
    {
        Throw.IfNull(i, nameof(i));
        return (i.Length, i[0].Length);
    }

    /// <summary>
    /// Get index of given <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (int X, int Y) IndexOf<T>(this T[][] i, T j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));

        var length = i.GetLength();
        for (var y = 0; y < length.Y; y++)
        {
            for (var x = 0; x < length.X; x++)
            {
                if (EqualityComparer<T>.Default.Equals(i[y][x], j))
                    return (y, x);
            }
        }
        return (-1, -1);
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="T"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] ToArray<T>(this T[][] i, Func<T, T> select)
        => i.ToArray<T>((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="T"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] ToArray<T>(this T[][] i, Func<int, T, T> select)
    {
        var index = -1;
        return i.ToArray<T>((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="T"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] ToArray<T>(this T[][] i, Func<int, int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var length = i.GetLength();
        return Array2D.Get(length.Y, length.X, (y, x) => select(y, x, i[y][x]));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="TOld"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][] ToArray<TOld, TNew>(this TOld[][] i, Func<TOld, TNew> select)
        => i.ToArray((_, _, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="TOld"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][] ToArray<TOld, TNew>(this TOld[][] i, Func<int, TOld, TNew> select)
    {
        var index = -1;
        return i.ToArray((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="TOld"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][] ToArray<TOld, TNew>(this TOld[][] i, Func<int, int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var length = i.GetLength();
        return Array2D.Get(length.Y, length.X, (y, x) => select(y, x, i[y][x]));
    }
}

[Extend(typeof(IArray2DRank<>))]
[Using(typeof(Array2D))]
public static partial class XArray2D
{
    /// <inheritdoc cref="Contains{T}(T[][], T)"/>
    public static bool Contains<T>(this IArray2DRank<T> i, T j) => i.IndexOf(j) != (-1, -1);

    /// <inheritdoc cref="ForEach{T}(T[][], Void{int, int, int, T})"/>
    public static void ForEach<T>(this IArray2DRank<T> i, Void<int, int, T> action)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(action, nameof(action));
        Array2D.Do(i.YLength, i.XLength, (y, x) => action(y, x, i[y][x]));
    }

    /// <inheritdoc cref="IndexOf{T}(T[][], T)"/>
    public static (int X, int Y) IndexOf<T>(this IArray2DRank<T> i, T j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        for (var y = 0; y < i.YLength; y++)
        {
            for (var x = 0; x < i.XLength; x++)
            {
                if (EqualityComparer<T>.Default.Equals(i[y][x], j))
                    return (y, x);
            }
        }
        return (-1, -1);
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][], Func{T, T})"/>
    public static T[][] ToArray<T>(this IArray2DRank<T> i, Func<T, T> select)
        => i.ToArray<T>((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][], Func{int, T, T})"/>
    public static T[][] ToArray<T>(this IArray2DRank<T> i, Func<int, T, T> select)
    {
        var index = -1;
        return i.ToArray<T>((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][], Func{int, int, T, T})"/>
    public static T[][] ToArray<T>(this IArray2DRank<T> i, Func<int, int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array2D.Get(i.YLength, i.XLength, (y, x) => select(y, x, i[y][x]));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][], Func{TOld, TNew})"/>
    public static TNew[][] ToArray<TOld, TNew>(this IArray2DRank<TOld> i, Func<TOld, TNew> select)
        => i.ToArray((_, _, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][], Func{int, TOld, TNew})"/>
    public static TNew[][] ToArray<TOld, TNew>(this IArray2DRank<TOld> i, Func<int, TOld, TNew> select)
    {
        var index = -1;
        return i.ToArray((_, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][] from <see cref="IArray2DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][], Func{int, int, TOld, TNew})"/>
    public static TNew[][] ToArray<TOld, TNew>(this IArray2DRank<TOld> i, Func<int, int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array2D.Get(i.YLength, i.XLength, (y, x) => select(y, x, i[y][x]));
    }
}

[Extend(typeof(IArray2D<,>))]
[Using(typeof(Array2D))]
public static partial class XArray2D
{
    /// <summary>
    /// Get new instance of <see cref="TSelf"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf New<TSelf, TValue>(this IArray2D<TSelf, TValue> i, Func<int, int, TValue, TValue> select)
        where TSelf : IArray2D<TSelf, TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var j = (TValue[][])i.GetArray();

        var length = j.GetLength();
        Array2D.Do(length.Y, length.X, (y, x) =>
        {
            j[y] ??= new TValue[length.X];
            j[y][x] = select(y, x, j[y][x]);
        });

        Throw.If<NotSupportedException>(j is null, nameof(j));
        return TSelf.Create((TSelf)i, j);
    }
}