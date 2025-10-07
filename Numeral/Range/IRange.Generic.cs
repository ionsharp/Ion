namespace Ion.Numeral;

/// <inheritdoc/>
public interface IRange<T> : IRange
{
    new T Maximum { get; }

    new T Minimum { get; }
}