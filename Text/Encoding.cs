namespace Ion.Text;

/// <inheritdoc cref="System.Text.Encoding"/>
public enum Encoding
{
    /// <inheritdoc cref="System.Text.Encoding.ASCII"/>
    ASCII,
    /// <inheritdoc cref="System.Text.Encoding.BigEndianUnicode"/>
    BigEndianUnicode,
    /// <inheritdoc cref="System.Text.Encoding.Default"/>
    Default,
    /// <inheritdoc cref="System.Text.Encoding.Unicode"/>
    Unicode,
    /// <inheritdoc cref="System.Text.Encoding.UTF7"/>
    UTF7,
    /// <inheritdoc cref="System.Text.Encoding.UTF8"/>
    UTF8,
    /// <inheritdoc cref="System.Text.Encoding.UTF32"/>
    UTF32,
}

[Extend<Encoding>]
public static class XEncoding
{
    /// <summary>
    /// Get new instance of <see cref="System.Text.Encoding"/> from <see cref="Encoding"/>.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static System.Text.Encoding New(this Encoding i) => i switch
    {
        Encoding.ASCII => System.Text.Encoding.ASCII,
        Encoding.Unicode => System.Text.Encoding.Unicode,
        Encoding.UTF32 => System.Text.Encoding.UTF32,
        _ => System.Text.Encoding.UTF8,
    };
}