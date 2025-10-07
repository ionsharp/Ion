namespace Ion.Core;

/// <summary>An <see cref="object"/> that subscribes and unsubscribes.</summary>
public interface ISubscribe
{
    /// <summary>Subscribe to something.</summary>
    void Subscribe();

    void Unsubscribe();
}

/// <summary>An <see cref="object"/> that subscribes and unsubscribes (with a parameter).</summary>
/// <typeparam name="T">A parameter.</typeparam>
public interface ISubscribe<T>
{
    /// <summary>Subscribe to something with a parameter.</summary>
    /// <param name="value">A parameter.</param>
    void Subscribe(T value);

    void Unsubscribe(T value);
}