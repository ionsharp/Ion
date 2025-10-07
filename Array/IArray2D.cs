namespace Ion;

/// <summary>
/// A 2-dimensional <see cref="IArray"/>.
/// </summary>
public interface IArray2D : IArray1D
{
    public object this[int y, int x] { get; }

    new public (int Y, int X) Length => (YLength, XLength);

    new public object[][] ToArray();

    public int YLength { get; }

    int IArray.Length => XLength * YLength;
}