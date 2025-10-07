using Ion.Numeral;
using System;
using System.Globalization;

namespace Ion.Collect;

/// <summary>
/// The limit of items in an <see cref="IListWritable{T}"/> and a <see cref="ListWritableLimit"/> to do when exceeded.
/// </summary>
public readonly record struct ListWritableLimit(int Value, ListWritableLimitAction Action = ListWritableLimit.DefaultAction) : IFormattable
{
    public const int Default = 500;

    public const ListWritableLimitAction DefaultAction = ListWritableLimitAction.RemoveFirst;

    public readonly ListWritableLimitAction Action { get; } = Action;

    public readonly int Count { get; } = Value;

    public ListWritableLimit() : this(Default, DefaultAction) { }

    public static implicit operator ListWritableLimit(int i) => new(i);

    public static implicit operator int(ListWritableLimit i) => i.Count;

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Count.ToString(format, provider);
}