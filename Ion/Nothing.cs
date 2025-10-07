namespace Ion;

/// <summary>
/// Something undefined (neither <see langword="null"/> nor <see langword="not null"/>).
/// </summary>
public static class No
{
    /// <summary>
    /// Something that is <see cref="Nothing"/>.
    /// </summary>
    public static readonly Nothing Thing = new();
}

/// <inheritdoc cref="No"/>
public sealed class Nothing { internal Nothing() { } }