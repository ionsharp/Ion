using System.Collections;

namespace Ion.Core;

/// <summary>
/// A scheme that indicates what items in either <see cref="ICollection"/> stay and go.
/// </summary>
public enum CollectionBindingScheme
{
    /// <summary>
    /// Items from both <see cref="ICollection"/> are preserved.
    /// </summary>
    Combine,
    /// <summary>
    /// <see cref="ICollection"/> with most items are preserved. <see cref="ICollection"/> with least is ignored.
    /// </summary>
    KeepGreater,
    /// <summary>
    /// Items from both <see cref="ICollection"/> are ignored.
    /// </summary>
    KeepNeither,
    /// <summary>
    /// Items from source <see cref="ICollection"/> are preserved. Items from target <see cref="ICollection"/> are ignored.
    /// </summary>
    KeepSource,
    /// <summary>
    /// Items from target <see cref="ICollection"/> are preserved. Items from source <see cref="ICollection"/> are ignored.
    /// </summary>
    KeepTarget,
}