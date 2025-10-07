namespace Ion;

/// <summary>
/// A 3-dimensional <see cref="IArray"/>.
/// </summary>
public interface IArray3D : IArray2D
{
    public object this[int z, int y, int x] { get; }

    new public (int Z, int Y, int X) Length { get; }

    new public object[][][] ToArray();

    public int ZLength { get; }
}