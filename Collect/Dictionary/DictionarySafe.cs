using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IDictionary{Key, Value}"/> with safe access (indexor doesn't <see langword="throw"/> when <see cref="{Key}"/> doesn't exist).
/// </summary>
/// <remarks>Wraps <see cref="Dictionary{Key, Value}"/> (inheriting isn't safe when cast to <see cref="IDictionary"/>).</remarks>
public class DictionarySafe<Key, Value>() : IDictionary, IDictionary<Key, Value>
{
    /// <see cref="Region.Field"/>

    private readonly Dictionary<Key, Value> _Dictionary = new();

    /// <see cref="Region.Property"/>

    private ICollection _ICollection => _Dictionary;

    private IDictionary _IDictionary1 => _Dictionary;

    private IDictionary<Key, Value> _IDictionary2 => _Dictionary;

    public virtual int Count => _Dictionary.Count;

    public virtual bool IsReadOnly => _IDictionary2.IsReadOnly;

    public virtual Value this[Key key]
    {
        get => (Value)this.GetOrAdd(key, null);
        set => this.SetOrAdd(key, value);
    }

    /// <see cref="IDictionary{Key, Value}"/>

    public virtual void Add(Key key, Value value) => _Dictionary.Add(key, value);

    public virtual void Add(KeyValuePair<Key, Value> item) => _IDictionary2.Add(item);

    public virtual void Clear() => _Dictionary.Clear();

    public virtual bool Contains(KeyValuePair<Key, Value> item) => _Dictionary.Contains(item);

    public virtual bool ContainsKey(Key key) => _Dictionary.ContainsKey(key);

    public virtual void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex) => _IDictionary2.CopyTo(array, arrayIndex);

    public virtual bool Remove(Key key) => _Dictionary.Remove(key);

    public virtual bool Remove(KeyValuePair<Key, Value> item) => _IDictionary2.Remove(item);

    public virtual bool TryGetValue(Key key, [MaybeNullWhen(false)] out Value value) => _Dictionary.TryGetValue(key, out value);

    /// <see cref="ICollection"/>

    bool ICollection.IsSynchronized => _ICollection.IsSynchronized;

    object ICollection.SyncRoot => _ICollection.SyncRoot;

    /// <see cref="ICollection{Key}"/>

    public ICollection<Key> Keys => _Dictionary.Keys;

    /// <see cref="ICollection{Value}"/>

    public ICollection<Value> Values => _Dictionary.Values;

    /// <see cref="IDictionary"/>

    object IDictionary.this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    bool IDictionary.IsFixedSize => _IDictionary1.IsFixedSize;

    ICollection IDictionary.Keys => _Dictionary.Keys;

    ICollection IDictionary.Values => _Dictionary.Values;

    void IDictionary.Add(object key, object value) => _IDictionary1.Add(key, value);

    bool IDictionary.Contains(object key) => _IDictionary1.Contains(key);

    void ICollection.CopyTo(Array array, int index) => _IDictionary1.CopyTo(array, index);

    IEnumerator IEnumerable.GetEnumerator() => _IDictionary1.GetEnumerator();

    IDictionaryEnumerator IDictionary.GetEnumerator() => _IDictionary1.GetEnumerator();

    void IDictionary.Remove(object key) => _IDictionary1.Remove(key);

    /// <see cref="IEnumerable"/>

    IEnumerator<KeyValuePair<Key, Value>> IEnumerable<KeyValuePair<Key, Value>>.GetEnumerator() => _Dictionary.GetEnumerator();
}