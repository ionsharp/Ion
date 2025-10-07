using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Ion.Input;

/// <summary>
/// A method that invokes based on a condition.
/// </summary>
/// <remarks>
/// Implements <see cref="ICommand"/>.
/// </remarks>
public abstract class Command(Action invoke, Func<bool> canInvoke = null) : object(), ICommand
{
    event EventHandler ICommand.CanExecuteChanged
    {
        add { throw new NotImplementedException(); }
        remove { throw new NotImplementedException(); }
    }

    public Func<bool> CanInvoke { get; }
        = canInvoke;

    public Action Invoke { get; }
        = invoke ?? throw new ArgumentNullException(nameof(invoke));

    protected Command() : this(() => { }, null) { }

    [DebuggerStepThrough]
    public virtual bool CanExecute(object i) => CanInvoke?.Invoke() != false;

    public virtual void Execute(object i) => Invoke();
}