namespace Ion.Numeral;

/// <summary>
/// Extends <see cref="IMatrix3D"/>.
/// </summary>
[Extend<IMatrix3D>]
public static class XMatrix3D
{
    /// <summary>
    /// Get if <see cref="Matrix3DProperty.Cube"/> (<see cref="IMatrix.Columns"/> = <see cref="IMatrix.Rows"/> = <see cref="IMatrix3D.Slices"/>).
    /// </summary>
    public static bool IsCube(this IMatrix3D i) => i.XLength == i.YLength && i.YLength == i.ZLength;
}