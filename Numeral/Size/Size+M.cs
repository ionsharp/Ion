using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="ISize"/>
[Description(ISize.Description)]
public record class MSize<T>
    : Model, ISize<MSize<T>, T>, ISizeMutable<T>
    where T : INumber<T>
{
    /// <see cref="Region.Property"/>

    public T Height { get => Get<T>(); set => Set(value); }

    public T Width { get => Get<T>(); set => Set(value); }

    /// <see cref="Region.Property.Indexor"/>

    T IArray<T>.this[int x] => ISize.This(this, x);

    object IArray1D.this[int x] => ISize.This(this, x);

    T IArray1D<T>.this[int x] => ISize.This(this, x);

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance with <see langword="default"/> height and width.
    /// </summary>
    public MSize() : this(default, default) { }

    /// <summary>
    /// Get new instance with height and width of given <b>dimensions</b>.
    /// </summary>
    public MSize(in T dimensions) : this(dimensions, dimensions) { }

    /// <summary>
    /// Get new instance with given <b>height</b> and <b>width</b>.
    /// </summary>
    public MSize(in T height, in T width)
        => (Height, Width) = (height, width);

    /// <summary>
    /// Get new instance from given <see cref="ValueTuple{T1, T2}"/>.
    /// </summary>
    public MSize(in (T Height, T Width) size)
        => (Height, Width) = size;

    /// <summary>
    /// Get new instance from given <see cref="ISize{}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public MSize(in ISize<T> i)
    {
        Throw.IfNull(i, nameof(i));
        (Height, Width) = (i.Height, i.Width);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator MSize<T>(in (T Height, T Width) i) => new(i);

    public static implicit operator MSize<T>(in Size<T> i) => new(i);

    ///

    public static MSize<T> operator +(MSize<T> i) => i;

    public static MSize<T> operator +(MSize<T> a, T b) => a.Do(Operator.Add, b);

    public static MSize<T> operator +(MSize<T> a, ISize<T> b) => a.Do(Operator.Add, b);

    public static MSize<T> operator ++(MSize<T> i) => i.Do(Operator.Add, T.One);

    public static MSize<T> operator -(MSize<T> i) => i.Do(Operator.Multiply, -T.One);

    public static MSize<T> operator -(MSize<T> a, T b) => a.Do(Operator.Subtract, b);

    public static MSize<T> operator -(MSize<T> a, ISize<T> b) => a.Do(Operator.Subtract, b);

    public static MSize<T> operator --(MSize<T> i) => i.Do(Operator.Subtract, T.One);

    public static MSize<T> operator *(MSize<T> a, T b) => a.Do(Operator.Multiply, b);

    public static MSize<T> operator *(MSize<T> a, ISize<T> b) => a.Do(Operator.Multiply, b);

    public static MSize<T> operator /(MSize<T> a, T b) => a.Do(Operator.Divide, b);

    public static MSize<T> operator /(MSize<T> a, ISize<T> b) => a.Do(Operator.Divide, b);

    public static MSize<T> operator %(MSize<T> a, T b) => a.Do(Operator.Modulo, b);

    public static MSize<T> operator %(MSize<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray{,}"/>

    Array IArray<MSize<T>, T>.GetArray() => new T[ISize.Length];

    static MSize<T> IArray<MSize<T>, T>.Create(MSize<T> oldSelf, Array newSelf)
    {
        (oldSelf.Height, oldSelf.Width) = ISize.Format<T>(newSelf);
        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ISize.GetEnumerator(this);

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => ISize.ToString(this, format, provider);

    /// <see cref="ISize{,}"/>

    public static MSize<T> Create(T height, T width) => new(height, width);

    object[] IArray1D.ToArray() => ISize.ToArray(this);

    T[] IArray1D<T>.ToArray() => ISize<T>.ToArray(this);
}