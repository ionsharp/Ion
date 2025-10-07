namespace Ion.Numeral;

/// <summary>
/// The side of an <see cref="IMatrix"/> where a diagonal lies.
/// </summary>
/// <remarks>
/// <para><b><see cref="Left"/></b> (Unconventional)</para>
/// [0, 0, 1]<br/>
/// [0, 1, 0]<br/>
/// [1, 0, 0]
/// <para><b><see cref="Right"/></b> (Conventional)</para>
/// [1, 0, 0]<br/>
/// [0, 1, 0]<br/>
/// [0, 0, 1]
/// </remarks>
public enum MatrixDiagonal
{
    /// <summary>
    /// [0, 0, 1]<br/>
    /// [0, 1, 0]<br/>
    /// [1, 0, 0]
    /// </summary>
    Left,
    /// <summary>
    /// [1, 0, 0]<br/>
    /// [0, 1, 0]<br/>
    /// [0, 0, 1]
    /// </summary>
    Right
}