using System;

namespace Ion.Input;

/// <see cref="CancelEventHandler"/>
#region

public class CancelEventArgs(object Parameter = default) : CancelEventArgs<object>(Parameter);

public class CancelEventArgs<T1>(T1 Parameter = default) : EventArgs<T1>(Parameter)
{
    public bool Cancel { get; set; }
}

public delegate void CancelEventHandler(object sender, CancelEventArgs e);

public delegate void CancelEventHandler<T1>(object sender, CancelEventArgs<T1> e);

#endregion

/// <see cref="ChangeEventHandler"/>
#region

public class ChangeEventArgs(object OldValue, object NewValue) : ChangeEventArgs<object>(OldValue, NewValue);

public class ChangeEventArgs<T1>(T1 OldValue, T1 NewValue) : ChangeEventArgs<T1, T1>(OldValue, NewValue);

public class ChangeEventArgs<T1, T2>(T1 OldValue, T2 NewValue) : EventArgs()
{
    public readonly T1 OldValue = OldValue;

    public readonly T2 NewValue = NewValue;
}

public delegate void ChangeEventHandler(object sender, ChangeEventArgs e);

public delegate void ChangeEventHandler<T1>(object sender, ChangeEventArgs<T1> e);

public delegate void ChangeEventHandler<T1, T2>(object sender, ChangeEventArgs<T1, T2> e);

#endregion

/// <see cref="CheckEventHandler"/>
#region

public class CheckEventArgs(bool? IsChecked, object Parameter = null) : CheckEventArgs<object>(IsChecked, Parameter);

public class CheckEventArgs<T1>(bool? IsChecked, T1 Parameter = default) : EventArgs<T1>(Parameter)
{
    public bool? IsChecked { get; } = IsChecked;
}

public delegate void CheckEventHandler(object sender, CheckEventArgs e);

public delegate void CheckEventHandler<T1>(object sender, CheckEventArgs<T1> e);

#endregion

/// <see cref="LockEventHandler"/>
#region

public class LockEventArgs(bool IsLocked, object Parameter = null) : LockEventArgs<object>(IsLocked, Parameter);

public class LockEventArgs<T1>(bool IsLocked, T1 Parameter = default) : EventArgs<T1>(Parameter)
{
    public bool IsLocked { get; } = IsLocked;
}

public delegate void LockEventHandler(object sender, LockEventArgs e);

public delegate void LockEventHandler<T1>(object sender, LockEventArgs<T1> e);

#endregion

/// <see cref="SelectEventHandler"/>
#region

public class SelectEventArgs(bool IsSelected, object Parameter = null) : SelectEventArgs<object>(IsSelected, Parameter);

public class SelectEventArgs<T1>(bool IsSelected, T1 Parameter = default) : EventArgs<T1>(Parameter)
{
    public bool IsSelected { get; } = IsSelected;
}

public delegate void SelectEventHandler(object sender, SelectEventArgs e);

public delegate void SelectEventHandler<T1>(object sender, SelectEventArgs<T1> e);

#endregion