using System;
using System.Collections.Generic;

namespace Ion.Collect;

public class CollectionAddedEventArgs(object NewItem)
    : EventArgs
{
    public object NewItem { get; } = NewItem;
}

public class CollectionAddingEventArgs(object NewItem)
    : CollectionAddedEventArgs(NewItem)
{
    public bool Cancel { get; set; }
}

public class CollectionChangedEventArgs(CollectionChange Change, object[] OldItems, object[] NewItems)
    : EventArgs
{
    public CollectionChange Change { get; } = Change;

    public IEnumerable<object> OldItems { get; } = OldItems;

    public IEnumerable<object> NewItems { get; } = NewItems;
}

public class CollectionChangingEventArgs(CollectionChange Change, object[] OldItems, object[] NewItems)
    : CollectionChangedEventArgs(Change, OldItems, NewItems)
{
    public bool Cancel { get; set; }
}

public class CollectionClearedEventArgs(int Count)
    : EventArgs
{
    public int Count { get; } = Count;
}

public class CollectionClearingEventArgs(int Count)
    : CollectionClearedEventArgs(Count)
{
    public bool Cancel { get; set; }
}

public class CollectionRemovedEventArgs(object OldItem)
    : EventArgs
{
    public object OldItem { get; } = OldItem;
}

public class CollectionRemovingEventArgs(object OldItem)
    : CollectionRemovedEventArgs(OldItem)
{
    public bool Cancel { get; set; }
}