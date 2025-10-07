using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc cref="ICollectionProtected{T}"/>
public abstract class ProtectedCollection<T>() : ICollectionProtected<T>
{
    /// <see cref="Region.Field"/>

    private readonly List<T> _Items = [];

    /// <see cref="Region.Property"/>

    public int Count => _Items.Count;

    bool ICollection<T>.IsReadOnly => throw new System.NotImplementedException();

    /// <see cref="Region.Property.Indexor"/>

    public T this[int i] { get => _Items[i]; protected set => _Items[i] = value; }

    /// <see cref="Region.Method"/>

    protected virtual void Add(T i)
        => _Items.Add(i);

    protected virtual void Add(T i, int index)
        => _Items.Insert(index, i);

    protected virtual void Clear()
        => _Items.Clear();

    protected virtual void Remove(T i)
        => _Items.Remove(i);

    protected virtual void RemoveAt(int i)
        => _Items.RemoveAt(i);

    /// <inheritdoc cref="ICollectionProtected{T}.Load(object)"/>
    public virtual void Load(object i) { }

    /// <inheritdoc cref="ICollectionProtected{T}.Unload(object)"/>
    public virtual void Unload(object i) { }

    /// <see cref="ICollection{T}"/>

    void ICollection<T>.Add(T item) => throw new NotSupportedException();

    void ICollection<T>.Clear() => throw new NotSupportedException();

    bool ICollection<T>.Contains(T item) => _Items.Contains(item);

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _Items.CopyTo(array, arrayIndex);

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

    /// <see cref="IEnumerable{T}"/>

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _Items.GetEnumerator();
}

/// <inheritdoc/>
public abstract class ProtectedCollection<Item, Parameter> : ProtectedCollection<Item>, ICollectionProtected<Item, Parameter>
{
    /// <inheritdoc cref="ICollectionProtected{X, Y}.Load(Y)"/>
    public virtual void Load(Parameter i) => base.Load(i);

    /// <inheritdoc cref="ICollectionProtected{X, Y}.Unload(Y)"/>
    public virtual void Unload(Parameter i) => base.Unload(i);
}