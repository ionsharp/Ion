using Ion.Input;

namespace Ion.Core;

/// <summary>
/// An <see cref="object"/> that can be selected and unselected.
/// </summary>
public interface ISelect
{
    event SelectEventHandler Selected;

    bool IsSelected { get; set; }
}

/// <summary>
/// A <see cref="Model"/> that implements <see cref="ISelect"/>.
/// </summary>
public record class Selectable() : Model(), ISelect
{
    public event SelectEventHandler Selected;

    public virtual bool IsSelected { get => Get<bool>(); set => Set(value); }

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(IsSelected))
            OnSelected(IsSelected);
    }

    public virtual void OnSelected(bool isSelected) => Selected?.Invoke(this, new(isSelected));
}