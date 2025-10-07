using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList{T}"/> that notifies listeners of dynamic changes.
/// </summary>
public interface IListObservable : IList, INotifyCollectionChanged;

/// <inheritdoc/>
public interface IListObservable<T> : IListChanged<T>, INotifyCollectionChanged;