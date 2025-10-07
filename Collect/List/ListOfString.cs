using System;
using System.Collections.Generic;

namespace Ion.Collect;

/// <inheritdoc/>
public class ListOfString : ListOf<String>
{
    /// <inheritdoc/>
    public ListOfString() : base() { }

    /// <inheritdoc/>
    public ListOfString(params String[] i) : base(i) { }

    /// <inheritdoc/>
    public ListOfString(IEnumerable<String> i) : base(i) { }
}