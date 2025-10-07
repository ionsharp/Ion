using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc/>
public class ListObservableOfString : ListObservable<String>
{
    /// <inheritdoc/>
    public ListObservableOfString() : base() { }

    /// <inheritdoc/>
    public ListObservableOfString(params String[] i) : base(i) { }

    /// <inheritdoc/>
    public ListObservableOfString(IEnumerable<String> i) : base(i) { }
}