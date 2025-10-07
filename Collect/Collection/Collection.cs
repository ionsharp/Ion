using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc/>
public class Collection : CollectionOf<Object>
{
    /// <inheritdoc/>
    public Collection() : base() { }

    /// <inheritdoc/>
    public Collection(params Object[] i) : base(i) { }

    /// <inheritdoc/>
    public Collection(IEnumerable<Object> i) : base(i) { }
}