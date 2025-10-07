using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Collect;

/// <summary>
/// The limit of items in an <see cref="ICollection{T}"/> and a <see cref="CollectionLimitAction"/> to do when exceeded.
/// </summary>
public readonly record struct CollectionLimit(int Value, CollectionLimitAction Action = CollectionLimit.DefaultAction) : IFormattable
{
    public const int Default = 500;

    public const CollectionLimitAction DefaultAction = CollectionLimitAction.Clear;

    public readonly CollectionLimitAction Action { get; } = Action;

    public readonly int Count { get; } = Value;

    public CollectionLimit() : this(Default, DefaultAction) { }

    public static implicit operator CollectionLimit(int i) => new(i);

    public static implicit operator int(CollectionLimit i) => i.Count;

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Count.ToString(format, provider);
}