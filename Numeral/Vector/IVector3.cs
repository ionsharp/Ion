using System;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>
/// A set of 3 values.
/// </summary>
public interface IVector3 : IVector2 
{
    new public const string Description = "A set of 3 generic values.";

    new public const string StringFormat = "X = {0}, Y = {1}, Z = {1}";

    int IVector.Length => Length;

    new public static int Length => 3;

    public object Z { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in T xyz, VectorType type = IVector.DefaultType)
        => Format(xyz, xyz, xyz, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in T x, in T y, VectorType type)
        => Format(x, y, default, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in (T x, T y) xy, VectorType type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.x, xy.y, default, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, T Z, VectorType Type) Format<T>(in T x, in T y, in T z, VectorType type)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(z, nameof(z));
        return (x, y, z, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in (T x, T y, T z) xyz, VectorType type)
    {
        Throw.IfNull(xyz, nameof(xyz));
        return (xyz.x, xyz.y, xyz.z, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in (T x, T y, T z, T w) xyzw, VectorType type)
    {
        Throw.IfNull(xyzw, nameof(xyzw));
        return (xyzw.x, xyzw.y, xyzw.z, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in IEnumerable<T> i, VectorType type)
    {
        Throw.IfNull(i, nameof(i));

        T[] array = [.. i];
        Throw.IfNotEqual(array.Length != Length, nameof(i));

        return array.To(j => (j[0], j[1], j[2], type));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(IVector<T> oldSelf, Array newSelf)
        => ((T)newSelf.GetValue(0), (T)newSelf.GetValue(1), (T)newSelf.GetValue(2), oldSelf.Type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, VectorType Type) Format<T>(in IVector2<T> xy, VectorType? type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.X, xy.Y, default, type ?? xy.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, T Z, VectorType Type) Format<T>(in IVector3<T> xyz, VectorType? type)
    {
        Throw.IfNull(xyz, nameof(xyz));
        return (xyz.X, xyz.Y, xyz.Z, type ?? xyz.Type);
    }

    /// <summary>
    /// Get <see cref="IVector3"/> as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(IVector3<T> i, string format, IFormatProvider provider)
    {
        if (i.X is IFormattable x && i.Y is IFormattable y && i.Z is IFormattable z)
            return StringFormat.F(x.ToString(format, provider), y.ToString(format, provider), z.ToString(format, provider));

        return StringFormat.F(i.X, i.Y, i.Z);
    }
}