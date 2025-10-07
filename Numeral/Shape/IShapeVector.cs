namespace Ion.Numeral;

/// <summary>A vector-based shape that consists of straight or curved line segments connected by anchor points.</summary>
public interface IShapeVector : IShape
{
    new public const string Description = "A vector-based shape that consists of straight or curved line segments connected by anchor points.";

    string Data { get; }
}