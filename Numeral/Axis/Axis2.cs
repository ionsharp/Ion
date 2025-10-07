using System;

namespace Ion.Numeral;

/// <summary>An axis in 1-dimensional space.</summary>
[Flags]
public enum Axis1
{
    None = 0,
    /// <summary>The X-axis.</summary>
    X = 1,
    All = X
}

/// <summary>An axis in 2-dimensional space.</summary>
[Flags]
public enum Axis2
{
    None = 0,
    /// <summary>The X-axis.</summary>
    X = 1,
    /// <summary>The Y-axis.</summary>
    Y = 2,
    All = X | Y
}