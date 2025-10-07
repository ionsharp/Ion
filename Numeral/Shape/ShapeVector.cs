using System;

namespace Ion.Numeral;

/// <inheritdoc cref="IShapeVector"/>
[Description(IShapeVector.Description)]
public record class ShapeVector() : Shape(), IShapeVector
{
    public const string StringFormat = "{0}";

    /// <see cref="Region.Property"/>

    public string Data { get => this.Get<string>(); set => this.Set(value); }

    /// <see cref="Region.Constructor"/>

    public ShapeVector(string data) : this() => Data = data;

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => StringFormat.F(Data);
}