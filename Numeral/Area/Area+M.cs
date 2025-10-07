using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Ion.Numeral.Models;

/// <inheritdoc cref="IArea"/>
[Description(IArea.Description)]
public record class MArea<T>
    : Model, IArea<MArea<T>, T>, IAreaMutable<T>
    where T : INumber<T>
{
    /// <see cref="Region.Property.Indexor"/>

    T IArray<T>.this[int x] => IArea.This(this, x);

    object IArray1D.this[int x] => IArea.This(this, x);

    T IArray1D<T>.this[int x] => IArea.This(this, x);

    /// <see cref="Region.Property"/>

    public T Height { get => Get<T>(); set => Set(value); }

    public T Width { get => Get<T>(); set => Set(value); }

    public T X { get => Get<T>(); set => Set(value); }

    public T Y { get => Get<T>(); set => Set(value); }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc cref="IArea.Format{T}(in T)"/>
    public MArea(in T i)
        => (X, Y, Height, Width) = IArea.Format(i);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T)"/>
    public MArea(in T position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in T)"/>
    public MArea(in T x, in T y, in T size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in T, in T)"/>
    public MArea(in T x, in T y, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(x, y, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in ValueTuple{T, T})"/>
    public MArea(in T x, in T y, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in T, in T, in ISize{T})"/>
    public MArea(in T x, in T y, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(x, y, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in T)"/>
    public MArea(in (T X, T Y) position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in T, in T)"/>
    public MArea(in (T X, T Y) position, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(position, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in ValueTuple{T, T})"/>
    public MArea(in (T X, T Y) position, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in ValueTuple{T, T}, in ISize{T})"/>
    public MArea(in (T X, T Y) position, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IArea{T})"/>
    public MArea(in IArea<T> area)
        => (X, Y, Height, Width) = IArea.Format(area);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in T)"/>
    public MArea(in IVector2<T> position, in T size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in T, in T)"/>
    public MArea(in IVector2<T> position, in T height, in T width)
        => (X, Y, Height, Width) = IArea.Format(position, height, width);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in ValueTuple{T, T})"/>
    public MArea(in IVector2<T> position, in (T Height, T Width) size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in ISize{T})"/>
    public MArea(in IVector2<T> position, in ISize<T> size)
        => (X, Y, Height, Width) = IArea.Format(position, size);

    /// <inheritdoc cref="IArea.Format{T}(in IVector2{T}, in IVector2{T})"/>
    public MArea(in IVector2<T> tLeft, in IVector2<T> bRight)
        => (X, Y, Height, Width) = IArea.Format(tLeft, bRight);

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator MArea<T>(in (T Height, T Width) i) => new(i);

    public static implicit operator MArea<T>(in Area<T> i) => new(i);

    ///

    public static MArea<T> operator +(MArea<T> i) => i;

    public static MArea<T> operator +(MArea<T> a, T b) => a.Do(Operator.Add, b);

    public static MArea<T> operator +(MArea<T> a, IArea<T> b) => a.Do(Operator.Add, b);

    public static MArea<T> operator +(MArea<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator +(MArea<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator ++(MArea<T> i) => i.Do(Operator.Add, T.One);

    public static MArea<T> operator -(MArea<T> i) => i.Do(Operator.Multiply, -T.One);

    public static MArea<T> operator -(MArea<T> a, T b) => a.Do(Operator.Subtract, b);

    public static MArea<T> operator -(MArea<T> a, IArea<T> b) => a.Do(Operator.Subtract, b);

    public static MArea<T> operator -(MArea<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator -(MArea<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator --(MArea<T> i) => i.Do(Operator.Subtract, T.One);

    public static MArea<T> operator *(MArea<T> a, T b) => a.Do(Operator.Multiply, b);

    public static MArea<T> operator *(MArea<T> a, IArea<T> b) => a.Do(Operator.Multiply, b);

    public static MArea<T> operator *(MArea<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator *(MArea<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator /(MArea<T> a, T b) => a.Do(Operator.Divide, b);

    public static MArea<T> operator /(MArea<T> a, IArea<T> b) => a.Do(Operator.Divide, b);

    public static MArea<T> operator /(MArea<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator /(MArea<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator %(MArea<T> a, T b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator %(MArea<T> a, IArea<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator %(MArea<T> a, ISize<T> b) => a.Do(Operator.Modulo, b);

    public static MArea<T> operator %(MArea<T> a, IVector2<T> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArea{,}"/>

    static MArea<T> IArea<MArea<T>, T>.Create(MArea<T> oldSelf, IVector2<T> position, ISize<T> size)
    {
        (oldSelf.X, oldSelf.Y, oldSelf.Height, oldSelf.Width) = IArea.Format(position, size);
        return oldSelf;
    }

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => IArea.ToArray(this);

    T[] IArray1D<T>.ToArray() => IArea<T>.ToArray(this);

    /// <see cref="IArray{,}"/>

    Array IArray<MArea<T>, T>.GetArray() => new T[IArea.Length];

    static MArea<T> IArray<MArea<T>, T>.Create(MArea<T> oldSelf, Array newSelf)
    {
        (oldSelf.X, oldSelf.Y, oldSelf.Height, oldSelf.Width) = IArea.Format<T>(newSelf);
        return oldSelf;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<T>).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => IArea.GetEnumerator(this);

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => IArea.ToString(this, format, provider);
}