using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion;

/// <inheritdoc cref="IArray3D"/>
/// <remarks><b><see langword="T"/>[][][] (for speed) with <see langword="T"/>[,,] access.</b></remarks>
[CollectionBuilder(typeof(Array3DBuilder), nameof(Array3DBuilder.Create))]
[Using(typeof(Array3D))]
public sealed class Array3D<T>
    : IArray3D<Array3D<T>, T>, IArray3DRank<T>, IArrayUnfixed<T>, IArrayUnfixedAlias<Array3D<T>, Array3D<T>, T>, IMutable
{
    /// <see cref="Region.Field"/>

    private readonly T[][][] _Value;

    /// <see cref="Region.Property"/>

    public int XLength => _Value[0][0].Length;

    public int YLength => _Value[0].Length;

    public int ZLength => _Value.Length;

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int x] => this[x];

    object IArray1D.this[int x] => this[x];

    object IArray2D.this[int y, int x] => this[y, x];

    object IArray3D.this[int z, int y, int x] => this[z, y, x];

    public T this[int z, int y, int x] { get => _Value[z][y][x]; set => _Value[z][y][x] = value; }

#pragma warning disable CA1819 /// Properties should not return arrays
    public T[] this[int z, int y] { get => _Value[z][y]; set => _Value[z][y] = value; }

    public T[][] this[int z] { get => _Value[z]; set => _Value[z] = value; }
#pragma warning restore CA1819

    /// <summary>
    /// Get new instance from given <see cref="object"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Array3D(T[] i)
    {
        Throw.IfNull(i, nameof(i));
        _Value = [[i]];
    }

    /// <summary>
    /// Get new instance from given <see cref="object"/>[][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Array3D(T[][] i)
    {
        Throw.IfNull(i, nameof(i));
        _Value = [i];
    }

    /// <summary>
    /// Get new instance from given <see cref="object"/>[][][].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public Array3D(params T[][][] i)
    {
        Throw.IfNull(i, nameof(i));
        _Value = i;
    }

    /// <see cref="Region.Operator"/>
    #region 

    public static implicit operator Array(Array3D<T> i) => i._Value;

    public static implicit operator T[][][](Array3D<T> i) => i._Value;

    public static implicit operator Array3D<T>(T[] i) => new(i);

    public static implicit operator Array3D<T>(T[][] i) => new(i);

    public static implicit operator Array3D<T>(T[][][] i) => new(i);

#pragma warning disable CA1814 /// Prefer jagged arrays
    public static implicit operator T[,,](Array3D<T> i) => i._Value.As();

    public static implicit operator Array3D<T>(T[,,] i) => new(i.As());
#pragma warning restore CA1814

    #endregion

    /// <see cref="IArray3D"/>

    object[][][] IArray3D.ToArray() => XArray3D.ToArray(_Value, i => (object)i);

    public T[][][] ToArray() => [.. _Value];

    public T[][] ToArray(int z) => this[z];

    public T[] ToArray(int z, int y) => this[z, y];

    /// <see cref="IArray{,}"/>

    Array IArray<Array3D<T>, T>.GetArray() => _Value;

    static Array3D<T> IArray<Array3D<T>, T>.Create(Array3D<T> oldSelf, Array newSelf) => oldSelf;

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (_Value as IEnumerable<T>).GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc/>
    public override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="Array3D.ToString{T}(T[][][], string, IFormatProvider)"/>
    public string ToString(string format, IFormatProvider provider) => Array3D.ToString(_Value, format, provider);
}