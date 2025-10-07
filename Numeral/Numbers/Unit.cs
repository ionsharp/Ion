using Ion.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>A value used in various forms of measurement.</summary>
/// <remarks>Always +. See <see cref="UnitType"/>.</remarks>
[Description(Description)]
public readonly record struct Unit(double Value, UnitType Type, double Resolution)
    : IConvertible, IFloatingPoint<Unit>, IFormattable, IMinMaxValue<Unit>, INumber<Unit>, ISignedNumber<Unit>
{
    /// <see cref="Region.Field"/>

    public const double DefaultResolution = 72.0;

    public const UnitType DefaultType = UnitType.Pixel;

    public const string Description
        = "A value used in various forms of measurement.";

    public const string StringFormat = "{0}{1}"; //123px

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value of <see cref="USingle"/>.</summary>
    public static Unit MaxValue { get; } = double.MaxValue;

    /// <summary>Get the smallest possible value of <see cref="USingle"/>.</summary>
    public static Unit MinValue { get; } = double.MinValue;

    public static Unit E => double.E;

    public static Unit Pi => double.Pi;

    public static Unit Tau => double.Tau;

    static Unit IAdditiveIdentity<Unit, Unit>.AdditiveIdentity => AdditiveIdentity;
    public static Unit AdditiveIdentity => Zero;

    static Unit IMultiplicativeIdentity<Unit, Unit>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static Unit MultiplicativeIdentity => One;

    public static Unit NegativeOne => -One;

    static Unit INumberBase<Unit>.One => One;
    public static Unit One => new(1);

    static int INumberBase<Unit>.Radix => Radix;
    public static int Radix => 10;

    static Unit INumberBase<Unit>.Zero => Zero;
    public static Unit Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly double Resolution { get; } = Resolution;

    private readonly double Value { get; } = Math.Clamp(Value, double.MinValue, double.MaxValue);

    private readonly UnitType Type { get; } = Type;

    /// <see cref="Region.Constructor"/>

    public Unit(double i) : this(i, DefaultType, DefaultResolution) { }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator double(Unit i) => i.Value;

    public static implicit operator Unit(double i) => new(i);

    ///

    public static bool operator <(Unit a, Unit b) => a.Value < b.Value;

    public static bool operator >(Unit a, Unit b) => a.Value > b.Value;

    public static bool operator <=(Unit a, Unit b) => a.Value <= b.Value;

    public static bool operator >=(Unit a, Unit b) => a.Value >= b.Value;

    ///

    public static Unit operator +(Unit i) => +i.Value;

    public static Unit operator +(Unit a, Unit b) => a.Value + b.Value;

    public static Unit operator ++(Unit i) => i.Value + 1;

    public static Unit operator -(Unit i) => -i.Value;

    public static Unit operator -(Unit a, Unit b) => a.Value - b.Value;

    public static Unit operator --(Unit i) => i.Value - 1;

    public static Unit operator /(Unit a, Unit b) => a.Value / b.Value;

    public static Unit operator *(Unit a, Unit b) => a.Value * b.Value;

    public static Unit operator %(Unit a, Unit b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    /// <summary>Convert to the given <see cref="UnitType"/>.</summary>
    /// <param name="b">The unit type to convert to.</param>
    /// <returns>A unit of the new type.</returns>
    public Unit Convert(UnitType b)
    {
        var pixels = 0d;
        switch (Type)
        {
            case UnitType.Pixel:
                pixels = Math.Round(Value, 0);
                break;
            case UnitType.Inch:
                pixels = Math.Round(Value * Resolution, 0);
                break;
            case UnitType.Centimeter:
                pixels = Math.Round(Value * Resolution / 2.54, 0);
                break;
            case UnitType.Millimeter:
                pixels = Math.Round(Value * Resolution / 25.4, 0);
                break;
            case UnitType.Point:
                pixels = Math.Round(Value * Resolution / 72, 0);
                break;
            case UnitType.Pica:
                pixels = Math.Round(Value * Resolution / 6, 0);
                break;
            case UnitType.Twip:
                pixels = Math.Round(Value * Resolution / 1140, 0);
                break;
            case UnitType.Character:
                pixels = Math.Round(Value * Resolution / 12, 0);
                break;
            case UnitType.En:
                pixels = Math.Round(Value * Resolution / 144.54, 0);
                break;
        }

        var inches = pixels / Resolution;
        var result = pixels;

        switch (b)
        {
            case UnitType.Inch:
                result = inches;
                break;
            case UnitType.Centimeter:
                result = inches * 2.54;
                break;
            case UnitType.Millimeter:
                result = inches * 25.4;
                break;
            case UnitType.Point:
                result = inches * 72.0;
                break;
            case UnitType.Pica:
                result = inches * 6.0;
                break;
            case UnitType.Twip:
                result = inches * 1140.0;
                break;
            case UnitType.Character:
                result = inches * 12.0;
                break;
            case UnitType.En:
                result = inches * 144.54;
                break;
        }

        return result;
    }

    public Unit Round(int digits = 0) => new(Math.Round(Value, digits), Type, Resolution);

    public static Unit Parse(string i) => double.Parse(i);

    public static bool TryParse(string i, out Unit j)
    {
        var k = double.TryParse(i, out double l);
        j = l; return k;
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Double;

    bool IConvertible.ToBoolean(IFormatProvider provider) => double.IsPositive(Value);

    byte IConvertible.ToByte(IFormatProvider provider) => System.Convert.ToByte(Value, provider);

    char IConvertible.ToChar(IFormatProvider provider) => System.Convert.ToChar(Value, provider);

    DateTime IConvertible.ToDateTime(IFormatProvider provider) => System.Convert.ToDateTime(Value, provider);

    decimal IConvertible.ToDecimal(IFormatProvider provider) => System.Convert.ToDecimal(Value, provider);

    double IConvertible.ToDouble(IFormatProvider provider) => System.Convert.ToDouble(Value, provider);

    short IConvertible.ToInt16(IFormatProvider provider) => System.Convert.ToInt16(Value, provider);

    int IConvertible.ToInt32(IFormatProvider provider) => System.Convert.ToInt32(Value, provider);

    long IConvertible.ToInt64(IFormatProvider provider) => System.Convert.ToInt64(Value, provider);

    sbyte IConvertible.ToSByte(IFormatProvider provider) => System.Convert.ToSByte(Value, provider);

    float IConvertible.ToSingle(IFormatProvider provider) => System.Convert.ToSingle(Value, provider);

    string IConvertible.ToString(IFormatProvider provider) => System.Convert.ToString(Value, provider);

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        => System.Convert.ChangeType(Value, conversionType);

    ushort IConvertible.ToUInt16(IFormatProvider provider) => System.Convert.ToUInt16(Value, provider);

    uint IConvertible.ToUInt32(IFormatProvider provider) => System.Convert.ToUInt32(Value, provider);

    ulong IConvertible.ToUInt64(IFormatProvider provider) => System.Convert.ToUInt64(Value, provider);

    #endregion

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
        => StringFormat.F(Value.ToString(format, provider), Type.GetAttribute<SymbolAttribute>().Symbol);

    /// <see cref="INumber{TSelf}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object i) => Value.CompareTo(i);

    public int CompareTo(Unit i) => Value.CompareTo(i.Value);

    /// <see cref="INumberBase{T}"/>

    public static Unit Abs(Unit i) => new(double.Abs(i.Value));

    public static bool IsCanonical(Unit i) => i.Type == UnitType.Pixel;
    public static bool IsComplexNumber(Unit i) => false;
    public static bool IsEvenInteger(Unit i) => double.IsEvenInteger(i.Value);
    public static bool IsFinite(Unit i) => double.IsFinite(i.Value);
    public static bool IsImaginaryNumber(Unit i) => false;
    public static bool IsInfinity(Unit i) => double.IsInfinity(i.Value);
    public static bool IsInteger(Unit i) => double.IsInteger(i.Value);
    public static bool IsNaN(Unit i) => double.IsNaN(i.Value);
    public static bool IsNegative(Unit i) => double.IsNegative(i.Value);
    public static bool IsNegativeInfinity(Unit i) => double.IsNegativeInfinity(i.Value);
    public static bool IsNormal(Unit i) => double.IsNormal(i.Value);
    public static bool IsOddInteger(Unit i) => double.IsOddInteger(i.Value);
    public static bool IsPositive(Unit i) => double.IsPositive(i.Value);
    public static bool IsPositiveInfinity(Unit i) => double.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(Unit i) => double.IsRealNumber(i.Value);
    public static bool IsSubnormal(Unit i) => double.IsSubnormal(i.Value);
    public static bool IsZero(Unit i) => i.Value == 0;

    public static Unit MaxMagnitude(Unit x, Unit y)
        => new(Math.MaxMagnitude(x.Value, y.Value));
    public static Unit MaxMagnitudeNumber(Unit x, Unit y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static Unit MinMagnitude(Unit x, Unit y)
        => new(Math.MinMagnitude(x.Value, y.Value));
    public static Unit MinMagnitudeNumber(Unit x, Unit y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<Unit>.TryConvertFromChecked<TOther>(TOther i, out Unit result)
    { result = default; return default; }
    static bool INumberBase<Unit>.TryConvertFromSaturating<TOther>(TOther i, out Unit result)
    { result = default; return default; }
    static bool INumberBase<Unit>.TryConvertFromTruncating<TOther>(TOther i, out Unit result)
    { result = default; return default; }
    static bool INumberBase<Unit>.TryConvertToChecked<TOther>(Unit i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Unit>.TryConvertToSaturating<TOther>(Unit i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Unit>.TryConvertToTruncating<TOther>(Unit i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static Unit Parse(string i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Unit Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out Unit result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Unit result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static Unit Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Unit Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out Unit result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Unit result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    #endregion

    /// <see cref="IFloatingPoint{TSelf}"/>
    #region

    private IFloatingPoint<double> Float => Value;

    public static Unit Round(Unit x, int digits, MidpointRounding mode) => double.Round(x, digits, mode);

    public int GetExponentByteCount() => Float.GetExponentByteCount();
    public int GetExponentShortestBitLength() => Float.GetExponentShortestBitLength();
    public int GetSignificandBitLength() => Float.GetSignificandBitLength();
    public int GetSignificandByteCount() => Float.GetSignificandByteCount();

    public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten) => Float.TryWriteExponentBigEndian(destination, out bytesWritten);
    public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten) => Float.TryWriteExponentLittleEndian(destination, out bytesWritten);
    public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten) => Float.TryWriteSignificandBigEndian(destination, out bytesWritten);
    public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten) => Float.TryWriteSignificandLittleEndian(destination, out bytesWritten);

    #endregion
}