using System;

namespace Ion.Numeral;

/// <summary>An axis in 3-dimensional space.</summary>
[Flags]
public enum Axis3
{
    None = 0,
    /// <summary>The X-axis.</summary>
    X = 1,
    /// <summary>The Y-axis.</summary>
    Y = 2,
    /// <summary>The Z-axis.</summary>
    Z = 4,
    All = X | Y | Z
}