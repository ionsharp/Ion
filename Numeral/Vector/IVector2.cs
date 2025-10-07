using System;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>
/// A set of 2 values.
/// </summary>
public interface IVector2 : IVector 
{ 
    new public const string Description = "A set of 2 generic values.";

    public const string StringFormat = "X = {0}, Y = {1}";

    int IVector.Length => Length;

    new public static int Length => 2;

    public object X { get; }

    public object Y { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in T xy, VectorType type) 
        => Format(xy, xy, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in T x, in T y, VectorType type)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        return (x, y, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in (T X, T Y) xy, VectorType type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.X, xy.Y, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in (T X, T Y, T Z) xyz, VectorType type)
    {
        Throw.IfNull(xyz, nameof(xyz));
        return (xyz.X, xyz.Y, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in (T X, T Y, T Z, T W) xyzw, VectorType type)
    {
        Throw.IfNull(xyzw, nameof(xyzw));
        return (xyzw.X, xyzw.Y, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, VectorType Type) Format<T>(in IEnumerable<T> i, VectorType type)
    {
        Throw.IfNull(i, nameof(i));

        T[] array = [.. i];
        Throw.IfNotEqual(array.Length != Length, nameof(i));

        return array.To(j => (j[0], j[1], type));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(IVector<T> oldSelf, Array newSelf)
        => ((T)newSelf.GetValue(0), (T)newSelf.GetValue(1), oldSelf.Type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, VectorType Type) Format<T>(in IVector2<T> xy, VectorType? type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.X, xy.Y, type ?? xy.Type);
    }

    /// <summary>
    /// Get <see cref="IVector2"/> as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(in IVector2<T> i, string format, IFormatProvider provider)
    {
        Throw.IfNull(i, nameof(i));
        if (i.X is IFormattable x && i.Y is IFormattable y)
            return StringFormat.F(x.ToString(format, provider), y.ToString(format, provider));

        return StringFormat.F(i.X, i.Y);
    }
}