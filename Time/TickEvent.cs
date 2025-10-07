using System;

namespace Ion.Time;

public delegate void TickEventHandler(BaseTimer sender, TickEventArgs e);

public class TickEventArgs(TimeSpan elapsed) : EventArgs
{
    public readonly TimeSpan Elapsed = elapsed;
}