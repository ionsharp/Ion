using System.Collections.Generic;

namespace Ion.Numeral;

/// <summary>A vector-based shape that consists of straight or curved line segments connected by anchor points.</summary>
public interface IShapePoint : IShape
{
    new public const string Description = "A vector-based shape that consists of straight or curved line segments connected by anchor points.";

    public int Count => Points?.Count ?? 0;

    public List<Vector2> Points { get; }
}