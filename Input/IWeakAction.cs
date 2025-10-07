using System;

namespace Ion.Input;

/// <summary>
/// An <see cref="Action"/> that can be defined weakly.
/// </summary>
public interface IWeakAction
{
    void Execute();
}

/// <inheritdoc/>
public interface IWeakActionWithParameter : IWeakAction
{
    void Execute(object parameter);
}