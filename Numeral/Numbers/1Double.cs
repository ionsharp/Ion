using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>A double-precision, floating-point number in range of (0, 1).</summary>
/// <remarks>A number out of range is truncated (see <see cref="Single1"/> for the <see cref="float"/> equivalent).</remarks>
[Description(Description)]
public readonly record struct Double1(double Value)
    : IConvertible, IFloatingPoint<Double1>, IFormattable, IMinMaxValue<Double1>, INumber<Double1>
{
    /// <see cref="Region.Field"/>

    public const string Description = "A double-precision, floating-point number in range of (0, 1).";

    public static readonly Double1 Maximum = 1;

    public static readonly Double1 Minimum = 0;

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value.</summary>
    public static Double1 MaxValue { get; } = new(1);

    /// <summary>Get the smallest possible value.</summary>
    public static Double1 MinValue { get; } = new(0);

    public static Double1 E => double.E;

    public static Double1 Pi => double.Pi;

    public static Double1 Tau => double.Tau;

    static Double1 IAdditiveIdentity<Double1, Double1>.AdditiveIdentity => AdditiveIdentity;
    public static Double1 AdditiveIdentity => Zero;

    static Double1 IMultiplicativeIdentity<Double1, Double1>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static Double1 MultiplicativeIdentity => One;

    public static Double1 NegativeOne => One;

    static Double1 INumberBase<Double1>.One => One;
    public static Double1 One => new(1);

    static int INumberBase<Double1>.Radix => Radix;
    public static int Radix => 10;

    static Double1 INumberBase<Double1>.Zero => Zero;
    public static Double1 Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly double Value { get; } = Math.Clamp(Value, 0, 1);

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Double1(double input) => new(input);

    public static implicit operator double(Double1 input) => input.Value;

    ///

    public static bool operator <(Double1 a, Double1 b) => a.Value < b.Value;

    public static bool operator >(Double1 a, Double1 b) => a.Value > b.Value;

    public static bool operator <=(Double1 a, Double1 b) => a.Value <= b.Value;

    public static bool operator >=(Double1 a, Double1 b) => a.Value >= b.Value;

    ///

    public static Double1 operator +(Double1 i) => +i.Value;

    public static Double1 operator +(Double1 a, Double1 b) => a.Value + b.Value;

    public static Double1 operator ++(Double1 i) => i.Value + 1;

    public static Double1 operator -(Double1 i) => -i.Value;

    public static Double1 operator -(Double1 a, Double1 b) => a.Value - b.Value;

    public static Double1 operator --(Double1 i) => i.Value - 1;

    public static Double1 operator /(Double1 a, Double1 b) => a.Value / b.Value;

    public static Double1 operator *(Double1 a, Double1 b) => a.Value * b.Value;

    public static Double1 operator %(Double1 a, Double1 b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    public static Double1 Parse(string i) => (Double1)double.Parse(i);

    public static bool TryParse(string i, out Double1 j)
    {
        var k = double.TryParse(i, out double l);
        j = (Double1)l;
        return k;
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Double;

    bool IConvertible.ToBoolean(IFormatProvider provider) => Value == 1;

    byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte(Value, provider);

    char IConvertible.ToChar(IFormatProvider provider) => Convert.ToChar(Value, provider);

    DateTime IConvertible.ToDateTime(IFormatProvider provider) => Convert.ToDateTime(Value, provider);

    decimal IConvertible.ToDecimal(IFormatProvider provider) => Convert.ToDecimal(Value, provider);

    double IConvertible.ToDouble(IFormatProvider provider) => Convert.ToDouble(Value, provider);

    short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16(Value, provider);

    int IConvertible.ToInt32(IFormatProvider provider) => Convert.ToInt32(Value, provider);

    long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt64(Value, provider);

    sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte(Value, provider);

    float IConvertible.ToSingle(IFormatProvider provider) => Convert.ToSingle(Value, provider);

    string IConvertible.ToString(IFormatProvider provider) => Convert.ToString(Value, provider);

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        => Convert.ChangeType(Value, conversionType);

    ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16(Value, provider);

    uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32(Value, provider);

    ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64(Value, provider);

    #endregion

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Value.ToString(format, provider);

    /// <see cref="INumber{TSelf}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object obj) => Value.CompareTo(obj);

    public int CompareTo(Double1 other) => Value.CompareTo(other.Value);

    /// <see cref="INumberBase{T}"/>

    public static Double1 Abs(Double1 i) => new(i.Value);

    public static bool IsCanonical(Double1 i) => true;
    public static bool IsComplexNumber(Double1 i) => false;
    public static bool IsEvenInteger(Double1 i) => double.IsEvenInteger(i.Value);
    public static bool IsFinite(Double1 i) => double.IsFinite(i.Value);
    public static bool IsImaginaryNumber(Double1 i) => false;
    public static bool IsInfinity(Double1 i) => double.IsInfinity(i.Value);
    public static bool IsInteger(Double1 i) => double.IsInteger(i.Value);
    public static bool IsNaN(Double1 i) => double.IsNaN(i.Value);
    public static bool IsNegative(Double1 i) => false;
    public static bool IsNegativeInfinity(Double1 i) => false;
    public static bool IsNormal(Double1 i) => double.IsNormal(i.Value);
    public static bool IsOddInteger(Double1 i) => double.IsOddInteger(i.Value);
    public static bool IsPositive(Double1 i) => true;
    public static bool IsPositiveInfinity(Double1 i) => double.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(Double1 i) => double.IsRealNumber(i.Value);
    public static bool IsSubnormal(Double1 i) => double.IsSubnormal(i.Value);
    public static bool IsZero(Double1 i) => i.Value == 0;

    public static Double1 MaxMagnitude(Double1 x, Double1 y)
        => new(Math.MaxMagnitude(x.Value, y.Value));
    public static Double1 MaxMagnitudeNumber(Double1 x, Double1 y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static Double1 MinMagnitude(Double1 x, Double1 y)
        => new(Math.MinMagnitude(x.Value, y.Value));
    public static Double1 MinMagnitudeNumber(Double1 x, Double1 y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<Double1>.TryConvertFromChecked<TOther>(TOther i, out Double1 result)
    { result = default; return default; }
    static bool INumberBase<Double1>.TryConvertFromSaturating<TOther>(TOther i, out Double1 result)
    { result = default; return default; }
    static bool INumberBase<Double1>.TryConvertFromTruncating<TOther>(TOther i, out Double1 result)
    { result = default; return default; }
    static bool INumberBase<Double1>.TryConvertToChecked<TOther>(Double1 i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Double1>.TryConvertToSaturating<TOther>(Double1 i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Double1>.TryConvertToTruncating<TOther>(Double1 i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static Double1 Parse(string i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Double1 Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out Double1 result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Double1 result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static Double1 Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Double1 Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out Double1 result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Double1 result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    #endregion

    /// <see cref="IFloatingPoint{TSelf}"/>
    #region

    private IFloatingPoint<double> Float => Value;

    public static Double1 Round(Double1 x, int digits, MidpointRounding mode) => double.Round(x, digits, mode);

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