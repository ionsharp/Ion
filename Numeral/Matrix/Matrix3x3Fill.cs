namespace Ion.Numeral;

/// <summary>How to fill an <see cref="IMatrix3x3"/> given an <see cref="IVector3"/>.</summary>
public enum Matrix3x3Fill
{
    /// <summary>See <see cref="Right"/>.</summary>
    Default = 0,
    /// <summary>
    /// [z, y, z]<br/>
    /// [y, x, y]<br/>
    /// [z, y, z]<br/>
    /// </summary>
    /// <remarks>Checkers, let x = z.</remarks>
    Alternate,
    /// <summary>
    /// [x, x, x]<br/>
    /// [y, y, y]<br/>
    /// [z, z, z]<br/>
    /// </summary>
    CenterX,
    /// <summary>
    /// [0, 0, 0]<br/>
    /// [x, y, z]<br/>
    /// [0, 0, 0]<br/>
    /// </summary>
    CenterY,
    /// <summary>
    /// [0, x, 0]<br/>
    /// [0, y, 0]<br/>
    /// [0, z, 0]<br/>
    /// </summary>
    CenterXY,
    /// <summary>
    /// [x, y, z]<br/>
    /// [x, y, z]<br/>
    /// [x, y, z]<br/>
    /// </summary>
    Down,
    /// <summary>
    /// [0, 0, x]<br/>
    /// [0, y, 0]<br/>
    /// [z, 0, 0]<br/>
    /// </summary>
    DiagonalLeft,
    /// <summary>
    /// [x, 0, x]<br/>
    /// [0, y, 0]<br/>
    /// [z, 0, z]<br/>
    /// </summary>
    DiagonalLeftRight,
    /// <summary>
    /// [x, 0, 0]<br/>
    /// [0, y, 0]<br/>
    /// [0, 0, z]<br/>
    /// </summary>
    /// <remarks>
    /// <see cref="Matrix2DProperty.Identity"/>
    /// </remarks>
    DiagonalRight,
    /// <summary>
    /// [x, x, x]<br/>
    /// [y, y, y]<br/>
    /// [z, z, z]<br/>
    /// </summary>
    Right,
    /// <summary>
    /// [x, 0, 0]<br/>
    /// [y, x, 0]<br/>
    /// [z, y, x]
    /// </summary>
    TriagonalLower,
    /// <summary>
    /// [x, y, z]<br/>
    /// [0, x, y]<br/>
    /// [0, 0, x]<br/>
    /// </summary>
    TriagonalUpper,
}