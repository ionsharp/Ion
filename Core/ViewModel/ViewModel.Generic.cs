namespace Ion.Core;

/// <summary>A <see cref="Model">model</see> of a <see cref="IView">view</see>.</summary>
public abstract record class ViewModel<T>() : ViewModel() where T : IView
{
    public virtual T View { get => Get<T>(default); set => Set(value); }

    protected ViewModel(T view) : this() => View = view;
}