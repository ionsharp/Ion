namespace Ion.Numeral;

public enum NumberProperty
{
    /// <summary>
    ///  A positive (non-prime) integer that has more than two factors.  composite numbers can be divided evenly by numbers other than 1 and themselves.
    /// </summary>
    Composite,
    /// <summary>
    /// A positive (non-composite) integer that has exactly two distinct positive divisors (1 and the number itself).
    /// </summary>
    Prime,
}