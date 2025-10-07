namespace Ion;

/// <summary>
/// A 1-dimensional <see cref="IArray"/>.
/// </summary>
public interface IArray1D : IArray
{
    new public object this[int x] { get; }

    public object[] ToArray();

    public int XLength => Length;
}