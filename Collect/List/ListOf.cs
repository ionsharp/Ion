using Ion.Core;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList{T}"/>.
/// </summary>
/// <remarks>Wraps <see cref="List{T}"/>.</remarks>
public class ListOf<T> : IList<T>, IListChanged<T>, IListReset<T>
{
    /// <see cref="Region.Event"/>

    /// <inheritdoc cref="IListChanged{T}.Changed"/>
    public event ListChangedEventHandler<T> Changed;

    /// <inheritdoc cref="IListChanged{T}.Changing"/>
    public event ListChangingEventHandler<T> Changing;

    /// <inheritdoc cref="IListChanged{T}.Cleared"/>
    public event ListClearedEventHandler<T> Cleared;

    /// <inheritdoc cref="IListChanged{T}.Clearing"/>
    public event ListClearingEventHandler<T> Clearing;

    /// <inheritdoc cref="IListChanged{T}.Added"/>
    public event ListAddedEventHandler<T> Added;

    /// <inheritdoc cref="IListChanged{T}.Adding"/>
    public event ListAddingEventHandler<T> Adding;

    /// <inheritdoc cref="IListChanged{T}.Moved"/>
    public event ListMovedEventHandler<T> Moved;

    /// <inheritdoc cref="IListChanged{T}.Moving"/>
    public event ListMovingEventHandler<T> Moving;

    /// <inheritdoc cref="IListChanged{T}.Removed"/>
    public event ListRemovedEventHandler<T> Removed;

    /// <inheritdoc cref="IListChanged{T}.Removing"/>
    public event ListRemovingEventHandler<T> Removing;

    /// <inheritdoc cref="IListChanged{T}.Replaced"/>
    public event ListReplacedEventHandler<T> Replaced;

    /// <inheritdoc cref="IListChanged{T}.Replacing"/>
    public event ListReplacingEventHandler<T> Replacing;

    private ICollection<T> _Collection => _List;

    private readonly List<T> _List = new();

    /// <inheritdoc cref="ICollection{T}.Count"/>
    public int Count => _List.Count;

    /// <inheritdoc cref="ICollection{T}.IsReadOnly"/>
    public bool IsReadOnly => _Collection.IsReadOnly;

    /// <inheritdoc cref="ICollectionReset{T}.DefaultItems"/>
    public virtual IReadOnlyCollection<T> DefaultItems => [];

    public virtual T this[int index]
    {
        get => _List[index];
        set
        {
            var oldItem = _List[index];

            var e1 = new ListReplacingEventArgs(index, oldItem, value);
            OnReplacing(e1);
            if (e1.Cancel) return;

            var e2 = new ListChangingEventArgs(ListChange.Replace, [oldItem], [value], -1, index);
            OnChanging(e2);

            if (!e2.Cancel)
            {
                _List[index] = value;
                OnReplaced(e1);
                OnChanged(e2);
            }
        }
    }

    /// <see cref="Region.Constructor"/>

    /// <summary>
    /// Get new instance.
    /// </summary>
    public ListOf() : this([]) { }

    /// <summary>
    /// Get new instance with given <see cref="{T}"/>[].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public ListOf(params T[] i)
    {
        Throw.IfNull(i, nameof(i));
        i.ForEach(Add);
    }

    /// <summary>
    /// Get new instance with given <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public ListOf(IEnumerable<T> i)
    {
        Throw.IfNull(i, nameof(i));
        i.ForEach(Add);
    }

    /// <see cref="Region.Method"/>

    /// <inheritdoc cref="IListChanged{T}.Added"/>
    protected virtual void OnAdded(ListAddedEventArgs e) => Added?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Adding"/>
    protected virtual void OnAdding(ListAddingEventArgs e) => Adding?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Changed"/>
    protected virtual void OnChanged(ListChangedEventArgs e) => Changed?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Changing"/>
    protected virtual void OnChanging(ListChangingEventArgs e) => Changing?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Cleared"/>
    protected virtual void OnCleared(ListClearedEventArgs e) => Cleared?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Clearing"/>
    protected virtual void OnClearing(ListClearingEventArgs e) => Clearing?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Moved"/>
    protected virtual void OnMoved(ListMovedEventArgs e) => Moved?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Moving"/>
    protected virtual void OnMoving(ListMovingEventArgs e) => Moving?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Removed"/>
    protected virtual void OnRemoved(ListRemovedEventArgs e) => Removed?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Removing"/>
    protected virtual void OnRemoving(ListRemovingEventArgs e) => Removing?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Replaced"/>
    protected virtual void OnReplaced(ListReplacedEventArgs e) => Replaced?.Invoke(this, e);

    /// <inheritdoc cref="IListChanged{T}.Replacing"/>
    protected virtual void OnReplacing(ListReplacingEventArgs e) => Replacing?.Invoke(this, e);

    /// <inheritdoc cref="List{T}.Contains(T)"/>
    public bool Contains(T item) => _List.Contains(item);

    /// <inheritdoc cref="List{T}.CopyTo(T[], int)"/>
    public void CopyTo(T[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);

    /// <inheritdoc cref="List{T}.IndexOf(T)"/>
    public int IndexOf(T item) => _List.IndexOf(item);

    /// <inheritdoc cref="List{T}.Add(T)"/>
    public virtual void Add(T item)
    {
        var newIndex = Count - 1;

        var e1 = new ListAddingEventArgs(newIndex, item);
        OnAdding(e1);

        var e2 = new ListChangingEventArgs(ListChange.Add, null, [item], -1, newIndex);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _List.Add(item);
            OnAdded(e1);
            OnChanged(e2);
        }
    }

    /// <inheritdoc cref="List{T}.Clear"/>
    public virtual void Clear()
    {
        var e1 = new ListClearingEventArgs(Count);
        OnClearing(e1);

        var e2 = new ListChangingEventArgs(ListChange.Clear, null, null, -1, -1);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _List.Clear();
            OnCleared(e1);
            OnChanged(e2);
        }
    }

    /// <inheritdoc cref="List{T}.TrimExcess"/>
    /// <remarks>
    /// <para>Minimizes memory overhead once it is known that no new elements will be added.</para>
    /// <para><b>To release all memory referenced</b>:</para>
    /// <code>
    /// <see langword="this"/>.Clear();<br/>
    /// <see langword="this"/>.ClearExcess();
    /// </code>
    /// </remarks>
    public virtual void ClearExcess() => _List.TrimExcess();

    /// <inheritdoc cref="List{T}.Insert(int, T)"/>
    public virtual void Insert(int index, T item)
    {
        var e1 = new ListAddingEventArgs(index, item);
        OnAdding(e1);
        if (e1.Cancel) return;

        var e2 = new ListChangingEventArgs(ListChange.Add, null, [item], -1, index);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _List.Insert(index, item);
            OnAdded(e1);
            OnChanged(e2);
        }
    }

    public virtual void Move(int oldIndex, int newIndex)
    {
        var aItem = this[oldIndex];

        var e1 = new ListMovingEventArgs(oldIndex, newIndex, aItem);
        OnMoving(e1);
        if (e1.Cancel) return;

        var e2 = new ListChangingEventArgs(ListChange.Move, [aItem], [aItem], oldIndex, newIndex);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            var bItem = this[newIndex];

            this[oldIndex] = bItem;
            this[newIndex] = aItem;

            OnMoved(e1);
            OnChanged(e2);
        }
    }

    /// <inheritdoc cref="List{T}.Remove(T)"/>
    public virtual bool Remove(T item)
    {
        var result = false;
        var oldIndex = IndexOf(item);

        var e1 = new ListRemovingEventArgs(oldIndex, item);
        OnRemoving(e1);
        if (e1.Cancel) return result;

        var e2 = new ListChangingEventArgs(ListChange.Remove, [item], null, oldIndex, -1);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            result = _List.Remove(item);
            OnRemoved(e1);
            OnChanged(e2);
        }
        return result;
    }

    /// <inheritdoc cref="List{T}.RemoveAt(int)"/>
    public virtual void RemoveAt(int index)
    {
        var oldItem = this[index];

        var e1 = new ListRemovingEventArgs(index, oldItem);
        OnRemoving(e1);
        if (e1.Cancel) return;

        var e2 = new ListChangingEventArgs(ListChange.Remove, [oldItem], null, index, -1);
        OnChanging(e2);

        if (!e2.Cancel)
        {
            _List.RemoveAt(index);
            OnRemoved(e1);
            OnChanged(e2);
        }
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => _List.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _List.GetEnumerator();

    /// <see cref="IReset"/>

    void IReset.Reset() => this.Reset();
}