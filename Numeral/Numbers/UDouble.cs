using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>An unsigned, double-precision, floating-point number.</summary>
/// <remarks>See <see cref="USingle"/> for the <see cref="float"/> equivalent.</remarks>
[Description(Description)]
public readonly record struct UDouble
    : IConvertible, IFloatingPoint<UDouble>, IFormattable, IMinMaxValue<UDouble>, INumber<UDouble>
{
    /// <see cref="Region.Field"/>

    public const string Description
        = "An unsigned, double-precision, floating-point number.";

    /// <summary>Equivalent to <see cref="double.Epsilon"/>.</summary>
    public readonly static UDouble Epsilon = double.Epsilon;

    /// <summary>Equivalent to <see cref="double.NaN"/>.</summary>
    public readonly static UDouble NaN = double.NaN;

    /// <summary>Equivalent to <see cref="double.PositiveInfinity"/>.</summary>
    public readonly static UDouble PositiveInfinity = double.PositiveInfinity;

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value.</summary>
    public static UDouble MaxValue { get; } = double.MaxValue;

    /// <summary>Get the smallest possible value.</summary>
    public static UDouble MinValue { get; } = 0;

    public static UDouble E => double.E;

    public static UDouble Pi => double.Pi;

    public static UDouble Tau => double.Tau;

    static UDouble IAdditiveIdentity<UDouble, UDouble>.AdditiveIdentity => AdditiveIdentity;
    public static UDouble AdditiveIdentity => Zero;

    static UDouble IMultiplicativeIdentity<UDouble, UDouble>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static UDouble MultiplicativeIdentity => One;

    public static UDouble NegativeOne => One;

    static UDouble INumberBase<UDouble>.One => One;
    public static UDouble One => new(1);

    static int INumberBase<UDouble>.Radix => Radix;
    public static int Radix => 10;

    static UDouble INumberBase<UDouble>.Zero => Zero;
    public static UDouble Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly double Value { get; }

    /// <see cref="Region.Constructor"/>

    /// <summary>Initializes an instance of the <see cref="UDouble"/> structure.</summary>
    /// <param name="i"></param>
    public UDouble(double i)
    {
        if (double.IsNegativeInfinity(i))
            throw new NotSupportedException();

        Value = Math.Clamp(i, double.MinValue, double.MaxValue);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator double(UDouble i) => i.Value;

    public static implicit operator UDouble(double i) => new(i);

    ///

    public static bool operator <(UDouble a, UDouble b) => a.Value < b.Value;

    public static bool operator >(UDouble a, UDouble b) => a.Value > b.Value;

    public static bool operator <=(UDouble a, UDouble b) => a.Value <= b.Value;

    public static bool operator >=(UDouble a, UDouble b) => a.Value >= b.Value;

    ///

    public static UDouble operator +(UDouble i) => +i.Value;

    public static UDouble operator +(UDouble a, UDouble b) => a.Value + b.Value;

    public static UDouble operator ++(UDouble i) => i.Value + 1;

    public static UDouble operator -(UDouble i) => -i.Value;

    public static UDouble operator -(UDouble a, UDouble b) => a.Value - b.Value;

    public static UDouble operator --(UDouble i) => i.Value - 1;

    public static UDouble operator /(UDouble a, UDouble b) => a.Value / b.Value;

    public static UDouble operator *(UDouble a, UDouble b) => a.Value * b.Value;

    public static UDouble operator %(UDouble a, UDouble b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    public static UDouble Parse(string i) => double.Parse(i);

    public static bool TryParse(string i, out UDouble j)
    {
        var k = double.TryParse(i, out double l);
        j = l; return k;
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Double;

    bool IConvertible.ToBoolean(IFormatProvider provider) => Convert.ToBoolean(Value);

    byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte(Value);

    char IConvertible.ToChar(IFormatProvider provider) => Convert.ToChar(Value);

    DateTime IConvertible.ToDateTime(IFormatProvider provider) => Convert.ToDateTime(Value);

    decimal IConvertible.ToDecimal(IFormatProvider provider) => Convert.ToDecimal(Value);

    double IConvertible.ToDouble(IFormatProvider provider) => Convert.ToDouble(Value);

    short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16(Value);

    int IConvertible.ToInt32(IFormatProvider provider) => Convert.ToInt32(Value);

    long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt64(Value);

    sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte(Value);

    float IConvertible.ToSingle(IFormatProvider provider) => Convert.ToSingle(Value);

    string IConvertible.ToString(IFormatProvider provider) => Convert.ToString(Value);

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        => Convert.ChangeType(Value, conversionType);

    ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16(Value);

    uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32(Value);

    ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64(Value);

    #endregion

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Value.ToString(format, provider);

    /// <see cref="INumber{TSelf}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object i) => Value.CompareTo(i);

    public int CompareTo(UDouble i) => Value.CompareTo(i.Value);

    /// <see cref="INumberBase{T}"/>

    public static UDouble Abs(UDouble i) => new(i.Value);

    public static bool IsCanonical(UDouble i) => true;
    public static bool IsComplexNumber(UDouble i) => false;
    public static bool IsEvenInteger(UDouble i) => double.IsEvenInteger(i.Value);
    public static bool IsFinite(UDouble i) => double.IsFinite(i.Value);
    public static bool IsImaginaryNumber(UDouble i) => false;
    public static bool IsInfinity(UDouble i) => double.IsInfinity(i.Value);
    public static bool IsInteger(UDouble i) => double.IsInteger(i.Value);
    public static bool IsNaN(UDouble i) => double.IsNaN(i.Value);
    public static bool IsNegative(UDouble i) => false;
    public static bool IsNegativeInfinity(UDouble i) => false;
    public static bool IsNormal(UDouble i) => double.IsNormal(i.Value);
    public static bool IsOddInteger(UDouble i) => double.IsOddInteger(i.Value);
    public static bool IsPositive(UDouble i) => true;
    public static bool IsPositiveInfinity(UDouble i) => double.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(UDouble i) => double.IsRealNumber(i.Value);
    public static bool IsSubnormal(UDouble i) => double.IsSubnormal(i.Value);
    public static bool IsZero(UDouble i) => i.Value == 0;

    public static UDouble MaxMagnitude(UDouble x, UDouble y)
        => new(Math.MaxMagnitude(x.Value, y.Value));
    public static UDouble MaxMagnitudeNumber(UDouble x, UDouble y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static UDouble MinMagnitude(UDouble x, UDouble y)
        => new(Math.MinMagnitude(x.Value, y.Value));
    public static UDouble MinMagnitudeNumber(UDouble x, UDouble y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<UDouble>.TryConvertFromChecked<TOther>(TOther i, out UDouble result)
    { result = default; return default; }
    static bool INumberBase<UDouble>.TryConvertFromSaturating<TOther>(TOther i, out UDouble result)
    { result = default; return default; }
    static bool INumberBase<UDouble>.TryConvertFromTruncating<TOther>(TOther i, out UDouble result)
    { result = default; return default; }
    static bool INumberBase<UDouble>.TryConvertToChecked<TOther>(UDouble i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<UDouble>.TryConvertToSaturating<TOther>(UDouble i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<UDouble>.TryConvertToTruncating<TOther>(UDouble i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static UDouble Parse(string i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static UDouble Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out UDouble result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out UDouble result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static UDouble Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static UDouble Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out UDouble result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out UDouble result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    #endregion

    /// <see cref="IFloatingPoint{TSelf}"/>
    #region

    private IFloatingPoint<double> Float => Value;

    public static UDouble Round(UDouble x, int digits, MidpointRounding mode) => double.Round(x, digits, mode);

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