using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList{T}"/> that notifies listeners of dynamic changes.
/// </summary>
public interface IListChanged<T> : IList<T>
{
    /// <summary>
    /// Occurs after item is added.
    /// </summary>
    public event ListAddedEventHandler<T> Added;

    /// <summary>
    /// Occurs before item is added.
    /// </summary>
    public event ListAddingEventHandler<T> Adding;

    /// <summary>
    /// Occurs after changing.
    /// </summary>
    public event ListChangedEventHandler<T> Changed;

    /// <summary>
    /// Occurs before changing.
    /// </summary>
    public event ListChangingEventHandler<T> Changing;

    /// <summary>
    /// Occurs after clearing.
    /// </summary>
    public event ListClearedEventHandler<T> Cleared;

    /// <summary>
    /// Occurs before clearing.
    /// </summary>
    public event ListClearingEventHandler<T> Clearing;

    /// <summary>
    /// Occurs after item is moved.
    /// </summary>
    public event ListMovedEventHandler<T> Moved;

    /// <summary>
    /// Occurs before item is moved.
    /// </summary>
    public event ListMovingEventHandler<T> Moving;

    /// <summary>
    /// Occurs after item is removed.
    /// </summary>
    public event ListRemovedEventHandler<T> Removed;

    /// <summary>
    /// Occurs before item is removed.
    /// </summary>
    public event ListRemovingEventHandler<T> Removing;

    /// <summary>
    /// Occurs after item is replaced.
    /// </summary>
    public event ListReplacedEventHandler<T> Replaced;

    /// <summary>
    /// Occurs before item is replaced.
    /// </summary>
    public event ListReplacingEventHandler<T> Replacing;
}