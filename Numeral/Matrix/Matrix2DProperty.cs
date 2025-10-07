namespace Ion.Numeral;

public enum Matrix2DProperty
{
    Complex,
    /// <summary>
    /// <see cref="Diagonal"/> = <see cref="Triagonal"/>
    /// </summary>
    /// <inheritdoc cref="Triagonal"/>
    Diagonal,
    /// <summary>
    /// An <see cref="IMatrix"/> where <see cref="Matrix2DProperty.Square"/> and <see cref="Matrix2DProperty.Triagonal"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [1, 0, 0]<br/>
    /// [0, 1, 0]<br/>
    /// [0, 0, 1]
    /// </remarks>
    Identity, 
    Invertible, 
    Orthogonal,
    NonSingular,
    PositiveDefinite,
    /// <summary>
    /// An <see cref="IMatrix"/> where <see cref="IMatrix.Columns"/> != <see cref="IMatrix.Rows"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, x, x]<br/>
    /// [x, x, x]<br/>
    /// <para><b>Example</b></para>
    /// [x, x]<br/>
    /// [x, x]<br/>
    /// [x, x]
    /// </remarks>
    Rectangular,
    Singular,
    SkewSymmetric,
    /// <summary>
    /// An <see cref="IMatrix"/> where <see cref="IMatrix.Columns"/> = <see cref="IMatrix.Rows"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, x, x]<br/>
    /// [x, x, x]<br/>
    /// [x, x, x]
    /// </remarks>
    Square,
    Symmetric,
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, 0, 0]<br/>
    /// [0, x, 0]<br/>
    /// [0, 0, x]
    /// <para><b>Example</b></para>
    /// [0, 0, x]<br/>
    /// [0, x, 0]<br/>
    /// [x, 0, 0]
    /// </remarks>
    Triagonal,
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, x, x]<br/>
    /// [0, x, x]<br/>
    /// [0, 0, x]
    /// <para><b>Example</b></para>
    /// [x, x, x]<br/>
    /// [x, x, 0]<br/>
    /// [x, 0, 0]
    /// </remarks>
    TriagonalLower,
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, 0, 0]<br/>
    /// [x, x, 0]<br/>
    /// [x, x, x]<br/>
    /// <para><b>Example</b></para>
    /// [0, 0, x]<br/>
    /// [0, x, x]<br/>
    /// [x, x, x]
    /// </remarks>
    TriagonalUpper,
    /// <summary>
    /// An <see cref="IMatrix"/> where <see cref="IMatrix.Columns"/> > 1 and <see cref="IMatrix.Rows"/> = 1, or <see cref="IMatrix.Columns"/> = 1 and <see cref="IMatrix.Rows"/> > 1.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [x, x, x]
    /// <para><b>Example</b></para>
    /// [x]<br/>
    /// [x]<br/>
    /// [x]
    /// </remarks>
    Vector,
}