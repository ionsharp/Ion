using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion;

/// <inheritdoc cref="IArray2D"/>
/// <remarks><b><see langword="T"/>[][] (for speed) with <see langword="T"/>[,] access.</b></remarks>
[CollectionBuilder(typeof(Array2DBuilder), nameof(Array2DBuilder.Create))]
[Using(typeof(Array2D))]
public sealed class Array2D<T>
    : IArray2D<Array2D<T>, T>, IArray2DRank<T>, IArrayUnfixed<T>, IArrayUnfixedAlias<Array2D<T>, Array2D<T>, T>, IMutable
{
    /// <see cref="Region.Field"/>

    private readonly T[][] _Value;

    /// <see cref="Region.Property"/>

    public int XLength => _Value[0].Length;

    public int YLength => _Value.Length;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => this[y];

    object IArray1D.this[int y] => this[y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public T[] this[int y] { get => _Value[y]; set => _Value[y] = value; }
#pragma warning restore CA1819

    public T this[int y, int x] { get => _Value[y][x]; set => _Value[y][x] = value; }

    /// <summary>
    /// Get new instance from given <see cref="object"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Array2D(params T[] i)
    {
        Throw.IfNull(i, nameof(i));
        _Value = [i];
    }

    /// <summary>
    /// Get new instance from given <see cref="object"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Array2D(T[][] i)
    {
        Throw.IfNull(i, nameof(i));
        _Value = i;
    }

    /// <see cref="Region.Operator"/>
    #region 

    public static implicit operator Array(Array2D<T> i) => i._Value;

    public static implicit operator T[][](Array2D<T> i) => i._Value;

    public static implicit operator Array2D<T>(T[] i) => new(i);

    public static implicit operator Array2D<T>(T[][] i) => new(i);

#pragma warning disable CA1814 /// Prefer jagged arrays
    public static implicit operator T[,](Array2D<T> i) => i._Value.As();

    public static implicit operator Array2D<T>(T[,] i) => new(i.As());
#pragma warning restore CA1814

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public T[][] ToArray() => [.. _Value];

    public T[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IArray{,}"/>

    Array IArray<Array2D<T>, T>.GetArray() => _Value;

    static Array2D<T> IArray<Array2D<T>, T>.Create(Array2D<T> oldSelf, Array newSelf) => oldSelf;

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (_Value as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc/>
    public override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="Array2D.ToString{T}(T[][], string, IFormatProvider)"/>
    public string ToString(string format, IFormatProvider provider) => Array2D.ToString(_Value, format, provider);
}