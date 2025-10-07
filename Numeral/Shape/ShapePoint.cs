using Ion;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ion.Numeral;

/// <inheritdoc/>
[Description(IShape.Description)]
public record class ShapePoint() : Shape(), IEnumerable<Vector2>, IShapePoint
{
    /// <see cref="Region.Field"/>

    public const string StringFormat = "({0}, {1})";

    public const string StringFormatDelimiter = ", ";

    /// <see cref="Region.Property"/>

    public List<Vector2> Points { get; set; } = [];

    /// <see cref="Region.Constructor"/>

    public ShapePoint(IEnumerable<Vector2> i) : this()
        => i?.ForEach(Points.Add);

    public ShapePoint(IEnumerable<Vector2<int>> i) : this()
        => i?.ForEach(j => Points.Add(new(j.X, j.Y)));

    public ShapePoint(params Vector2[] i) : this()
        => i?.ForEach(Points.Add);

    public ShapePoint(params Vector2<int>[] i) : this()
        => i?.ForEach(j => Points.Add(new(j.X, j.Y)));

    /// <see cref="Region.Method"/>

    public object Construct(bool closed)
    {
        /*
        var shape = this;

        var myPathFigure = new PathFigure();
        if (shape.Points?.Count > 0)
        {
            myPathFigure.StartPoint = shape.Points[0];
            var myLineSegment = new PolyLineSegment();

            for (var i = 0; i < shape.Points.Count; i++)
                myLineSegment.Points.Add(shape.Points[i]);

            myLineSegment.Points.Add(shape.Points[0]);

            var myPathSegmentCollection = new PathSegmentCollection() { myLineSegment };
            myPathFigure.Segments = myPathSegmentCollection;
        }

        return new PathGeometry() { Figures = [myPathFigure] };
        */
        return default;
    }

    public void Rotate(double degree, Vector2 center = default)
    {
        for (var i = 0; i < Points.Count; i++)
        {
            var x = (Points[i].X - center.X) * degree.DCos() - (Points[i].Y - center.Y) * degree.DSin() + center.X;
            var y = (Points[i].X - center.X) * degree.DSin() - (Points[i].Y - center.Y) * degree.DCos() + center.Y;
            Points[i] = new(x, y);
        }
        this.Reset(() => Points);
    }

    /// <see cref="Shape"/>

    public override object Create(double height, double width)
    {
        var result = new ShapePoint(Points);
        result.Scale(new Vector2(width, height));
        return result.Construct(true);
    }

    public override void Normalize()
    {
        Normalize(Points);
        this.Reset(() => Points);
    }

    public override void Scale(Vector2<int> scale)
    {
        Scale(Points, scale);
        this.Reset(() => Points);
    }

    public override void Scale(Vector2 scale)
    {
        Scale(Points, scale);
        this.Reset(() => Points);
    }

    public override void Translate(AxisQuadrant quadrant = AxisQuadrant.I)
    {
        var bounds = GetBounds(Points);

        var x = bounds.Width / 2;
        var y = bounds.Height / 2;

        Points = Translate(Points, x, y);
    }

    /// <see cref="IEnumerable"/>

    IEnumerator IEnumerable.GetEnumerator() => Points.GetEnumerator();

    IEnumerator<Vector2> IEnumerable<Vector2>.GetEnumerator() => Points.GetEnumerator();

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider)
        => Points.ToString(StringFormatDelimiter, i => StringFormat.F(i.X.ToString(format, provider), i.Y.ToString(format, provider)));
}