using Ion.Collect;
using Ion.Input;
using System;

namespace Ion.Core;

/// <summary>
/// A <see cref="Model"/> that implements <see cref="IName"/>.
/// </summary>
public record class Namable() : Model(), IName
{
    public const string DefaultName = "Untitled";

    public const string StringFormat = "{0}";

    [field: NonSerialized]
    public event EventHandler<EventArgs<string>> NameChanged;

    public virtual string Name { get => Get(""); set => Set(value); }

    [Hide]
    public static object DefaultNames => new ListObservable<string>() { DefaultName };

    public Namable(string name) : this() => Name = name;

    protected virtual void OnNameChanged(string Value) => NameChanged?.Invoke(this, new EventArgs<string>(Value));

    protected virtual string OnPreviewNameChanged(string OldValue, string NewValue) => NewValue;

    public override void OnSettingProperty(PropertySettingEventArgs e)
    {
        base.OnSettingProperty(e);
        if (e.PropertyName == nameof(Name))
            e.NewValue = OnPreviewNameChanged((string)e.OldValue, (string)e.NewValue);
    }

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(Name))
            OnNameChanged(Name);
    }

    public override string ToString(string format, IFormatProvider provider) => StringFormat.F(Name);
}

/// <inheritdoc/>
public record class Namable<T>() : Namable()
{
    public virtual T Value { get => Get<T>(); set => Set(value); }

    public Namable(string name) : this() => Name = name;

    public Namable(string name, T value) : this(name) => Value = value;
}