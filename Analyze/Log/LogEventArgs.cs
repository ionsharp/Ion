using System;

namespace Ion.Analysis;

/// <inheritdoc/>
public class LogEventArgs(IEntry Entry) : EventArgs
{
    public IEntry Entry { get; } = Entry;
}