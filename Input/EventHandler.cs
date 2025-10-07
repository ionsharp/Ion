namespace Ion.Input;

/// <summary>An <see langword="event"/> handler with 1 parameter.</summary>
public delegate void EventHandle<T1>(object sender, EventArgs<T1> e);

/// <summary>An <see langword="event"/> handler with 2 parameters.</summary>
public delegate void EventHandler<T1, T2>(object sender, EventArgs<T1, T2> e);

/// <summary>An <see langword="event"/> handler with 3 parameters.</summary>
public delegate void EventHandler<T1, T2, T3>(object sender, EventArgs<T1, T2, T3> e);

/// <summary>An <see langword="event"/> handler with 4 parameters.</summary>
public delegate void EventHandler<T1, T2, T3, T4>(object sender, EventArgs<T1, T2, T3, T4> e);

/// <summary>An <see langword="event"/> handler with 5 parameters.</summary>
public delegate void EventHandler<T1, T2, T3, T4, T5>(object sender, EventArgs<T1, T2, T3, T4, T5> e);

/// <summary>An <see langword="event"/> handler with 6 parameters.</summary>
public delegate void EventHandler<T1, T2, T3, T4, T5, T6>(object sender, EventArgs<T1, T2, T3, T4, T5, T6> e);

/// <summary>An <see langword="event"/> handler with 7 parameters.</summary>
public delegate void EventHandler<T1, T2, T3, T4, T5, T6, T7>(object sender, EventArgs<T1, T2, T3, T4, T5, T6, T7> e);

/// <summary>An <see langword="event"/> handler with 8+ parameters.</summary>
public delegate void EventHandler<T1, T2, T3, T4, T5, T6, T7, TRest>(object sender, EventArgs<T1, T2, T3, T4, T5, T6, T7, TRest> e);