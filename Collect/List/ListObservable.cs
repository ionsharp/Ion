using Ion.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Ion.Collect;

/// <remarks>Extends <see cref="ObservableCollection{T}"/>.</remarks>
/// <inheritdoc cref="ObservableCollection{T}"/>
public class ListObservable<T> : ListOf<T>, IChange, IListLimited<T>, IListObservable, IListObservable<T>, IPropertyGet, IPropertySet
{
    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    /// <see cref="Monitor"/>

    /// <summary>
    /// Prevent reentrant calls.
    /// </summary>
    private sealed class Monitor : IDisposable
    {
        int _busyCount;

        public bool Busy => _busyCount > 0;

        public void Enter() => ++_busyCount;

        public void Dispose() => --_busyCount;
    }

    /// <inheritdoc cref="Monitor"/>
    private readonly Monitor _Monitor = new();

    /// <see cref="Region.Field"/>

    /// <summary>
    /// <b>Binding.IndexorName</b> is declared here to avoid dependency (<b>PresentationFramework.dll</b>).
    /// </summary>
    private const string IndexorName = "Item[]";

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="ListLimit"/>
    public ListLimit Limit
    {
        get; set
        {
            field = value;
            this.AssertLimit();
        }
    }
    = ListLimit.Default;

    public override T this[int index]
    {
        get => base[index];
        set
        {
            CheckReentrancy();

            T oldItem = this[index];
            base[index] = value;

            this.Reset(IndexorName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, value, index));
        }
    }

    /// <see cref="Region.Constructor"/>

    /// <inheritdoc/>
    public ListObservable() : base() { }

    /// <inheritdoc/>
    public ListObservable(params T[] items) : base(items) { }

    /// <inheritdoc/>
    public ListObservable(IEnumerable<T> items) : base(items) { }

    /// <see cref="Region.Method"/>
    #region

    /// <summary>
    /// Block reentrant attempts to change this collection (e.g., an <see cref="EventHandler"/> of <see cref="CollectionChanged"/> is not allowed to make changes to this collection.)
    /// </summary>
    /// <remarks>
    /// <para><b>Code</b></para>
    /// <code>
    /// <see langword="using"/> (<see cref="BlockReentrancy"/>)
    /// {
    ///     <see cref="ListObservable{T}"/>.OnCollectionChanged(<see langword="this"/>, <see langword="new"/> <see cref="NotifyCollectionChangedEventArgs"/>(action, item, index));
    /// }
    /// </code>
    /// </remarks>
    private Monitor BlockReentrancy()
    {
        _Monitor.Enter();
        return _Monitor;
    }

    /// <summary> 
    /// Check for reentrant attempts to change this collection. <see cref="Throw"/> if collection changes while another collection change is still being notified to other listeners.
    /// </summary>
    /// <remarks>
    /// Allow changes if only one listener. A problem arises if reentrant changes make the original <see cref="EventArgs"/> invalid for later listeners. This keeps existing code working (e.g., <b>Selector.SelectedItems</b>).
    /// </remarks>
    /// <exception cref="InvalidOperationException"/>
    private void CheckReentrancy()
    {
        if (_Monitor.Busy)
            Throw.If<InvalidOperationException>(CollectionChanged is not null && CollectionChanged.GetInvocationList().Length > 1);
    }

    /// <inheritdoc cref="INotifyCollectionChanged.CollectionChanged"/>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (CollectionChanged is not null)
        {
            using var _ = BlockReentrancy();
            CollectionChanged(this, e);
        }
    }

    /// <inheritdoc/>
    public override void Add(T item)
    {
        CheckReentrancy();
        base.Add(item);

        this.Reset(() => Count);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
    }

    /// <inheritdoc/>
    public override void Clear()
    {
        CheckReentrancy();
        base.Clear();

        this.Reset(() => Count);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    /// <inheritdoc/>
    public override void Insert(int index, T item)
    {
        CheckReentrancy();
        base.Insert(index, item);

        this.Reset(() => Count);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    /// <inheritdoc/>
    public override void Move(int oldIndex, int newIndex)
    {
        CheckReentrancy();
        T oldItem = this[oldIndex];

        base.Move(oldIndex, newIndex);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, oldItem, newIndex, oldIndex));
    }

    /// <inheritdoc/>
    public override bool Remove(T item)
    {
        CheckReentrancy();

        var oldIndex = IndexOf(item);
        var result = base.Remove(item);

        this.Reset(() => Count);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, oldIndex));
        return result;
    }

    /// <inheritdoc/>
    public override void RemoveAt(int index)
    {
        CheckReentrancy();

        T oldItem = this[index];
        base.RemoveAt(index);

        this.Reset(() => Count);
        this.Reset(IndexorName);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
    }

    #endregion

    /// <see cref="IChange"/>

    public virtual bool IsChanged { get => this.Get(false, false); set => this.Set(value, false); }

    /// <see cref="IPropertyGet"/>

    public virtual void OnGetProperty(PropertyGetEventData e) { }

    /// <see cref="IPropertySet"/>
    #region

    [field: NonSerialized]
    protected event PropertyChangedEventHandler PropertyChanged;
    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add => PropertyChanged += value; remove => PropertyChanged -= value; }

    [field: NonSerialized]
    public event PropertySetEventHandler PropertySet;

    [field: NonSerialized]
    Dictionary<string, object> IPropertySet.NonSerializedProperties { get; set; } = [];

    Dictionary<string, object> IPropertySet.SerializedProperties { get; set; } = [];

    public virtual void OnSetProperty(PropertySetEventArgs e)
    {
        PropertyChanged
            ?.Invoke(this, new(e.PropertyName));
        PropertySet
            ?.Invoke(this, e);
    }

    public virtual void OnSettingProperty(PropertySettingEventArgs e) { }

    [NotComplete]
    bool IList.IsFixedSize => false;

    [NotComplete]
    bool ICollection.IsSynchronized => false;

    [NotComplete]
    object ICollection.SyncRoot => null;

    object IList.this[int index] 
    { 
        get => this[index]; 
        set
        { 
            if (value is T i)
            {
                this[index] = i;
            }
        }
    }

    int IList.Add(object value)
    {
        if (value is T i)
        {
            Add(i);
            return Count - 1;
        }
        return -1;
    }

    bool IList.Contains(object value) => value is T i && Contains(i);

    int IList.IndexOf(object value) => value is T i ? IndexOf(i) : -1;

    void IList.Insert(int index, object value)
    {
        if (value is T i)
            Insert(index, i);
    }

    void IList.Remove(object value)
    {
        if (value is T i)
            Remove(i);
    }

    [NotComplete]
    void ICollection.CopyTo(Array array, int index)
    {

    }

    #endregion
}

/// <inheritdoc/>
public class ListObservable : ListObservable<Object>
{
    /// <inheritdoc/>
    public ListObservable() : base() { }

    /// <inheritdoc/>
    public ListObservable(params Object[] i) : base(i) { }

    /// <inheritdoc/>
    public ListObservable(IEnumerable<Object> i) : base(i) { }
}