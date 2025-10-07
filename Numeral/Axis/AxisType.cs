namespace Ion.Numeral;

/// <summary>Specifies a dimension with <i>n</i> [1, 3] axes.</summary>
[Description("A dimension with one to three axes.")]
public enum AxisType
{
    /// <summary>Specifies a dimension with 1 axis (X).</summary>
    [Description("A dimension with 1 axes.")] //[Name("1D")]
    One,
    /// <summary>Specifies a dimension with 2 axes (X|Y).</summary>
    [Description("A dimension with 2 axes.")] //[Name("2D")]
    Two,
    /// <summary>Specifies a dimension with 3 axes (X|Y|Z).</summary>
    [Description("A dimension with 3 axes.")] //[Name("3D")]
    Three
}