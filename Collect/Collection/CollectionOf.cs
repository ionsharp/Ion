using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="ICollection"/>.
/// </summary>
public class CollectionOf<T> : ICollection<T>, ICollectionChanged<T>, ICollectionReset<T>
{
    /// <see cref="Region.Event"/>

    /// <inheritdoc cref="ICollectionChanged{T}.Added"/>
    public event CollectionAddedEventHandler<T> Added;

    /// <inheritdoc cref="ICollectionChanged{T}.Adding"/>
    public event CollectionAddingEventHandler<T> Adding;

    /// <inheritdoc cref="ICollectionChanged{T}.Changed"/>
    public event CollectionChangedEventHandler<T> Changed;

    /// <inheritdoc cref="ICollectionChanged{T}.Changing"/>
    public event CollectionChangingEventHandler<T> Changing;

    /// <inheritdoc cref="ICollectionChanged{T}.Cleared"/>
    public event CollectionClearedEventHandler<T> Cleared;

    /// <inheritdoc cref="ICollectionChanged{T}.Clearing"/>
    public event CollectionClearingEventHandler<T> Clearing;

    /// <inheritdoc cref="ICollectionChanged{T}.Removed"/>
    public event CollectionRemovedEventHandler<T> Removed;

    /// <inheritdoc cref="ICollectionChanged{T}.Removing"/>
    public event CollectionRemovingEventHandler<T> Removing;

    private ICollection<T> _Items = new List<T>();

    /// <inheritdoc cref="ICollection{T}.Count"/>
    public int Count => _Items.Count;

    /// <inheritdoc cref="ICollection{T}.IsReadOnly"/>
    public bool IsReadOnly => false;

    /// <inheritdoc cref="ICollectionReset{T}.DefaultItems"/>
    public virtual IReadOnlyCollection<T> DefaultItems => [];

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance.
    /// </summary>
    public CollectionOf() : this([]) { }

    /// <summary>
    /// Get new instance with given <see cref="{T}"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public CollectionOf(params T[] i)
    {
        Throw.IfNull(i, nameof(i));
        i.ForEach(Add);
    }

    /// <summary>
    /// Get new instance with given <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public CollectionOf(IEnumerable<T> i)
    {
        Throw.IfNull(i, nameof(i));
        i.ForEach(Add);
    }

    /// <see cref="Region.Method"/>

    /// <inheritdoc cref="ICollectionChanged{T}.Added"/>
    protected virtual void OnAdded(CollectionAddedEventArgs e) => Added?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Adding"/>
    protected virtual void OnAdding(CollectionAddingEventArgs e) => Adding?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Changed"/>
    protected virtual void OnChanged(CollectionChangedEventArgs e) => Changed?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Changing"/>
    protected virtual void OnChanging(CollectionChangingEventArgs e) => Changing?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Cleared"/>
    protected virtual void OnCleared(CollectionClearedEventArgs e) => Cleared?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Clearing"/>
    protected virtual void OnClearing(CollectionClearingEventArgs e) => Clearing?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Removed"/>
    protected virtual void OnRemoved(CollectionRemovedEventArgs e) => Removed?.Invoke(this, e);

    /// <inheritdoc cref="ICollectionChanged{T}.Removing"/>
    protected virtual void OnRemoving(CollectionRemovingEventArgs e) => Removing?.Invoke(this, e);

    /// <inheritdoc cref="ICollection{T}.Add(T)"/>
    public virtual void Add(T item)
    {
        var e1 = new CollectionAddingEventArgs(item);
        OnAdding(e1);

        var e2 = new CollectionChangingEventArgs(CollectionChange.Add, null, [item]);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _Items.Add(item);
            OnAdded(e1);
            OnChanged(e2);
        }
    }

    /// <inheritdoc cref="ICollection{T}.Clear"/>
    public virtual void Clear()
    {
        var e1 = new CollectionClearingEventArgs(Count);
        OnClearing(e1);

        var e2 = new CollectionChangingEventArgs(CollectionChange.Clear, null, null);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _Items.Clear();
            OnCleared(e1);
            OnChanged(e2);
        }
    }

    /// <inheritdoc cref="ICollection{T}.Contains(T)"/>
    public virtual bool Contains(T item) => _Items.Contains(item);

    /// <inheritdoc cref="ICollection{T}.CopyTo(T[], int)"/>
    public virtual void CopyTo(T[] array, int arrayIndex) => _Items.CopyTo(array, arrayIndex);

    /// <inheritdoc cref="ICollection{T}.Remove(T)"/>
    public virtual bool Remove(T item)
    {
        var result = false;

        var e1 = new CollectionRemovingEventArgs(item);
        OnRemoving(e1);
        if (e1.Cancel) return result;

        var e2 = new CollectionChangingEventArgs(CollectionChange.Remove, [item], null);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            result = _Items.Remove(item);
            OnRemoved(e1);
            OnChanged(e2);
        }
        return result;
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => _Items.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _Items.GetEnumerator();

    /// <see cref="IReset"/>

    void IReset.Reset() => this.Reset();
}