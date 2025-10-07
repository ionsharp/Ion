namespace Ion.Collect;

public delegate void CollectionAddedEventHandler<T>(ICollectionChanged<T> sender, CollectionAddedEventArgs e);

public delegate void CollectionAddingEventHandler<T>(ICollectionChanged<T> sender, CollectionAddingEventArgs e);

public delegate void CollectionChangedEventHandler<T>(ICollectionChanged<T> sender, CollectionChangedEventArgs e);

public delegate void CollectionChangingEventHandler<T>(ICollectionChanged<T> sender, CollectionChangingEventArgs e);

public delegate void CollectionClearedEventHandler<T>(ICollectionChanged<T> sender, CollectionClearedEventArgs e);

public delegate void CollectionClearingEventHandler<T>(ICollectionChanged<T> sender, CollectionClearingEventArgs e);

public delegate void CollectionRemovedEventHandler<T>(ICollectionChanged<T> sender, CollectionRemovedEventArgs e);

public delegate void CollectionRemovingEventHandler<T>(ICollectionChanged<T> sender, CollectionRemovingEventArgs e);