using System;
using System.Collections.Generic;

namespace Ion.Collect;

public class ListAddedEventArgs(int NewIndex, object NewItem) : EventArgs
{
    public int NewIndex { get; } = NewIndex;

    public object NewItem { get; } = NewItem;
}

public class ListAddingEventArgs(int NewIndex, object NewItem)
    : ListAddedEventArgs(NewIndex, NewItem)
{
    public bool Cancel { get; set; }
}

public class ListChangedEventArgs(ListChange Change, object[] OldItems, object[] NewItems, int OldIndex, int NewIndex)
    : EventArgs
{
    public ListChange Change { get; } = Change;

    public IEnumerable<object> OldItems { get; } = OldItems;

    public IEnumerable<object> NewItems { get; } = NewItems;

    public int OldIndex { get; } = OldIndex;

    public int NewIndex { get; } = NewIndex;
}

public class ListChangingEventArgs(ListChange Change, object[] OldItems, object[] NewItems, int OldIndex, int NewIndex)
    : ListChangedEventArgs(Change, OldItems, NewItems, OldIndex, NewIndex)
{
    public bool Cancel { get; set; }
}

public class ListClearedEventArgs(int Count)
    : EventArgs
{
    public int Count { get; } = Count;
}

public class ListClearingEventArgs(int Count)
    : ListClearedEventArgs(Count)
{
    public bool Cancel { get; set; }
}

public class ListMovedEventArgs(int OldIndex, int NewIndex, object Item)
    : EventArgs
{
    public int OldIndex { get; } = OldIndex;

    public int NewIndex { get; } = NewIndex;

    public object Item { get; } = Item;
}

public class ListMovingEventArgs(int OldIndex, int NewIndex, object Item)
    : ListMovedEventArgs(OldIndex, NewIndex, Item)
{
    public bool Cancel { get; set; }
}

public class ListRemovedEventArgs(int OldIndex, object OldItem)
    : EventArgs
{
    public int OldIndex { get; } = OldIndex;

    public object OldItem { get; } = OldItem;
}

public class ListRemovingEventArgs(int OldIndex, object OldItem)
    : ListRemovedEventArgs(OldIndex, OldItem)
{
    public bool Cancel { get; set; }
}

public class ListReplacedEventArgs(int Index, object OldItem, object NewItem)
    : EventArgs
{
    public int Index { get; } = Index;

    public object OldItem { get; } = OldItem;

    public object NewItem { get; } = NewItem;
}

public class ListReplacingEventArgs(int Index, object OldItem, object NewItem)
    : ListReplacedEventArgs(Index, OldItem, NewItem)
{
    public bool Cancel { get; set; }
}