namespace Ion.Collect;

public delegate void ListAddedEventHandler<T>(IListChanged<T> sender, ListAddedEventArgs e);

public delegate void ListAddingEventHandler<T>(IListChanged<T> sender, ListAddingEventArgs e);

public delegate void ListChangedEventHandler<T>(IListChanged<T> sender, ListChangedEventArgs e);

public delegate void ListChangingEventHandler<T>(IListChanged<T> sender, ListChangingEventArgs e);

public delegate void ListClearedEventHandler<T>(IListChanged<T> sender, ListClearedEventArgs e);

public delegate void ListClearingEventHandler<T>(IListChanged<T> sender, ListClearingEventArgs e);

public delegate void ListMovedEventHandler<T>(IListChanged<T> sender, ListMovedEventArgs e);

public delegate void ListMovingEventHandler<T>(IListChanged<T> sender, ListMovingEventArgs e);

public delegate void ListRemovedEventHandler<T>(IListChanged<T> sender, ListRemovedEventArgs e);

public delegate void ListRemovingEventHandler<T>(IListChanged<T> sender, ListRemovingEventArgs e);

public delegate void ListReplacedEventHandler<T>(IListChanged<T> sender, ListReplacedEventArgs e);

public delegate void ListReplacingEventHandler<T>(IListChanged<T> sender, ListReplacingEventArgs e);