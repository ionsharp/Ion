using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc/>
public class CollectionOfString : CollectionOf<String>
{
    /// <inheritdoc/>
    public CollectionOfString() : base() { }

    /// <inheritdoc/>
    public CollectionOfString(params String[] i) : base(i) { }

    /// <inheritdoc/>
    public CollectionOfString(IEnumerable<String> i) : base(i) { }
}