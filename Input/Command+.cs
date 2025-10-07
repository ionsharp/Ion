using System;
using System.Diagnostics;

namespace Ion.Input;

/// <summary>
/// A <see cref="Command"/> with a parameter.
/// </summary>
/// <inheritdoc/>
public abstract class Command<T>(Action<T> invoke, Predicate<T> canInvoke) : Command()
{
    new public Predicate<T> CanInvoke => canInvoke;

    new public Action<T> Invoke => invoke ?? throw new ArgumentNullException(nameof(invoke));

    [DebuggerStepThrough]
    public override bool CanExecute(object i) => CanInvoke?.Invoke((T)i) != false;

    public override void Execute(object i) => Invoke((T)i);
}