using Ion;
using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Ion.Numeral;

/// <summary>Extends <see cref="INumber{}"/>.</summary>
[Extend(typeof(INumber<>))]
[Extend(typeof(IFloatingPoint<>))]
[Extend(typeof(IMinMaxValue<>))]
[Extend(typeof(ISignedNumber<>))]
public static partial class XNumber;

[Extend(typeof(object))]
public static partial class XNumber
{
    /// <see cref="INumber{}"/> | <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>
    /// Get absolute value (by negating if negative).
    /// <para><b>Examples</b></para>
    /// <list type="bullet">
    /// <item>±0 ⇒ +0</item>
    /// <item>±1 ⇒ +1</item>
    /// </list>
    /// </summary>
    public static T Abs<T>(this T i)
        where T : INumber<T> => T.Abs(i);

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Byte i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Decimal i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Double i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Int16 i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <summary>
    /// Get instance of new type from instance of old type with given <see cref="NumberCreate"/>.
    /// </summary>
    /// <inheritdoc cref="NumberCreate"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="OverflowException"/>
    public static T Create<T>(this Int32 i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Int64 i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <inheritdoc cref="Create{}(int, NumberCreate)"/>
    public static T Create<T>(this Single i, NumberCreate create = 0)
        where T : INumber<T> => create switch { NumberCreate.Checked => T.CreateChecked(i), NumberCreate.Saturated => T.CreateSaturating(i), NumberCreate.Truncated => T.CreateTruncating(i) };

    /// <summary>
    /// Get <b>i</b> ÷ <b>j</b>.
    /// </summary>
    /// <exception cref="DivideByZeroException"/>
    public static T Divide<T>(this T i, T j) where T : INumber<T> => i / j;

    /// <summary>
    /// Get <b>i</b> ÷ 2.
    /// </summary>
    public static T Divide2<T>(this T i) where T : INumber<T> => i / 2.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 3.
    /// </summary>
    public static T Divide3<T>(this T i) where T : INumber<T> => i / 3.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 4.
    /// </summary>
    public static T Divide4<T>(this T i) where T : INumber<T> => i / 4.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 5.
    /// </summary>
    public static T Divide5<T>(this T i) where T : INumber<T> => i / 5.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 6.
    /// </summary>
    public static T Divide6<T>(this T i) where T : INumber<T> => i / 6.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 7.
    /// </summary>
    public static T Divide7<T>(this T i) where T : INumber<T> => i / 7.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 8.
    /// </summary>
    public static T Divide8<T>(this T i) where T : INumber<T> => i / 8.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 9.
    /// </summary>
    public static T Divide9<T>(this T i) where T : INumber<T> => i / 9.Create<T>();

    /// <summary>
    /// Get <b>i</b> ÷ 10.
    /// </summary>
    public static T Divide10<T>(this T i) where T : INumber<T> => i / 10.Create<T>();

    /// <summary>
    /// <see cref="Do"/> between <b>i</b> and <b>j</b>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T Do<T>(this T i, Operator action, T j)
        where T : INumber<T>
        => action switch
        {
            Numeral.Operator.Add
                => i + j,
            Numeral.Operator.Divide
                => i / j,
            Numeral.Operator.Modulo
                => i % j,
            Numeral.Operator.Multiply
                => i * j,
            Numeral.Operator.Subtract
                => i - j,
            _ => throw new ArgumentOutOfRangeException(nameof(action))
        };

    /// <summary>
    /// Get <b>i</b> - <b>j</b>.
    /// </summary>
    public static T Minus<T>(this T i, T j)
        where T : INumber<T> => i - j;

    /// <summary>
    /// Get <b>i</b> % <b>j</b>.
    /// </summary>
    public static T Modulo<T>(this T i, T j) where T : INumber<T> => i % j;

    /// <summary>
    /// Get <b>i</b> % 2.
    /// </summary>
    public static T Modulo2<T>(this T i) where T : INumber<T> => i % 2.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 3.
    /// </summary>
    public static T Modulo3<T>(this T i) where T : INumber<T> => i % 3.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 4.
    /// </summary>
    public static T Modulo4<T>(this T i) where T : INumber<T> => i % 4.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 5.
    /// </summary>
    public static T Modulo5<T>(this T i) where T : INumber<T> => i % 5.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 6.
    /// </summary>
    public static T Modulo6<T>(this T i) where T : INumber<T> => i % 6.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 7.
    /// </summary>
    public static T Modulo7<T>(this T i) where T : INumber<T> => i % 7.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 8.
    /// </summary>
    public static T Modulo8<T>(this T i) where T : INumber<T> => i % 8.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 9.
    /// </summary>
    public static T Modulo9<T>(this T i) where T : INumber<T> => i % 9.Create<T>();

    /// <summary>
    /// Get <b>i</b> % 10.
    /// </summary>
    public static T Modulo10<T>(this T i) where T : INumber<T> => i % 10.Create<T>();

    /// <summary>
    /// Get <b>i</b> × <b>j</b>.
    /// </summary>
    public static T Multiply<T>(this T i, T j) where T : INumber<T> => i * j;

    /// <summary>
    /// Get <b>i</b> × 2.
    /// </summary>
    public static T Multiply2<T>(this T i) where T : INumber<T> => i * 2.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 3.
    /// </summary>
    public static T Multiply3<T>(this T i) where T : INumber<T> => i * 3.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 4.
    /// </summary>
    public static T Multiply4<T>(this T i) where T : INumber<T> => i * 4.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 5.
    /// </summary>
    public static T Multiply5<T>(this T i) where T : INumber<T> => i * 5.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 6.
    /// </summary>
    public static T Multiply6<T>(this T i) where T : INumber<T> => i * 6.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 7.
    /// </summary>
    public static T Multiply7<T>(this T i) where T : INumber<T> => i * 7.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 8.
    /// </summary>
    public static T Multiply8<T>(this T i) where T : INumber<T> => i * 8.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 9.
    /// </summary>
    public static T Multiply9<T>(this T i) where T : INumber<T> => i * 9.Create<T>();

    /// <summary>
    /// Get <b>i</b> × 10.
    /// </summary>
    public static T Multiply10<T>(this T i) where T : INumber<T> => i * 10.Create<T>();

    /// <summary>
    /// Get nearest multiple (or factor).
    /// <para><b>Example</b></para>
    /// Nearest(0.00, 4) = 0<br/>
    /// Nearest(1.00, 4) = 0<br/>
    /// Nearest(1.99, 4) = 0<br/>
    /// Nearest(2.00, 4) = 4<br/>
    /// Nearest(3.00, 4) = 4<br/>
    /// Nearest(4.00, 4) = 4<br/>
    /// Nearest(5.00, 4) = 4<br/>
    /// Nearest(5.99, 4) = 4<br/>
    /// Nearest(6.00, 4) = 8<br/>
    /// Nearest(7.00, 4) = 8<br/>
    /// Nearest(8.00, 4) = 8<br/>
    /// </summary>
    /// <remarks>
    /// <b>Converts to <see cref="double"/> and back.</b>
    /// </remarks>
    public static T Nearest<T>(this T i, T multiple) 
        where T : INumber<T> => ((i / multiple).ToDouble().Round(MidpointRounding.ToEven) * multiple.ToDouble()).Create<T>();

    /// <summary>
    /// Get <b>i</b> + <b>j</b>.
    /// </summary>
    public static T Plus<T>(this T i, T j)
        where T : INumber<T> => i + j;

    /// <summary>
    /// Get <b>i</b>^<b>j</b>.
    /// </summary>
    public static T Pow<T>(this T i, T j) 
        where T : INumber<T> => Pow(i.ToDouble(), j.ToDouble()).Create<T>();

    /// <summary>
    /// Get <b>i</b>^2.
    /// </summary>
    public static T Pow2<T>(this T i) where T : INumber<T> => i.Pow(2.Create<T>());

    /// <summary>
    /// Get <b>i</b>^3.
    /// </summary>
    public static T Pow3<T>(this T i) where T : INumber<T> => i.Pow(3.Create<T>());

    /// <summary>
    /// Get <b>i</b>^4.
    /// </summary>
    public static T Pow4<T>(this T i) where T : INumber<T> => i.Pow(4.Create<T>());

    /// <summary>
    /// Get <b>i</b>^5.
    /// </summary>
    public static T Pow5<T>(this T i) where T : INumber<T> => i.Pow(5.Create<T>());

    /// <summary>
    /// Get <b>i</b>^6.
    /// </summary>
    public static T Pow6<T>(this T i) where T : INumber<T> => i.Pow(6.Create<T>());

    /// <summary>
    /// Get <b>i</b>^7.
    /// </summary>
    public static T Pow7<T>(this T i) where T : INumber<T> => i.Pow(7.Create<T>());

    /// <summary>
    /// Get <b>i</b>^8.
    /// </summary>
    public static T Pow8<T>(this T i) where T : INumber<T> => i.Pow(8.Create<T>());

    /// <summary>
    /// Get <b>i</b>^9.
    /// </summary>
    public static T Pow9<T>(this T i) where T : INumber<T> => i.Pow(9.Create<T>());

    /// <summary>
    /// Get <b>i</b>^10.
    /// </summary>
    public static T Pow10<T>(this T i) where T : INumber<T> => i.Pow(10.Create<T>());

    /// <inheritdoc cref="Convert.ToByte(object?)"/>
    public static Byte ToByte<T>(this T i)
        where T : INumber<T> => Convert.ToByte(i);

    /// <inheritdoc cref="Convert.ToDecimal(object?)"/>
    public static Decimal ToDecimal<T>(this T i)
        where T : INumber<T> => Convert.ToDecimal(i);

    /// <inheritdoc cref="Convert.ToDouble(object?)"/>
    public static Double ToDouble<T>(this T i)
        where T : INumber<T> => Convert.ToDouble(i);

    /// <inheritdoc cref="Convert.ToInt16(object?)"/>
    public static Int16 ToInt16<T>(this T i)
        where T : INumber<T> => Convert.ToInt16(i);

    /// <inheritdoc cref="Convert.ToInt32(object?)"/>
    public static Int32 ToInt32<T>(this T i)
        where T : INumber<T> => Convert.ToInt32(i);

    /// <inheritdoc cref="Convert.ToInt64(object?)"/>
    public static Int64 ToInt64<T>(this T i)
        where T : INumber<T> => Convert.ToInt64(i);

    /// <inheritdoc cref="Convert.ToByte(object?)"/>
    public static SByte ToSByte<T>(this T i)
        where T : INumber<T> => Convert.ToSByte(i);

    /// <inheritdoc cref="Convert.ToSingle(object?)"/>
    public static Single ToSingle<T>(this T i)
        where T : INumber<T> => Convert.ToSingle(i);

    /// <inheritdoc cref="Convert.ToUInt16(object?)"/>
    public static UInt16 ToUInt16<T>(this T i)
        where T : INumber<T> => Convert.ToUInt16(i);

    /// <inheritdoc cref="Convert.ToUInt32(object?)"/>
    public static UInt32 ToUInt32<T>(this T i)
        where T : INumber<T> => Convert.ToUInt32(i);

    /// <inheritdoc cref="Convert.ToUInt64(object?)"/>
    public static UInt64 ToUInt64<T>(this T i)
        where T : INumber<T> => Convert.ToUInt64(i);

    #endregion

    /// <see cref="INumber{}"/> | <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical(TSelf)"/>
    public static bool IsCanonical<T>(this T i)
        where T : INumber<T>
        => T.IsCanonical(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber(TSelf)"/>
    public static bool IsComplex<T>(this T i)
        where T : INumber<T>
        => T.IsComplexNumber(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsEvenInteger(TSelf)"/>
    public static bool IsEven<T>(this T i)
        where T : INumber<T>
        => T.IsEvenInteger(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsFinite(TSelf)"/>
    public static bool IsFinite<T>(this T i)
        where T : INumber<T>
        => T.IsFinite(i);

    /// <summary>Get if <b>i</b> > <b>j</b>.</summary>
    public static bool IsGreater<T>(this T i, T j)
        where T : INumber<T>
        => i < j;

    /// <summary>Get if <b>i</b> >= <b>j</b>.</summary>
    public static bool IsGreaterOrEqual<T>(this T i, T j)
        where T : INumber<T>
        => i <= j;

    /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber(TSelf)"/>
    public static bool IsImaginary<T>(this T i)
        where T : INumber<T>
        => T.IsImaginaryNumber(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity(TSelf)"/>
    public static bool IsInfinity<T>(this T i)
        where T : INumber<T>
        => T.IsInfinity(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsInteger(TSelf)"/>
    public static bool IsInteger<T>(this T i)
        where T : INumber<T>
        => T.IsInteger(i);

    /// <summary>Get if <b>i</b> &lt; <b>j</b>.</summary>
    public static bool IsLess<T>(this T i, T j)
        where T : INumber<T>
        => i < j;

    /// <summary>Get if <b>i</b> &lt;= <b>j</b>.</summary>
    public static bool IsLessOrEqual<T>(this T i, T j)
        where T : INumber<T>
        => i <= j;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNaN(TSelf)"/>
    public static bool IsNaN<T>(this T i)
        where T : INumber<T>
        => T.IsNaN(i);

    /// <summary>Get if <b>i</b> &lt; <see cref="INumberBase{TSelf}.Zero"/>.</summary>
    public static bool IsNegative<T>(this T i)
        where T : INumber<T>
        => i < T.Zero;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity(TSelf)"/>
    public static bool IsNegativeInfinity<T>(this T i)
        where T : INumber<T>
        => T.IsNegativeInfinity(i);

    /// <summary>Get if <b>i</b> = -<see cref="INumberBase{TSelf}.One"/>.</summary>
    public static bool IsNegativeOne<T>(this T i)
        where T : INumber<T>
        => i == -T.One;

    /// <summary>Get if <b>i</b> &lt;= <see cref="INumberBase{TSelf}.Zero"/>.</summary>
    public static bool IsNegativeOrZero<T>(this T i)
        where T : INumber<T>
        => i <= T.Zero;

    /// <inheritdoc cref="INumberBase{TSelf}.IsNormal(TSelf)"/>
    public static bool IsNormal<T>(this T i)
        where T : INumber<T>
        => T.IsNormal(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsOddInteger(TSelf)"/>
    public static bool IsOdd<T>(this T i)
        where T : INumber<T>
        => T.IsOddInteger(i);

    /// <summary>Get if <b>i</b> > <see cref="INumberBase{TSelf}.Zero"/>.</summary>
    public static bool IsPositive<T>(this T i)
        where T : INumber<T>
        => i > T.Zero;

    /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity(TSelf)"/>
    public static bool IsPositiveInfinity<T>(this T i)
        where T : INumber<T>
        => T.IsPositiveInfinity(i);

    /// <summary>Get if <b>i</b> = <see cref="INumberBase{TSelf}.One"/>.</summary>
    public static bool IsPositiveOne<T>(this T i)
        where T : INumber<T>
        => i == T.One;

    /// <summary>Get if <b>i</b> >= <see cref="INumberBase{TSelf}.Zero"/>.</summary>
    public static bool IsPositiveOrZero<T>(this T i)
        where T : INumber<T>
        => i >= T.Zero;

    /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber(TSelf)"/>
    public static bool IsReal<T>(this T i)
        where T : INumber<T>
        => T.IsRealNumber(i);

    /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal(TSelf)"/>
    public static bool IsSubnormal<T>(this T i)
        where T : INumber<T>
        => T.IsSubnormal(i);

    /// <summary>Get if <b>i</b> = <see cref="INumberBase{TSelf}.Zero"/>.</summary>
    public static bool IsZero<T>(this T i)
        where T : INumber<T> => i == T.Zero;

    #endregion

    /// <see cref="INumber{}"/> | <see cref="return"/> = <see cref="Double"/>
    #region

    /// <inheritdoc cref="Math.Acos(double)(double)"/>
    public static double Acos<T>(this T i) where T : INumber<T> => Math.Acos(i.ToDouble());

    /// <inheritdoc cref="Math.Asin(double)(double)"/>
    public static double Asin<T>(this T i) where T : INumber<T> => Math.Asin(i.ToDouble());

    /// <inheritdoc cref="Math.Atan(double)(double)"/>
    public static double Atan<T>(this T i) where T : INumber<T> => Math.Atan(i.ToDouble());

    /// <inheritdoc cref="Math.Atan2(double, double)"/>
    public static double Atan2<T>(this T i, T j) where T : INumber<T> => Math.Atan2(i.ToDouble(), j.ToDouble());

    /// <inheritdoc cref="Math.Cos(double)"/>
    public static double Cos<T>(this T i) where T : INumber<T> => Math.Cos(i.ToDouble());

    /// <inheritdoc cref="Math.Acos(double)(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DAcos<T>(this T i) where T : INumber<T> => double.RadiansToDegrees(Math.Acos(i.ToDouble()));

    /// <inheritdoc cref="Math.Asin(double)(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DAsin<T>(this T i) where T : INumber<T> => double.RadiansToDegrees(Math.Asin(i.ToDouble()));

    /// <inheritdoc cref="Math.Atan(double)(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DAtan<T>(this T i) where T : INumber<T> => double.RadiansToDegrees(Math.Atan(i.ToDouble()));

    /// <inheritdoc cref="Math.Atan2(double, double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DAtan2<T>(this T i, T j) where T : INumber<T> => double.RadiansToDegrees(Math.Atan2(i.ToDouble(), j.ToDouble()));

    /// <inheritdoc cref="Math.Cos(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DCos<T>(this T i) where T : INumber<T> => Math.Cos(double.DegreesToRadians(i.ToDouble()));

    /// <inheritdoc cref="Math.Sin(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DSin<T>(this T i) where T : INumber<T> => Math.Sin(double.DegreesToRadians(i.ToDouble()));

    /// <inheritdoc cref="Math.SinCos(double)"/>
    public static (double DSin, double DCos) DSinCos<T>(this T i) where T : INumber<T> => Math.SinCos(double.DegreesToRadians(i.ToDouble()));

    /// <inheritdoc cref="Math.Tan(double)"/>
    /// <remarks><b>In degrees.</b></remarks>
    public static double DTan<T>(this T i) where T : INumber<T> => Math.Tan(double.DegreesToRadians(i.ToDouble()));

    /// <inheritdoc cref="Math.Sin(double)"/>
    public static double Sin<T>(this T i) where T : INumber<T> => Math.Sin(i.ToDouble());

    /// <inheritdoc cref="Math.SinCos(double)"/>
    public static (double Sin, double Cos) SinCos<T>(this T i) where T : INumber<T> => Math.SinCos(i.ToDouble());

    /// <inheritdoc cref="Math.Tan(double)"/>
    public static double Tan<T>(this T i) where T : INumber<T> => Math.Tan(i.ToDouble());

    #endregion

    /// <see cref="INumber{}"/> | <see cref="IFloatingPoint{}"/>
    #region

    /// <summary>
    /// Get <b>j</b> if <b>i</b> is a number.
    /// </summary>
    /// <remarks>When <see cref="INumberBase{TSelf}.IsNaN(TSelf)"/> = <see langword="false"/>.</remarks>
    public static T IaN<T>(this T i, T j) where T : IFloatingPoint<T>, INumber<T> => !i.IsNaN() ? j : i;

    /// <summary>
    /// Get <b>j</b> if <b>i</b> isn't a number.
    /// </summary>
    /// <remarks>When <see cref="INumberBase{TSelf}.IsNaN(TSelf)"/> = <see langword="true"/>.</remarks>
    public static T NaN<T>(this T i, T j) where T : IFloatingPoint<T>, INumber<T> =>  i.IsNaN() ? j : i;

    /// <summary>
    /// Get <b>j</b>√<b>i</b> (or <b>i</b>^1/<b>j</b>).
    /// </summary>
    public static T Root<T>(this T i, T j) where T : IFloatingPoint<T>, INumber<T> => i.ToDouble().Pow(1D / j.ToDouble()).Create<T>();

    /// <summary>
    /// Get 2√<b>i</b> (or <b>i</b>^½).
    /// </summary>
    public static T Root2<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(2.Create<T>());

    /// <summary>
    /// Get 3√<b>i</b> (or <b>i</b>^1/3).
    /// </summary>
    public static T Root3<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(3.Create<T>());

    /// <summary>
    /// Get 4√<b>i</b> (or <b>i</b>^¼).
    /// </summary>
    public static T Root4<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(4.Create<T>());

    /// <summary>
    /// Get 5√<b>i</b> (or <b>i</b>^1/5).
    /// </summary>
    public static T Root5<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(5.Create<T>());

    /// <summary>
    /// Get 6√<b>i</b> (or <b>i</b>^1/6).
    /// </summary>
    public static T Root6<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(6.Create<T>());

    /// <summary>
    /// Get 7√<b>i</b> (or <b>i</b>^1/7).
    /// </summary>
    public static T Root7<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(7.Create<T>());

    /// <summary>
    /// Get 8√<b>i</b> (or <b>i</b>^1/8).
    /// </summary>
    public static T Root8<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(8.Create<T>());

    /// <summary>
    /// Get 9√<b>i</b> (or <b>i</b>^1/9).
    /// </summary>
    public static T Root9<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(9.Create<T>());

    /// <summary>
    /// Get 10√<b>i</b> (or <b>i</b>^1/10).
    /// </summary>
    public static T Root10<T>(this T i) where T : IFloatingPoint<T>, INumber<T> => i.Root(10.Create<T>());

    /// <summary>
    /// Round to given digits.
    /// </summary>
    public static T Round<T>(this T i, int digits = 0)
        where T : IFloatingPoint<T>, INumber<T> => T.Round(i, digits);

    /// <summary>
    /// Round with given <see cref="MidpointRounding"/>.
    /// </summary>
    public static T Round<T>(this T i, MidpointRounding mode)
        where T : IFloatingPoint<T>, INumber<T> => T.Round(i, mode);

    /// <summary>
    /// Round to given digits and with given <see cref="MidpointRounding"/>.
    /// </summary>
    public static T Round<T>(this T i, int digits, MidpointRounding mode)
        where T : IFloatingPoint<T>, INumber<T> => T.Round(i, digits, mode);

    /// <summary>
    /// Round down to the nearest number.
    /// </summary>
    public static T Round0<T>(this T i)
        where T : IFloatingPoint<T>, INumber<T> => T.Floor(i);

    /// <summary>
    /// Round up to the nearest number.
    /// </summary>
    public static T Round1<T>(this T i)
        where T : IFloatingPoint<T>, INumber<T> => T.Ceiling(i);

    /// <summary>
    /// Shift decimal by given times.
    /// <para><b>Times (-)</b></para>
    /// <list type="bullet">
    /// <item>12.3 ⇒ 1.23 ⇒ 0.123</item>
    /// </list>
    /// <para><b>Times (+)</b></para>
    /// <list type="bullet">
    /// <item>1.23 ⇒ 12.3 ⇒ 123.0</item>
    /// </list>
    /// </summary>
    public static T Shift<T>(this T i, int times = 1)
        where T : IFloatingPoint<T>, INumber<T>
    {
        T result = i, k = 10.Create<T>();
        Array1D.Do(times.Abs(), j => result = times.IsNegative() ? result / k : result * k);
        return result;
    }

    /// <summary>
    /// Truncate. 
    /// <para><b>Example</b></para>
    /// <list type="bullet">
    /// <item>1.23 ⇒ 1.0</item>
    /// </list>
    /// </summary>
    public static T Truncate<T>(this T i)
        where T : IFloatingPoint<T>, INumber<T> => T.Truncate(i);

    #endregion

    /// <see cref="INumber{}"/> | <see cref="IMinMaxValue{}"/> | <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>
    /// Clamp to given range of [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>maximum</b>]. 
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b><see cref="maximum"/></b>]</remarks>
    public static T Clamp<T>(this T i, T maximum)
        where T : IMinMaxValue<T>, INumber<T>
        => i.Clamp(T.MinValue, maximum);

    /// <summary>
    /// Clamp to given range of [<b><see cref="minimum"/></b>, <b><see cref="maximum"/></b>]. 
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<b><see cref="minimum"/></b>, <b><see cref="maximum"/></b>]</remarks>
    public static T Clamp<T>(this T i, T minimum, T maximum)
        where T : IMinMaxValue<T>, INumber<T>
        => T.Min(T.Max(i, minimum), maximum);

    /// <summary>
    /// Clamp to given range of [<b><see cref="range"/></b>]. 
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<b><see cref="range"/></b>]</remarks>
    public static T Clamp<T>(this T i, (T minimum, T maximum) range)
        where T : IMinMaxValue<T>, INumber<T>
        => i.Clamp(range.minimum, range.maximum);

    /// <summary>
    /// Clamp to given range of [<see cref="IRange"/>]. 
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<see cref="IRange"/>]</remarks>
    public static T Clamp<T>(this T i, IRange<T> range)
        where T : IMinMaxValue<T>, INumber<T>
        => i.Clamp(range.Minimum, range.Maximum);

    /// <summary>
    /// Get from <see langword="default"/> range in range with given <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(T.MinValue, newMaximum);

    /// <summary>
    /// Get from <see langword="default"/> range in range with given <b>newMinimum</b> and <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<b>newMinimum</b>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, T newMinimum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(T.MinValue, T.MaxValue, newMinimum, newMaximum);

    /// <summary>
    /// Get from range with given <b>oldMinimum</b> and <b>oldMaximum</b> in range with given <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldMinimum</b>, <b>oldMaximum</b>] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, T oldMinimum, T oldMaximum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldMinimum, oldMaximum, T.MinValue, newMaximum);

    /// <summary>
    /// Get from range with given <b>oldMinimum</b> and <b>oldMaximum</b> in range with given <b>newMinimum</b> and <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldMinimum</b>, <b>oldMaximum</b>] ⇒ [<b>newMinimum</b>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, T oldMinimum, T oldMaximum, T newMinimum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldMinimum, oldMaximum, (newMinimum, newMaximum));

    /// <summary>
    /// Get from range with given <b>oldMinimum</b> and <b>oldMaximum</b> in given <b>newRange</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldMinimum</b>, <b>oldMaximum</b>] ⇒ [<b>newRange</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, T oldMinimum, T oldMaximum, (T Minimum, T Maximum) newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldMinimum, oldMaximum, new Range<T>(newRange));

    /// <summary>
    /// Get from range with given <b>oldMinimum</b> and <b>oldMaximum</b> in given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<b>oldMinimum</b>, <b>oldMaximum</b>] ⇒ [<see cref="IRange"/>]
    /// </remarks>
    public static T ToRange<T>(this T i, T oldMinimum, T oldMaximum, IRange<T> newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(new Range<T>(oldMinimum, oldMaximum), newRange);

    /// <summary>
    /// Get from <see langword="default"/> range in given <b>newRange</b>.
    /// </summary>
    /// <remarks>
    /// [<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<b>newRange</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, (T Minimum, T Maximum) newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(T.MinValue, T.MaxValue, newRange.Minimum, newRange.Maximum);

    /// <summary>
    /// Get from given <b>oldRange</b> in range with given <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldRange</b>] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, (T Minimum, T Maximum) oldRange, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, T.MinValue, newMaximum);

    /// <summary>
    /// Get from given <b>oldRange</b> in range with given <b>newMinimum</b> and <b>newMaximum</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldRange</b>] ⇒ [<b>newMinimum</b>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, (T Minimum, T Maximum) oldRange, T newMinimum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, (newMinimum, newMaximum));

    /// <summary>
    /// Get from given <b>oldRange</b> in given <b>newRange</b>.
    /// </summary>
    /// <remarks>
    /// [<b>oldRange</b>] ⇒ [<b>newRange</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, (T Minimum, T Maximum) oldRange, (T Minimum, T Maximum) newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(new Range<T>(oldRange), new Range<T>(newRange));

    /// <summary>
    /// Get from given <b>oldRange</b> in given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<b>oldRange</b>] ⇒ [<see cref="IRange"/>]
    /// </remarks>
    public static T ToRange<T>(this T i, (T Minimum, T Maximum) oldRange, IRange<T> newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(new Range<T>(oldRange), newRange);

    /// <summary>
    /// Get from <see langword="default"/> range in given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<see cref="IMinMaxValue{TSelf}"/>] ⇒ [<see cref="IRange"/>]
    /// </remarks>
    public static T ToRange<T>(this T i, IRange<T> newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(new Range<T>(), newRange);

    /// <summary>
    /// Get from given <see cref="IRange"/> in range with given <b>newMaximum</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<see cref="IRange"/>] ⇒ [<see cref="IMinMaxValue{TSelf}"/>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, IRange<T> oldRange, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, T.MinValue, newMaximum);

    /// <summary>
    /// Get from given <see cref="IRange"/> in range with given <b>newMinimum</b> and <b>newMaximum</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<see cref="IRange"/>] ⇒ [<b>newMinimum</b>, <b>newMaximum</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, IRange<T> oldRange, T newMinimum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, (newMinimum, newMaximum));

    /// <summary>
    /// Get from given <see cref="IRange"/> in given <b>newRange</b>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<see cref="IRange"/>] ⇒ [<b>newRange</b>]
    /// </remarks>
    public static T ToRange<T>(this T i, IRange<T> oldRange, (T Minimum, T Maximum) newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, new Range<T>(newRange));

    /// <summary>
    /// Get from given <see cref="IRange"/> in another given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>
    /// [<see cref="IRange"/>] ⇒ [<see cref="IRange"/>]
    /// </remarks>
    public static T ToRange<T>(this T i, IRange<T> oldRange, IRange<T> newRange)
        where T : IMinMaxValue<T>, INumber<T>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(oldRange, nameof(oldRange));
        Throw.IfNull(newRange, nameof(newRange));
        var n = (newRange.Maximum - newRange.Minimum) / (oldRange.Maximum - oldRange.Minimum);
        return newRange.Minimum + (i - oldRange.Minimum) * n;
    }

    #endregion

    /// <see cref="INumber{}"/> | <see cref="IMinMaxValue{}"/> | <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <summary>
    /// Get if in range of [<see cref="INumberBase{TSelf}.Zero"/>, <see cref="INumberBase{TSelf}.One"/>].
    /// </summary>
    public static bool IsNormalized<T>(this T i)
        where T : IMinMaxValue<T>, INumber<T>
        => i.OfRange(T.Zero, T.One);

    /// <summary>
    /// Get if <see cref="INumberBase{TSelf}.Zero"/> ≥ <b>i</b> ≤ <b>maximum</b>.
    /// </summary>
    public static bool OfRange<T>(this T i, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.OfRange(T.MinValue, maximum);

    /// <summary>
    /// Get if <b>minimum</b> ≥ <b>i</b> ≤ <b>maximum</b>.
    /// </summary>
    public static bool OfRange<T>(this T i, T minimum, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i >= minimum && i <= maximum;

    /// <summary>
    /// Get if <see cref="IRange.Minimum"/> ≥ <b>i</b> ≤ <see cref="IRange.Maximum"/>.
    /// </summary>
    public static bool OfRange<T>(this T i, IRange<T> range)
        where T : IMinMaxValue<T>, INumber<T> => i.OfRange(range.Minimum, range.Maximum);

    #endregion

    /// <see cref="INumber{}"/> | <see cref="IMinMaxValue{}"/> | <see cref="return"/> = <see cref="Double1"/>
    #region

    /// <summary>
    /// Get from range of [0, 1] in <see langword="default"/> range.
    /// </summary>
    /// <remarks>[0, 1] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <see cref="IMinMaxValue{TSelf}.MaxValue"/>]</remarks>
    [Refactor("Replace 'double' with 'Double1'.")]
    public static T Denormalize<T>(this double i)
        where T : IMinMaxValue<T>, INumber<T> => new Double1(i).Denormalize(T.MaxValue);

    /// <summary>
    /// Get from range of [0, 1] in range with given <b>maximum</b>.
    /// </summary>
    /// <remarks>[0, 1] ⇒ [<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>maximum</b>]</remarks>
    public static T Denormalize<T>(this Double1 i, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.Denormalize(T.MinValue, maximum);

    /// <summary>
    /// Get from range of [0, 1] in range with given <b>minimum</b> and <b>maximum</b>.
    /// </summary>
    /// <remarks>[0, 1] ⇒ [<b>minimum</b>, <b>maximum</b>]</remarks>
    public static T Denormalize<T>(this Double1 i, T minimum, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.Denormalize((minimum, maximum));

    /// <summary>
    /// Get from range of [0, 1] in given <b>range</b>.
    /// </summary>
    /// <remarks>[0, 1] ⇒ [<b>range</b>]</remarks>
    public static T Denormalize<T>(this Double1 i, (T Minimum, T Maximum) range)
        where T : IMinMaxValue<T>, INumber<T> => i.Denormalize(new Range<T>(range));

    /// <summary>
    /// Get from range of [0, 1] in given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>[0, 1] ⇒ [<see cref="IRange"/>]</remarks>
    public static T Denormalize<T>(this Double1 i, IRange<T> range)
        where T : IMinMaxValue<T>, INumber<T>
    {
        Throw.IfNull(range, nameof(range));
        return i.ToDouble().ToRange(0, 1, range.Minimum.ToDouble(), range.Maximum.ToDouble()).Create<T>();
    }

    /// <summary>
    /// Get in range of [0, 1] from <see langword="default"/> range.
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}.MinValue"/>, <see cref="IMinMaxValue{TSelf}.MaxValue"/>] ⇒ [0, 1]</remarks>
    public static Double1 Normalize<T>(this T i)
        where T : IMinMaxValue<T>, INumber<T> => i.Normalize(T.MaxValue);

    /// <summary>
    /// Get in range of [0, 1] from range with given <b>maximum</b>.
    /// </summary>
    /// <remarks>[<see cref="IMinMaxValue{TSelf}.MinValue"/>, <b>maximum</b>] ⇒ [0, 1]</remarks>
    public static Double1 Normalize<T>(this T i, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.Normalize(T.MinValue, maximum);

    /// <summary>
    /// Get in range of [0, 1] from range with given <b>minimum</b> and <b>maximum</b>.
    /// </summary>
    /// <remarks>[<b>minimum</b>, <b>maximum</b>] ⇒ [0, 1]</remarks>
    public static Double1 Normalize<T>(this T i, T minimum, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.Normalize((minimum, maximum));

    /// <summary>
    /// Get in range of [0, 1] from given <b>range</b>.
    /// </summary>
    /// <remarks>[<b>range</b>] ⇒ [0, 1]</remarks>
    public static Double1 Normalize<T>(this T i, (T Minimum, T Maximum) range)
        where T : IMinMaxValue<T>, INumber<T> => i.Normalize(range.Minimum, range.Maximum);

    /// <summary>
    /// Get in range of [0, 1] from given <see cref="IRange"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <remarks>[<see cref="IRange"/>] ⇒ [0, 1]</remarks>
    public static Double1 Normalize<T>(this T i, IRange<T> range)
        where T : IMinMaxValue<T>, INumber<T>
    {
        Throw.IfNull(range, nameof(range));
        return i.ToDouble().ToRange(range.Minimum.ToDouble(), range.Maximum.ToDouble(), 0, 1).To<Double1>();
    }

    #endregion

    /// <see cref="INumber{}"/> | <see cref="ISignedNumber{}"/>
    #region

    /// <summary>
    /// Get negated value (by multiplying by -1).
    /// <para><b>Examples</b></para>
    /// <list type="bullet">
    /// <item>-0 ⇒ +0</item>
    /// <item>+0 ⇒ -0</item>
    /// <item>-1 ⇒ +1</item>
    /// <item>+1 ⇒ -1</item>
    /// </list>
    /// </summary>
    public static T Negate<T>(this T i)
        where T : ISignedNumber<T> => -i;

    #endregion
}

[Extend(typeof(IArea<>))]
public static partial class XNumber
{
    public static T Bottom<T>(this IArea<T> i) 
        where T : INumber<T> => i.Y + i.Height - T.One;

    public static Vector2<T> BottomLeft<T>(this IArea<T> i)
        where T : INumber<T> => new(i.X, i.Y + i.Height);

    public static Vector2<T> BottomRight<T>(this IArea<T> i)
        where T : INumber<T> => new(i.X + i.Width, i.Y + i.Height);

    public static Vector2<T> Center<T>(this IArea<T> i)
        where T : INumber<T> => new(i.X + (i.Width / 2.Create<T>()), i.Y - (i.Height / 2.Create<T>()));

    public static bool Contains<T>(this IArea<T> i, Vector2<T> j) where T : INumber<T>
        => j.X >= i.X && j.X <= i.Right() && j.Y >= i.Y && j.Y <= i.Bottom();

    public static T Left<T>(this IArea<T> i) 
        where T : INumber<T> => i.X;

    public static T Right<T>(this IArea<T> i) 
        where T : INumber<T> => i.X + i.Width - T.One;

    public static T Top<T>(this IArea<T> i) 
        where T : INumber<T> => i.Y;

    public static Vector2<T> TopLeft<T>(this IArea<T> i)
        where T : INumber<T> => new(i.X, i.Y);

    public static Vector2<T> TopRight<T>(this IArea<T> i)
        where T : INumber<T> => new(i.X + i.Width, i.Y);

    public static Vector2<T> Position<T>(this IArea<T> i) 
        where T : INumber<T> => i.TopLeft();

    public static Size<T> Size<T>(this IArea<T> i) 
        where T : INumber<T> => (i.Height, i.Width);

    public static Rectangle ToRectangle<T>(this IArea<T> i) where T : INumber<T>
        => new(i.X.ToInt32(), i.Y.ToInt32(), i.Width.ToInt32(), i.Height.ToInt32());

    public static RectangleF ToRectangleF<T>(this IArea<T> i) where T : INumber<T>
        => new(i.X.ToInt32(), i.Y.ToInt32(), i.Width.ToInt32(), i.Height.ToInt32());
}

[Extend(typeof(IArea<,>))]
public static partial class XNumber
{
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Do<TSelf, TValue>(this IArea<TSelf, TValue> i, Operator @operator, in ISize<TValue> j)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
    {
        var q = i.Size().Do(@operator, j);
        return TSelf.Create((TSelf)i, i.Position(), q);
    }

    /// <exception cref="ArgumentNullException"/>
    public static TSelf Do<TSelf, TValue>(this IArea<TSelf, TValue> i, Operator @operator, in IVector2<TValue> j)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
    {
        var q = i.TopLeft().Do(@operator, j);
        return TSelf.Create((TSelf)i, q, i.Size());
    }

    /// <exception cref="ArgumentNullException"/>
    public static TSelf Grow<TSelf, TValue>(this IArea<TSelf, TValue> i, in TValue height, in TValue width)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.Do(Operator.Add, new Size<TValue>(height, width));

    /// <exception cref="ArgumentNullException"/>
    public static TSelf Grow<TSelf, TValue>(this IArea<TSelf, TValue> i, in ISize<TValue> size)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.Grow(size.Height, size.Width);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf GrowFixed<TSelf, TValue>(this IArea<TSelf, TValue> i, in TValue height, in TValue width)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => TSelf.Create((TSelf)i, new Vector2<TValue>(i.Left() - width, i.Top() - height), new Size<TValue>(i.Height + 2.Create<TValue>() * height, i.Width + 2.Create<TValue>() * width));

    /// <exception cref="ArgumentNullException"/>
    public static TSelf GrowFixed<TSelf, TValue>(this IArea<TSelf, TValue> i, ISize<TValue> size)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.GrowFixed(size.Height, size.Width);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf Shrink<TSelf, TValue>(this IArea<TSelf, TValue> i, in TValue height, in TValue width)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.Do(Operator.Subtract, new Size<TValue>(height, width));

    /// <exception cref="ArgumentNullException"/>
    public static TSelf Shrink<TSelf, TValue>(this IArea<TSelf, TValue> i, in ISize<TValue> size)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.Shrink(size.Height, size.Width);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf ShrinkFixed<TSelf, TValue>(this IArea<TSelf, TValue> i, in TValue height, in TValue width)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => TSelf.Create((TSelf)i, new Vector2<TValue>(i.Left() + width, i.Top() + height), new Size<TValue>(i.Height - 2.Create<TValue>() * height, i.Width - 2.Create<TValue>() * width));

    /// <exception cref="ArgumentNullException"/>
    public static TSelf ShrinkFixed<TSelf, TValue>(this IArea<TSelf, TValue> i, in ISize<TValue> size)
        where TSelf : IArea<TSelf, TValue> where TValue : INumber<TValue>
        => i.ShrinkFixed(size.Height, size.Width);
}

[Extend(typeof(IArray<,>))]
public static partial class XNumber
{
    /// <see cref="INumber{}"/>
    #region

    /// <inheritdoc cref="Abs{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Abs<TSelf, TValue>(this IArray<TSelf, TValue> i)
        where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(TValue.Abs);

    /// <inheritdoc cref="Do{}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Do<TSelf, TValue>(this IArray<TSelf, TValue> i, Operator @operator, TValue j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>
        => i.New((x, y) => y.Do(@operator, j));

    /// <inheritdoc cref="Do{}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Do<TSelf, TValue>(this IArray<TSelf, TValue> i, Operator @operator, IArray1D<TValue> j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>
    {
        Throw.If<ArrayLengthMismatch>(i.Length > j.Length, nameof(j));
        return i.New((x, y) => y.Do(@operator, j[x]));
    }

    /// <inheritdoc cref="Divide{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>
        => i.New((x, y) => y.Do(Operator.Divide, j));

    /// <inheritdoc cref="Divide2{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide2<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide2);

    /// <inheritdoc cref="Divide3{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide3<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide3);

    /// <inheritdoc cref="Divide4{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide4<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide4);

    /// <inheritdoc cref="Divide5{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide5<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide5);

    /// <inheritdoc cref="Divide6{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide6<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide6);

    /// <inheritdoc cref="Divide7{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide7<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide7);

    /// <inheritdoc cref="Divide8{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide8<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide8);

    /// <inheritdoc cref="Divide9{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide9<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide9);

    /// <inheritdoc cref="Divide10{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Divide10<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Divide10);

    /// <inheritdoc cref="Modulo{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>
        => i.New(x => x.Modulo(j));

    /// <inheritdoc cref="Modulo2{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo2<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo2);

    /// <inheritdoc cref="Modulo3{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo3<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo3);

    /// <inheritdoc cref="Modulo4{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo4<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo4);

    /// <inheritdoc cref="Modulo5{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo5<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo5);

    /// <inheritdoc cref="Modulo6{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo6<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo6);

    /// <inheritdoc cref="Modulo7{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo7<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo7);

    /// <inheritdoc cref="Modulo8{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo8<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo8);

    /// <inheritdoc cref="Modulo9{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo9<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo9);

    /// <inheritdoc cref="Modulo10{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo10<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Modulo10);

    /// <inheritdoc cref="Multiply{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>
        => i.New(x => x.Multiply(j));

    /// <inheritdoc cref="Multiply2{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply2<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply2);

    /// <inheritdoc cref="Multiply3{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply3<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply3);

    /// <inheritdoc cref="Multiply4{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply4<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply4);

    /// <inheritdoc cref="Multiply5{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply5<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply5);

    /// <inheritdoc cref="Multiply6{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply6<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply6);

    /// <inheritdoc cref="Multiply7{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply7<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply7);

    /// <inheritdoc cref="Multiply8{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply8<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply8);

    /// <inheritdoc cref="Multiply9{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply9<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply9);

    /// <inheritdoc cref="Multiply10{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply10<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue>, IMatrixImmutable where TValue : INumber<TValue> => i.New(XNumber.Multiply10);

    /// <inheritdoc cref="Nearest{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue multiple)
        where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(j => j.Nearest(multiple));

    /// <inheritdoc cref="Pow{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IArray<TSelf, TValue> i, double j) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New((x, y) => TValue.CreateSaturating(Math.Pow(Convert.ToDouble(y), j)));

    /// <inheritdoc cref="Pow2{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow2<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow2);

    /// <inheritdoc cref="Pow3{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow3<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow3);

    /// <inheritdoc cref="Pow4{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow4<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow4);

    /// <inheritdoc cref="Pow5{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow5<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow5);

    /// <inheritdoc cref="Pow6{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow6<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow6);

    /// <inheritdoc cref="Pow7{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow7<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow7);

    /// <inheritdoc cref="Pow8{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow8<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow8);

    /// <inheritdoc cref="Pow9{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow9<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow9);

    /// <inheritdoc cref="Pow10{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow10<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue> => i.New(XNumber.Pow10);

    #endregion

    /// <see cref="INumber{}"/> | <see cref="return"/> = <see cref="Double"/>
    #region

    /// <inheritdoc cref="Acos{}(TValue)"/>
    public static TSelf Acos<TSelf>(this IArray<TSelf, double> i)
        where TSelf : IArray<TSelf, double> => i.New(Acos);

    /// <inheritdoc cref="Asin{}(TValue)"/>
    public static TSelf Asin<TSelf>(this IArray<TSelf, double> i)
        where TSelf : IArray<TSelf, double> => i.New(Asin);

    /// <inheritdoc cref="Atan{}(TValue)"/>
    public static TSelf Atan<TSelf>(this IArray<TSelf, double> i)
        where TSelf : IArray<TSelf, double> => i.New(Atan);

    /// <inheritdoc cref="Atan2{}(TValue)"/>
    public static TSelf Atan2<TSelf>(this IArray<TSelf, double> i, double j)
        where TSelf : IArray<TSelf, double> => i.New(x => x.Atan2(j));

    /// <inheritdoc cref="Cos{}(TValue)"/>
    public static TSelf Cos<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(Cos);

    /// <inheritdoc cref="DAcos{}(TValue)"/>
    public static TSelf DAcos<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DAcos);

    /// <inheritdoc cref="DAsin{}(TValue)"/>
    public static TSelf DAsin<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DAsin);

    /// <inheritdoc cref="DAtan{}(TValue)"/>
    public static TSelf DAtan<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DAtan);

    /// <inheritdoc cref="DAtan2{}(TValue)"/>
    public static TSelf DAtan2<TSelf>(this IArray<TSelf, double> i, double j) where TSelf : IArray<TSelf, double> => i.New(x => x.DAtan2(j));

    /// <inheritdoc cref="DCos{}(TValue)"/>
    public static TSelf DCos<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DCos);

    /// <inheritdoc cref="DSin{}(TValue)"/>
    public static TSelf DSin<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DSin);

    /// <inheritdoc cref="DTan{}(TValue)"/>
    public static TSelf DTan<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(DTan);

    /// <inheritdoc cref="Sin{}(TValue)"/>
    public static TSelf Sin<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(Sin);

    /// <inheritdoc cref="Tan{}(TValue)"/>
    public static TSelf Tan<TSelf>(this IArray<TSelf, double> i) where TSelf : IArray<TSelf, double> => i.New(Tan);

    #endregion

    /// <see cref="IFloatingPoint{}"/>
    #region

    /// <inheritdoc cref="IaN{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(x => x.IaN(j));

    /// <inheritdoc cref="NaN{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(x => x.NaN(j));

    /// <inheritdoc cref="Root{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue j) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(value => value.Root(j));

    /// <inheritdoc cref="Root2{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root2<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root2);

    /// <inheritdoc cref="Root3{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root3<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root3);

    /// <inheritdoc cref="Root4{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root4<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root4);

    /// <inheritdoc cref="Root5{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root5<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root5);

    /// <inheritdoc cref="Root6{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root6<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root6);

    /// <inheritdoc cref="Root7{}(TValue)"/>
    public static TSelf Root7<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root7);

    /// <inheritdoc cref="Root8{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root8<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root8);

    /// <inheritdoc cref="Root9{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root9<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root9);

    /// <inheritdoc cref="Root10{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root10<TSelf, TValue>(this IArray<TSelf, TValue> i) where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(XNumber.Root10);

    /// <inheritdoc cref="Round{}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IArray<TSelf, TValue> i, int digits = 0)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(j => j.Round(digits));

    /// <inheritdoc cref="Round{}(TValue, MidpointRounding)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IArray<TSelf, TValue> i, MidpointRounding mode)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(j => j.Round(mode));

    /// <inheritdoc cref="Round{}(TValue, int, MidpointRounding)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IArray<TSelf, TValue> i, int digits, MidpointRounding mode)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => i.New(j => j.Round(digits, mode));

    /// <inheritdoc cref="Round0{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round0<TSelf, TValue>(this IArray<TSelf, TValue> a)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => a.New(TValue.Floor);

    /// <inheritdoc cref="Round1{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round1<TSelf, TValue>(this IArray<TSelf, TValue> a)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => a.New(TValue.Ceiling);

    /// <inheritdoc cref="Shift{}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Shift<TSelf, TValue>(this IArray<TSelf, TValue> a, int times = 1)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => a.New(j => j.Shift(times));

    /// <inheritdoc cref="Truncate{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Truncate<TSelf, TValue>(this IArray<TSelf, TValue> a)
        where TSelf : IArray<TSelf, TValue> where TValue : IFloatingPoint<TValue>, INumber<TValue> => a.New(TValue.Truncate);

    #endregion

    /// <see cref="IMinMaxValue{}"/>
    #region

    /// <inheritdoc cref="Clamp{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue maximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="Clamp{}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue minimum, TValue maximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        return i.New(j => j.Clamp(minimum, maximum));

    }

    /// <inheritdoc cref="Clamp{}(TValue, IRange{})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> range)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue>
    {
        Throw.IfNull(range, nameof(range));
        return i.Clamp(range.Minimum, range.Maximum);
    }

    /// <inheritdoc cref="ToRange{}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue newMinimum, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(newMinimum, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue oldMinimum, TValue oldMaximum, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldMinimum, oldMaximum, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, TValue, TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue oldMinimum, TValue oldMaximum, TValue newMinimum, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldMinimum, oldMaximum, newMinimum, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, TValue, TValue, ValueTuple{TValue, TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue oldMinimum, TValue oldMaximum, (TValue Minimum, TValue Maximum) newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldMinimum, oldMaximum, newRange));

    /// <inheritdoc cref="ToRange{}(TValue, TValue, TValue, IRange{})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, TValue oldMinimum, TValue oldMaximum, IRange<TValue> newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldMinimum, oldMaximum, newRange));

    /// <inheritdoc cref="ToRange{}(TValue, ValueTuple{TValue, TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, (TValue Minimum, TValue Maximum) newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(newRange));

    /// <inheritdoc cref="ToRange{}(TValue, ValueTuple{TValue, TValue}, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, (TValue Minimum, TValue Maximum) oldRange, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, ValueTuple{TValue, TValue}, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, (TValue Minimum, TValue Maximum) oldRange, TValue newMinimum, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newMinimum, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, ValueTuple{TValue, TValue}, ValueTuple{TValue, TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, (TValue Minimum, TValue Maximum) oldRange, (TValue Minimum, TValue Maximum) newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newRange));

    /// <inheritdoc cref="ToRange{}(TValue, ValueTuple{TValue, TValue}, IRange{})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, (TValue Minimum, TValue Maximum) oldRange, IRange<TValue> newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newRange));

    /// <inheritdoc cref="ToRange{}(TValue, IRange{})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(newRange));

    /// <inheritdoc cref="ToRange{}(TValue, IRange{}, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> oldRange, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, IRange{}, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> oldRange, TValue newMinimum, TValue newMaximum)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newMinimum, newMaximum));

    /// <inheritdoc cref="ToRange{}(TValue, IRange{}, ValueTuple{TValue, TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> oldRange, (TValue Minimum, TValue Maximum) newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newRange));

    /// <inheritdoc cref="ToRange{}(TValue, IRange{}, IRange{})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IArray<TSelf, TValue> i, IRange<TValue> oldRange, IRange<TValue> newRange)
        where TSelf : IArray<TSelf, TValue> where TValue : IMinMaxValue<TValue>, INumber<TValue> => i.New(j => j.ToRange(oldRange, newRange));

    #endregion

    /// <see cref="ISignedNumber{}"/>
    #region

    /// <inheritdoc cref="Negate{}(TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Negate<TSelf, TValue>(this IArray<TSelf, TValue> i)
        where TSelf : IArray<TSelf, TValue> where TValue : INumber<TValue>, ISignedNumber<TValue> => i.New(XNumber.Negate);

    #endregion
}

[Extend(typeof(IEnumerable<>))]
public static partial class XNumber
{
    /// <see cref="return"/> = <see cref="T"/>
    #region

    /// <summary>Get <see cref="NumberAggregation.Mean"/>.</summary>
    public static T GetMean<T>(this IEnumerable<T> i)
        where T : INumber<T>
    {
        T index = default, result = default;
        foreach (var j in i)
        {
            result += j;
            index++;
        }
        return result / index;
    }

    /// <summary>Get <see cref="NumberAggregation.Median"/>.</summary>
    public static T GetMedian<T>(this IEnumerable<T> i)
        where T : IFloatingPoint<T>, INumber<T>
    {
        var j = i.OrderBy(x => x).ToArray();

        int size = j.Length, mid = size / 2;
        if (size % 2 != 0)
            return j[mid];

        return (j[mid] + j[mid - 1]).Divide2();
    }

    /// <summary>Get <see cref="NumberAggregation.Mode"/>.</summary>
    public static T GetMode<T>(this IEnumerable<T> i)
        where T : INumber<T>
    {
#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection (algorithm requires n^2?)
        if (i.GroupBy(j => j).OrderByDescending(j => j.Count()).ThenBy(j => j.Key).FirstOrDefault() is IGrouping<T, T> group)
            return group.Key;

        return i.First<T>();
#pragma warning restore CA1851
    }

    /// <summary>Get <see cref="NumberAggregation.Std"/>.</summary>
    public static T GetStd<T>(this IEnumerable<T> i)
        where T : IFloatingPoint<T>, INumber<T>
        => i.GetVariance().Root2();

    /// <summary>Get <see cref="NumberAggregation.Sum"/>.</summary>
    public static T GetSum<T>(this IEnumerable<T> i)
        where T : INumber<T>
    {
        T result = T.Zero;
        i.ForEach(j => result += j);
        return result;
    }

    /// <summary>Get <see cref="NumberAggregation.Variance"/>.</summary>
    public static T GetVariance<T>(this IEnumerable<T> i)
        where T : IFloatingPoint<T>, INumber<T>
    {
        T mean = i.GetMean(), variance = default;

        var index = 0;
        foreach (var j in i)
        {
            var a = j - mean;
            var b = a.Pow2();
            variance += b;
            index++;
        }
        return variance / index.Create<T>();
    }

    #endregion

    /// <see cref="return"/> = <see cref="Boolean"/>
    #region

    /// <inheritdoc cref="IsCanonical{}(T)"/>
    public static bool IsCanonical<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsCanonical);

    /// <inheritdoc cref="IsComplex{}(T)"/>
    public static bool IsComplex<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsComplex);

    /// <inheritdoc cref="IsEven{}(T)"/>
    public static bool IsEven<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsEven);

    /// <inheritdoc cref="IsFinite{}(T)"/>
    public static bool IsFinite<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsFinite);

    /// <inheritdoc cref="IsImaginary{}(T)"/>
    public static bool IsImaginary<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsImaginary);

    /// <inheritdoc cref="IsInfinity{}(T)"/>
    public static bool IsInfinity<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsInfinity);

    /// <inheritdoc cref="IsInteger{}(T)"/>
    public static bool IsInteger<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsInteger);

    /// <inheritdoc cref="IsNaN{}(T)"/>
    public static bool IsNaN<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNaN);

    /// <inheritdoc cref="IsNegative{}(T)"/>
    public static bool IsNegative<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNegative);

    /// <inheritdoc cref="IsNegativeInfinity{}(T)"/>
    public static bool IsNegativeInfinity<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNegativeInfinity);

    /// <inheritdoc cref="IsNegativeOne{}(T)"/>
    public static bool IsNegativeOne<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNegativeOne);

    /// <inheritdoc cref="IsNegativeOrZero{}(T)"/>
    public static bool IsNegativeOrZero<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNegativeOrZero);

    /// <inheritdoc cref="IsNormal{}(T)"/>
    public static bool IsNormal<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsNormal);

    /// <inheritdoc cref="IsOdd{}(T)"/>
    public static bool IsOdd<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsOdd);

    /// <inheritdoc cref="IsPositive{}(T)"/>
    public static bool IsPositive<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsPositive);

    /// <inheritdoc cref="IsPositiveInfinity{}(T)"/>
    public static bool IsPositiveInfinity<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsPositiveInfinity);

    /// <inheritdoc cref="IsPositiveOne{}(T)"/>
    public static bool IsPositiveOne<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsPositiveOne);

    /// <inheritdoc cref="IsPositiveOrZero{}(T)"/>
    public static bool IsPositiveOrZero<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsPositiveOrZero);

    /// <inheritdoc cref="IsReal{}(T)"/>
    public static bool IsReal<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsReal);

    /// <inheritdoc cref="IsSubnormal{}(T)"/>
    public static bool IsSubnormal<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsSubnormal);

    /// <inheritdoc cref="IsZero{}(T)"/>
    public static bool IsZero<T>(this IEnumerable<T> i)
        where T : INumber<T>
        => i.All(IsZero);

    #endregion

    /// <see cref="return"/> = <see cref="Boolean"/> | <see cref="IMinMaxValue{}"/>
    #region

    /// <inheritdoc cref="IsNormalized{}(T)"/>
    public static bool IsNormalized<T>(this IEnumerable<T> i)
        where T : IMinMaxValue<T>, INumber<T> => i.All(IsNormalized);

    /// <inheritdoc cref="OfRange{}(T, T)"/>
    public static bool OfRange<T>(this IEnumerable<T> i, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.All(j => j.OfRange(maximum));

    /// <inheritdoc cref="OfRange{}(T, T, T)"/>
    public static bool OfRange<T>(this IEnumerable<T> i, T minimum, T maximum)
        where T : IMinMaxValue<T>, INumber<T> => i.All(j => j.OfRange(minimum, maximum));

    /// <inheritdoc cref="OfRange{}(T, IRange{})"/>
    public static bool OfRange<T>(this IEnumerable<T> i, IRange<T> range)
        where T : IMinMaxValue<T>, INumber<T> => i.All(j => j.OfRange(range));

    #endregion
}

[Extend(typeof(IRange<>))]
public static partial class XNumber
{
    /// <inheritdoc cref="Denormalize{}(Double1, IRange{})"/>
    public static T Denormalize<T>(this IRange<T> i, Double1 value)
        where T : IMinMaxValue<T>, INumber<T> => value.Denormalize(i);

    /// <inheritdoc cref="Normalize{}(T, IRange{})"/>
    public static Double1 Normalize<T>(this IRange<T> i, in T value) 
        where T : IMinMaxValue<T>, INumber<T> => value.Normalize(i);

    /// <inheritdoc cref="ToRange{}(T, IRange{}, T)"/>
    public static T ToRange<T>(this IRange<T> oldRange, T i, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange.Minimum, oldRange.Maximum, T.MinValue, newMaximum);

    /// <inheritdoc cref="ToRange{}(T, IRange{}, T, T)"/>
    public static T ToRange<T>(this IRange<T> oldRange, T i, T newMinimum, T newMaximum)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange.Minimum, oldRange.Maximum, newMinimum, newMaximum);

    /// <inheritdoc cref="ToRange{}(T, IRange{}, ValueTuple{T, T})"/>
    public static T ToRange<T>(this IRange<T> oldRange, T i, (T Minimum, T Maximum) newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange((oldRange.Minimum, oldRange.Maximum), newRange);

    /// <inheritdoc cref="ToRange{}(T, IRange{}, IRange{})"/>
    public static T ToRange<T>(this IRange<T> oldRange, T i, IRange<T> newRange)
        where T : IMinMaxValue<T>, INumber<T> => i.ToRange(oldRange, newRange);
}

[Extend(typeof(ISize<>))]
public static partial class XNumber
{
    /// <summary>
    /// Get new instance of <see cref="ISize"/> with given <see cref="ISize.Height"/> (based on current dimensions).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf SetHeight<TSelf, TValue>(this ISize<TSelf, TValue> i, in TValue value) 
        where TSelf : ISize<TSelf, TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));

        TValue oldHeight = i.Height, oldWidth = i.Width, newHeight, newWidth;
        newHeight = value;

        newWidth = newHeight / oldHeight / oldWidth;
        return TSelf.Create(newHeight, newWidth);
    }

    /// <summary>
    /// Get new instance of <see cref="ISize"/> with given <see cref="ISize.Width"/> (based on current dimensions).
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf SetWidth<TSelf, TValue>(this ISize<TSelf, TValue> i, in TValue value) 
        where TSelf : ISize<TSelf, TValue> where TValue : INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));

        TValue oldHeight = i.Height, oldWidth = i.Width, newHeight, newWidth;
        newWidth = value;

        newHeight = newWidth * (oldHeight / oldWidth);
        return TSelf.Create(newHeight, newWidth);
    }
}

[Extend(typeof(String))]
public static partial class XNumber
{
    /// <summary>Get in decimal numeral system (radix = 10) from numeral system with given radix ([2, 36]).</summary>
    /// <remarks><b>Converts to <see cref="long"/> first.</b></remarks>
    /// <exception cref="ArgumentIsGreater"/>
    /// <exception cref="ArgumentIsLess"/>
    public static T ToBase10<T>(this string i, int radix) where T : INumber<T>
    {
        if (i.IsEmpty()) return T.Zero;

        const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        Throw.IfLess(radix, 2, nameof(radix));
        Throw.IfGreater(radix, Digits.Length, nameof(radix));

        // Make sure the arbitrary numeral system number is in upper case
        i = i.ToUpperInvariant();

        long result = 0;
        long multiplier = 1;
        for (int index = i.Length - 1; index >= 0; index--)
        {
            char c = i[index];
            if (index == 0 && c == '-')
            {
                // This is the negative sign symbol
                result = -result;
                break;
            }

            int digit = Digits.IndexOf(c);
            if (digit == -1)
                throw new ArgumentException(
                    "Invalid character in the arbitrary numeral system number",
                    "number");

            result += digit * multiplier;
            multiplier *= radix;
        }

        return result.Create<T>();
    }

    /// <summary>
    /// Get in binary numeral system (radix = 2) from decimal system (radix = 10).
    /// </summary>
    /// <remarks><b>Converts to <see cref="long"/>.</b></remarks>
    public static string ToBase02<T>(this T i) where T : INumber<T> => i.ToBaseX(2);

    /// <summary>
    /// Get in hexadecimal numeral system (radix = 16) from decimal system (radix = 10).
    /// </summary>
    /// <remarks><b>Converts to <see cref="long"/>.</b></remarks>
    public static string ToBase16<T>(this T i) where T : INumber<T> => i.ToBaseX(16);

    /// <summary>
    /// Get in numeral system with given radix ([2, 36]) from decimal system (radix = 10).
    /// </summary>
    /// <remarks><b>Converts to <see cref="long"/>.</b></remarks>
    /// <exception cref="ArgumentIsGreater"/>
    /// <exception cref="ArgumentIsLess"/>
    public static string ToBaseX<T>(this T i, int radix) 
        where T : INumber<T>
    {
        var j = i.ToInt64();

        const int BitsInLong = 64;
        const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        Throw.IfLess(radix, 2, nameof(radix));
        Throw.IfGreater(radix, Digits.Length, nameof(radix));

        if (j == 0)
            return "0";

        int index = BitsInLong - 1;
        long currentNumber = Abs(j);
        char[] charArray = new char[BitsInLong];

        while (currentNumber != 0)
        {
            int remainder = (int)(currentNumber % radix);
            charArray[index--] = Digits[remainder];
            currentNumber /= radix;
        }

        string result = new(charArray, index + 1, BitsInLong - index - 1);
        if (j < 0)
            result = "-" + result;

        return result;
    }
}

[Extend(typeof(Thread))]
public static partial class XNumber
{    
    /// <summary>Suspend the current thread for the given number of <i>seconds</i>, minutes, and hours.</summary>
    /// <remarks><b>Not milliseconds!</b></remarks>
    /// <exception cref="ArgumentIsNegative"/>
    /// <exception cref="ArgumentIsZero"/>
    async public static Task Sleep<T>(this T s, T m = default, T h = default) where T : INumber<T>
    {
        Throw.IfNegative(s, nameof(s));
        Throw.IfNegative(m, nameof(m));
        Throw.IfNegative(h, nameof(h));

        Throw.IfZero(Kind.All, [s, m, h], [nameof(s), nameof(m), nameof(h)]);
        double[] seconds =
        [
            EqualityComparer<T>.Default.Equals(m, default) ? 0D : Convert.ToDouble(h) * 60 * 60,
            EqualityComparer<T>.Default.Equals(m, default) ? 0D : Convert.ToDouble(m) * 60,
            Convert.ToDouble(s)
        ];

        var milliseconds = seconds.Sum() * 1000;
        await Task.Run(() => Thread.Sleep(milliseconds.ToInt32()));
    }
}

[Extend(typeof(TimeSpan))]
public static partial class XNumber
{
    /// <summary>Get <see cref="TimeSpan"/> for given number of days.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Days<T>(this T i) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromDays(i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of hours.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Hours<T>(this T i) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromHours(i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of milliseconds.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Milliseconds<T>(this T i) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromMilliseconds(i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of minutes.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Minutes<T>(this T i) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromMinutes(i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of months.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Months<T>(this T i, int days = 30) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromDays(days * i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of seconds.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Seconds<T>(this T i) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromSeconds(i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of weeks.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Weeks<T>(this T i, int days = 7) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromDays(days * i.ToDouble());
    }

    /// <summary>Get <see cref="TimeSpan"/> for given number of years.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TimeSpan Years<T>(this T i, int days = 365) where T : INumber<T>
    {
        Throw.IfNegative(i, nameof(i));
        return TimeSpan.FromDays(days * i.ToDouble());
    }
}