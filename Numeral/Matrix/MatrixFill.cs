namespace Ion.Numeral;

/// <summary>
/// How to fill an <see cref="IMatrix"/> given a length and 2 values.
/// </summary>
/// <remarks><b>The <see cref="IMatrix"/> is always square!</b></remarks>
public enum MatrixFill
{
    /// <summary>
    /// [a, b, a, b, a]<br/>
    /// [a, b, a, b, a]<br/>
    /// [a, b, a, b, a]<br/>
    /// [a, b, a, b, a]<br/>
    /// [a, b, a, b, a]<br/>
    /// </summary>
    AlternateX = 1,
    /// <summary>
    /// [a, a, a, a, a]<br/>
    /// [b, b, b, b, b]<br/>
    /// [a, a, a, a, a]<br/>
    /// [b, b, b, b, b]<br/>
    /// [a, a, a, a, a]<br/>
    /// </summary>
    AlternateY = 2,
    /// <summary>
    /// [a, b, a, b, a]<br/>
    /// [b, a, b, a, b]<br/>
    /// [a, b, a, b, a]<br/>
    /// [b, a, b, a, b]<br/>
    /// [a, b, a, b, a]<br/>
    /// </summary>
    /// <remarks><i>Default</i></remarks>
    AlternateXY = 0,
    /// <summary>
    /// <para><b>Motion Blur</b></para>
    /// [a, 0, 0, 0, a]<br/>
    /// [0, a, 0, a, 0]<br/>
    /// [0, 0, a, 0, 0]<br/>
    /// [0, a, 0, a, 0]<br/>
    /// [a, 0, 0, 0, a]<br/>
    /// </summary>
    DiagonalBoth = 3,
    /// <summary>
    /// <para><b>Identity Inverse</b></para>
    /// [b, b, b, b, a]<br/>
    /// [b, b, b, a, b]<br/>
    /// [b, b, a, b, b]<br/>
    /// [b, a, b, b, b]<br/>
    /// [a, b, b, b, b]<br/>
    /// </summary>
    DiagonalLeft,
    /// <summary>
    /// <para><b>Identity</b></para>
    /// [a, b, b, b, b]<br/>
    /// [b, a, b, b, b]<br/>
    /// [b, b, a, b, b]<br/>
    /// [b, b, b, a, b]<br/>
    /// [b, b, b, b, a]<br/>
    /// </summary>
    DiagonalRight,
    /// <summary>
    /// [b, b, b, b, a]<br/>
    /// [b, b, b, a, a]<br/>
    /// [b, b, a, a, a]<br/>
    /// [b, a, a, a, a]<br/>
    /// [a, a, a, a, a]<br/>
    /// </summary>
    TriagonalLowerLeft,
    /// <summary>
    /// [a, b, b, b, b]<br/>
    /// [a, a, b, b, b]<br/>
    /// [a, a, a, b, b]<br/>
    /// [a, a, a, a, b]<br/>
    /// [a, a, a, a, a]<br/>
    /// </summary>
    TriagonalLowerRight,
    /// <summary>
    /// [a, a, a, a, a]<br/>
    /// [a, a, a, a, b]<br/>
    /// [a, a, a, b, b]<br/>
    /// [a, a, b, b, b]<br/>
    /// [a, b, b, b, b]<br/>
    /// </summary>
    TriagonalUpperLeft,
    /// <summary>
    /// [a, a, a, a, a]<br/>
    /// [b, a, a, a, a]<br/>
    /// [b, b, a, a, a]<br/>
    /// [b, b, b, a, a]<br/>
    /// [b, b, b, b, a]<br/>
    /// </summary>
    TriagonalUpperRight,
}