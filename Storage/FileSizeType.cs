namespace Ion.Storage;

public enum FileSizeType
{
    Bytes,
    /// <summary>
    /// 1,000 <see cref="Bytes"/>
    /// </summary>
    KiloBytes,  //
    /// <summary>
    /// 1,000 <see cref="KiloBytes"/>
    /// </summary>
    MegaBytes,
    /// <summary>
    /// 1,000 <see cref="MegaBytes"/>
    /// </summary>
    GigaBytes,
    /// <summary>
    /// 1,000 <see cref="GigaBytes"/>
    /// </summary>
    TeraBytes,
    /// <summary>
    /// 1,000 <see cref="TeraBytes"/>
    /// </summary>
    PetaBytes,
    /// <summary>
    /// 1,000 <see cref="PetaBytes"/>
    /// </summary>
    ExaByte,
}