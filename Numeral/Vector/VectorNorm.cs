namespace Ion.Numeral;

/// <summary>A scalar as measurement of size or magnitude.</summary>
/// <remarks>
/// <list type="bullet">
/// <item>Homogeneity: The norm is scaled by a scalar factor when the vector is scaled by the same factor.</item>
/// <item>Positive Definiteness: The norm is only zero for the zero vector.</item>
/// <item>Triangle Inequality: The norm of the sum of two vectors is bounded by the sum of their individual norms.</item>
/// </list>
/// </remarks>
public enum VectorNorm
{
    /// <summary>
    /// <b>L1 (1-Norm)</b><br/>
    /// The sum of the absolute values in the set.
    /// </summary>
    /// <remarks>|x1| + |x2| + ... + |xn|</remarks>
    L1,
    /// <summary>
    /// <b>L2 (2-Norm/Euclidean)</b><br/>
    /// The square root of the sum of values squared (most commonly used).
    /// </summary>
    /// <remarks>√(x1^2 + x2^2 + ... + xn^2)</remarks>
    L2,
    /// <summary>
    /// <b>L∞ (∞-Norm)</b><br/>
    /// The maximum absolute value of the set.
    /// </summary>
    /// <remarks>max(|x1|, |x2|, ..., |xn|)</remarks>
    LInfinity
}