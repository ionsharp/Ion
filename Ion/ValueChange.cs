using Ion.Core;
using System;

namespace Ion;

/// <summary>
/// A value that changed.
/// </summary>
public interface IValueChange
{
    /// <summary>
    /// The old value.
    /// </summary>
    object OldValue { get; }

    /// <summary>
    /// The new value.
    /// </summary>
    object NewValue { get; }
}

/// <summary>
/// A value that changed by a property.
/// </summary>
public interface IValueChangeOfProperty : IValueChange
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The <see cref="object"/> the property belongs to.
    /// </summary>
    object Source { get; }
}

/// <inheritdoc cref="IValueChange"/>
public record class ValueChange<T>(T OldValue, T NewValue) : object(), IImmutable, IValueChange
{
    public T OldValue { get; } = OldValue;

    public T NewValue { get; } = NewValue;

    public ValueChange(T newValue) : this(default, newValue) { }

    public static implicit operator ValueChange<T>(in (T OldValue, T NewValue) i) => new(i.OldValue, i.NewValue);

    public static implicit operator ValueChange<T>(in PropertySetEventArgs i) => ((T)i.OldValue, (T)i.NewValue);

    public static implicit operator (T OldValue, T NewValue)(in ValueChange<T> i) => new(i.OldValue, i.NewValue);

    object IValueChange.OldValue => OldValue;

    object IValueChange.NewValue => NewValue;
}

/// <inheritdoc/>
public record class ValueChange(Object OldValue, Object NewValue) : ValueChange<Object>(OldValue, NewValue)
{
    public ValueChange(object newValue) : this(default, newValue) { }
}

/// <inheritdoc cref="IValueChangeOfProperty"/>
public record class ValueChangeOfProperty<T>(object Source, string Name, T OldValue, T NewValue) : ValueChange<T>(OldValue, NewValue), IValueChangeOfProperty
{
    public string Name { get; } = Name;

    public object Source { get; } = Source;
}

/// <inheritdoc/>
public record class ValueChangeOfProperty(object Source, string Name, object OldValue, object NewValue) : ValueChangeOfProperty<object>(Source, Name, OldValue, NewValue);