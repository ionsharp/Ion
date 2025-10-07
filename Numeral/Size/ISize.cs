using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>
/// A height and width.
/// </summary>
public interface ISize : IArray1D, IArray1DRank, IEnumerable, IFormattable
{
    public const string Description = "A measure of height and width.";

    public const string StringFormat = "Height = {0}, Width = {1}";

    new public const int Length = 2;

    object Height { get; }

    object Width { get; }

    int IArray1D.XLength => Length;

    public static (T Height, T Width) Format<T>(Array i) => ((T)i.GetValue(0), (T)i.GetValue(1));

    public static IEnumerator<T> GetEnumerator<T>(ISize<T> i) { yield return i.Height; yield return i.Width; }

    public static T This<T>(ISize<T> i, int index) => index switch { 0 => i.Height, 1 => i.Width, _ => throw new NotSupportedException() };

    public static object[] ToArray(ISize i) => [i.Height, i.Width];

    public static string ToString<T>(ISize<T> i, string format, IFormatProvider provider)
    {
        if (i.Height is IFormattable height && i.Width is IFormattable width)
            return StringFormat.F(height.ToString(format, provider), width.ToString(format, provider));

        return StringFormat.F(i.Height, i.Width);
    }
}

/// <inheritdoc/>
public interface ISize<T> : ISize, IArray1D<T>, IArray1DRank<T>, IEnumerable<T>
{
    new T Height { get; }

    new T Width { get; }

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    object IArray.this[int i] => i switch { 0 => Height, 1 => Width };

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Length;

    object ISize.Height => Height;

    object ISize.Width => Width;

    public static T[] ToArray(ISize<T> i) => [i.Height, i.Width];
}

/// <inheritdoc/>
public interface ISize<TSelf, TValue> : ISize<TValue>, IArray1D<TSelf, TValue> where TSelf : ISize<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    object IArray.this[int i] => i switch { 0 => Height, 1 => Width };

    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Length;

    public static abstract TSelf Create(TValue height, TValue width);
}