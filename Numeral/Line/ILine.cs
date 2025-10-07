using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>
/// A line that is formed by connecting two points.
/// </summary>
public interface ILine : IArray1D, IArray1DRank, IFormattable, IEnumerable
{
    public const string Description = "A line that is formed by connecting two points.";

    public const string StringFormat = "X1 = {0}, Y1 = {1}, X2 = {2}, Y2 = {3}";

    new public const int Length = 4;

    object X1 { get; }

    object X2 { get; }

    object Y1 { get; }

    object Y2 { get; }

    int IArray1D.XLength => Length;

    public static (T X1, T Y1, T X2, T Y2) Format<T>(Array i) => ((T)i.GetValue(0), (T)i.GetValue(1), (T)i.GetValue(2), (T)i.GetValue(3));

    public static IEnumerator<T> GetEnumerator<T>(ILine<T> i) { yield return i.X1; yield return i.Y1; yield return i.X2; yield return i.Y2; }

    public static T This<T>(ILine<T> i, int index) => index switch { 0 => i.X1, 1 => i.Y1, 2 => i.X2, 3 => i.Y2, _ => throw new NotSupportedException() };

    public static object[] ToArray(ILine i) => [i.X1, i.Y1, i.X2, i.Y2];

    public static string ToString<T>(ILine<T> i, string format, IFormatProvider provider)
    {
        if (i.X1 is IFormattable x1 && i.Y1 is IFormattable y1 && i.X2 is IFormattable x2 && i.Y2 is IFormattable y2)
            return StringFormat.F(x1.ToString(format, provider), y1.ToString(format, provider), x2.ToString(format, provider), y2.ToString(format, provider));

        return StringFormat.F(i.X1, i.Y1, i.X2, i.Y2);
    }
}

/// <inheritdoc/>
public interface ILine<T> : ILine, IArray1D<T>, IArray1DRank<T>, IEnumerable<T>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Length;

    new T X1 { get; }

    new T X2 { get; }

    new T Y1 { get; }

    new T Y2 { get; }

    object ILine.X1 => X1;

    object ILine.X2 => X2;

    object ILine.Y1 => Y1;

    object ILine.Y2 => Y2;

    public static T[] ToArray(ILine<T> i) => [i.X1, i.Y1, i.X2, i.Y2];
}

/// <inheritdoc/>
public interface ILine<TSelf, TValue> : ILine<TValue>, IArray1D<TSelf, TValue> where TSelf : ILine<TSelf, TValue>
{
    /// <remarks><b>Required as <i>most specific implementation</i>.</b></remarks>
    int IArray.Length => Length;

    object IArray.this[int i] => i switch { 0 => X1, 1 => Y1, 2 => X2, 3 => Y2 };

    public (TValue X, TValue Y) XY1 => (X1, Y1);

    public (TValue X, TValue Y) XY2 => (X2, Y2);
}