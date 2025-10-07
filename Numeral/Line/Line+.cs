using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="ILine"/>
[Description(ILine.Description)]
public readonly record struct Line<T>
    : ILine<Line<T>, T>, IImmutable
    where T : INumber<T>
{
    /// <see cref="Region.Property"/>

    public readonly T X1 { get; }

    public readonly T X2 { get; }

    public readonly T Y1 { get; }

    public readonly T Y2 { get; }

    /// <see cref="Region.Property.Indexor"/>

    T IArray<T>.this[int x] => ILine.This(this, x);

    object IArray1D.this[int x] => ILine.This(this, x);

    T IArray1D<T>.this[int x] => ILine.This(this, x);

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in T xy) : this(xy, xy, xy, xy) { }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in T x1, in T y1, in T x2, in T y2)
    {
        Throw.IfNull(x1, nameof(x1));
        Throw.IfNull(y1, nameof(y1));
        Throw.IfNull(x2, nameof(x2));
        Throw.IfNull(y2, nameof(y2));
        (X1, Y1, X2, Y2) = (x1, y1, x2, y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in (T X, T Y) xy)
    {
        Throw.IfNull(xy.X, nameof(xy));
        Throw.IfNull(xy.Y, nameof(xy));
        (X1, Y1, X2, Y2) = (xy.X, xy.Y, xy.X, xy.Y);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in (T X, T Y) x1y1, in T x2y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        Throw.IfNull(x2y2, nameof(x2y2));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2y2, x2y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in (T X, T Y) x1y1, in T x2, in T y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        Throw.IfNull(x2, nameof(x2));
        Throw.IfNull(y2, nameof(y2));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2, y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in (T X, T Y) x1y1, in (T X, T Y) x2y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        Throw.IfNull(x2y2.X, nameof(x2y2));
        Throw.IfNull(x2y2.Y, nameof(x2y2));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2y2.X, x2y2.Y);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in (T X1, T Y1, T X2, T Y2) line)
    {
        Throw.IfNull(line.X1, nameof(line));
        Throw.IfNull(line.Y1, nameof(line));
        Throw.IfNull(line.X2, nameof(line));
        Throw.IfNull(line.Y2, nameof(line));
        (X1, Y1, X2, Y2) = line;
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in ILine<T> line)
    {
        Throw.IfNull(line, nameof(line));
        (X1, X2, Y1, Y2) = (line.X1, line.X2, line.Y1, line.Y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in IVector2<T> xy)
    {
        Throw.IfNull(xy.X, nameof(xy));
        Throw.IfNull(xy.Y, nameof(xy));
        (X1, Y1, X2, Y2) = (xy.X, xy.Y, xy.X, xy.Y);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in IVector2<T> x1y1, in T x2y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2y2, x2y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in IVector2<T> x1y1, in T x2, in T y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2, y2);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in IVector2<T> x1y1, in (T X, T Y) x2y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        Throw.IfNull(x2y2.X, nameof(x2y2));
        Throw.IfNull(x2y2.Y, nameof(x2y2));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2y2.X, x2y2.Y);
    }

    /// <summary>
    /// Get new instance.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Line(in IVector2<T> x1y1, in IVector2<T> x2y2)
    {
        Throw.IfNull(x1y1.X, nameof(x1y1));
        Throw.IfNull(x1y1.Y, nameof(x1y1));
        Throw.IfNull(x2y2.X, nameof(x2y2));
        Throw.IfNull(x2y2.Y, nameof(x2y2));
        (X1, Y1, X2, Y2) = (x1y1.X, x1y1.Y, x2y2.X, x2y2.Y);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Line<T>(in (T Height, T Width) i) => new(i);

    public static implicit operator Line<T>(in MLine<T> i) => new(i);

    ///

    public static Line<T> operator +(Line<T> i) => i;

    public static Line<T> operator +(Line<T> a, T b) => a.Do(Operator.Add, b);

    public static Line<T> operator +(Line<T> a, ILine<T> b) => a.Do(Operator.Add, b);

    public static Line<T> operator ++(Line<T> i) => i.Do(Operator.Add, T.One);

    public static Line<T> operator -(Line<T> i) => i.Do(Operator.Multiply, -T.One);

    public static Line<T> operator -(Line<T> a, T b) => a.Do(Operator.Subtract, b);

    public static Line<T> operator -(Line<T> a, ILine<T> b) => a.Do(Operator.Subtract, b);

    public static Line<T> operator --(Line<T> i) => i.Do(Operator.Subtract, T.One);

    public static Line<T> operator *(Line<T> a, T b) => a.Do(Operator.Multiply, b);

    public static Line<T> operator *(Line<T> a, ILine<T> b) => a.Do(Operator.Multiply, b);

    public static Line<T> operator /(Line<T> a, T b) => a.Do(Operator.Divide, b);

    public static Line<T> operator /(Line<T> a, ILine<T> b) => a.Do(Operator.Divide, b);

    public static Line<T> operator %(Line<T> a, T b) => a.Do(Operator.Modulo, b);

    public static Line<T> operator %(Line<T> a, ILine<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => ILine.ToArray(this);

    T[] IArray1D<T>.ToArray() => ILine<T>.ToArray(this);

    /// <see cref="IArray{,}"/>

    Array IArray<Line<T>, T>.GetArray() => new T[ILine.Length];

    static Line<T> IArray<Line<T>, T>.Create(Line<T> oldSelf, Array newSelf) => new(ILine.Format<T>(newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => ILine.GetEnumerator(this);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => ILine.ToString(this, format, provider);
}