using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using X = System.Convert;

namespace Ion.Numeral;

/// <summary>A figure formed by two rays sharing a common vertex (measured in degrees or radians).</summary>
/// <remarks>A number out of range is normalized (see <see cref="Normalize(double, AngleType)"/>).</remarks>
[Description(Description)]
public readonly record struct Angle(double Value, AngleType Type = AngleType.Degree)
    : IConvertible, IFloatingPoint<Angle>, IFormattable, IMinMaxValue<Angle>, INumber<Angle>, ISignedNumber<Angle>
{
    /// <see cref="Region.Field"/>

    public const string Description
        = "A figure formed by two rays sharing a common vertex.";

    public const string StringFormatDegree = "{0}°";

    /// <summary>A prefix to append to the string format to indicate value in degrees is desired.</summary>
    public const string StringFormatDegreePrefix = "D-";

    public const string StringFormatRadian = "{0}rad";

    /// <summary>A prefix to append to the string format to indicate value in radians is desired.</summary>
    public const string StringFormatRadianPrefix = "R-";

    /// <summary>360</summary>
    public const double C = 360.0;

    /// <summary>180</summary>
    public const double D = C / 2;

    /// <summary><see cref="Math.PI"/> / 180</summary>
    public const double Pd = Math.PI / D;

    /// <summary>180 / <see cref="Math.PI"/></summary>
    public const double Dp = D / Math.PI;

    /// <see cref="Region.Property"/>

    /// <summary>Get the largest possible value.</summary>
    public static Angle MaxValue { get; } = new(C);

    /// <summary>Get the smallest possible value.</summary>
    public static Angle MinValue { get; } = new(0);

    public static Angle E => double.E;

    public static Angle Pi => double.Pi;

    public static Angle Tau => double.Tau;

    static Angle IAdditiveIdentity<Angle, Angle>.AdditiveIdentity => AdditiveIdentity;
    public static Angle AdditiveIdentity => Zero;

    static Angle IMultiplicativeIdentity<Angle, Angle>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static Angle MultiplicativeIdentity => One;

    public static Angle NegativeOne => -One;

    static Angle INumberBase<Angle>.One => One;
    public static Angle One => new(1);

    static int INumberBase<Angle>.Radix => Radix;
    public static int Radix => 10;

    static Angle INumberBase<Angle>.Zero => Zero;
    public static Angle Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly AngleType Type { get; } = Type;

    /// <summary>The value of the angle (in degrees).</summary>
    private readonly double Value { get; } = Normalize(Value, Type);

    /// <see cref="Region.Constructor"/>

    public Angle() : this(default) { }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator double(Angle i) => i.Value;

    public static implicit operator Angle(double i) => new(i);

    ///

    public static bool operator <(Angle a, Angle b) => a.Value < b.Value;

    public static bool operator >(Angle a, Angle b) => a.Value > b.Value;

    public static bool operator <=(Angle a, Angle b) => a.Value <= b.Value;

    public static bool operator >=(Angle a, Angle b) => a.Value >= b.Value;

    ///

    public static Angle operator +(Angle i) => +i.Value;

    public static Angle operator +(Angle a, Angle b) => a.Value + b.Value;

    public static Angle operator ++(Angle i) => i.Value + 1;

    public static Angle operator -(Angle i) => -i.Value;

    public static Angle operator -(Angle a, Angle b) => a.Value - b.Value;

    public static Angle operator --(Angle i) => i.Value - 1;

    public static Angle operator /(Angle a, Angle b) => a.Value / b.Value;

    public static Angle operator *(Angle a, Angle b) => a.Value * b.Value;

    public static Angle operator %(Angle a, Angle b) => a.Value % b.Value;

    #endregion

    /// <see cref="Region.Method"/>

    public Angle Convert(AngleType to)
    {
        return to switch
        {
            AngleType.Degree => Type == AngleType.Degree ? new(Value, AngleType.Degree) : new(Value * Dp, AngleType.Degree),
            AngleType.Radian => Type == AngleType.Radian ? new(Value, AngleType.Radian) : new(Pd * Value, AngleType.Radian),
        };
    }

    ///

    /// <summary>Get a normalized angle from the current instance.</summary>
    /// <remarks><see cref="Normalize(double, AngleType)"/></remarks>
    public Angle Normalize() => Normalize(Value, Type);

    /// <summary>
    /// <para>Normalize the value as an angle of the given type.</para>
    /// <para>Example, 720 → 360.</para>
    /// </summary>
    /// <remarks><b>This differs from traditional (range-based) normalization.</b></remarks>
    /// <param name="angle">The angle to normalize.</param>
    /// <param name="angleType">The type of the angle.</param>
    public static double Normalize(double angle, AngleType angleType = AngleType.Degree)
    {
        /// Convert to degrees first
        if (angleType == AngleType.Radian)
            angle = Pd * angle;

        /// Clamp to relevant range
        var j = angle % MaxValue;
        j = j >= 0 ? j : (j + MaxValue);
        return Math.Clamp(j, MinValue, MaxValue);
    }

    ///

    public static Angle Parse(string i) => double.Parse(i);

    public static bool TryParse(string i, out Angle j)
    {
        var l = double.TryParse(i, out double k);
        j = k; return l;
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Double;

    bool IConvertible.ToBoolean(IFormatProvider provider) => double.IsPositive(Value);

    byte IConvertible.ToByte(IFormatProvider provider) => X.ToByte(Value, provider);

    char IConvertible.ToChar(IFormatProvider provider) => X.ToChar(Value, provider);

    DateTime IConvertible.ToDateTime(IFormatProvider provider) => X.ToDateTime(Value, provider);

    decimal IConvertible.ToDecimal(IFormatProvider provider) => X.ToDecimal(Value, provider);

    double IConvertible.ToDouble(IFormatProvider provider) => X.ToDouble(Value, provider);

    short IConvertible.ToInt16(IFormatProvider provider) => X.ToInt16(Value, provider);

    int IConvertible.ToInt32(IFormatProvider provider) => X.ToInt32(Value, provider);

    long IConvertible.ToInt64(IFormatProvider provider) => X.ToInt64(Value, provider);

    sbyte IConvertible.ToSByte(IFormatProvider provider) => X.ToSByte(Value, provider);

    float IConvertible.ToSingle(IFormatProvider provider) => X.ToSingle(Value, provider);

    string IConvertible.ToString(IFormatProvider provider) => X.ToString(Value, provider);

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        => X.ChangeType(Value, conversionType);

    ushort IConvertible.ToUInt16(IFormatProvider provider) => X.ToUInt16(Value, provider);

    uint IConvertible.ToUInt32(IFormatProvider provider) => X.ToUInt32(Value, provider);

    ulong IConvertible.ToUInt64(IFormatProvider provider) => X.ToUInt64(Value, provider);

    #endregion

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
    {
        double a = 0;
        string template = null;

        if (format.StartsWith(StringFormatDegreePrefix))
        {
            a = Value;
            template = StringFormatDegree;
            format = format[StringFormatDegreePrefix.Length..];
        }

        else if (format.StartsWith(StringFormatRadianPrefix))
        {
            a = Convert(AngleType.Radian);
            template = StringFormatRadian;
            format = format[StringFormatRadianPrefix.Length..];
        }

        var b = a.ToString(format, provider);
        return template?.F(b) ?? b;
    }

    /// <see cref="INumber{TSelf}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object i) => Value.CompareTo(i);

    public int CompareTo(Angle i) => Value.CompareTo(i.Value);

    /// <see cref="INumberBase{T}"/>

    public static Angle Abs(Angle i) => new(double.Abs(i.Value));

    public static bool IsCanonical(Angle i) => i.Type == AngleType.Degree;
    public static bool IsComplexNumber(Angle i) => false;
    public static bool IsEvenInteger(Angle i) => double.IsEvenInteger(i.Value);
    public static bool IsFinite(Angle i) => double.IsFinite(i.Value);
    public static bool IsImaginaryNumber(Angle i) => false;
    public static bool IsInfinity(Angle i) => double.IsInfinity(i.Value);
    public static bool IsInteger(Angle i) => double.IsInteger(i.Value);
    public static bool IsNaN(Angle i) => double.IsNaN(i.Value);
    public static bool IsNegative(Angle i) => double.IsNegative(i.Value);
    public static bool IsNegativeInfinity(Angle i) => double.IsNegativeInfinity(i.Value);
    public static bool IsNormal(Angle i) => double.IsNormal(i.Value);
    public static bool IsOddInteger(Angle i) => double.IsOddInteger(i.Value);
    public static bool IsPositive(Angle i) => double.IsPositive(i.Value);
    public static bool IsPositiveInfinity(Angle i) => double.IsPositiveInfinity(i.Value);
    public static bool IsRealNumber(Angle i) => double.IsRealNumber(i.Value);
    public static bool IsSubnormal(Angle i) => double.IsSubnormal(i.Value);
    public static bool IsZero(Angle i) => i.Value == 0;

    public static Angle MaxMagnitude(Angle x, Angle y)
        => new(Math.MaxMagnitude(x.Value, y.Value));
    public static Angle MaxMagnitudeNumber(Angle x, Angle y)
        => IsNaN(x) ? y : MaxMagnitude(x, y);
    public static Angle MinMagnitude(Angle x, Angle y)
        => new(Math.MinMagnitude(x.Value, y.Value));
    public static Angle MinMagnitudeNumber(Angle x, Angle y)
        => IsNaN(x) ? y : MinMagnitude(x, y);

    static bool INumberBase<Angle>.TryConvertFromChecked<TOther>(TOther i, out Angle result)
    { result = default; return default; }
    static bool INumberBase<Angle>.TryConvertFromSaturating<TOther>(TOther i, out Angle result)
    { result = default; return default; }
    static bool INumberBase<Angle>.TryConvertFromTruncating<TOther>(TOther i, out Angle result)
    { result = default; return default; }
    static bool INumberBase<Angle>.TryConvertToChecked<TOther>(Angle i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Angle>.TryConvertToSaturating<TOther>(Angle i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Angle>.TryConvertToTruncating<TOther>(Angle i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static Angle Parse(string i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Angle Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out Angle result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Angle result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static Angle Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => double.Parse(i, provider);
    public static Angle Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => double.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out Angle result)
    { var j = double.TryParse(i, provider, out double k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Angle result)
    { var j = double.TryParse(i, style, provider, out double k); result = new(k); return j; }

    #endregion

    /// <see cref="IFloatingPoint{TSelf}"/>
    #region

    private IFloatingPoint<double> Float => Value;

    public static Angle Round(Angle x, int digits, MidpointRounding mode) => double.Round(x, digits, mode);

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