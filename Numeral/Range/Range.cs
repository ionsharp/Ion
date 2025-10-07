using System;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <inheritdoc cref="IRange"/>
[Description(Description)]
public readonly record struct Range<T>(T Minimum, T Maximum)
    : IRange<T>, IImmutable where T : IMinMaxValue<T>
{
    public const string Description = "A lower and upper bound.";

    public const string StringFormat = "Minimum = {0}, Maximum = {1}";

    /// <see cref="Region.Property"/>

    public readonly T Minimum { get; } = Minimum;

    public readonly T Maximum { get; } = Maximum;

    /// <see cref="Region.Constructor"/>

    public Range() : this(T.MinValue, T.MaxValue) { }

    public Range(T range) : this(range, range) { }

    public Range((T Minimum, T Maximum) range) : this(range.Minimum, range.Maximum) { }

    /// <see cref="IRange"/>

    readonly object IRange.Maximum => Maximum;

    readonly object IRange.Minimum => Minimum;

    /// <see cref="IRange{T}"/>

    readonly T IRange<T>.Maximum => Minimum;

    readonly T IRange<T>.Minimum => Minimum;

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
    {
        if (Minimum is IFormattable m && Maximum is IFormattable n)
            return StringFormat.F(m.ToString(format, provider), n.ToString(format, provider));

        return StringFormat.F(Minimum, Maximum);
    }
}