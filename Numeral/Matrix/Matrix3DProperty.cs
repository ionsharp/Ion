namespace Ion.Numeral;

public enum Matrix3DProperty
{
    /// <summary>
    /// <see cref="IMatrix3D.Slices"/> = <see cref="IMatrix.Rows"/> = <see cref="IMatrix.Columns"/>.
    /// </summary>
    Cube,
    /// <summary>
    /// <see cref="IMatrix3D.Slices"/> != <see cref="IMatrix.Rows"/> != <see cref="IMatrix.Columns"/>.
    /// </summary>
    Quadrilateral
}