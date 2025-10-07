using Ion.Numeral.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="IArea"/>
[Description(IArea.Description)]
public readonly record struct Area<T>
    : IArea<Area<T>, T>, IImmutable
    where T : INumber<T>
{
    /// <see cref="Region.Property.Indexor"/>

    T IArray<T>.this[int x] => IArea.This(this, x);

    object IArray1D.this[int x] => IArea.This(this, x);

    T IArray1D<T>.this[int x] => IArea.This(this, x);

    /// <see cref="Region.Property"/>

    public readonly T Height { get; }

    public readonly T Width { get; }

    public readonly T X { get; }

    public readonly T Y { get; }

    /// <see cref="Region.Constructor"/>

    private Area(in (T X, T Y, T Height, T Width) i) => (X, Y, Height, Width) = i;

    /// <inheritdoc cref="IArea.Format{T}(in T)"/>
    public Area(in T i)
        => (X, Y, Height, Width) = IArea.Format(i);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T)"/>
    public Area(in T position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in T)"/>
    public Area(in T x, in T y, in T size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in T, in T)"/>
    public Area(in T x, in T y, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(x, y, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in ValueTuple{T, T})"/>
    public Area(in T x, in T y, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in ISize{T})"/>
    public Area(in T x, in T y, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in T)"/>
    public Area(in (T X, T Y) position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in T, in T)"/>
    public Area(in (T X, T Y) position, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(position, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in ValueTuple{T, T})"/>
    public Area(in (T X, T Y) position, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in ISize{T})"/>
    public Area(in (T X, T Y) position, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IArea{T})"/>
    public Area(in IArea<T> area)
        => (X, Y, Height, Width) = IArea.Format(area);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in T)"/>
    public Area(in IVector2<T> position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in T, in T)"/>
    public Area(in IVector2<T> position, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(position, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in ValueTuple{T, T})"/>
    public Area(in IVector2<T> position, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in ISize{T})"/>
    public Area(in IVector2<T> position, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in IVector2{T})"/>
    public Area(in IVector2<T> tLeft, in IVector2<T> bRight)
        => (X, Y, Height, Width) = IArea.Format(tLeft, bRight);

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Area<T>(in (T X, T Y, T Height, T Width) i) => new(i);

    public static implicit operator Area<T>(in MArea<T> i) => new(i);

    ///

    public static Area<T> operator +(Area<T> i) => i;

    public static Area<T> operator +(Area<T> a, T b) => a.Do(Operator.Add, b);

    public static Area<T> operator +(Area<T> a, IArea<T> b) => a.Do(Operator.Add, b);

    public static Area<T> operator +(Area<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator +(Area<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator ++(Area<T> i) => i.Do(Operator.Add, T.One);

    public static Area<T> operator -(Area<T> i) => i.Do(Operator.Multiply, -T.One);

    public static Area<T> operator -(Area<T> a, T b) => a.Do(Operator.Subtract, b);

    public static Area<T> operator -(Area<T> a, IArea<T> b) => a.Do(Operator.Subtract, b);

    public static Area<T> operator -(Area<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator -(Area<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator --(Area<T> i) => i.Do(Operator.Subtract, T.One);

    public static Area<T> operator *(Area<T> a, T b) => a.Do(Operator.Multiply, b);

    public static Area<T> operator *(Area<T> a, IArea<T> b) => a.Do(Operator.Multiply, b);

    public static Area<T> operator *(Area<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator *(Area<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator /(Area<T> a, T b) => a.Do(Operator.Divide, b);

    public static Area<T> operator /(Area<T> a, IArea<T> b) => a.Do(Operator.Divide, b);

    public static Area<T> operator /(Area<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator /(Area<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator %(Area<T> a, T b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator %(Area<T> a, IArea<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator %(Area<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static Area<T> operator %(Area<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArea{,}"/>

    static Area<T> IArea<Area<T>, T>.Create(Area<T> oldSelf, IVector2<T> position, ISize<T> size) => new(position, size);

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => IArea.ToArray(this);

    T[] IArray1D<T>.ToArray() => IArea<T>.ToArray(this);

    /// <see cref="IArray{,}"/>

    Array IArray<Area<T>, T>.GetArray() => new T[IArea.Length];

    static Area<T> IArray<Area<T>, T>.Create(Area<T> oldSelf, Array newSelf) => new(IArea.Format<T>(newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => IArea.GetEnumerator(this);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IArea.ToString(this, format, provider);
}