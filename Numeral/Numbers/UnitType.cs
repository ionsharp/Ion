namespace Ion.Numeral;

/// <summary>A unit of measurement.</summary>
public enum UnitType
{
    /// <summary>A device pixel as the unit of measure.</summary>
    [Description("px")]
    Pixel,
    /// <summary>An inch as the unit of measure.</summary>
    [Description("in")]
    Inch,
    /// <summary>A centimeter (1/2.54 inch) as the unit of measure.</summary>
    [Description("cm")]
    Centimeter,
    /// <summary>A millimeter (1/25.4 inch) as the unit of measure.</summary>
    [Description("mm")]
    Millimeter,
    /// <summary>A printer's point (1/72 inch) as the unit of measure.</summary>
    [Description("pt")]
    Point,
    /// <summary>A pica (1/6 inch) as the unit of measure.</summary>
    [Description("pc")]
    Pica,
    /// <summary>A twip (1/1140 inch) as the unit of measure.</summary>
    [Description("tw")]
    Twip,
    /// <summary>A character (1/12 inch) as the unit of measure.</summary>
    [Description("ch")]
    Character,
    /// <summary>An en (1/144.54 inch) as the unit of measure.</summary>
    [Description("en")]
    En
}