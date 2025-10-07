using Ion.Analysis;

namespace Ion.Core;

/// <summary>
/// An <see cref="object"/> that serializes.
/// </summary>
public interface ISerialize : IModel
{
    Result Serialize();
}