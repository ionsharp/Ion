using Ion.Numeral;
using System;
using System.Collections;

namespace Ion;

/// <summary>
/// An <see cref="Array"/> with 3 dimensions.
/// </summary>
/// <remarks>
/// <b><see cref="Array"/> ≡ <see cref="IArray3D"/> ≡ <see cref="IEnumerable"/>[][] ≡ <see cref="T"/>[][][] ≡ <see cref="T"/>[,,]</b>
/// </remarks>
[Using(typeof(Throw))]
public static class Array3D
{
    /// <summary>
    /// Do <see cref="Void"/> for given 3-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> z = 0; z &lt; <b>zCount</b>; z++)<br/>
    /// ... <see langword="for"/> (<see cref="int"/> y = 0; y &lt; <b>yCount</b>; y++)<br/>
    /// ... ... <see langword="for"/> (<see cref="int"/> x = 0; x &lt; <b>xCount</b>; x++)<br/>
    /// </remarks>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int zCount, int yCount, int xCount, Void Do)
        => Array3D.Do(zCount, yCount, xCount, _ => Do());

    /// <inheritdoc cref="Do(int, int, int, Void)"/>
    public static void Do(int zCount, int yCount, int xCount, Void<int> Do)
    {
        var index = 0;
        Array3D.Do(zCount, yCount, xCount, (_, _, _) => index.Do(i => Do(i), _ => index++));
    }

    /// <inheritdoc cref="Do(int, int, int, Void)"/>
    public static void Do(int zCount, int yCount, int xCount, Void<int, int, int> Do)
    {
        Throw.IfLessOrEqual(zCount, 0, nameof(zCount));
        Throw.IfLessOrEqual(yCount, 0, nameof(yCount));
        Throw.IfLessOrEqual(xCount, 0, nameof(xCount));
        Array3D.Do(0, zCount, 0, yCount, 0, xCount, 1, 1, 1, Do);
    }

    /// <summary>
    /// Do <see cref="Void"/> for given 3-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> z = <b>zStart</b>; z &lt; <b>zCount</b>; z += <b>zIncrement</b>)<br/>
    /// ... <see langword="for"/> (<see cref="int"/> y = <b>yStart</b>; y &lt; <b>yCount</b>; y += <b>yIncrement</b>)<br/>
    /// ... ... <see langword="for"/> (<see cref="int"/> x = <b>xStart</b>; x &lt; <b>xCount</b>; x += <b>xIncrement</b>)<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int zStart, int zCount, int yStart, int yCount, int xStart, int xCount, int zIncrement, int yIncrement, int xIncrement, Void Do)
        => Array3D.Do(zStart, zCount, yStart, yCount, xStart, xCount, zIncrement, yIncrement, xIncrement, _ => Do());

    /// <inheritdoc cref="Do(int, int, int, int, int, int, int, int, int, Void)"/>
    public static void Do(int zStart, int zCount, int yStart, int yCount, int xStart, int xCount, int zIncrement, int yIncrement, int xIncrement, Void<int> Do)
    {
        var index = 0;
        Array3D.Do(zStart, zCount, yStart, yCount, xStart, xCount, zIncrement, yIncrement, xIncrement, (_, _, _) => index.Do(i => Do(i), _ => index++));
    }

    /// <inheritdoc cref="Do(int, int, int, int, int, int, int, int, int, Void)"/>
    public static void Do(int zStart, int zCount, int yStart, int yCount, int xStart, int xCount, int zIncrement, int yIncrement, int xIncrement, Void<int, int, int> Do)
    {
        Throw.IfNull(Do, nameof(Do));
        for (var z = zStart; z < zCount; z += zIncrement)
        {
            for (var y = yStart; y < yCount; y += yIncrement)
            {
                for (var x = xStart; x < xCount; x += xIncrement)
                    Do(z, y, x);
            }
        }
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, and <b>xLength</b>, and <see langword="default"/> <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength) => Get(zLength, yLength, xLength, default(T));

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, T value)
    {
        Throw.IfNull(value, nameof(value));
        return Get(zLength, yLength, xLength, () => value);
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, Func<T> value)
    {
        Throw.IfNull(value, nameof(value));
        return Get(zLength, yLength, xLength, _ => value());
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, Func<int, T> value)
    {
        Throw.IfNull(value, nameof(value));

        var index = -1;
        return Get(zLength, yLength, xLength, (_, _, _) => { index++; return value(index); });
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, Func<int, int, int, T> value)
    {
        Throw.IfNull(value, nameof(value));

        Throw.IfLessOrEqual(zLength, 0, nameof(zLength));
        Throw.IfLessOrEqual(yLength, 0, nameof(yLength));
        Throw.IfLessOrEqual(xLength, 0, nameof(xLength));

        var result = new T[yLength][][];
        for (var z = 0; z < zLength; z++)
        {
            result[z] = new T[yLength][];
            for (var y = 0; y < yLength; y++)
            {
                result[z][y] = new T[xLength];
                for (var x = 0; x < xLength; x++)
                    result[z][y][x] = value(z, y, x);
            }
        }
        return result;
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, T value, Func<T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        return Get(zLength, yLength, xLength, value, (_, i) => select(i));
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, T value, Func<int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));

        var index = -1;
        return Get(zLength, yLength, xLength, value, (_, _, _, i) => { index++; return select(index, i); });
    }

    /// <summary>
    /// Get <see cref="Array3D"/> with given <b>zLength</b>, <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][][] Get<T>(int zLength, int yLength, int xLength, T value, Func<int, int, int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));

        Throw.IfLessOrEqual(zLength, 0, nameof(yLength));
        Throw.IfLessOrEqual(yLength, 0, nameof(yLength));
        Throw.IfLessOrEqual(xLength, 0, nameof(xLength));

        var result = new T[zLength][][];
        for (var z = 0; z < zLength; z++)
        {
            result[z] = new T[yLength][];
            for (var y = 0; y < yLength; y++)
            {
                result[z][y] = new T[xLength];
                for (var x = 0; x < xLength; x++)
                    result[z][y][x] = select(z, y, x, value);
            }
        }
        return result;
    }

    /// <summary>
    /// Get as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(T[][][] i, string format, IFormatProvider provider)
    {
        Throw.IfNull(i, nameof(i));
        return default;
    }
}