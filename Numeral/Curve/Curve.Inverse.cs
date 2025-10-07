namespace Ion.Numeral;

/// <summary>A curve with an inverse.</summary>
/// <inheritdoc/>
[Description(Description)]
public abstract record class CurveInverse() : Curve()
{
    public abstract double DoInverse(double start, double end, double value);
}