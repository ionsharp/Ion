namespace Ion.Numeral;

/// <summary>A linear curve with an inverse.</summary>
/// <inheritdoc/>
[Description(Description)]
public record class LinearCurve() : CurveInverse()
{
    public override double Do(double start, double end, double value)
        => start + value * (end - start);

    public override double DoInverse(double start, double end, double value)
        => end - start;
}