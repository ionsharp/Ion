using System;

namespace Ion.Threading;

public class TaskEventArgs(DateTime DateTime) : EventArgs
{
    public DateTime DateTime { get; } = DateTime;
}

public class TaskProgressedEventArgs(DateTime DateTime, ValueChange<double> Progress) : TaskEventArgs(DateTime)
{
    public ValueChange<double> Progress { get; } = Progress;
}