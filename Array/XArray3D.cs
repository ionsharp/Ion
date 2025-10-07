using System;
using System.Collections.Generic;

namespace Ion;

[Extend(typeof(object[,,]))]
[Using(typeof(Array3D))]
public static partial class XArray3D
{
#pragma warning disable CA1814 /// Prefer jagged arrays
    /// <summary>
    /// Get new instance of 3-dimensional <see cref="object"/>[,,] from given <see cref="object"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[,,] As<T>(this T[][][] i)
    {
        Throw.IfNull(i, nameof(i));

        var length = i.GetLength();

        var result = new T[length.Z, length.Y, length.X];
        Array3D.Do(length.Z, length.Y, length.X, (z, y, x) => result[z, y, x] = i[z][y][x]);
        return result;
    }

    /// <summary>
    /// Get new instance of jagged <see cref="object"/>[][][] from given 3-dimensional <see cref="object"/>[,,].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] As<T>(this T[,,] i)
    {
        Throw.IfNull(i, nameof(i));
        return Array3D.Get(i.GetLength(0), i.GetLength(1), i.GetLength(2), (z, y, x) => i[z, y, x]);
    }
#pragma warning restore CA1814
}

[Extend(typeof(object[][][]))]
[Using(typeof(Array3D))]
public static partial class XArray3D
{
    /// <summary>
    /// Get if contains given <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static bool Contains<T>(this T[][][] i, T j) => i.IndexOf(j) != (-1, -1, -1);

    /// <summary>
    /// Do given <b>action</b> for each value.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void ForEach<T>(this T[][][] i, Void<int, int, int, T> action)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(action, nameof(action));

        var length = i.GetLength();
        Array3D.Do(length.Z, length.Y, length.X, (z, y, x) => action(z, y, x, i[z][y][x]));
    }

    /// <summary>
    /// Get 3-dimensional length.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (int Z, int Y, int X) GetLength<T>(this T[][][] i)
    {
        Throw.IfNull(i, nameof(i));
        return (i.Length, i[0].Length, i[0][0].Length);
    }

    /// <summary>
    /// Get index of given <see cref="T"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (int X, int Y, int Z) IndexOf<T>(this T[][][] i, T j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));

        var length = i.GetLength();
        for (var z = 0; z < length.Z; z++)
        {
            for (var y = 0; y < length.Y; y++)
            {
                for (var x = 0; x < length.X; x++)
                {
                    if (EqualityComparer<T>.Default.Equals(i[z][y][x], j))
                        return (z, y, x);
                }
            }
        }
        return (-1, -1, -1);
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="T"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] ToArray<T>(this T[][][] i, Func<T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.ToArray<T>((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="T"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] ToArray<T>(this T[][][] i, Func<int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));

        var index = -1;
        return i.ToArray<T>((_, _, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="T"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] ToArray<T>(this T[][][] i, Func<int, int, int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var length = i.GetLength();
        return Array3D.Get(length.Z, length.Y, length.X, (z, y, x) => select(z, y, x, i[z][y][x]));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="TOld"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][][] ToArray<TOld, TNew>(this TOld[][][] i, Func<TOld, TNew> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.ToArray((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="TOld"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][][] ToArray<TOld, TNew>(this TOld[][][] i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(select, nameof(select));

        var index = -1;
        return i.ToArray((_, _, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="TOld"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TNew[][][] ToArray<TOld, TNew>(this TOld[][][] i, Func<int, int, int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var length = i.GetLength();
        return Array3D.Get(length.Z, length.Y, length.X, (z, y, x) => select(z, y, x, i[z][y][x]));
    }
}

[Extend(typeof(IArray3DRank<>))]
[Using(typeof(Array3D))]
public static partial class XArray3D
{
    /// <inheritdoc cref="Contains{T}(T[][][], T)"/>
    public static bool Contains<T>(this IArray3DRank<T> i, T j) => i.IndexOf(j) != (-1, -1, -1);

    /// <inheritdoc cref="ForEach{T}(T[][][], Void{int, int, int, T})"/>
    public static void ForEach<T>(this IArray3DRank<T> i, Void<int, int, int, T> action)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(action, nameof(action));
        Array3D.Do(i.ZLength, i.YLength, i.XLength, (z, y, x) => action(z, y, x, i[z][y][x]));
    }

    /// <inheritdoc cref="IndexOf{T}(T[][][], T)"/>
    public static (int X, int Y, int Z) IndexOf<T>(this IArray3DRank<T> i, T j)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        for (var z = 0; z < i.ZLength; z++)
        {
            for (var y = 0; y < i.YLength; y++)
            {
                for (var x = 0; x < i.XLength; x++)
                {
                    if (EqualityComparer<T>.Default.Equals(i[z][y][x], j))
                        return (z, y, x);
                }
            }
        }
        return (-1, -1, -1);
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][][], Func{T, T})"/>
    public static T[][][] ToArray<T>(this IArray3DRank<T> i, Func<T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.ToArray<T>((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][][], Func{int, T, T})"/>
    public static T[][][] ToArray<T>(this IArray3DRank<T> i, Func<int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));

        var index = -1;
        return i.ToArray<T>((_, _, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="T"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="T"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{T}(T[][][], Func{int, int, int, T, T})"/>
    public static T[][][] ToArray<T>(this IArray3DRank<T> i, Func<int, int, int, T, T> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array3D.Get(i.ZLength, i.YLength, i.XLength, (z, y, x) => select(z, y, x, i[z][y][x]));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][][], Func{TOld, TNew})"/>
    public static TNew[][][] ToArray<TOld, TNew>(this IArray3DRank<TOld> i, Func<TOld, TNew> select)
    {
        Throw.IfNull(select, nameof(select));
        return i.ToArray((_, j) => select(j));
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][][], Func{int, TOld, TNew})"/>
    public static TNew[][][] ToArray<TOld, TNew>(this IArray3DRank<TOld> i, Func<int, TOld, TNew> select)
    {
        Throw.IfNull(select, nameof(select));

        var index = -1;
        return i.ToArray((_, _, _, j) => { index++; return select(index, j); });
    }

    /// <summary>
    /// Get new instance of <see cref="TNew"/>[][][] from <see cref="IArray3DRank"/>&lt;<see cref="TOld"/>>.
    /// </summary>
    /// <inheritdoc cref="ToArray{TOld, TNew}(TOld[][][], Func{int, int, int, TOld, TNew})"/>
    public static TNew[][][] ToArray<TOld, TNew>(this IArray3DRank<TOld> i, Func<int, int, int, TOld, TNew> select)
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));
        return Array3D.Get(i.ZLength, i.YLength, i.XLength, (z, y, x) => select(z, y, x, i[z][y][x]));
    }
}

[Extend(typeof(IArray3D<,>))]
[Using(typeof(Array3D))]
public static partial class XArray3D
{
    /// <summary>
    /// Get new instance of <see cref="TSelf"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf New<TSelf, TValue>(this IArray3D<TSelf, TValue> i, Func<int, int, int, TValue, TValue> select)
        where TSelf : IArray3D<TSelf, TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(select, nameof(select));

        var array = i.GetArray();
        if (array is TValue[][][] j)
        {
            var length = j.GetLength();
            Array3D.Do(length.Z, length.Y, length.X, (z, y, x) =>
            {
                j[z] ??= new TValue[length.Y][];
                j[z][y] ??= new TValue[length.X];
                j[z][y][x] = select(z, y, x, j[z][y][x]);
            });
        }

        Throw.If<NotSupportedException>(array is null, nameof(array));
        return TSelf.Create((TSelf)i, array);
    }
}