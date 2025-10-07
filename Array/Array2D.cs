using Ion.Numeral;
using System;
using System.Collections;

namespace Ion;

/// <summary>
/// An <see cref="Array"/> with 2 dimensions.
/// </summary>
/// <remarks>
/// <b><see cref="Array"/> ≡ <see cref="IArray2D"/> ≡ <see cref="IEnumerable"/>[] ≡ <see cref="T"/>[][] ≡ <see cref="T"/>[,]</b>
/// </remarks>
[Using(typeof(Throw))]
public static class Array2D
{
    /// <summary>
    /// Do <see cref="Void"/> for given 2-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> i = 0; i &lt; <b>yCount</b>; i++)<br/>
    /// ... <see langword="for"/> (<see cref="int"/> i = 0; i &lt; <b>xCount</b>; i++)<br/>
    /// </remarks>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int yCount, int xCount, Void Do)
        => Array2D.Do(yCount, xCount, _ => Do());

    /// <inheritdoc cref="Do(int, int, Void)"/>
    public static void Do(int yCount, int xCount, Void<int> Do)
    {
        var index = 0;
        Array2D.Do(yCount, xCount, (_, _) => index.Do(i => Do(i), _ => index++));
    }

    /// <inheritdoc cref="Do(int, int, Void)"/>
    public static void Do(int yCount, int xCount, Void<int, int> Do)
    {
        Throw.IfLessOrEqual(yCount, 0, nameof(yCount));
        Throw.IfLessOrEqual(xCount, 0, nameof(xCount));
        Array2D.Do(0, yCount, 0, xCount, 1, 1, Do);
    }

    /// <summary>
    /// Do <see cref="Void"/> for given 2-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> i = <b>yStart</b>; i &lt; <b>yCount</b>; i += <b>yIncrement</b>)<br/>
    /// ... <see langword="for"/> (<see cref="int"/> i = <b>xStart</b>; i &lt; <b>xCount</b>; i += <b>xIncrement</b>)<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int yStart, int yCount, int xStart, int xCount, int yIncrement, int xIncrement, Void Do)
        => Array2D.Do(yStart, yCount, xStart, xCount, yIncrement, xIncrement, _ => Do());

    /// <inheritdoc cref="Do(int, int, int, int, int, int, Void)"/>
    public static void Do(int yStart, int yCount, int xStart, int xCount, int yIncrement, int xIncrement, Void<int> Do)
    {
        var index = 0;
        Array2D.Do(yStart, yCount, xStart, xCount, yIncrement, xIncrement, (_, _) => index.Do(i => Do(i), _ => index++));
    }

    /// <inheritdoc cref="Do(int, int, int, int, int, int, Void)"/>
    public static void Do(int yStart, int yCount, int xStart, int xCount, int yIncrement, int xIncrement, Void<int, int> Do)
    {
        Throw.IfNull(Do, nameof(Do));
        for (var y = yStart; y < yCount; y += yIncrement)
        {
            for (var x = xStart; x < xCount; x += xIncrement)
                Do(y, x);
        }
    }

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b> and <b>xLength</b>, and <see langword="default"/> <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[][] Get<T>(int yLength, int xLength) => Get(yLength, xLength, default(T));

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[][] Get<T>(int yLength, int xLength, T value)
        => Get(yLength, xLength, () => value);

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, Func<T> value)
        => Get(yLength, xLength, _ => value());

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, Func<int, T> value)
    {
        var index = -1;
        return Get(yLength, xLength, (_, _) => { index++; return value(index); });
    }

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, Func<int, int, T> value)
    {
        Throw.IfNull(value, nameof(value));
        Throw.IfLessOrEqual(yLength, 0, nameof(yLength));
        Throw.IfLessOrEqual(xLength, 0, nameof(xLength));

        var result = new T[yLength][];
        for (var row = 0; row < yLength; row++)
        {
            result[row] = new T[xLength];
            for (var column = 0; column < xLength; column++)
                result[row][column] = value(row, column);
        }
        return result;
    }

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, T value, Func<T, T> select)
        => Get(yLength, xLength, value, (_, i) => select(i));

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, T value, Func<int, T, T> select)
    {
        var index = -1;
        return Get(yLength, xLength, value, (_, _, i) => { index++; return select(index, i); });
    }

    /// <summary>
    /// Get <see cref="Array2D"/> with given <b>yLength</b>, <b>xLength</b>, and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[][] Get<T>(int yLength, int xLength, T value, Func<int, int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        Throw.IfLessOrEqual(yLength, 0, nameof(yLength));
        Throw.IfLessOrEqual(xLength, 0, nameof(xLength));

        var result = new T[yLength][];
        for (var row = 0; row < yLength; row++)
        {
            result[row] = new T[xLength];
            for (var column = 0; column < xLength; column++)
                result[row][column] = select(row, column, value);
        }
        return result;
    }

    /// <summary>
    /// Get as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(T[][] i, string format, IFormatProvider provider)
    {
        Throw.IfNull(i, nameof(i));
        return default;
    }
}