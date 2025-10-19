using Ion;
using Ion.Core;
using Ion.Numeral.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Numeral;

/// <inheritdoc cref="IShape"/>
[Description(IShape.Description)]
public abstract record class Shape() : Model(), IShape
{
    public static Vector2 Double(Vector2<int> a)
        => new(a.X, a.Y);

    public static Vector2<int> Int32(Vector2 a)
        => new(Convert.ToInt32(a.X), Convert.ToInt32(a.Y));

    /// <see cref="Region.Method"/>

    public virtual object Create(double height, double width) => default;

    public virtual void Normalize() { }

    public virtual void Scale(Vector2<int> scale) { }

    public virtual void Scale(Vector2 scale) { }

    public virtual void Translate(AxisQuadrant quadrant = AxisQuadrant.I) { }

    /// <see cref="Region.Method.Static"/>
    #region

    public static MArea<int> GetBounds(int[] points)
    {
        int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
        Each(points, point =>
        {
            minX = System.Math.Min(minX, point.X);
            minY = System.Math.Min(minY, point.Y);

            maxX = System.Math.Max(maxX, point.X);
            maxY = System.Math.Max(maxY, point.Y);
        });

        //bottom left = minX, minY
        //bottom right = maxX, minY
        //top left = minX, maxY <------------------ What we want!
        //top right = maxX, maxY
        return new MArea<int>(minX, maxY, maxX - minX, maxY - minY);
    }

    public static MArea<double> GetBounds(IList<Vector2> points)
    {
        double minX = double.MaxValue, minY = double.MaxValue, maxX = double.MinValue, maxY = double.MinValue;
        points.ForEach(i =>
        {
            minX = System.Math.Min(minX, i.X);
            minY = System.Math.Min(minY, i.Y);

            maxX = System.Math.Max(maxX, i.X);
            maxY = System.Math.Max(maxY, i.Y);
        });
        return new MArea<double>(minX, minY, maxX - minX, maxY - minY);
    }

    ///

    private static double GetConcaveRadius(uint num_points, int skip)
    {
        // For really small numbers of points.
        if (num_points < 5) return 0.33f;

        // Calculate angles to key points.
        double dtheta = 2 * System.Math.PI / num_points;
        double theta00 = -System.Math.PI / 2;
        double theta01 = theta00 + dtheta * skip;
        double theta10 = theta00 + dtheta;
        double theta11 = theta10 - dtheta * skip;

        // Find the key points.
        Vector2 pt00 = new(System.Math.Cos(theta00), System.Math.Sin(theta00));
        Vector2 pt01 = new(System.Math.Cos(theta01), System.Math.Sin(theta01));
        Vector2 pt10 = new(System.Math.Cos(theta10), System.Math.Sin(theta10));
        Vector2 pt11 = new(System.Math.Cos(theta11), System.Math.Sin(theta11));

        // See where the segments connecting the points intersect.
        GetIntersection(pt00, pt01, pt10, pt11,
            out _,
            out _,
            out Vector2 intersection,
            out _,
            out _);

        // Calculate the distance between the
        // point of intersection and the center.
        return System.Math.Sqrt(intersection.X * intersection.X + intersection.Y * intersection.Y);
    }

    /// <summary>
    /// Find the point of intersection between the lines p1 --> p2 and p3 --> p4.
    /// </summary>
    private static void GetIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out bool lines_intersect, out bool segments_intersect, out Vector2 intersection, out Vector2 close_p1, out Vector2 close_p2)
    {
        // Get the segments' parameters.
        double dx12 = p2.X - p1.X;
        double dy12 = p2.Y - p1.Y;
        double dx34 = p4.X - p3.X;
        double dy34 = p4.Y - p3.Y;

        // Solve for t1 and t2
        double denominator = (dy12 * dx34 - dx12 * dy34);

        double t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
        if (double.IsInfinity(t1))
        {
            // The lines are parallel (or close enough to it).
            lines_intersect = false;
            segments_intersect = false;
            intersection = new Vector2(double.NaN, double.NaN);
            close_p1 = new Vector2(double.NaN, double.NaN);
            close_p2 = new Vector2(double.NaN, double.NaN);
            return;
        }
        lines_intersect = true;

        double t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

        // Find the point of intersection.
        intersection = new Vector2(p1.X + dx12 * t1, p1.Y + dy12 * t1);

        // The segments intersect if t1 and t2 are between 0 and 1.
        segments_intersect =
            ((t1 >= 0) && (t1 <= 1) &&
                (t2 >= 0) && (t2 <= 1));

        // Find the closest points on the segments.
        if (t1 < 0)
        {
            t1 = 0;
        }
        else if (t1 > 1)
        {
            t1 = 1;
        }

        if (t2 < 0)
        {
            t2 = 0;
        }
        else if (t2 > 1)
        {
            t2 = 1;
        }

        close_p1 = new Vector2(p1.X + dx12 * t1, p1.Y + dy12 * t1);
        close_p2 = new Vector2(p3.X + dx34 * t2, p3.Y + dy34 * t2);
    }

    ///

    public static IEnumerable<Vector2> Close(IEnumerable<Vector2> input)
    {
        Vector2? j = null;
        foreach (var i in input)
        {
            j ??= i;
            yield return i;
        }
        if (j != null)
            yield return j.Value;
    }

    public static bool Contains(IList<Vector2> path, Vector2 point)
    {
        var c = path.Skip(1).Select((p, i) => (point.Y - path[i].Y) * (p.X - path[i].X) - (point.X - path[i].X) * (p.Y - path[i].Y)).ToList();
        if (c.Any(p => p == 0))
            return true;

        for (int i = 1; i < c.Count; i++)
        {
            if (c[i] * c[i - 1] < 0)
                return false;
        }
        return true;
    }

    public static int[] Copy(int[] input)
    {
        var result = new int[input.Length];
        for (var i = 0; i < input.Length; i++)
            result[i] = input[i];

        return result;
    }

    public static void Each(int[] input, Action<Vector2<int>> action)
    {
        for (var i = 0; i < input.Length; i += 2)
            action(new Vector2<int>(input[i], input[i + 1]));
    }

    public static void Each(int[] input, Func<Vector2<int>, Vector2<int>> action)
    {
        for (var i = 0; i < input.Length; i += 2)
        {
            var result = action(new Vector2<int>(input[i], input[i + 1]));
            input[i] = result.X;
            input[i + 1] = result.Y;
        }
    }

    public static int[] From(Vector2<int>[] input)
    {
        var count = input.Length * 2;
        var result = new int[count + 2];

        for (var i = 0; i < input.Length; i++)
        {
            result[i * 2]
                = input[i].X;
            result[(i * 2) + 1]
                = input[i].Y;
        }

        result[count] = result[0];
        result[count + 1] = result[1];
        return result;
    }

    public static int[] From(IList<Vector2> input)
    {
        var count = input.Count * 2;
        var result = new int[count];

        for (var i = 0; i < input.Count; i++)
        {
            result[i * 2]
                = Convert.ToInt32(input[i].X);
            result[(i * 2) + 1]
                = Convert.ToInt32(input[i].Y);
        }
        return result;
    }

    /// <summary>
    /// Converts points of existing range to points of range [0, 1].
    /// </summary>
    public static void Normalize(IList<Vector2> input)
    {
        var bounds = GetBounds(input);

        var xRange = new Range<double>(bounds.X, bounds.X + bounds.Width);
        var yRange = new Range<double>(bounds.Y, bounds.Y + bounds.Height);

        for (var i = 0; i < input.Count; i++)
            input[i] = new Vector2(xRange.ToRange(0, 1, input[i].X), yRange.ToRange(0, 1, input[i].Y));
    }

    public static void Reflect(ref double x, ref double y, int quadrant)
    {
        switch (quadrant)
        {
            case 0:
                y = -y;
                break;
            case 1:
                break;
            case 2:
                x = -x;
                break;
            case 3:
                x = -x;
                y = -y;
                break;
        }
    }

    public static IEnumerable<Vector2> Round(IList<Vector2> points)
    {
        for (var i = 0; i < points.Count; i++)
        {
            if (i == points.Count - 1)
            {
                yield return points[i];
                break;
            }

            var a = Double(Int32(points[i]));
            var b = Double(Int32(points[i + 1]));

            if (a == b)
                continue;

            var c = b - a;
            var d = a;

            var ax = c.X.Abs();
            var ay = c.Y.Abs();

            var ix = (c.X / ax);
            var iy = (c.Y / ay);

            var cx = ax;
            var cy = ay;

            yield return d;

            if (ax == ay)
            {
                while (d != b)
                {
                    if (cx > 0)
                    {
                        d = new Vector2(d.X + ix, d.Y);
                        yield return d;
                        cx--;
                    }
                    if (cy > 0)
                    {
                        d = new Vector2(d.X, d.Y + iy);
                        yield return d;
                        cy--;
                    }
                }
            }
            else if (ax > ay)
            {
                //For every n ax, do ay
                while (cy > 0)
                {
                    cx = ax / ay;
                    while (cx > 0)
                    {
                        d = new Vector2(d.X + ix, d.Y);
                        yield return d;
                        cx--;
                    }
                    d = new Vector2(d.X, d.Y + iy);
                    yield return d;
                    cy--;
                }
            }
            else if (ax < ay)
            {
                //For every n ay, do ax
                while (cx > 0)
                {
                    cy = ay / ax;
                    while (cy > 0)
                    {
                        d = new Vector2(d.X, d.Y + iy);
                        yield return d;
                        cy--;
                    }
                    d = new Vector2(d.X + ix, d.Y);
                    yield return d;
                    cx--;
                }
            }
        }
    }

    public static void Scale(int[] input, Vector2<int> scale)
    {
        var bounds = Shape.GetBounds(input);
        Each(input, point => new Vector2<int>((point.X * scale.X / bounds.Width), (point.Y * scale.Y / bounds.Height)));
    }

    public static void Scale(IList<Vector2> input, Vector2<int> scale)
    {
        for (var i = 0; i < input.Count; i++)
            input[i] = new Vector2(input[i].X * scale.X, input[i].Y * scale.Y);
    }

    public static void Scale(IList<Vector2> input, Vector2 scale)
    {
        for (var i = 0; i < input.Count; i++)
            input[i] = new Vector2(input[i].X * scale.X, input[i].Y * scale.Y);
    }

    ///

    /// <summary>
    /// Moves all points in the direction of the origin until each lives in quadrant I.
    /// </summary>
    public static void Translate(int[] points, AxisQuadrant quadrant = AxisQuadrant.I)
    {
        var bounds = GetBounds(points);

        var x = Convert.ToInt32(Convert.ToDouble(bounds.Width) / 2.0);
        var y = Convert.ToInt32(Convert.ToDouble(bounds.Height) / 2.0);

        Translate(points, new Vector2<int>(x, y));
    }

    public static void Translate(int[] input, Vector2<int> newCenter)
    {
        var bounds = GetBounds(input);
        var oldCenter = bounds.Center();

        var x = (newCenter.X - oldCenter.X);
        var y = (newCenter.Y - oldCenter.Y);

        Each(input, point => new Vector2<int>(Convert.ToInt32(point.X + x), Convert.ToInt32(point.Y + y)));
    }

    public static List<Vector2> Translate(IList<Vector2> input, double xCenter, double yCenter)
    {
        var result = new List<Vector2>();

        var bounds = GetBounds(input);
        var oldCenter = bounds.Center();

        var x = xCenter - oldCenter.X;
        var y = yCenter - oldCenter.Y;

        for (var i = 0; i < input.Count; i++)
            result.Add(new Vector2(input[i].X + x, input[i].Y + y));

        return result;
    }

    ///

    public static IEnumerable<Vector2> GetEllipse(double centerX, double centerY, double width, double height, int numPoints = 360)
    {
        var points = new List<Vector2>();
        double radiusX = width / 2.0;
        double radiusY = height / 2.0;
        double angleStep = (2 * Math.PI) / numPoints;

        for (int i = 0; i <= numPoints; i++)
        {
            double angle = i * angleStep;
            double x = centerX + radiusX * Math.Cos(angle);
            double y = centerY + radiusY * Math.Sin(angle);
            points.Add(new Vector2(x, y));
        }

        return points;
    }

    public static IEnumerable<Vector2> GetPolygon(MArea<int> region, double angle, uint sides, int quadrant)
    {
        var centerX = region.X + (region.Width / 2.0);
        var centerY = region.Y + (region.Height / 2.0);

        var center = new Vector2(centerX, centerY);
        var size = new Size<double>(Math.Clamp(region.Width, 0, int.MaxValue), Math.Clamp(region.Height, 0, int.MaxValue));

        var oldPoints
            = GetPolygon(new Angle(angle).Convert(AngleType.Radian), center, size, sides, quadrant);

        for (var i = 0; i < oldPoints.Length; i++)
            yield return new Vector2(oldPoints[i].X, oldPoints[i].Y);
    }

    public static Vector2[] GetPolygon(double startAngle, Vector2 center, Size<double> size, uint sides, int quadrant)
    {
        Math.Clamp(sides, 3, 32);

        var a = 2.0 * Math.PI / Convert.ToDouble(sides);
        var b = startAngle;
        double x = 0, y = 0;

        var points = new Vector2[sides + 1];
        for (int i = 0; i < sides; i++)
        {
            x = size.Width * Math.Cos(b);
            y = size.Height * Math.Sin(b);

            Reflect(ref x, ref y, quadrant);

            x += center.X;
            y += center.Y;

            points[i] = new Vector2(x, y);
            b += a;
        }

        points[sides] = points[0];
        return points;
    }

    public static IEnumerable<ShapePoint> GetPolygons(uint sidesMinimum = 3, uint sidesMaximum = 16, double angle = 180, double height = 1, double width = 1)
    {
        var center = new Vector2(width, height);
        for (uint i = sidesMinimum; i <= sidesMaximum; i++)
        {
            var shape = new ShapePoint(GetPolygon(new Angle(angle).Convert(AngleType.Radian), center, new Size<double>(width, height), i, 0));
            shape.Translate(); shape.Normalize();
            yield return shape;
        }
    }

    ///

    public static IEnumerable<Vector2> GetStar(MArea<int> region, double angle, uint sides, int indent, int quadrant)
    {
        var centerX = region.X + (region.Width / 2.0);
        var centerY = region.Y + (region.Height / 2.0);

        var center = new Vector2(centerX, centerY);
        var size = new Size<double>(Math.Clamp(region.Width, 0, int.MaxValue), Math.Clamp(region.Height, 0, int.MaxValue));

        var oldPoints
            = GetStar(new Angle(angle).Convert(AngleType.Radian), sides, indent, new MArea<double>(center.X, center.Y, size.Width, size.Height), quadrant);

        for (var i = 0; i < oldPoints.Length; i++)
            yield return new Vector2(oldPoints[i].X, oldPoints[i].Y);
    }

    public static Vector2<int>[] GetStar(double startAngle, uint sides, int skip, MArea<double> rect, int quadrant)
    {
        Math.Clamp(sides, 2, 32);

        double theta, dtheta;
        Vector2<int>[] result;

        double rx = Convert.ToSingle(rect.Width) / 2f;
        double ry = Convert.ToSingle(rect.Height) / 2f;
        double cx = Convert.ToSingle(rect.X) + rx;
        double cy = Convert.ToSingle(rect.Y) + ry;

        // If this is a polygon, don't bother with concave points.
        if (skip == 1)
        {
            result = new Vector2<int>[sides];
            theta = startAngle;
            dtheta = 2 * System.Math.PI / sides;
            for (int i = 0; i < sides; i++)
            {
                var x = rx * System.Math.Cos(theta);
                var y = ry * System.Math.Sin(theta);

                Reflect(ref x, ref y, quadrant);

                result[i] = new Vector2<int>(Convert.ToInt32(x) + Convert.ToInt32(cx), Convert.ToInt32(y) + Convert.ToInt32(cy));
                theta += dtheta;
            }
            return result;
        }

        // Find the radius for the concave vertices.
        double concave_radius = GetConcaveRadius(sides, skip);

        // Make the points.
        result = new Vector2<int>[2 * sides + 1];
        theta = startAngle;
        dtheta = System.Math.PI / sides;
        for (int i = 0; i < sides; i++)
        {
            var x = Convert.ToInt32(cx + rx * Math.Cos(theta));
            var y = Convert.ToInt32(cy + ry * Math.Sin(theta));
            result[2 * i] = new Vector2<int>(x, y);
            theta += dtheta;

            x = Convert.ToInt32(cx + rx * Math.Cos(theta) * concave_radius);
            y = Convert.ToInt32(cy + ry * Math.Sin(theta) * concave_radius);
            result[2 * i + 1] = new Vector2<int>(x, y);
            theta += dtheta;
        }

        result[2 * sides] = result[0];
        return result;
    }

    public static IEnumerable<ShapePoint> GetStars(uint sidesMinimum = 3, uint sidesMaximum = 16, double height = 20, double width = 20)
    {
        var origin = new Vector2(width, height);
        for (uint i = sidesMinimum; i <= sidesMaximum; i++)
        {
            var shape = new ShapePoint(Shape.GetStar(new Angle(270).Convert(AngleType.Radian), i, 2, new MArea<double>(origin.X, origin.Y, width, height), 0));
            shape.Translate();
            shape.Normalize();
            yield return shape;
        }
    }

    ///

    public static IEnumerable<Vector2> GetRectangle(MArea<int> region, bool close = false)
    {
        yield return new Vector2(region.X, region.Y);
        yield return new Vector2(region.X, region.Y + region.Height);
        yield return new Vector2(region.X + region.Width, region.Y + region.Height);
        yield return new Vector2(region.X + region.Width, region.Y);
        if (close) yield return new Vector2(region.X, region.Y);
    }

    #endregion

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => default;
}