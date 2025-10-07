using System;

namespace Ion;

[Extend(typeof(object[]))]
[Using(typeof(Array1D))]
public static partial class XArray1D
{
    /// <summary>
    /// Get new instance of <see cref="T"/>[] from <see cref="T"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[] ToArray<T>(this T[] i, Func<T, T> select)
        => i.ToArray<T>((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="T"/>[] from <see cref="T"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[] ToArray<T>(this T[] i, Func<int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array1D.Get(i.Length, j => i[j]);
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[] from <see cref="TOld"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[] ToArray<TOld, TNew>(this TOld[] i, Func<TOld, TNew> select)
        => i.ToArray((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[] from <see cref="TOld"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[] ToArray<TOld, TNew>(this TOld[] i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array1D.Get(i.Length, j => select(j, i[j]));
    }
}

[Extend(typeof(IArray1DRank<>))]
[Using(typeof(Array1D))]
public static partial class XArray1D
{
    /// <summary>
    /// Get new instance of <see cref="T"/>[] from <see cref="IArray1DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[], Func{T, T})"/>
    public static T[] ToArray<T>(this IArray1DRank<T> i, Func<T, T> select)
        => i.ToArray<T>((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="T"/>[] from <see cref="IArray1DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[], Func{int, T, T})"/>
    public static T[] ToArray<T>(this IArray1DRank<T> i, Func<int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array1D.Get(i.Length, j => i[j]);
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[] from <see cref="IArray1DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[], Func{TOld, TNew})"/>
    public static TNew[] ToArray<TOld, TNew>(this IArray1DRank<TOld> i, Func<TOld, TNew> select)
        => i.ToArray((_, j) => select(j));

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[] from <see cref="IArray1DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[], Func{int, TOld, TNew})"/>
    public static TNew[] ToArray<TOld, TNew>(this IArray1DRank<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array1D.Get(i.Length, j => select(j, i[j]));
    }
}

[Extend(typeof(IArray1D<,>))]
[Using(typeof(Array1D))]
public static partial class XArray1D
{
    /// <summary>
    /// Get new instance of <see cref="TSelf"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf New<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> => i.New(j => j);

    /// <summary>
    /// Get new instance of <see cref="TSelf"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf New<TSelf, TValue>(this IArray<TSelf, TValue> i, Func<TValue, TValue> select)
        where TSelf : IArray<TSelf, TValue>
    {
        Throw.IfNull(select, nameof(select));
        return i.New((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance of <see cref="TSelf"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf New<TSelf, TValue>(this IArray<TSelf, TValue> i, Func<int, TValue, TValue> select)
        where TSelf : IArray<TSelf, TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var array = i.GetArray();

        if (array is TValue[] a)
            Array1D.Do(array.Length, x => a[x] = select(x, a[x]));

        if (array is TValue[][] b)
        {
            var index = 0;
            var length = b.GetLength();
            Array2D.Do(length.Y, length.X, (y, x) =>
            {
                b[y] ??= new TValue[length.X];
                b[y][x] = select(index, b[y][x]);
                index++;
            });
        }

        if (array is TValue[][][] c)
        {
            var index = 0;
            var length = c.GetLength();
            Array3D.Do(length.Z, length.Y, length.X, (z, y, x) => index.Do(_ => c[z][y][x] = select(index, c[z][y][x]), _ => index++));
        }

        Throw.If<NotSupportedException>(array is null, nameof(array));
        return TSelf.Create((TSelf)i, array);
    }
}