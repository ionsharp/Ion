using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>A list that manages items internally.</summary>
/// <typeparam name="T">The type of items.</typeparam>
public interface ICollectionProtected<T> : ICollection<T>
{
    T this[int index] { get; }

    /// <summary>Load items with a parameter.</summary>
    /// <param name="i">A parameter used to load items.</param>
    void Load(object parameter);

    /// <summary>Unload items with a parameter.</summary>
    /// <param name="i">A parameter used to unload items.</param>
    void Unload(object parameter);
}

public interface ICollectionProtected<TValue, TParameter> : ICollectionProtected<TValue>
{
    /// <summary>Load items with parameter of type <see cref="TParameter"/>.</summary>
    /// <param name="i">A parameter used to load items.</param>
    void Load(TParameter parameter);

    /// <summary>Unload items with parameter of type <see cref="TParameter"/>.</summary>
    /// <param name="i">A parameter used to unload items.</param>
    void Unload(TParameter parameter);
}