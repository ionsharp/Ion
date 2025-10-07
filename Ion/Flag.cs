using Ion;
using Ion.Collect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ion;

public readonly record struct Flag() : IFormattable
{
    public const string StringFormat = "{0}";

    readonly Dictionary<object, bool> Value = [];

    public int Count => Value?.Count ?? 0;

    public IEnumerable<object> Keys => Value?.Select(i => i.Key) ?? [];

    public Flag(Dictionary<object, bool> input) : this()
    {
        foreach (var i in input)
            Value.Add(i.Key, i.Value);
    }

    public Flag AddFlag(object key)
    {
        if (Value.ContainsKey(key))
            Value[key] = true;

        return new Flag(Value);
    }

    public Flag AddOrSet(object key, bool value)
    {
        Value.SetOrAdd(key, value);
        return this;
    }

    public bool Contains(object key) => Value.ContainsKey(key);

    public void Each(Action<object, bool> action)
        => Value?.ForEach(i => action(i.Key, i.Value));

    public void EachKey(Action<object> action)
        => Value?.ForEach(i => action(i.Key));

    public bool Has(object key)
        => Value.ContainsKey(key) && Value[key];

    public Flag RemoveFlag(object key)
    {
        if (Value.ContainsKey(key))
            Value[key] = false;

        return new Flag(Value);
    }

    public bool SameAs(Flag b)
    {
        if (Value is not null)
        {
            if (Count == b.Count)
            {
                for (var i = Count - 1; i >= 0; i--)
                {
                    var x = Value.ElementAt(i).Key;
                    var y = b.Value.ElementAt(i).Key;

                    if (!Equals(x, y))
                        return false;
                }
                return true;
            }
        }
        return false;
    }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(Text.StringFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => StringFormat.F(Value);
}