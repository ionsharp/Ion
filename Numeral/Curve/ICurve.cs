namespace Ion.Numeral;

/// <summary>
/// The rate of change relative to a parameter over time.
/// </summary>
public interface ICurve
{
    /// <summary>
    /// Do with given <b>value</b> where <b>x</b> = 0 and <b>y</b> = 1.
    /// </summary>
    public double Do(double value) => Do(0, 1, value);

    /// <summary>
    /// Do with given <b>x</b>, <b>y</b>, and <b>value</b>.
    /// </summary>
    public double Do(double x, double y, double value);
}