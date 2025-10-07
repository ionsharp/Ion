namespace Ion.Core;

/// <summary>
/// A scheme that indicates what value to prioritize when binding two-way between properties.
/// </summary>
public enum PropertyBindingScheme
{
    /// <summary>
    /// Value of either property if not <see langword="null"/>.
    /// </summary>
    KeepNotNull,
    /// <summary>
    /// Value of source property is preserved. Value of target property is ignored.
    /// </summary>
    KeepSource,
    /// <summary>
    /// Value of target property is preserved. Value of source property is ignored.
    /// </summary>
    KeepTarget,
}