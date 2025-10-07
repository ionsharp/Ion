using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Ion.Numeral;

/// <summary>A number that is either 0 or 1.</summary>
[Description(Description)]
public readonly record struct Bit()
    : IConvertible, IFormattable, IMinMaxValue<Bit>, INumber<Bit>
{
    public const string Description = "A number that is either 0 or 1.";

    public const string StringFormat = "{0}";

    /// <summary>Get the largest possible value.</summary>
    public static Bit MaxValue { get; } = new(1);

    /// <summary>Get the smallest possible value.</summary>
    public static Bit MinValue { get; } = new(0);

    static Bit IAdditiveIdentity<Bit, Bit>.AdditiveIdentity => AdditiveIdentity;
    public static Bit AdditiveIdentity => Zero;

    static Bit IMultiplicativeIdentity<Bit, Bit>.MultiplicativeIdentity => MultiplicativeIdentity;
    public static Bit MultiplicativeIdentity => One;

    static Bit INumberBase<Bit>.One => One;
    public static Bit One => new(1);

    static int INumberBase<Bit>.Radix => Radix;
    public static int Radix => 2;

    static Bit INumberBase<Bit>.Zero => Zero;
    public static Bit Zero => new(0);

    /// <see cref="Region.Property"/>

    private readonly short Value { get; }

    /// <see cref="Region.Constructor"/>

    public Bit(bool i) : this(i ? 1 : 0) { }

    public Bit(int i) : this()
    {
        Throw.If<ArgumentOutOfRangeException>(i != 0 && i != 1);
        Value = i.ToInt16();
    }

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Bit(bool i) => new(i);

    public static implicit operator Bit(int i) => new(i);

    public static implicit operator bool(Bit i) => i.Value == 1;

    public static implicit operator int(Bit i) => i.Value;

    public static bool operator >(Bit left, Bit right) => throw new NotImplementedException();

    public static bool operator >=(Bit left, Bit right) => throw new NotImplementedException();

    public static bool operator <(Bit left, Bit right) => throw new NotImplementedException();

    public static bool operator <=(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator %(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator +(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator --(Bit value) => throw new NotImplementedException();

    public static Bit operator /(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator ++(Bit value) => throw new NotImplementedException();

    public static Bit operator *(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator -(Bit left, Bit right) => throw new NotImplementedException();

    public static Bit operator -(Bit value) => throw new NotImplementedException();

    public static Bit operator +(Bit value) => throw new NotImplementedException();

    static bool IComparisonOperators<Bit, Bit, bool>.operator >(Bit left, Bit right) => throw new NotImplementedException();

    static bool IComparisonOperators<Bit, Bit, bool>.operator >=(Bit left, Bit right) => throw new NotImplementedException();

    static bool IComparisonOperators<Bit, Bit, bool>.operator <(Bit left, Bit right) => throw new NotImplementedException();

    static bool IComparisonOperators<Bit, Bit, bool>.operator <=(Bit left, Bit right) => throw new NotImplementedException();

    static Bit IModulusOperators<Bit, Bit, Bit>.operator %(Bit left, Bit right) => throw new NotImplementedException();

    static Bit IAdditionOperators<Bit, Bit, Bit>.operator +(Bit left, Bit right) => throw new NotImplementedException();

    static Bit IDecrementOperators<Bit>.operator --(Bit value) => throw new NotImplementedException();

    static Bit IDivisionOperators<Bit, Bit, Bit>.operator /(Bit left, Bit right) => throw new NotImplementedException();

    static bool IEqualityOperators<Bit, Bit, bool>.operator ==(Bit left, Bit right) => throw new NotImplementedException();

    static bool IEqualityOperators<Bit, Bit, bool>.operator !=(Bit left, Bit right) => throw new NotImplementedException();

    static Bit IIncrementOperators<Bit>.operator ++(Bit value) => throw new NotImplementedException();

    static Bit IMultiplyOperators<Bit, Bit, Bit>.operator *(Bit left, Bit right) => throw new NotImplementedException();

    static Bit ISubtractionOperators<Bit, Bit, Bit>.operator -(Bit left, Bit right) => throw new NotImplementedException();

    static Bit IUnaryNegationOperators<Bit, Bit>.operator -(Bit value) => throw new NotImplementedException();

    static Bit IUnaryPlusOperators<Bit, Bit>.operator +(Bit value) => throw new NotImplementedException();

    #endregion

    /// <see cref="Region.Method"/>

    public static Bit Parse(string i) => (Bit)int.Parse(i);

    public static bool TryParse(string i, out Bit j)
    {
        _ = int.TryParse(i, out int k);
        if (k != 0 && k != 1)
        {
            j = default;
            return false;
        }
        else
        {
            j = (Bit)k;
            return true;
        }
    }

    /// <see cref="IConvertible"/>
    #region

    TypeCode IConvertible.GetTypeCode() => TypeCode.Boolean;

    bool IConvertible.ToBoolean(IFormatProvider provider) => Value == 1;

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

    public readonly string ToString(string format, IFormatProvider provider) => StringFormat.F(Value.ToString(format, provider));

    /// <see cref="INumber{T}"/>
    #region

    /// <see cref="IComparable"/>

    public int CompareTo(object obj) => Value.CompareTo(obj);

    public int CompareTo(Bit other) => Value.CompareTo(other.Value);

    /// <see cref="INumberBase{T}"/>

    public static Bit Abs(Bit i) => new(i.Value);

    public static bool IsCanonical(Bit i) => true;
    public static bool IsComplexNumber(Bit i) => false;
    public static bool IsEvenInteger(Bit i) => i.Value == 0;
    public static bool IsFinite(Bit i) => true;
    public static bool IsImaginaryNumber(Bit i) => false;
    public static bool IsInfinity(Bit i) => false;
    public static bool IsInteger(Bit i) => true;
    public static bool IsNaN(Bit i) => false;
    public static bool IsNegative(Bit i) => false;
    public static bool IsNegativeInfinity(Bit i) => false;
    public static bool IsNormal(Bit i) => false;
    public static bool IsOddInteger(Bit i) => i.Value == 1;
    public static bool IsPositive(Bit i) => true;
    public static bool IsPositiveInfinity(Bit i) => false;
    public static bool IsRealNumber(Bit i) => false;
    public static bool IsSubnormal(Bit i) => false;
    public static bool IsZero(Bit i) => i.Value == 0;

    public static Bit MaxMagnitude(Bit x, Bit y)
        => new(x.Value == 1 || y.Value == 1 ? 1 : 0);
    public static Bit MaxMagnitudeNumber(Bit x, Bit y)
        => MaxMagnitude(x, y);
    public static Bit MinMagnitude(Bit x, Bit y)
        => new(x.Value == 0 || y.Value == 0 ? 0 : 1);
    public static Bit MinMagnitudeNumber(Bit x, Bit y)
        => MinMagnitude(x, y);

    static bool INumberBase<Bit>.TryConvertFromChecked<TOther>(TOther i, out Bit result)
    { result = default; return default; }
    static bool INumberBase<Bit>.TryConvertFromSaturating<TOther>(TOther i, out Bit result)
    { result = default; return default; }
    static bool INumberBase<Bit>.TryConvertFromTruncating<TOther>(TOther i, out Bit result)
    { result = default; return default; }
    static bool INumberBase<Bit>.TryConvertToChecked<TOther>(Bit i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Bit>.TryConvertToSaturating<TOther>(Bit i, out TOther result)
    { result = default; return default; }
    static bool INumberBase<Bit>.TryConvertToTruncating<TOther>(Bit i, out TOther result)
    { result = default; return default; }

    /// <see cref="IParsable{T}"/>

    public static Bit Parse(string i, IFormatProvider provider)
        => short.Parse(i, provider);
    public static Bit Parse(string i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => short.Parse(i, style, provider);
    public static bool TryParse([NotNullWhen(true)] string i, IFormatProvider provider, [MaybeNullWhen(false)] out Bit result)
    { var j = short.TryParse(i, provider, out short k); result = new(k); return j; }
    public static bool TryParse([NotNullWhen(true)] string i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Bit result)
    { var j = short.TryParse(i, style, provider, out short k); result = new(k); return j; }

    /// <see cref="ISpanFormattable"/>

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    { charsWritten = default; return default; }

    /// <see cref="ISpanParsable{T}"/>

    public static Bit Parse(ReadOnlySpan<char> i, IFormatProvider provider)
        => short.Parse(i, provider);
    public static Bit Parse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider)
        => short.Parse(i, style, provider);
    public static bool TryParse(ReadOnlySpan<char> i, IFormatProvider provider, [MaybeNullWhen(false)] out Bit result)
    { var j = short.TryParse(i, provider, out short k); result = new(k); return j; }
    public static bool TryParse(ReadOnlySpan<char> i, System.Globalization.NumberStyles style, IFormatProvider provider, [MaybeNullWhen(false)] out Bit result)
    { var j = short.TryParse(i, style, provider, out short k); result = new(k); return j; }

    #endregion
}