namespace Ion.Numeral;

/// <summary>
/// How to join one <see cref="IMatrix"/> with another.
/// </summary>
public enum MatrixJoin
{    
    /// <summary>
    /// = <see cref="Right"/>
    /// </summary>
    Default = 0,
    /// <summary>
    /// [a] ∘ [b]
    /// <para><b>Result</b></para>
    /// [b]<br/>
    /// [a]
    /// </summary>
    Above,
    /// <summary>
    /// [a] ∘ [b]
    /// <para><b>Result</b></para>
    /// [a]<br/>
    /// [b]
    /// </summary>
    Below,
    /// <summary>
    /// [a] ∘ [b]
    /// <para><b>Result</b></para>
    /// [b][a]
    /// </summary>
    Left,
    /// <summary>
    /// [a] ∘ [b]
    /// <para><b>Result</b></para>
    /// [a][b]<br/>
    /// </summary>
    Right,
}