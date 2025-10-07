using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="ISize"/>
[Description(ISize.Description)]
public readonly record struct Size<T>
    : ISize<Size<T>, T>, IImmutable
    where T : INumber<T>
{
    /// <see cref="Region.Property"/>

    public readonly T Height { get; }

    public readonly T Width { get; }

    /// <see cref="Region.Property.Indexor"/>

    T IArray<T>.this[int x] => ISize.This(this, x);

    object IArray1D.this[int x] => ISize.This(this, x);

    T IArray1D<T>.this[int x] => ISize.This(this, x);

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance with height and width of given <b>dimensions</b>.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public Size(in T dimensions) : this(dimensions, dimensions) { }

    /// <summary>
    /// Get new instance with given <b>height</b> and <b>width</b>.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public Size(in T height, in T width)
    {
        Throw.IfNull(height, nameof(height));
        Throw.IfNull(width, nameof(width));
        (Height, Width) = (height, width);
    }

    /// <summary>
    /// Get new instance from given <see cref="ValueTuple{T1, T2}"/>.
    /// </summary>
    /// <see cref="ArgumentNullException"/>
    public Size(in (T Height, T Width) size)
    {
        Throw.IfNull(size.Height, nameof(size));
        Throw.IfNull(size.Width, nameof(size));
        (Height, Width) = size;
    }

    /// <summary>
    /// Get new instance from given <see cref="ISize{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Size(in ISize<T> i)
    {
        Throw.IfNull(i, nameof(i));
        (Height, Width) = (i.Height, i.Width);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Size<T>(in (T Height, T Width) i) => new(i);

    public static implicit operator Size<T>(in MSize<T> i) => new(i);

    ///

    public static Size<T> operator +(Size<T> i) => i;

    public static Size<T> operator +(Size<T> a, T b) => a.Do(Operator.Add, b);

    public static Size<T> operator +(Size<T> a, ISize<T> b) => a.Do(Operator.Add, b);

    public static Size<T> operator ++(Size<T> i) => i.Do(Operator.Add, T.One);

    public static Size<T> operator -(Size<T> i) => i.Do(Operator.Multiply, -T.One);

    public static Size<T> operator -(Size<T> a, T b) => a.Do(Operator.Subtract, b);

    public static Size<T> operator -(Size<T> a, ISize<T> b) => a.Do(Operator.Subtract, b);

    public static Size<T> operator --(Size<T> i) => i.Do(Operator.Subtract, T.One);

    public static Size<T> operator *(Size<T> a, T b) => a.Do(Operator.Multiply, b);

    public static Size<T> operator *(Size<T> a, ISize<T> b) => a.Do(Operator.Multiply, b);

    public static Size<T> operator /(Size<T> a, T b) => a.Do(Operator.Divide, b);

    public static Size<T> operator /(Size<T> a, ISize<T> b) => a.Do(Operator.Divide, b);

    public static Size<T> operator %(Size<T> a, T b) => a.Do(Operator.Modulo, b);

    public static Size<T> operator %(Size<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => ISize.ToArray(this);

    T[] IArray1D<T>.ToArray() => ISize<T>.ToArray(this);

    /// <see cref="IArray{,}"/>

    Array IArray<Size<T>, T>.GetArray() => new T[ISize.Length];

    static Size<T> IArray<Size<T>, T>.Create(Size<T> oldSelf, Array newSelf) => new(ISize.Format<T>(newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => ISize.GetEnumerator(this);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => ISize.ToString(this, format, provider);

    /// <see cref="ISize{,}"/>

    public static Size<T> Create(T height, T width) => new(height, width);
}