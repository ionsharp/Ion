using System;

namespace Ion.Analysis;

/// <summary>
/// The level of an <see cref="IEntry"/>.
/// </summary>
[Flags]
public enum EntryLevel
{
    [Hide]
    None = 0,
    Low = 1,
    Normal = 2,
    High = 4,
    [Hide]
    All = Low | Normal | High
}