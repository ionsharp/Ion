namespace Ion.Numeral;

public record class CurveDelegate : Curve
{
    public virtual CurveMethod Method { get; set; }

    public sealed override double Do(double a, double b, double value) => Method(a, b, value);
}