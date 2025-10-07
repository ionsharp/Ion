using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Collect;

/// <summary>
/// The limit of items in an <see cref="IList{T}"/> and a <see cref="ListLimitAction"/> to do when exceeded.
/// </summary>
public readonly record struct ListLimit(int Value, ListLimitAction Action = ListLimit.DefaultAction) : IFormattable
{
    public const int Default = 500;

    public const ListLimitAction DefaultAction = ListLimitAction.RemoveFirst;

    public readonly ListLimitAction Action { get; } = Action;

    public readonly int Count { get; } = Value;

    public ListLimit() : this(Default, DefaultAction) { }

    public static implicit operator ListLimit(int i) => new(i);

    public static implicit operator int(ListLimit i) => i.Count;

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Count.ToString(format, provider);
}