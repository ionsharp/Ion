using Ion;
using Ion.Numeral;
using System;
using System.Linq;

namespace Ion.Numeral;

[Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <inheritdoc cref="Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector<TValue> Do<TValue>(this IVector<TValue> a, Operator action, IMatrix<TValue> b)
        where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(b.Rows, a.Length, nameof(b));

        var result = new TValue[b.Columns];
        Array2D.Do(b.Columns, a.Length, (y, x) => result[y] += b[y, x].Do(action, a[x]));
        return new(result);
    }

    /// <see cref="return"/> = <see cref="{TValue}"/>
    #region

    /// <summary>Dot multiply by the given <see cref="Array"/> (from left to right).</summary>
    /// <inheritdoc cref="Dot{TValue}(IVector{TValue}, IVector{TValue})"/>
    public static TValue Dot<TValue>(this IVector<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue>
        => i.Dot(new Vector<TValue>(j));

    /// <summary>Dot multiply by the given <see cref="Array"/> (from left to right).</summary>
    /// <inheritdoc cref="Dot{TValue}(IVector{TValue}, IVector{TValue})"/>
    public static TValue Dot<TValue>(this IVector<TValue> i, TValue[] j) where TValue : System.Numerics.INumber<TValue>
        => i.Dot(new Vector<TValue>(j));

    /// <summary>Dot multiply by the given <see cref="IVector"/> (from left to right).</summary>
    /// <remarks>
    /// <para>Both must have same <see cref="IVector.Length"/>.</para>
    /// <para>
    /// a[1,2,3] x b[1,2,3] = (1a x 1b) + (2a x 2b) + (3a x 3b)
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static TValue Dot<TValue>(this IVector<TValue> i, IVector<TValue> j) where TValue : System.Numerics.INumber<TValue>
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(i.Length, j.Length);
        return i.Aggregate((x, y, z) => y + (z * j[x]));
    }

    /// <summary>
    /// Get the given <see cref="VectorNorm"/>.
    /// </summary>
    public static TValue Norm<TValue>(this IVector<TValue> i, VectorNorm norm = VectorNorm.L2) where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>, System.Numerics.ISignedNumber<TValue>
    {
        var result = i.Aggregate((j, k) =>
        {
            if (norm == VectorNorm.LInfinity)
                return TValue.Max(j, k.Abs());

            return k + norm switch
            {
                VectorNorm.L1 => k.Abs(),
                VectorNorm.L2 => k.Pow2(),
            };
        });
        return norm switch { VectorNorm.LInfinity => result.Root2(), _ => result };
    }

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector<byte> ToByte<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToByte);

    /// <inheritdoc cref="XNumber.ToDecimal{TValue}(TValue)"/>
    public static Vector<decimal> ToDecimal<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDecimal);

    /// <inheritdoc cref="XNumber.ToDouble{TValue}(TValue)"/>
    public static Vector<double> ToDouble<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDouble);

    /// <inheritdoc cref="XNumber.ToInt16{TValue}(TValue)"/>
    public static Vector<short> ToInt16<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt16);

    /// <inheritdoc cref="XNumber.ToInt32{TValue}(TValue)"/>
    public static Vector<int> ToInt32<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt32);

    /// <inheritdoc cref="XNumber.ToInt64{TValue}(TValue)"/>
    public static Vector<long> ToInt64<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt64);

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector<sbyte> ToSByte<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSByte);

    /// <inheritdoc cref="XNumber.ToSingle{TValue}(TValue)"/>
    public static Vector<float> ToSingle<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSingle);

    /// <inheritdoc cref="XNumber.ToUInt16{TValue}(TValue)"/>
    public static Vector<ushort> ToUInt16<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt16);

    /// <inheritdoc cref="XNumber.ToUInt32{TValue}(TValue)"/>
    public static Vector<uint> ToUInt32<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt32);

    /// <inheritdoc cref="XNumber.ToUInt64{TValue}(TValue)"/>
    public static Vector<ulong> ToUInt64<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt64);

    #endregion

    /// <see cref="System.Numerics.IFloatingPoint{TSelf}"/> | <see cref="System.Numerics.ISignedNumber{TSelf}"/>
    #region

    /// <summary>
    /// Get distance from point [0.5, 0.5].
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static TValue Distance<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>, System.Numerics.ISignedNumber<TValue>
        => i.Distance(TValue.One.Divide2());

    /// <summary>Get distance from given value (from left to right).</summary>
    /// <inheritdoc cref="Distance{TValue}(IVector{TValue}, IVector{TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    public static TValue Distance<TValue>(this IVector<TValue> i, TValue j)
        where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>, System.Numerics.ISignedNumber<TValue>
        => i.Distance(j.ToArray(i.Length));

    /// <summary>Get distance from given <see cref="Array"/> (from left to right).</summary>
    /// <inheritdoc cref="Distance{TValue}(IVector{TValue}, IVector{TValue})"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TValue Distance<TValue>(this IVector<TValue> i, TValue[] j)
        where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>, System.Numerics.ISignedNumber<TValue>
        => i.Distance(new Vector<TValue>(j));

    /// <summary>Get distance from given <see cref="IVector"/> (from left to right).</summary>
    /// <remarks>
    /// <para>
    /// <b>i</b>[1,2,3] x <b>j</b>[1,2,3] = √(|1<b>i</b> - 1<b>j</b>|^2 + |2<b>i</b> - 2<b>j</b>|^2 + |3<b>i</b> - 3<b>j</b>|^2)
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TValue Distance<TValue>(this IVector<TValue> i, IVector<TValue> j)
        where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>, System.Numerics.ISignedNumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length);
        return i.Aggregate((x, y, z) => y + (z - j[x]).Abs().Pow2()).Root2();
    }

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Denormalize{T}(double)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TValue> Denormalize<TValue>(this IVector<Double1> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TValue> Denormalize<TValue>(this IVector<Double1> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TValue> Denormalize<TValue>(this IVector<Double1> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        return Array1D.Get(i.Length, j => i[j].Denormalize(minimum, maximum));
    }

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<TValue> Denormalize<TValue>(this IVector<Double1> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(range.Minimum, range.Maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<Double1> Normalize<TValue>(this IVector<TValue> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<Double1> Normalize<TValue>(this IVector<TValue> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<Double1> Normalize<TValue>(this IVector<TValue> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        return Array1D.Get(i.Length, j => i[j].Normalize(minimum, maximum));
    }

    /// <inheritdoc cref="XNumber.Normalize{T}(T, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector<Double1> Normalize<TValue>(this IVector<TValue> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(range.Minimum, range.Maximum);

    public static Vector<Double1> Normalize<TValue>(this IVector<TValue> i, RangeType By)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        if (By == RangeType.Type)
            return i.Normalize();

        TValue minimum = i.Minimum(), maximum = i.Maximum();
        minimum = minimum == maximum ? maximum - TValue.One : minimum;

        return new(Array1D.Get(i.Length, j => i[j].Normalize(minimum, maximum)));
    }

    #endregion
}

[Extend(typeof(IVector<,>))]
[Extend(typeof(IVector2<byte>), typeof(IVector3<byte>), typeof(IVector4<byte>))]
public static partial class XVector
{
    /// <exception cref="ArgumentNullException"/>
    public static TSelf A<TSelf>(this IVector<TSelf, byte> i, byte a)
        where TSelf : IVector<TSelf, byte>, IVector4<byte>
        => i.New((j, k) => j == 3 ? a : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf A<TSelf>(this IVector<TSelf, byte> i, Func<byte, byte> a)
        where TSelf : IVector<TSelf, byte>, IVector4<byte>
        => i.New((j, k) => j == 3 ? a(k) : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf R<TSelf>(this IVector<TSelf, byte> i, byte r)
        where TSelf : IVector<TSelf, byte>, IVector2<byte>
        => i.New((j, k) => j == 0 ? r : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf R<TSelf>(this IVector<TSelf, byte> i, Func<byte, byte> r)
        where TSelf : IVector<TSelf, byte>, IVector2<byte>
        => i.New((j, k) => j == 0 ? r(k) : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf G<TSelf>(this IVector<TSelf, byte> i, byte g)
        where TSelf : IVector<TSelf, byte>, IVector2<byte>
        => i.New((j, k) => j == 1 ? g : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf G<TSelf>(this IVector<TSelf, byte> i, Func<byte, byte> g)
        where TSelf : IVector<TSelf, byte>, IVector2<byte>
        => i.New((j, k) => j == 1 ? g(k) : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf B<TSelf>(this IVector<TSelf, byte> i, byte b)
        where TSelf : IVector<TSelf, byte>, IVector3<byte>
        => i.New((j, k) => j == 2 ? b : k);

    /// <exception cref="ArgumentNullException"/>
    public static TSelf B<TSelf>(this IVector<TSelf, byte> i, Func<byte, byte> b)
        where TSelf : IVector<TSelf, byte>, IVector3<byte>
        => i.New((j, k) => j == 2 ? b(k) : k);
}

[Extend<IVector2>, Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <inheritdoc cref="Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector2<TValue> Do<TValue>(this IVector2<TValue> a, Operator action, IMatrix<TValue> b)
        where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(b.Rows, IVector2.Length, nameof(b));

        var result = new TValue[b.Columns];
        Array2D.Do(b.Rows, IVector2.Length, (y, x) => result[y] += b[y, x].Do(action, a[x]));
        return new(result[0], result[1]);
    }

    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector2<byte> ToByte<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToByte);

    /// <inheritdoc cref="XNumber.ToDecimal{TValue}(TValue)"/>
    public static Vector2<decimal> ToDecimal<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDecimal);

    /// <inheritdoc cref="XNumber.ToDouble{TValue}(TValue)"/>
    public static Vector2<double> ToDouble<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDouble);

    /// <inheritdoc cref="XNumber.ToInt16{TValue}(TValue)"/>
    public static Vector2<short> ToInt16<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt16);

    /// <inheritdoc cref="XNumber.ToInt32{TValue}(TValue)"/>
    public static Vector2<int> ToInt32<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt32);

    /// <inheritdoc cref="XNumber.ToInt64{TValue}(TValue)"/>
    public static Vector2<long> ToInt64<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt64);

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector2<sbyte> ToSByte<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSByte);

    /// <inheritdoc cref="XNumber.ToSingle{TValue}(TValue)"/>
    public static Vector2<float> ToSingle<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSingle);

    /// <inheritdoc cref="XNumber.ToUInt16{TValue}(TValue)"/>
    public static Vector2<ushort> ToUInt16<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt16);

    /// <inheritdoc cref="XNumber.ToUInt32{TValue}(TValue)"/>
    public static Vector2<uint> ToUInt32<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt32);

    /// <inheritdoc cref="XNumber.ToUInt64{TValue}(TValue)"/>
    public static Vector2<ulong> ToUInt64<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt64);

    #endregion

    /// <see cref="System.Numerics.INumber{TSelf}"/> | <see cref="return"/> = <see cref="Double"/>
    #region

    /// <inheritdoc cref="XNumber.Acos{TValue}(TValue)"/>
    public static Vector2 Acos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Acos);

    /// <inheritdoc cref="XNumber.Asin{TValue}(TValue)"/>
    public static Vector2 Asin<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Asin);

    /// <inheritdoc cref="XNumber.Atan{TValue}(TValue)"/>
    public static Vector2 Atan<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Atan);

    /// <inheritdoc cref="XNumber.Atan2{TValue}(TValue, TValue)"/>
    public static Vector2 Atan2<TValue>(this IVector2<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.Atan2(j));

    /// <inheritdoc cref="XNumber.Cos{TValue}(TValue)"/>
    public static Vector2 Cos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Cos);

    /// <inheritdoc cref="XNumber.DAcos{TValue}(TValue)"/>
    public static Vector2 DAcos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAcos);

    /// <inheritdoc cref="XNumber.DAsin{TValue}(TValue)"/>
    public static Vector2 DAsin<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAsin);

    /// <inheritdoc cref="XNumber.DAtan{TValue}(TValue)"/>
    public static Vector2 DAtan<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAtan);

    /// <inheritdoc cref="XNumber.DAtan2{TValue}(TValue, TValue)"/>
    public static Vector2 DAtan2<TValue>(this IVector2<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.DAtan2(j));

    /// <inheritdoc cref="XNumber.DCos{TValue}(TValue)"/>
    public static Vector2 DCos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DCos);

    /// <inheritdoc cref="XNumber.DSin{TValue}(TValue)"/>
    public static Vector2 DSin<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSin);

    /// <inheritdoc cref="XNumber.DSinCos{TValue}(TValue)"/>
    public static Vector2<(double DSin, double DCos)> DSinCos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSinCos);

    /// <inheritdoc cref="XNumber.DTan{TValue}(TValue)"/>
    public static Vector2 DTan<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DTan);

    /// <inheritdoc cref="XNumber.Sin{TValue}(TValue)"/>
    public static Vector2 Sin<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Sin);

    /// <inheritdoc cref="XNumber.SinCos{TValue}(TValue)"/>
    public static Vector2<(double Sin, double Cos)> SinCos<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.SinCos);

    /// <inheritdoc cref="XNumber.Tan{TValue}(TValue)"/>
    public static Vector2 Tan<TValue>(this IVector2<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Tan);

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Denormalize{T}(double)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum), (maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, TValue minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, TValue minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, (TValue X, TValue Y) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, (TValue X, TValue Y) minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector2<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, (TValue X, TValue Y) minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector2<TValue>(minimum), new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, IVector2<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, IVector2<TValue> minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, IVector2<TValue> minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector2.Length, j => i[j].Denormalize(minimum[j], maximum[j])).To(j => new Vector2<TValue>(j[0], j[1], i.Type));
    }

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<TValue> Denormalize<TValue>(this IVector2<Double1> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(range.Minimum, range.Maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum), (maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, TValue minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, TValue minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, (TValue X, TValue Y) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, (maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, (TValue X, TValue Y) minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector2<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, (TValue X, TValue Y) minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector2<TValue>(minimum), new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector2<TValue>(TValue.MinValue), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, IVector2<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, IVector2<TValue> minimum, (TValue X, TValue Y) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, IVector2<TValue> minimum, IVector2<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector2.Length, j => i[j].Normalize(minimum[j], maximum[j])).To(j => new Vector2<Double1>(j[0], j[1], i.Type));
    }

    /// <inheritdoc cref="XNumber.Normalize{T}(T, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector2<Double1> Normalize<TValue>(this IVector2<TValue> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(range.Minimum, range.Maximum);

    #endregion
}

[Extend<IVector2>, Extend(typeof(IVector<,>))]
public static partial class XVector
{
    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Do(action, new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Do(action, j[x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Divide(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Divide(j[x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Minus(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Minus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Modulo(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Modulo(j[x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Multiply(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Multiply(j[x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) multiple)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(multiple, nameof(multiple));
        return i.Nearest(new Vector2<TValue>(multiple));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> multiple)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((j, k) => k.Nearest(multiple[j]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Plus(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Plus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
      where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Pow(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Pow(j[x]));
    }

    #endregion

    /// <see cref="System.Numerics.IFloatingPoint{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.IaN(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.IaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.NaN(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.NaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Root(new Vector2<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Root(j[x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, (int X, int Y) digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(digits, nameof(digits));
        return i.Round(new Vector2<int>(digits), mode);
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<int> digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((x, y) => y.Round(digits[x], mode));
    }

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, (TValue X, TValue Y) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, IVector2<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) minimum, (TValue X, TValue Y) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector2<TValue>(minimum), new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y) minimum, IVector2<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector2<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> minimum, (TValue X, TValue Y) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, new Vector2<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> minimum, IVector2<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector2<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return i.New((j, k) => k.Clamp(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> newMinimum, IVector2<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMinimum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> oldMinimum, IVector2<TValue> oldMaximum, IVector2<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector2<TValue> oldMinimum, IVector2<TValue> oldMaximum, IVector2<TValue> newMinimum, IVector2<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMinimum[j], newMaximum[j]));

    #endregion
}

[Extend<IVector3>, Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <inheritdoc cref="Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector3<TValue> Do<TValue>(this IVector3<TValue> a, Operator action, IMatrix<TValue> b)
        where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(b.Rows, IVector3.Length, nameof(b));

        var result = new TValue[b.Columns];
        Array2D.Do(b.Rows, IVector3.Length, (y, x) => result[y] += b[y, x].Do(action, a[x]));
        return new(result[0], result[1], result[2]);
    }

    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector3<byte> ToByte<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToByte);

    /// <inheritdoc cref="XNumber.ToDecimal{TValue}(TValue)"/>
    public static Vector3<decimal> ToDecimal<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDecimal);

    /// <inheritdoc cref="XNumber.ToDouble{TValue}(TValue)"/>
    public static Vector3<double> ToDouble<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDouble);

    /// <inheritdoc cref="XNumber.ToInt16{TValue}(TValue)"/>
    public static Vector3<short> ToInt16<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt16);

    /// <inheritdoc cref="XNumber.ToInt32{TValue}(TValue)"/>
    public static Vector3<int> ToInt32<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt32);

    /// <inheritdoc cref="XNumber.ToInt64{TValue}(TValue)"/>
    public static Vector3<long> ToInt64<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt64);

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector3<sbyte> ToSByte<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSByte);

    /// <inheritdoc cref="XNumber.ToSingle{TValue}(TValue)"/>
    public static Vector3<float> ToSingle<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSingle);

    /// <inheritdoc cref="XNumber.ToUInt16{TValue}(TValue)"/>
    public static Vector3<ushort> ToUInt16<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt16);

    /// <inheritdoc cref="XNumber.ToUInt32{TValue}(TValue)"/>
    public static Vector3<uint> ToUInt32<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt32);

    /// <inheritdoc cref="XNumber.ToUInt64{TValue}(TValue)"/>
    public static Vector3<ulong> ToUInt64<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt64);

    #endregion

    /// <see cref="System.Numerics.INumber{TSelf}"/> | <see cref="return"/> = <see cref="Double"/>
    #region

    /// <inheritdoc cref="XNumber.Acos{TValue}(TValue)"/>
    public static Vector3 Acos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Acos);

    /// <inheritdoc cref="XNumber.Asin{TValue}(TValue)"/>
    public static Vector3 Asin<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Asin);

    /// <inheritdoc cref="XNumber.Atan{TValue}(TValue)"/>
    public static Vector3 Atan<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Atan);

    /// <inheritdoc cref="XNumber.Atan2{TValue}(TValue, TValue)"/>
    public static Vector3 Atan2<TValue>(this IVector3<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.Atan2(j));

    /// <inheritdoc cref="XNumber.Cos{TValue}(TValue)"/>
    public static Vector3 Cos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Cos);

    /// <inheritdoc cref="XNumber.DAcos{TValue}(TValue)"/>
    public static Vector3 DAcos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAcos);

    /// <inheritdoc cref="XNumber.DAsin{TValue}(TValue)"/>
    public static Vector3 DAsin<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAsin);

    /// <inheritdoc cref="XNumber.DAtan{TValue}(TValue)"/>
    public static Vector3 DAtan<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAtan);

    /// <inheritdoc cref="XNumber.DAtan2{TValue}(TValue, TValue)"/>
    public static Vector3 DAtan2<TValue>(this IVector3<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.DAtan2(j));

    /// <inheritdoc cref="XNumber.DCos{TValue}(TValue)"/>
    public static Vector3 DCos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DCos);

    /// <inheritdoc cref="XNumber.DSin{TValue}(TValue)"/>
    public static Vector3 DSin<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSin);

    /// <inheritdoc cref="XNumber.DSinCos{TValue}(TValue)"/>
    public static Vector3<(double DSin, double DCos)> DSinCos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSinCos);

    /// <inheritdoc cref="XNumber.DTan{TValue}(TValue)"/>
    public static Vector3 DTan<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DTan);

    /// <inheritdoc cref="XNumber.Sin{TValue}(TValue)"/>
    public static Vector3 Sin<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Sin);

    /// <inheritdoc cref="XNumber.SinCos{TValue}(TValue)"/>
    public static Vector3<(double Sin, double Cos)> SinCos<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.SinCos);

    /// <inheritdoc cref="XNumber.Tan{TValue}(TValue)"/>
    public static Vector3 Tan<TValue>(this IVector3<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Tan);

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Denormalize{T}(double)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum), (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, TValue minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, TValue minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, (TValue X, TValue Y, TValue Z) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, (TValue X, TValue Y, TValue Z) minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector3<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, (TValue X, TValue Y, TValue Z) minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector3<TValue>(minimum), new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, IVector3<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, IVector3<TValue> minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, IVector3<TValue> minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector3.Length, j => i[j].Denormalize(minimum[j], maximum[j])).To(j => new Vector3<TValue>(j[0], j[1], j[2], i.Type));
    }

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<TValue> Denormalize<TValue>(this IVector3<Double1> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(range.Minimum, range.Maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum), (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, TValue minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, TValue minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, (TValue X, TValue Y, TValue Z) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, (TValue X, TValue Y, TValue Z) minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector3<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, (TValue X, TValue Y, TValue Z) minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector3<TValue>(minimum), new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector3<TValue>(TValue.MinValue), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, IVector3<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, IVector3<TValue> minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, IVector3<TValue> minimum, IVector3<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector3.Length, j => i[j].Normalize(minimum[j], maximum[j])).To(j => new Vector3<Double1>(j[0], j[1], j[2], i.Type));
    }

    /// <inheritdoc cref="XNumber.Normalize{T}(T, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector3<Double1> Normalize<TValue>(this IVector3<TValue> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(range.Minimum, range.Maximum);

    #endregion
}

[Extend<IVector3>, Extend(typeof(IVector<,>))]
public static partial class XVector
{
    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Do(action, new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Do(action, j[x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Divide(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Divide(j[x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Minus(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Minus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Modulo(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Modulo(j[x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Multiply(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Multiply(j[x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) multiple)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(multiple, nameof(multiple));
        return i.Nearest(new Vector3<TValue>(multiple));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> multiple)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((j, k) => k.Nearest(multiple[j]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Plus(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Plus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
      where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Pow(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Pow(j[x]));
    }

    #endregion

    /// <see cref="System.Numerics.IFloatingPoint{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.IaN(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.IaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.NaN(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.NaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Root(new Vector3<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Root(j[x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, (int X, int Y, int Z) digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(digits, nameof(digits));
        return i.Round(new Vector3<int>(digits), mode);
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<int> digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((x, y) => y.Round(digits[x], mode));
    }

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, IVector3<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector3<TValue>(minimum), new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z) minimum, IVector3<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector3<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> minimum, (TValue X, TValue Y, TValue Z) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, new Vector3<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> minimum, IVector3<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return i.New((j, k) => k.Clamp(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> newMinimum, IVector3<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMinimum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> oldMinimum, IVector3<TValue> oldMaximum, IVector3<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector3<TValue> oldMinimum, IVector3<TValue> oldMaximum, IVector3<TValue> newMinimum, IVector3<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector3<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMinimum[j], newMaximum[j]));

    #endregion
}

[Extend<IVector4>, Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="MatrixVectorMismatch"/>
    public static Vector4<TValue> Do<TValue>(this IVector4<TValue> a, Operator action, IMatrix<TValue> b)
        where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNotEqual<MatrixVectorMismatch>(b.Rows, IVector4.Length, nameof(b));

        var result = new TValue[b.Columns];
        Array2D.Do(b.Rows, IVector4.Length, (y, x) => result[y] += b[y, x].Do(action, a[x]));
        return new(result[0], result[1], result[2], result[3]);
    }

    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector4<byte> ToByte<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToByte);

    /// <inheritdoc cref="XNumber.ToDecimal{TValue}(TValue)"/>
    public static Vector4<decimal> ToDecimal<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDecimal);

    /// <inheritdoc cref="XNumber.ToDouble{TValue}(TValue)"/>
    public static Vector4<double> ToDouble<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToDouble);

    /// <inheritdoc cref="XNumber.ToInt16{TValue}(TValue)"/>
    public static Vector4<short> ToInt16<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt16);

    /// <inheritdoc cref="XNumber.ToInt32{TValue}(TValue)"/>
    public static Vector4<int> ToInt32<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt32);

    /// <inheritdoc cref="XNumber.ToInt64{TValue}(TValue)"/>
    public static Vector4<long> ToInt64<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToInt64);

    /// <inheritdoc cref="XNumber.ToByte{TValue}(TValue)"/>
    public static Vector4<sbyte> ToSByte<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSByte);

    /// <inheritdoc cref="XNumber.ToSingle{TValue}(TValue)"/>
    public static Vector4<float> ToSingle<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToSingle);

    /// <inheritdoc cref="XNumber.ToUInt16{TValue}(TValue)"/>
    public static Vector4<ushort> ToUInt16<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt16);

    /// <inheritdoc cref="XNumber.ToUInt32{TValue}(TValue)"/>
    public static Vector4<uint> ToUInt32<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt32);

    /// <inheritdoc cref="XNumber.ToUInt64{TValue}(TValue)"/>
    public static Vector4<ulong> ToUInt64<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.ToUInt64);

    #endregion

    /// <see cref="System.Numerics.INumber{TSelf}"/> | <see cref="return"/> = <see cref="Double"/>
    #region

    /// <inheritdoc cref="XNumber.Acos{TValue}(TValue)"/>
    public static Vector4 Acos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Acos);

    /// <inheritdoc cref="XNumber.Asin{TValue}(TValue)"/>
    public static Vector4 Asin<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Asin);

    /// <inheritdoc cref="XNumber.Atan{TValue}(TValue)"/>
    public static Vector4 Atan<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Atan);

    /// <inheritdoc cref="XNumber.Atan2{TValue}(TValue, TValue)"/>
    public static Vector4 Atan2<TValue>(this IVector4<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.Atan2(j));

    /// <inheritdoc cref="XNumber.Cos{TValue}(TValue)"/>
    public static Vector4 Cos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Cos);

    /// <inheritdoc cref="XNumber.DAcos{TValue}(TValue)"/>
    public static Vector4 DAcos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAcos);

    /// <inheritdoc cref="XNumber.DAsin{TValue}(TValue)"/>
    public static Vector4 DAsin<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAsin);

    /// <inheritdoc cref="XNumber.DAtan{TValue}(TValue)"/>
    public static Vector4 DAtan<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DAtan);

    /// <inheritdoc cref="XNumber.DAtan2{TValue}(TValue, TValue)"/>
    public static Vector4 DAtan2<TValue>(this IVector4<TValue> i, TValue j) where TValue : System.Numerics.INumber<TValue> => i.NewType(x => x.DAtan2(j));

    /// <inheritdoc cref="XNumber.DCos{TValue}(TValue)"/>
    public static Vector4 DCos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DCos);

    /// <inheritdoc cref="XNumber.DSin{TValue}(TValue)"/>
    public static Vector4 DSin<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSin);

    /// <inheritdoc cref="XNumber.DSinCos{TValue}(TValue)"/>
    public static Vector4<(double DSin, double DCos)> DSinCos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DSinCos);

    /// <inheritdoc cref="XNumber.DTan{TValue}(TValue)"/>
    public static Vector4 DTan<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.DTan);

    /// <inheritdoc cref="XNumber.Sin{TValue}(TValue)"/>
    public static Vector4 Sin<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Sin);

    /// <inheritdoc cref="XNumber.SinCos{TValue}(TValue)"/>
    public static Vector4<(double Sin, double Cos)> SinCos<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.SinCos);

    /// <inheritdoc cref="XNumber.Tan{TValue}(TValue)"/>
    public static Vector4 Tan<TValue>(this IVector4<TValue> i) where TValue : System.Numerics.INumber<TValue> => i.NewType(XNumber.Tan);

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Denormalize{T}(double)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum, minimum), (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, TValue minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, TValue minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector4<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector4<TValue>(minimum), new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, IVector4<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, IVector4<TValue> minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, IVector4<TValue> minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector4.Length, j => i[j].Denormalize(minimum[j], maximum[j])).To(j => new Vector4<TValue>(j[0], j[1], j[2], j[3], i.Type));
    }

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<TValue> Denormalize<TValue>(this IVector4<Double1> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(range.Minimum, range.Maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, TValue.MaxValue);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, TValue minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum, minimum), (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, TValue minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, TValue minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector4<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector4<TValue>(minimum), new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector4<TValue>(TValue.MinValue), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, IVector4<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, IVector4<TValue> minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, IVector4<TValue> minimum, IVector4<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return Array1D.Get(IVector4.Length, j => i[j].Normalize(minimum[j], maximum[j])).To(j => new Vector4<Double1>(j[0], j[1], j[2], j[3], i.Type));
    }

    /// <inheritdoc cref="XNumber.Normalize{T}(T, IRange{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public static Vector4<Double1> Normalize<TValue>(this IVector4<TValue> i, IRange<TValue> range)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(range.Minimum, range.Maximum);

    #endregion
}

[Extend<IVector4>, Extend(typeof(IVector<,>))]
public static partial class XVector
{
    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Do(action, new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Do(action, j[x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Divide(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Divide(j[x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Minus(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Minus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Modulo(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Modulo(j[x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Multiply(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Multiply(j[x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) multiple)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(multiple, nameof(multiple));
        return i.Nearest(new Vector4<TValue>(multiple));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> multiple)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        return i.New((j, k) => k.Nearest(multiple[j]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Plus(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Plus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
      where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Pow(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Pow(j[x]));
    }

    #endregion

    /// <see cref="System.Numerics.IFloatingPoint{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.IaN(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.IaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.NaN(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.NaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Root(new Vector4<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        return i.New((x, y) => y.Root(j[x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, (int X, int Y, int Z, int W) digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(digits, nameof(digits));
        return i.Round(new Vector4<int>(digits), mode);
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<int> digits, MidpointRounding mode = default)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        return i.New((x, y) => y.Round(digits[x], mode));
    }

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, IVector4<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp((minimum, minimum, minimum, minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector4<TValue>(minimum), new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, (TValue X, TValue Y, TValue Z, TValue W) minimum, IVector4<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(new Vector4<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, (maximum, maximum, maximum, maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> minimum, (TValue X, TValue Y, TValue Z, TValue W) maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, new Vector4<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> minimum, IVector4<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        return i.New((j, k) => k.Clamp(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> newMinimum, IVector4<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(newMinimum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> oldMinimum, IVector4<TValue> oldMaximum, IVector4<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMaximum[j]));

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector4<TValue> oldMinimum, IVector4<TValue> oldMaximum, IVector4<TValue> newMinimum, IVector4<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVector4<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMinimum[j], newMaximum[j]));

    #endregion
}

[Extend<IVectorUnfixed>]
[Extend(typeof(IVector<>))]
public static partial class XVector
{
    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue[] minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, maximum.ToArray(i.Length));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue[] minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector<TValue>(minimum), new Vector<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, TValue[] minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(new Vector<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, IVector<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, new Vector<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, IVector<TValue> minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Denormalize(minimum, new Vector<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Denormalize{T}(Double1, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<TValue> Denormalize<TValue>(this IVectorUnfixed<Double1> i, IVector<TValue> minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, minimum.Length, nameof(minimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, maximum.Length, nameof(maximum));
        return Array1D.Get(i.Length, j => i[j].Denormalize(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue[] minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, maximum.ToArray(i.Length));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue[] minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, TValue[] minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(new Vector<TValue>(minimum), maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, IVector<TValue> minimum, TValue maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, maximum.ToArray(i.Length));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, IVector<TValue> minimum, TValue[] maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue> => i.Normalize(minimum, new Vector<TValue>(maximum));

    /// <inheritdoc cref="XNumber.Normalize{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static Vector<Double1> Normalize<TValue>(this IVectorUnfixed<TValue> i, IVector<TValue> minimum, IVector<TValue> maximum)
        where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, minimum.Length, nameof(minimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, maximum.Length, nameof(maximum));
        return Array1D.Get(i.Length, j => i[j].Normalize(minimum[j], maximum[j]));
    }
}

[Extend<IVectorUnfixed>]
[Extend(typeof(IVector<,>))]
public static partial class XVector
{
    /// <see cref="System.Numerics.INumber{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Do(action, new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Do{TValue}(TValue, Operator, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Do<TSelf, TValue>(this IVector<TSelf, TValue> i, Operator action, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Do(action, j[x]));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Divide(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Divide{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DivideByZeroException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Divide<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Divide(j[x]));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Minus(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Minus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Minus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Minus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Modulo(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Modulo{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Modulo<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Modulo(j[x]));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Multiply(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Multiply{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Multiply<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Multiply(j[x]));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] multiple)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(multiple, nameof(multiple));
        return i.Nearest(new Vector<TValue>(multiple));
    }

    /// <inheritdoc cref="XNumber.Nearest{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Nearest<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> multiple)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(multiple, nameof(multiple));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, multiple.Length, nameof(multiple));
        return i.New((j, k) => k.Nearest(multiple[j]));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Plus(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Plus{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Plus<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Plus(j[x]));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
      where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Pow(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Pow{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Pow<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(j));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length, nameof(j));
        return i.New((x, y) => y.Pow(j[x]));
    }

    #endregion

    /// <see cref="System.Numerics.IFloatingPoint{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.IaN(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.IaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf IaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<ArrayLengthMismatch>(i.Length, j.Length);
        return i.New((x, y) => y.IaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.NaN(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.NaN{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf NaN<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<ArrayLengthMismatch>(i.Length, j.Length);
        return i.New((x, y) => y.NaN(j[x]));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(j, nameof(j));
        return i.Root(new Vector<TValue>(j));
    }

    /// <inheritdoc cref="XNumber.Root{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Root<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> j)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(j, nameof(j));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, j.Length);
        return i.New((x, y) => y.Root(j[x]));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, int[] digits)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(digits, nameof(digits));
        return i.Round(new Vector<int>(digits));
    }

    /// <inheritdoc cref="XNumber.Round{TValue}(TValue, int)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Round<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<int> digits)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(digits, nameof(digits));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, digits.Length);
        return i.New((x, y) => y.Round(digits[x]));
    }

    #endregion

    /// <see cref="System.Numerics.IMinMaxValue{TSelf}"/>
    #region

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, TValue[] maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue minimum, IVector<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum.ToArray(i.Length), maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, maximum.ToArray(i.Length));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] minimum, TValue[] maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, minimum.Length, nameof(minimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, maximum.Length, nameof(maximum));
        return i.New((j, k) => k.Clamp(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, TValue[] minimum, IVector<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(minimum, nameof(minimum));
        return i.Clamp(new Vector<TValue>(minimum), maximum);
    }

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(TValue.MinValue, maximum);

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> minimum, TValue maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
        => i.Clamp(minimum, maximum.ToArray(i.Length));

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> minimum, TValue[] maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(maximum, nameof(maximum));
        return i.Clamp(minimum, new Vector<TValue>(maximum));
    }

    /// <inheritdoc cref="XNumber.Clamp{TValue}(TValue, TValue, TValue)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf Clamp<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> minimum, IVector<TValue> maximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(minimum, nameof(minimum));
        Throw.IfNull(maximum, nameof(maximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, minimum.Length, nameof(minimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, maximum.Length, nameof(maximum));
        return i.New((j, k) => k.Clamp(minimum[j], maximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(newMaximum, nameof(newMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMaximum.Length, nameof(newMaximum));
        return i.New((j, k) => k.ToRange(newMaximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> newMinimum, IVector<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(newMinimum, nameof(newMinimum));
        Throw.IfNull(newMaximum, nameof(newMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMinimum.Length, nameof(newMinimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMaximum.Length, nameof(newMaximum));
        return i.New((j, k) => k.ToRange(newMinimum[j], newMaximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> oldMinimum, IVector<TValue> oldMaximum, IVector<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(oldMinimum, nameof(oldMinimum));
        Throw.IfNull(oldMaximum, nameof(oldMaximum));
        Throw.IfNull(newMaximum, nameof(newMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, oldMinimum.Length, nameof(oldMinimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, oldMaximum.Length, nameof(oldMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMaximum.Length, nameof(newMaximum));
        return i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMaximum[j]));
    }

    /// <inheritdoc cref="XNumber.ToRange{T}(T, T, T, T, T)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="VectorLengthMismatch"/>
    public static TSelf ToRange<TSelf, TValue>(this IVector<TSelf, TValue> i, IVector<TValue> oldMinimum, IVector<TValue> oldMaximum, IVector<TValue> newMinimum, IVector<TValue> newMaximum)
        where TSelf : IVector<TSelf, TValue>, IVectorUnfixed<TValue> where TValue : System.Numerics.IMinMaxValue<TValue>, System.Numerics.INumber<TValue>
    {
        Throw.IfNull(i, nameof(i));
        Throw.IfNull(oldMinimum, nameof(oldMinimum));
        Throw.IfNull(oldMaximum, nameof(oldMaximum));
        Throw.IfNull(newMinimum, nameof(newMinimum));
        Throw.IfNull(newMaximum, nameof(newMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, oldMinimum.Length, nameof(oldMinimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, oldMaximum.Length, nameof(oldMaximum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMinimum.Length, nameof(newMinimum));
        Throw.IfNotEqual<VectorLengthMismatch>(i.Length, newMaximum.Length, nameof(newMaximum));
        return i.New((j, k) => k.ToRange(oldMinimum[j], oldMaximum[j], newMinimum[j], newMaximum[j]));
    }

    #endregion
}