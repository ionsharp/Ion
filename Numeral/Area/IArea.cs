using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>
/// A position and size.
/// </summary>
public interface IArea : IArray1D, IArray1DRank, IEnumerable, IFormattable
{
    public const string Description = "A generic position and size.";

    public const string StringFormat = "X = {0}, Y = {1}, Height = {2}, Width = {3}";

    new public const int Length = 4;

    object Height { get; }

    object Width { get; }

    object X { get; }

    object Y { get; }

    int IArray.Length => Length;

    object IArray.this[int i] => i switch { 0 => X, 1 => Y, 2 => Height, 3 => Width };

    int IArray1D.XLength => Length;

    public static (T X, T Y, T Height, T Width) Format<T>(Array i) => ((T)i.GetValue(0), (T)i.GetValue(1), (T)i.GetValue(2), (T)i.GetValue(3));

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T i)
        => Format(i, i, i, i);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T position, in T size)
        => Format(position, position, size, size);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T X, in T Y, in T size)
        => Format(X, Y, size, size);

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T x, in T y, in T height, in T width)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(height, nameof(height));
        Throw.IfNull(width, nameof(width));
        return (x, y, height, width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T x, in T y, in (T Height, T Width) size)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(size, nameof(size));
        return (x, y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in T x, in T y, in ISize<T> size)
    {
        Throw.IfNull(x, nameof(x));
        Throw.IfNull(y, nameof(y));
        Throw.IfNull(size, nameof(size));
        return (x, y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in (T X, T Y) position, in T size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size, size);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in (T X, T Y) position, in T height, in T width)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(height, nameof(height));
        Throw.IfNull(width, nameof(width));
        return (position.X, position.Y, height, width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in (T X, T Y) position, in (T Height, T Width) size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in (T X, T Y) position, in ISize<T> size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IArea<T> area)
    {
        Throw.IfNull(area, nameof(area));
        return (area.X, area.Y, area.Height, area.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IVector2<T> position, in T size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size, size);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IVector2<T> position, in T height, in T width)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(height, nameof(height));
        Throw.IfNull(width, nameof(width));
        return (position.X, position.Y, height, width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IVector2<T> position, in (T Height, T Width) size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IVector2<T> position, in ISize<T> size)
    {
        Throw.IfNull(position, nameof(position));
        Throw.IfNull(size, nameof(size));
        return (position.X, position.Y, size.Height, size.Width);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public static (T X, T Y, T Height, T Width) Format<T>(in IVector2<T> tLeft, in IVector2<T> bRight) 
        where T : INumber<T>
    {
        Throw.IfNull(tLeft, nameof(tLeft));
        Throw.IfNull(bRight, nameof(bRight));
        return (tLeft.X, tLeft.Y, bRight.Y - tLeft.Y + T.One, bRight.X - tLeft.X + T.One);
    }

    public static IEnumerator<T> GetEnumerator<T>(IArea<T> i) { yield return i.X; yield return i.Y; yield return i.Height; yield return i.Width; }

    public static T This<T>(IArea<T> i, int index) => index switch { 0 => i.X, 1 => i.Y, 2 => i.Height, 3 => i.Width, _ => throw new NotSupportedException() };

    public static object[] ToArray(IArea i) => [i.X, i.Y, i.Height, i.Width];

    public static string ToString<T>(IArea<T> i, string format, IFormatProvider provider)
    {
        if (i.X is IFormattable x && i.Y is IFormattable y && i.Height is IFormattable height && i.Width is IFormattable width)
            return StringFormat.F(x.ToString(format, provider), y.ToString(format, provider), height.ToString(format, provider), width.ToString(format, provider));

        return StringFormat.F(i.X, i.Y, i.Height, i.Width);
    }
}

/// <inheritdoc/>
public interface IArea<T> : IArea, IArray1D<T>, IArray1DRank<T>, IEnumerable<T>
{
    new T Height { get; }

    new T Width { get; }

    new T X { get; }

    new T Y { get; }

    object IArea.Height => Height;

    object IArea.Width => Width;

    object IArea.X => X;

    object IArea.Y => Y;

    public static T[] ToArray(IArea<T> i) => [i.X, i.Y, i.Height, i.Width];
}

/// <inheritdoc/>
public interface IArea<TSelf, TValue> : IArea<TValue>, IArray1D<TSelf, TValue> where TSelf : IArea<TSelf, TValue>
{
    public static abstract TSelf Create(TSelf oldSelf, IVector2<TValue> position, ISize<TValue> size);
}