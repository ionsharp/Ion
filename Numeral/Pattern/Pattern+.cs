using Ion.Collect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="IPattern"/>
[Description(IPattern.Description)]
public readonly struct Pattern<T>
    : IPattern<Pattern<T>, Line<T>>, IArrayUnfixed<Line<T>>, IArrayUnfixedAlias<Pattern<T>, Pattern<T>, Line<T>>, IImmutable
    where T : INumber<T>
{
    /// <see cref="Region.Property"/>

    public readonly int Length => _Value.Length;

    private readonly Line<T>[] _Value { get; }

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int i] => _Value[i];

    object IArray1D.this[int i] => _Value[i];

    public Line<T> this[int i] => _Value[i];

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance from given <see cref="ILine"/>&lt;<see cref="T"/>>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayLengthZero"/>
    public Pattern(params ILine<T>[] i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArrayLengthZero>(i.Length == 0, nameof(i));
        _Value = Array1D.Get(i.Length, j =>
        {
            var k = i[j];
            Throw.IfNull(k, nameof(i));

            return new Line<T>(k);
        });
    }

    /// <summary>
    /// Get new instance from given <see cref="IEnumerable"/>&lt;<see cref="ILine"/>&lt;<see cref="T"/>>>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public Pattern(in IEnumerable<ILine<T>> i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<EnumerableEmpty>(!i.Any(), nameof(i));
        _Value = i.Select(j =>
        {
            Throw.IfNull(j, nameof(i));
            return new Line<T>(j);
        })
        .ToArray();
    }

    /// <summary>
    /// Get new instance from given <see cref="IEnumerable"/>&lt;<see cref="ILine"/>&lt;<see cref="T"/>>>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public Pattern(in IEnumerable<Line<T>> i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<EnumerableEmpty>(!i.Any(), nameof(i));
        _Value = i.ToArray();
    }

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public Line<T>[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<Pattern<T>, Line<T>>.GetArray() => new T[Length];

    static Pattern<T> IArray<Pattern<T>, Line<T>>.Create(Pattern<T> oldSelf, Array newSelf) => new((Line<T>[])newSelf);

    /// <see cref="IEnumerable{T}"/>

    public IEnumerator<Line<T>> GetEnumerator() => (_Value as IEnumerable<Line<T>>).GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IEquatable{}"/>

    public static bool operator ==(Pattern<T> a, Pattern<T> b) => XEquatable.CheckOperator(a, b);

    public static bool operator !=(Pattern<T> a, Pattern<T> b) => !(a == b);

    public readonly bool Equals(Pattern<T> i) => IPattern.Equals(this, i);

    public readonly override bool Equals(object i) => i is Pattern<T> j && Equals(j);

    public readonly override int GetHashCode() => IPattern.GetHashCode(this);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IPattern.ToString(this, format, provider);
}