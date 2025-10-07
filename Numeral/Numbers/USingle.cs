using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using static System.Math;

namespace Ion.Numeral;

/// <summary>An unsigned, single-precision, floating-point number.</summary>
/// <remarks>See <see cref="UDouble"/> for the <see cref="double"/> equivalent.</remarks>
[Description(Description)]
public readonly record struct USingle
    : IConvertible, IFloatingPoint<USingle>, IFormattable, IMinMaxValue<USingle>, INumber<USingle>
{
    /// <see cref="Region.Field"/>

    public const string Description
        = "An unsigned, single-precision, floating-point number.";

    /// <summary>Equivalent to <see cref="float.Epsilon"/>.</summary>
    public readonly static USingle Epsilon = float.Epsilon;

    /// <summary>Equivalent to <see cref="float.NaN"/>.</summary>
    public readonly static USingle NaN = float.NaN;

    /// <summary>Equivalent to <see cref="float.PositiveInfinity"/>.</summary>
    public readonly static USingle PositiveInfinity = float.PositiveInfinity;

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value of <see cref="USingle"/>.</summary>
    public static USingle MaxValue { get; } = float.MaxValue;

    /// <summary>Get the smallest possible value of <see cref="USingle"/>.</summary>
    public static USingle MinValue { get; } = float.MinValue;

    public static USingle E => float.E;

    public static USingle Pi => float.Pi;

    public static USingle Tau => float.Tau;

    static USingle IAdditiveIdentity<USingle, USingle>.AdditiveIdentity => AdditiveIdentity;
    public static USingle AdditiveIdentity => Zero;

    static USingle IMultiplicativeIdentity<USingle, USingle>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static USingle MultiplicativeIdentity => One;

    public static USingle NegativeOne => One;

    static USingle INumberBase<USingle>.One => One;
    public static USingle One => new(1);

    static int INumberBase<USingle>.Radix => Radix;
    public static int Radix => 10;

    static USingle INumberBase<USingle>.Zero => Zero;
    public static USingle Zero => new(0);

    /// <see cref="Region.Property"/>

    /// <summary></summary>
    private readonly float Value { get; }

    /// <see cref="Region.Constructor"/>

    /// <summary>Initializes an instance of the <see cref="USingle"/> structure.</summary>
    /// <param name="i"></param>
    public USingle(float i)
    {
        if (float.IsNegativeInfinity(i))
            throw new NotSupportedException();

        Value = Clamp(i, float.MinValue, float.MaxValue);
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator float(USingle i) => i.Value;

    public static implicit operator USingle(float i) => new(i);

    ///

    public static bool operator <(USingle a, USingle b) => a.Value < b.Value;

    public static bool operator >(USingle a, USingle b) => a.Value > b.Value;

    public static bool operator <=(USingle a, USingle b) => a.Value <= b.Value;

    public static bool operator >=(USingle a, USingle b) => a.Value >= b.Value;

    ///

    public static USingle operator +(USingle i) => +i.Value;

    public static USingle operator +(USingle a, USingle b) => a.Value + b.Value;

    public static USingle operator ++(USingle i) => i.Value + 1;

    public static USingle operator -(USingle i) => -i.Value;

    public static USingle operator -(USingle a, USingle b) => a.Value - b.Value;

    public static USingle operator --(USingle i) => i.Value - 1;

    public static USingle operator /(USingle a, USingle b) => a.Value / b.Value;

    public static USingle operator *(USingle a, USingle b) => a.Value * b.Value;

    public static USingle operator %(USingle a, USingle b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    public static USingle Parse(string i) => float.Parse(i);

    public static bool TryParse(string i, out USingle j)
    {
        var k = float.TryParse(i, out float l);
        j = l; return k;
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Single;

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

    object IConvertible.ToType(Type conversionType, IFormatProvider provider) => Convert.ChangeType(Value, conversionType);

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

    public int CompareTo(USingle i) => Value.CompareTo(i.Value);

    /// <see cref="INumberBase{T}"/>

    public static USingle Abs(USingle i) => new(i.Value);

    public static bool IsCanonical(USingle i) => true;
    public static bool IsComplexNumber(USingle i) => false;
    public static bool IsEvenInteger(USingle i) => float.IsEvenInteger(i.Value);
    public static bool IsFinite(USingle i) => float.IsFinite(i.Value);
    public static bool IsImaginaryNumber(USingle i) => false;
    public static bool IsInfinity(USingle i) => float.IsInfinity(i.Value);
    public static bool IsInteger(USingle i) => float.IsInteger(i.Value);
    public static bool IsNaN(USingle i) => float.IsNaN(i.Value);
    public static bool IsNegative(USingle i) => false;
    public static bool IsNegativeInfinity(USingle i) => false;
    public static bool IsNormal(USingle i) => float.IsNormal(i.Value);
    public static bool IsOddInteger(USingle i) => float.IsOddInteger(i.Value);
    public static bool IsPositive(USingle i) => true;
    public static bool IsPositiveInfinity(USingle i) => float.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(USingle i) => float.IsRealNumber(i.Value);
    public static bool IsSubnormal(USingle i) => float.IsSubnormal(i.Value);
    public static bool IsZero(USingle i) => i.Value == 0;

    public static USingle MaxMagnitude(USingle x, USingle y)
        => new(Convert.ToSingle(Math.MaxMagnitude(Convert.ToDouble(x.Value), Convert.ToDouble(y.Value))));
    public static USingle MaxMagnitudeNumber(USingle x, USingle y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static USingle MinMagnitude(USingle x, USingle y)
        => new(Convert.ToSingle(Math.MinMagnitude(Convert.ToDouble(x.Value), Convert.ToDouble(y.Value))));
    public static USingle MinMagnitudeNumber(USingle x, USingle y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<USingle>.TryConvertFromChecked<TOther>(TOther i, out USingle result)
    { result = default; return default; }
    static bool INumberBase<USingle>.TryConvertFromSaturating<TOther>(TOther i, out USingle result)
    { result = default; return default; }
    static bool INumberBase<USingle>.TryConvertFromTruncating<TOther>(TOther i, out USingle result)
    { result = default; return default; }
    static bool INumberBase<USingle>.TryConvertToChecked<TOther>(USingle i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<USingle>.TryConvertToSaturating<TOther>(USingle i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<USingle>.TryConvertToTruncating<TOther>(USingle i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static USingle Parse(string i, IFormatProvider provider)
        => float.Parse(i, provider);
    public static USingle Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => float.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out USingle result)
    { var j = float.TryParse(i, provider, out float k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out USingle result)
    { var j = float.TryParse(i, style, provider, out float k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static USingle Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => float.Parse(i, provider);
    public static USingle Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => float.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out USingle result)
    { var j = float.TryParse(i, provider, out float k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out USingle result)
    { var j = float.TryParse(i, style, provider, out float k); result = new(k); return j; }

    #endregion

    /// <see cref="IFloatingPoint{TSelf}"/>
    #region

    private IFloatingPoint<float> Float => Value;

    public static USingle Round(USingle x, int digits, MidpointRounding mode) => float.Round(x, digits, mode);

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