using System;

namespace Ion.Numeral;

/// <summary>A lower and upper bound.</summary>
public interface IRange : IFormattable
{
    object Maximum { get; }

    object Minimum { get; }
}