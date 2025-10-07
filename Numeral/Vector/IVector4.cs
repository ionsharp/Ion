using System;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>
/// A set of 4 values.
/// </summary>
public interface IVector4 : IVector3 
{
    new public const string Description = "A set of 4 generic values.";

    new public const string StringFormat = "X = {0}, Y = {1}, Z = {2}, W = {3}";

    int IVector.Length => Length;

    new public static int Length => 4;

    public object W { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in T xyzw, VectorType type)
        => Format(xyzw, xyzw, xyzw, xyzw, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in T x, in T y, VectorType type)
        => Format(x, y, default, default, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in (T X, T Y) xy, VectorType type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.X, xy.Y, default, default, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in T x, in T y, in T z, VectorType type)
        => Format(x, y, z, default, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in (T X, T Y, T Z) xyz, VectorType type)
    {
        Throw.IfNull(xyz, nameof(xyz));
        return (xyz.X, xyz.Y, xyz.Z, default, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in T x, in T y, in T z, in T w, VectorType type)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(z, nameof(z));
        Throw.IfNull(w, nameof(w));
        return (x, y, z, w, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in (T X, T Y, T Z, T W) xyzw, VectorType type)
    {
        Throw.IfNull(xyzw, nameof(xyzw));
        return (xyzw.X, xyzw.Y, xyzw.Z, xyzw.W, type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(IEnumerable<T> i, VectorType type)
    {
        Throw.IfNull(i, nameof(i));

        T[] array = [.. i];
        Throw.IfNotEqual(array.Length != 4, nameof(i));

        return array.To(j => (j[0], j[1], j[2], j[3], type));
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(IVector<T> oldSelf, Array newSelf)
        => ((T)newSelf.GetValue(0), (T)newSelf.GetValue(1), (T)newSelf.GetValue(2), (T)newSelf.GetValue(3), oldSelf.Type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in IVector2<T> xy, VectorType? type)
    {
        Throw.IfNull(xy, nameof(xy));
        return (xy.X, xy.Y, default, default, type ?? xy.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in IVector3<T> xyz, VectorType? type)
    {
        Throw.IfNull(xyz, nameof(xyz));
        return (xyz.X, xyz.Y, xyz.Z, default, type ?? xyz.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (T X, T Y, T Z, T W, VectorType Type) Format<T>(in IVector4<T> xyzw, VectorType? type)
    {
        Throw.IfNull(xyzw, nameof(xyzw));
        return (xyzw.X, xyzw.Y, xyzw.Z, xyzw.W, type ?? xyzw.Type);
    }

    /// <summary>
    /// Get <see cref="IVector4"/> as <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static string ToString<T>(IVector4<T> i, string format, IFormatProvider provider)
    {
        if (i.X is IFormattable x && i.Y is IFormattable y && i.Z is IFormattable z && i.W is IFormattable w)
            return StringFormat.F(x.ToString(format, provider), y.ToString(format, provider), z.ToString(format, provider), w.ToString(format, provider));

        return StringFormat.F(i.X, i.Y, i.Z, i.W);
    }
}