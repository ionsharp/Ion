using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>A single-precision, floating-point number in range of (0, 1).</summary>
/// <remarks>A number out of range is truncated (see <see cref="Double1"/> for the <see cref="double"/> equivalent).</remarks>
[Description(Description)]
public readonly record struct Single1(float Value)
    : IConvertible, IFormattable, IMinMaxValue<Single1>, INumber<Single1>
{
    /// <see cref="Region.Field"/>

    public const string Description = "A single-precision, floating-point number in range of (0, 1).";

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value.</summary>
    public static Single1 MaxValue { get; } = new(1);

    /// <summary>Get the smallest possible value.</summary>
    public static Single1 MinValue { get; } = new(0);

    static Single1 IAdditiveIdentity<Single1, Single1>.AdditiveIdentity => AdditiveIdentity;
    public static Single1 AdditiveIdentity => Zero;

    static Single1 IMultiplicativeIdentity<Single1, Single1>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static Single1 MultiplicativeIdentity => One;

    static Single1 INumberBase<Single1>.One => One;
    public static Single1 One => new(1);

    static int INumberBase<Single1>.Radix => Radix;
    public static int Radix => 10;

    static Single1 INumberBase<Single1>.Zero => Zero;
    public static Single1 Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly float Value { get; } = Math.Clamp(Value, 0, 1);

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Single1(float input) => new(input);

    public static implicit operator float(Single1 input) => input.Value;

    ///

    public static bool operator <(Single1 a, Single1 b) => a.Value < b.Value;

    public static bool operator >(Single1 a, Single1 b) => a.Value > b.Value;

    public static bool operator <=(Single1 a, Single1 b) => a.Value <= b.Value;

    public static bool operator >=(Single1 a, Single1 b) => a.Value >= b.Value;

    ///

    public static Single1 operator +(Single1 i) => +i.Value;

    public static Single1 operator +(Single1 a, Single1 b) => a.Value + b.Value;

    public static Single1 operator ++(Single1 i) => i.Value + 1;

    public static Single1 operator -(Single1 i) => -i.Value;

    public static Single1 operator -(Single1 a, Single1 b) => a.Value - b.Value;

    public static Single1 operator --(Single1 i) => i.Value - 1;

    public static Single1 operator /(Single1 a, Single1 b) => a.Value / b.Value;

    public static Single1 operator *(Single1 a, Single1 b) => a.Value * b.Value;

    public static Single1 operator %(Single1 a, Single1 b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    public static Single1 Parse(string i) => (Single1)float.Parse(i);

    public static bool TryParse(string i, out Single1 j)
    {
        var k = float.TryParse(i, out float l);
        j = (Single1)l;
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

    /// <see cref="INumber{T}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object obj) => Value.CompareTo(obj);

    public int CompareTo(Single1 other) => Value.CompareTo(other.Value);

    /// <see cref="INumberBase{T}"/>

    public static Single1 Abs(Single1 i) => new(i.Value);

    public static bool IsCanonical(Single1 i) => true;
    public static bool IsComplexNumber(Single1 i) => false;
    public static bool IsEvenInteger(Single1 i) => float.IsEvenInteger(i.Value);
    public static bool IsFinite(Single1 i) => float.IsFinite(i.Value);
    public static bool IsImaginaryNumber(Single1 i) => false;
    public static bool IsInfinity(Single1 i) => float.IsInfinity(i.Value);
    public static bool IsInteger(Single1 i) => float.IsInteger(i.Value);
    public static bool IsNaN(Single1 i) => float.IsNaN(i.Value);
    public static bool IsNegative(Single1 i) => false;
    public static bool IsNegativeInfinity(Single1 i) => false;
    public static bool IsNormal(Single1 i) => float.IsNormal(i.Value);
    public static bool IsOddInteger(Single1 i) => float.IsOddInteger(i.Value);
    public static bool IsPositive(Single1 i) => true;
    public static bool IsPositiveInfinity(Single1 i) => float.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(Single1 i) => float.IsRealNumber(i.Value);
    public static bool IsSubnormal(Single1 i) => float.IsSubnormal(i.Value);
    public static bool IsZero(Single1 i) => i.Value == 0;

    public static Single1 MaxMagnitude(Single1 x, Single1 y)
        => new(Convert.ToSingle(Math.MaxMagnitude(x.Value, y.Value)));
    public static Single1 MaxMagnitudeNumber(Single1 x, Single1 y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static Single1 MinMagnitude(Single1 x, Single1 y)
        => new(Convert.ToSingle(Math.MinMagnitude(x.Value, y.Value)));
    public static Single1 MinMagnitudeNumber(Single1 x, Single1 y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<Single1>.TryConvertFromChecked<TOther>(TOther i, out Single1 result)
    { result = default; return default; }
    static bool INumberBase<Single1>.TryConvertFromSaturating<TOther>(TOther i, out Single1 result)
    { result = default; return default; }
    static bool INumberBase<Single1>.TryConvertFromTruncating<TOther>(TOther i, out Single1 result)
    { result = default; return default; }
    static bool INumberBase<Single1>.TryConvertToChecked<TOther>(Single1 i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Single1>.TryConvertToSaturating<TOther>(Single1 i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Single1>.TryConvertToTruncating<TOther>(Single1 i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static Single1 Parse(string i, IFormatProvider provider)
        => float.Parse(i, provider);
    public static Single1 Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => float.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out Single1 result)
    { var j = float.TryParse(i, provider, out float k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Single1 result)
    { var j = float.TryParse(i, style, provider, out float k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static Single1 Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => float.Parse(i, provider);
    public static Single1 Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => float.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out Single1 result)
    { var j = float.TryParse(i, provider, out float k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Single1 result)
    { var j = float.TryParse(i, style, provider, out float k); result = new(k); return j; }

    #endregion
}