using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ion.Input;

/// <summary>
/// An <see cref="ICommand"/> that can be defined weakly.
/// </summary>
public sealed class WeakCommand : ICommand
{
    private List<WeakReference> _CanExecuteHandlers;

    private readonly Func<object, Task> _Execute;

    private readonly Func<object, bool> _CanExecute;

    /// <exception cref="ArgumentNullException"/>
    public WeakCommand(Action<object> execute, Func<object, bool> canExecute)
    {
        Throw.IfNull(execute, nameof(execute));
        Throw.IfNull(canExecute, nameof(canExecute));

        _Execute = i => { execute(i); return Task.Delay(0); };
        _CanExecute = canExecute;

    }

    /// <exception cref="ArgumentNullException"/>
    public WeakCommand(Func<object, Task> execute, Func<object, bool> canExecute)
    {
        Throw.IfNull(execute, nameof(execute));
        Throw.IfNull(canExecute, nameof(canExecute));

        _Execute = execute;
        _CanExecute = canExecute;

    }

    private void OnCanExecuteChanged()
        => WeakCommandManager.CallWeakReferenecHandlers(this, _CanExecuteHandlers);

    public void RaiseCanExecuteChanger()
        => OnCanExecuteChanged();

    async void ICommand.Execute(object parameter) 
        => await Execute(parameter);

    private async Task Execute(object parameter)
        => await _Execute(parameter);

    bool ICommand.CanExecute(object parameter) 
        => CanExecute(parameter);

    private bool CanExecute(object parameter) 
        => _CanExecute == null || _CanExecute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add => WeakCommandManager.AddWeakReferenceHandler(ref _CanExecuteHandlers, value, 2);
        remove => WeakCommandManager.RemoveWeakReferenceHandler(_CanExecuteHandlers, value);
    }
}