using Ion.Collect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ion.Numeral;

/// <inheritdoc cref="IPattern"/>
[Description(IPattern.Description)]
public readonly struct Pattern
    : IPattern<Pattern, Line<int>>, IArrayUnfixed<Line<int>>, IArrayUnfixedAlias<Pattern, Pattern, Line<int>>, IEquatable<Pattern>, IImmutable
{
    /// <see cref="Region.Field"/>

    public readonly int Length => _Value.Length;

    private readonly Line<int>[] _Value { get; }

    object IArray.this[int i] => _Value[i];

    object IArray1D.this[int i] => _Value[i];

    public Line<int> this[int i] => _Value[i];

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance from given <see cref="ILine"/>&lt;<see cref="int"/>>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArrayLengthZero"/>
    public Pattern(params ILine<int>[] i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<ArrayLengthZero>(i.Length == 0, nameof(i));
        _Value = Array1D.Get(i.Length, j =>
        {
            var k = i[j];
            Throw.IfNull(k, nameof(i));

            return new Line<int>(k);
        });
    }

    /// <summary>
    /// Get new instance from given <see cref="IEnumerable"/>&lt;<see cref="ILine"/>&lt;<see cref="int"/>>>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public Pattern(in IEnumerable<ILine<int>> i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<EnumerableEmpty>(!i.Any(), nameof(i));
        _Value = i.Select(j =>
        {
            Throw.IfNull(j, nameof(i));
            return new Line<int>(j);
        })
        .ToArray();
    }

    /// <summary>
    /// Get new instance from given <see cref="IEnumerable"/>&lt;<see cref="ILine"/>&lt;<see cref="int"/>>>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EnumerableEmpty"/>
    public Pattern(in IEnumerable<Line<int>> i)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<EnumerableEmpty>(!i.Any(), nameof(i));
        _Value = i.ToArray();
    }

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public Line<int>[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<Pattern, Line<int>>.GetArray() => new int[Length];

    static Pattern IArray<Pattern, Line<int>>.Create(Pattern oldSelf, Array newSelf) => new((Line<int>[])newSelf);

    /// <see cref="IEnumerable{T}"/>

    public IEnumerator<Line<int>> GetEnumerator() => (_Value as IEnumerable<Line<int>>).GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IEquatable{}"/>

    public static bool operator ==(Pattern a, Pattern b) => XEquatable.CheckOperator(a, b);

    public static bool operator !=(Pattern a, Pattern b) => !(a == b);

    public readonly bool Equals(Pattern i) => IPattern.Equals(this, i);

    public readonly override bool Equals(object i) => i is Pattern j && Equals(j);

    public readonly override int GetHashCode() => IPattern.GetHashCode(this);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IPattern.ToString(this, format, provider);
}