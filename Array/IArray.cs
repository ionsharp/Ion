using System;
using System.Collections;

namespace Ion;

/// <summary>
/// An <i>n</i>-dimensional set of values.
/// </summary>
public interface IArray : IEnumerable, IFormattable
{
    public int Length { get; }

    public object this[int x] { get; }
}