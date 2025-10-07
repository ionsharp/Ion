namespace Ion.Numeral;

/// <summary>The type of <see cref="IVector"/>.</summary>
/// <remarks>
/// <para><b><see cref="X"/></b></para>
/// <see cref="IMatrix"/> ∈ [<b>n</b>, 1] <see langword="where"/> <b>c</b> = <see cref="IMatrix.Columns"/>.
/// <br/>[1]
/// <br/>[2]
/// <br/>[3]
/// <para><b><see cref="Y"/></b></para>
/// <see cref="IMatrix"/> ∈ [1, <b>n</b>] <see langword="where"/> <b>r</b> = <see cref="IMatrix.Rows"/>.
/// <br/>[1, 2, 3]
/// </remarks>
public enum VectorType
{
    /// <summary>
    /// <see cref="IMatrix"/> ∈ [<b>n</b>, 1] <see langword="where"/> <b>c</b> = <see cref="IMatrix.Rows"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// <br/>[1]
    /// <br/>[2]
    /// <br/>[3]
    /// </remarks>
    X,
    /// <summary>
    /// <see cref="IMatrix"/> ∈ [1, <b>n</b>] <see langword="where"/> <b>n</b> = <see cref="IMatrix.Columns"/>.
    /// </summary>
    /// <remarks>
    /// <para><b>Example</b></para>
    /// [1, 2, 3]
    /// </remarks>
    Y
}