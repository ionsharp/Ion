namespace Ion.Core;

/// <summary>
/// An <see cref="object"/> that can be changed.
/// </summary>
public interface IChange
{
    bool IsChanged { get; set; }
}