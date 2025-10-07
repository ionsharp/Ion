using Ion.Input;

namespace Ion.Core;

/// <summary>
/// Specifies an <see cref="object"/> that can be locked and unlocked.
/// </summary>
public interface ILock
{
    event LockEventHandler Locked;

    bool IsLocked { get; set; }
}