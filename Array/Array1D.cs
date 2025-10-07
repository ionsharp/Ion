using System;
using System.Collections;

namespace Ion;

/// <summary>
/// An <see cref="Array"/> with 1 dimension.
/// </summary>
/// <remarks>
/// <b><see cref="Array"/> ≡ <see cref="IArray1D"/> ≡ <see cref="IEnumerable"/> ≡ <see cref="T"/>[]</b>
/// </remarks>
[Extend(typeof(object[]))]
[Using(typeof(Throw), typeof(XEnumerable))]
public static class Array1D
{
    /// <summary>
    /// Do <see cref="Void"/> for given 1-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> i = 0; i &lt; <b>count</b>; i++) ...<br/>
    /// </remarks>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int XCount, Void Do)
        => Array1D.Do(0, XCount, 1, _ => Do());

    /// <inheritdoc cref="Do(int, Void, int)"/>
    public static void Do(int xCount, Void<int> Do)
    {
        Throw.IfLessOrEqual(xCount, 0, nameof(xCount));
        Array1D.Do(0, xCount, 1, Do);
    }

    /// <summary>
    /// Do <see cref="Void"/> for given 1-dimensional length.
    /// </summary>
    /// <remarks>
    /// <see langword="for"/> (<see cref="int"/> i = <b>start</b>; i &lt; <b>count</b>; i += <b>increment</b>) ...<br/>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(int xStart, int xCount, int increment, Void Do)
        => Array1D.Do(xStart, xCount, increment, _ => Do());

    /// <inheritdoc cref="Do(int, int, int, Void)"/>
    public static void Do(int xStart, int xCount, int increment, Void<int> Do)
    {
        Throw.IfNull(Do);
        for (var i = xStart; i < xCount; i += increment)
            Do(i);
    }

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <see langword="default"/> <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[] Get<T>(int length) => Get(length, default(T));

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    public static T[] Get<T>(int length, T value)
        => Get(length, () => value);

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[] Get<T>(int length, Func<T> value)
        => Get(length, _ => value());

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLess"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[] Get<T>(int length, Func<int, T> value)
    {
        Throw.IfNull(value, nameof(value));
        Throw.IfLess(length, 0, nameof(length));

        var result = new T[length];
        Do(length, i => result[i] = value(i));
        return result;
    }

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLessOrEqual"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[] Get<T>(int length, T value, Func<T, T> select)
        => Get(length, value, (_, i) => select(i));

    /// <summary>
    /// Get <see cref="Array1D"/> with given <b>length</b> and <b>value</b>.
    /// </summary>
    /// <exception cref="ArgumentIsLess"/>
    /// <exception cref="ArgumentNullException"/>
    public static T[] Get<T>(int length, T value, Func<int, T, T> select)
    {
        Throw.IfNull(select, nameof(select));
        Throw.IfLess(length, 0, nameof(length));

        var result = new T[length];
        Do(length, i => result[i] = select(i, value));
        return result;
    }

    /// <summary>
    /// Get as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(T[] i, string format, IFormatProvider provider)
    {
        Throw.IfNull(i, nameof(i));
        return i.ToString(", ", i => i is IFormattable j ? j.ToString(format, provider) : i.ToString());
    }
}