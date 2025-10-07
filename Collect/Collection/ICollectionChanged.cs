using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="ICollection{T}"/> that notifies listeners of dynamic changes.
/// </summary>
public interface ICollectionChanged<T> : ICollection<T>
{
    /// <summary>
    /// Occurs after item is added.
    /// </summary>
    public event CollectionAddedEventHandler<T> Added;

    /// <summary>
    /// Occurs before item is added.
    /// </summary>
    public event CollectionAddingEventHandler<T> Adding;

    /// <summary>
    /// Occurs after changing.
    /// </summary>
    public event CollectionChangedEventHandler<T> Changed;

    /// <summary>
    /// Occurs before changing.
    /// </summary>
    public event CollectionChangingEventHandler<T> Changing;

    /// <summary>
    /// Occurs after clearing.
    /// </summary>
    public event CollectionClearedEventHandler<T> Cleared;

    /// <summary>
    /// Occurs before clearing.
    /// </summary>
    public event CollectionClearingEventHandler<T> Clearing;

    /// <summary>
    /// Occurs after item is removed.
    /// </summary>
    public event CollectionRemovedEventHandler<T> Removed;

    /// <summary>
    /// Occurs before item is removed.
    /// </summary>
    public event CollectionRemovingEventHandler<T> Removing;
}