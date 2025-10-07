namespace Ion.Storage;

public enum FileSizeFormat
{
    /// <summary>Number of bytes with no base quantity.</summary><returns>Total number of bytes.</returns>
    Bytes = 0,
    /// <summary>Number of bytes with a base quantity of 1024</summary><remarks>B, KiB, MiB, GiB, TiB, PiB, EiB, ZiB, YiB</remarks>
    IECBinary,
    /// <summary>Number of bytes with a base quantity of 1000</summary><remarks>B, kB, MB, GB, TB, PB, EB, ZB, YB</remarks>
    BinaryUsingSI,
    /// <summary>Number of bytes with a base quantity of 1000</summary><remarks>B, kB, MB, GB, TB, PB, EB, ZB, YB</remarks>
    DecimalUsingSI
}