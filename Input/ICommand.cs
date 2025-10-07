using System.Windows.Input;

namespace Ion.Input;

/// <summary>
/// Extends <see cref="ICommand"/>.
/// </summary>
[Extend<ICommand>]
public static class XCommand
{
    public static bool CanExecute(this ICommand i) => i.CanExecute(null);

    public static void Execute(this ICommand i) => i.Execute(null);
}