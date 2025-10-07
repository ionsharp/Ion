using Ion.Core;

/// <summary>
/// A binding between two instances of <see cref="object"/>.
/// </summary>
public interface IBinding : ISubscribe
{
    BindMode Mode { get; }

    object Source { get; }

    object Target { get; }
}

/// <summary>
/// A binding between two instances of <see cref="{T}"/>.
/// </summary>
public interface IBinding<T> : IBinding
{
    new T Source { get; }

    new T Target { get; }
}

/// <inheritdoc cref="IBinding"/>
/// <remarks><b>Implements <see cref="IBinding"/>.</b></remarks>
public abstract class Binding<T>(BindMode Mode, T Source, T Target) : IBinding<T>
{
    public BindMode Mode { get; } = Mode;

    public T Source { get; } = Source;

    public T Target { get; } = Target;

    object IBinding.Source => Source;

    object IBinding.Target => Target;

    public abstract void Subscribe();

    public abstract void Unsubscribe();
}