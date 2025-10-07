using System;

namespace Ion.Numeral;

/// <summary>A closed set of line segments connected by points.</summary>
public interface IShape : IFormattable
{
    public const string Description = "A closed set of line segments connected by points.";
}