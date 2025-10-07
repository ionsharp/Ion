namespace Ion.Input;

/// <summary>
/// A <see cref="Func"/> that can be defined weakly.
/// </summary>
public interface IWeakFunc
{
    object Execute();
}

/// <inheritdoc/>
public interface IWeakFunctionWithParameter : IWeakFunc
{
    object Execute(object parameter);
}