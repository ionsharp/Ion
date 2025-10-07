namespace Ion.Numeral;

public readonly record struct Matrix2DFromArray1D(int Repeat = 1, Axis2 Axis = default)
{
    public readonly Axis2 Axis { get; } = Axis;

    public readonly int Repeat { get; } = Repeat; 
}