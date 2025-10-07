using System;

namespace Ion.Core;

/// <summary>
/// A <see cref="Model"/> that gets and sets a value.
/// </summary>
public record class Accessor<T>(Func<T> get, Action<T> set) : Model
{
    public readonly Func<T> Get = get;

    public readonly Action<T> Set = set;

    public T Value
    {
        get => Get();
        set
        {
            Set(value);
            Reset(() => Value);
        }
    }
}

/// <inheritdoc/>
public record class Accessor(Func<Object> get, Action<Object> set) : Accessor<Object>(get, set);