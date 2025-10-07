using Ion.Input;
using System;

namespace Ion.Core;

/// <summary>
/// A <see cref="Model"/> that implements <see cref="ICheck"/>.
/// </summary>
public record class Checkable() : Model(), ICheck
{
    public event CheckEventHandler Checked;

    public virtual bool? IsChecked { get => Get<bool?>(); set => Set(value); }

    public Checkable(bool isChecked = false) : this() => IsChecked = isChecked;

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(IsChecked))
        {
            IsChecked?.If(OnChecked, OnUnchecked);
            OnChecked(IsChecked);
        }
    }

    public override string ToString(string format, IFormatProvider provider)
        => IsChecked?.ToString(provider) ?? "Indeterminate";

    protected virtual void OnChecked() { }

    protected virtual void OnChecked(bool? state) => Checked?.Invoke(this, new(state));

    protected virtual void OnIndeterminate() => OnChecked(null);

    protected virtual void OnUnchecked() { }
}

/// <inheritdoc/>
public record class Checkable<T>(bool isChecked) : Checkable(isChecked)
{
    public T Value { get => Get<T>(); set => Set(value); }

    public Checkable() : this(false) { }

    public Checkable(T value, bool isChecked = false) : this(isChecked) => Value = value;
}