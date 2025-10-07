using Ion.Core;
using System;

namespace Ion.Numeral;

/// <inheritdoc cref="ICurve"/>
[Description(Description)]
public abstract record class Curve() : Model(), ICurve, IFormattable
{
    public const string Description = "The rate of change relative to a parameter over time.";

    /// <inheritdoc/>
    public abstract double Do(double x, double y, double value);
}