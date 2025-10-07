using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion;

/// <inheritdoc cref="IArray1D"/>
[CollectionBuilder(typeof(Array1DBuilder), nameof(Array1DBuilder.Create))]
[Using(typeof(Array1D))]
public sealed class Array<T>(params T[] Array)
    : IArray1D<Array<T>, T>, IArray1DRank<T>, IArrayUnfixed<T>, IArrayUnfixedAlias<Array<T>, Array<T>, T>, IMutable
{
    /// <see cref="Region.Field"/>

    private readonly T[] _Value = Array;

    /// <see cref="Region.Property"/>

    public int Length => _Value.Length;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int x] => this[x];

    object IArray1D.this[int x] => this[x];

    public T this[int x] { get => _Value[x]; set => _Value[x] = value; }

    /// <see cref="Region.Operator"/>
    #region 

    public static implicit operator Array(Array<T> i) => i._Value;

    public static implicit operator T[](Array<T> i) => i._Value;

    public static implicit operator Array<T>(T[] i) => new(i);

    public static implicit operator Array<T>(Tuple<T, T> i) => new(i.Item1, i.Item2);

    public static implicit operator Array<T>(Tuple<T, T, T> i) => new(i.Item1, i.Item2, i.Item3);

    public static implicit operator Array<T>(Tuple<T, T, T, T> i) => new(i.Item1, i.Item2, i.Item3, i.Item4);

    public static implicit operator Array<T>(Tuple<T, T, T, T, T> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5);

    public static implicit operator Array<T>(Tuple<T, T, T, T, T, T> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6);

    public static implicit operator Array<T>(Tuple<T, T, T, T, T, T, T> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7);

    public static implicit operator Array<T>(Tuple<T, T, T, T, T, T, T, T> i) => new(i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6, i.Item7, i.Rest);

    public static implicit operator Array<T>(TupleMutable<T, T> i) => new(i.A, i.B);

    public static implicit operator Array<T>(TupleMutable<T, T, T> i) => new(i.A, i.B, i.C);

    public static implicit operator Array<T>(TupleMutable<T, T, T, T> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Array<T>(TupleMutable<T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Array<T>(TupleMutable<T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Array<T>(TupleMutable<T, T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Array<T>(TupleMutable<T, T, T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    public static implicit operator Array<T>((T i1, T i2) i)
        => new(i.i1, i.i2);

    public static implicit operator Array<T>((T i1, T i2, T i3) i)
        => new(i.i1, i.i2, i.i3);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4) i)
        => new(i.i1, i.i2, i.i3, i.i4);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11, T i12) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11, i.i12);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11, T i12, T i13) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11, i.i12, i.i13);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11, T i12, T i13, T i14) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11, i.i12, i.i13, i.i14);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11, T i12, T i13, T i14, T i15) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11, i.i12, i.i13, i.i14, i.i15);

    public static implicit operator Array<T>((T i1, T i2, T i3, T i4, T i5, T i6, T i7, T i8, T i9, T i10, T i11, T i12, T i13, T i14, T i15, T i16) i)
        => new(i.i1, i.i2, i.i3, i.i4, i.i5, i.i6, i.i7, i.i8, i.i9, i.i10, i.i11, i.i12, i.i13, i.i14, i.i15, i.i16);

    public static implicit operator Array<T>(Value<T, T> i) => new(i.A, i.B);

    public static implicit operator Array<T>(Value<T, T, T> i) => new(i.A, i.B, i.C);

    public static implicit operator Array<T>(Value<T, T, T, T> i) => new(i.A, i.B, i.C, i.D);

    public static implicit operator Array<T>(Value<T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E);

    public static implicit operator Array<T>(Value<T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F);

    public static implicit operator Array<T>(Value<T, T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G);

    public static implicit operator Array<T>(Value<T, T, T, T, T, T, T, T> i) => new(i.A, i.B, i.C, i.D, i.E, i.F, i.G, i.Rest);

    #endregion

    /// <see cref="IArray1D"/>

    T[] IArray1D<T>.ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<Array<T>, T>.GetArray() => _Value;

    static Array<T> IArray<Array<T>, T>.Create(Array<T> oldSelf, Array newSelf) => oldSelf;

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (_Value as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc/>
    public override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="Array1D.ToString{T}(T[], string, IFormatProvider)"/>
    public string ToString(string format, IFormatProvider provider) => Array1D.ToString(_Value, format, provider);
}