using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc/>
public class List : ListOf<Object>
{
    /// <inheritdoc/>
    public List() : base() { }

    /// <inheritdoc/>
    public List(params Object[] i) : base(i) { }

    /// <inheritdoc/>
    public List(IEnumerable<Object> i) : base(i) { }
}