namespace Ion.Numeral;

/// <summary>How the range of values in a set is calculated.</summary>
public enum RangeType
{
    /// <summary>The range is defined by the type of values in the set.</summary>
    /// <remarks>Example, [<see cref="double.MinValue"/>, <see cref="double.MaxValue"/>]</remarks>
    Type,
    /// <summary>The range is defined by the minimum and maximum value of the set.</summary>
    Weight,
}