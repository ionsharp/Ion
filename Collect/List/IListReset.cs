using System.Collections.Generic;

namespace Ion.Collect;

/// <summary>
/// An <see cref="IList{T}"/> that resets.
/// </summary>
public interface IListReset<T> : IList<T>, ICollectionReset<T>;