using Ion.Input;

namespace Ion.Core;

/// <summary>
/// A <see cref="Model"/> that implements <see cref="ILock"/>.
/// </summary>
public record class Lockable() : Model(), ILock
{
    public event LockEventHandler Locked;

    public virtual bool IsLocked { get => Get<bool>(); set => Set(value); }

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(IsLocked))
            OnLocked(IsLocked);
    }

    public virtual void OnLocked(bool isLocked) => Locked?.Invoke(this, new(isLocked));
}