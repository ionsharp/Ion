using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <inheritdoc cref="IVector4Byte"/>
[Description(Description)]
public readonly record struct ByteVector4
    : IVector4Byte, IVector4<ByteVector4, byte>, IVectorAlias<Vector4<byte>, byte>, IVectorImmutable<byte>, IVectorUnfixedAlias<ByteVector4, Vector<byte>, byte>, IVectorShort<byte>, System.Numerics.IMinMaxValue<ByteVector4>
{
    public const string Description = "A set of 4 bytes.";

    /// <see cref="Region.Field"/>

    public const string StringFormat = "A = {0}, R = {1}, G = {2}, B = {3}";

    public const string StringFormatHex = "#{0}{1}{2}{3}"; //A = {0}, R = {1}, G = {2}, B = {3}

    public const string StringFormatHexShort = "#{0}";

    public const string StringFormatHexShortSymbol = "short";

    /// <see cref="Region.Property"/>

    /// <remarks>See <see cref="White"/>.</remarks>
    /// <inheritdoc cref="byte.MaxValue"/>
    public static ByteVector4 MaxValue => new(byte.MaxValue);

    /// <remarks>See <see cref="Transparent"/>.</remarks>
    /// <inheritdoc cref="byte.MinValue"/>
    public static ByteVector4 MinValue => new(byte.MinValue);

    ///

    public static ByteVector4 One => new(1);

    public static ByteVector4 Zero => new(0);

    ///

    public static ByteVector4 Black => new(byte.MinValue, byte.MinValue, byte.MinValue, byte.MaxValue);

    public static ByteVector4 White => MaxValue;

    public static ByteVector4 Transparent => MinValue;

    ///

    public static IEnumerable<ByteVector4> Gray05
        => 5D.ToArray(5, (x, y) => new ByteVector4((x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), byte.MaxValue));

    public static IEnumerable<ByteVector4> Gray10
        => 10D.ToArray(10, (x, y) => new ByteVector4((x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), byte.MaxValue));

    public static IEnumerable<ByteVector4> Gray20
        => 20D.ToArray(20, (x, y) => new ByteVector4((x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), byte.MaxValue));

    public static IEnumerable<ByteVector4> Gray25
        => 25D.ToArray(25, (x, y) => new ByteVector4((x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), (x.ToDouble() / y).Denormalize<byte>(), byte.MaxValue));

    ///

    public static ByteVector4[] Neutral => [Black, Transparent, White];

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector2.X"/>
    public readonly byte X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly byte Y { get; }

    /// <inheritdoc cref="IVector3.Z"/>
    public readonly byte Z { get; }

    /// <inheritdoc cref="IVector4.W"/>
    public readonly byte W { get; }

    /// <inheritdoc cref="IVector4{T}.XY"/>
    public readonly (byte X, byte Y) XY => (X, Y);

    /// <inheritdoc cref="IVector4{T}.XYZ"/>
    public readonly (byte X, byte Y, byte Z) XYZ => (X, Y, Z);

    /// <inheritdoc cref="IVector4Byte.A"/>
    public readonly byte A => W;

    /// <inheritdoc cref="IVector2Byte.R"/>
    public readonly byte R => X;

    /// <inheritdoc cref="IVector2Byte.G"/>
    public readonly byte G => Y;

    /// <inheritdoc cref="IVector3Byte.B"/>
    public readonly byte B => Z;

    /// <inheritdoc cref="IVector4Byte.RG"/>
    public readonly (byte R, byte G) RG => (R, G);

    /// <inheritdoc cref="IVector4Byte.RGB"/>
    public readonly (byte R, byte G, byte B) RGB => (R, G, B);

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector4.Format{T}(IEnumerable{T}, VectorType)"/>
    private ByteVector4(in (byte X, byte Y, byte Z, byte W, VectorType Type) i) => (X, Y, Z, W, Type) = i;

    /// <inheritdoc cref="IVector4Byte.Format()"/>
    public ByteVector4()
        => (X, Y, Z, W, Type) = IVector4Byte.Format();

    /// <inheritdoc cref="IVector4Byte.Format(byte, VectorType)"/>
    public ByteVector4(byte i, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(i, type);

    /// <inheritdoc cref="IVector4Byte.Format(byte, byte, VectorType)"/>
    public ByteVector4(byte r, byte g, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(r, g, type);

    /// <inheritdoc cref="IVector4Byte.Format(ValueTuple{byte, byte}, VectorType)"/>
    public ByteVector4((byte r, byte g) rg, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rg, type);

    /// <inheritdoc cref="IVector4Byte.Format(byte, byte, byte, VectorType)"/>
    public ByteVector4(byte r, byte g, byte b, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(r, g, b, type);

    /// <inheritdoc cref="IVector4Byte.Format(ValueTuple{byte, byte, byte}, VectorType)"/>
    public ByteVector4((byte r, byte g, byte b) rgb, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rgb, type);

    /// <inheritdoc cref="IVector4Byte.Format(byte, byte, byte, byte, VectorType)"/>
    public ByteVector4(byte r, byte g, byte b, byte a, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(r, g, b, a, type);

    /// <inheritdoc cref="IVector4Byte.Format(ValueTuple{byte, byte, byte, byte}, VectorType)"/>
    public ByteVector4((byte r, byte g, byte b, byte a) rgba, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rgba, type);

    /// <inheritdoc cref="IVector4Byte.Format(in IVector2{byte}, VectorType?)"/>
    public ByteVector4(in IVector2<byte> rg, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rg, type);

    /// <inheritdoc cref="IVector4Byte.Format(in IVector3{byte}, VectorType?)"/>
    public ByteVector4(in IVector3<byte> rgb, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rgb, type);

    /// <inheritdoc cref="IVector4Byte.Format(in IVector4{byte}, VectorType?)"/>
    public ByteVector4(in IVector4<byte> rgba, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(rgba, type);

    /// <inheritdoc cref="IVector4Byte.Format(in string, VectorType)"/>
    public ByteVector4(in String i, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4Byte.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator ByteVector(ByteVector4 i) => new(i);

    public static implicit operator Vector<byte>(ByteVector4 i) => new(i);

    public static implicit operator VectorM<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector2<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector2M<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector3<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector3M<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector4<byte>(ByteVector4 i) => new(i);

    public static implicit operator Vector4M<byte>(ByteVector4 i) => new(i);

    ///

    public static implicit operator ByteVector4(ByteVector2 i) => new(i);

    public static implicit operator ByteVector4(ByteVector3 i) => new(i);

    public static implicit operator ByteVector4(String i) => new(i);

    public static implicit operator ByteVector4(Vector2<byte> i) => new(i);

    public static implicit operator ByteVector4(Vector2M<byte> i) => new(i);

    public static implicit operator ByteVector4(Vector3<byte> i) => new(i);

    public static implicit operator ByteVector4(Vector3M<byte> i) => new(i);

    public static implicit operator ByteVector4(Vector4<byte> i) => new(i);

    public static implicit operator ByteVector4(Vector4M<byte> i) => new(i);

    public static implicit operator ByteVector4((byte X, byte Y) i) => new(i.X, i.Y);

    public static implicit operator ByteVector4((byte X, byte Y, byte Z) i) => new(i);

    public static implicit operator ByteVector4((byte X, byte Y, byte Z, byte W) i) => new(i);

    ///

    public static ByteVector4 operator +(ByteVector4 i) => i;

    public static ByteVector4 operator +(ByteVector4 a, byte b) => a.Do(Operator.Add, b);

    public static ByteVector4 operator +(ByteVector4 a, IVector4<byte> b) => a.Do(Operator.Add, b);

    public static ByteVector4 operator ++(ByteVector4 i) => i.Do(Operator.Add, (byte)1);

    public static ByteVector4 operator -(ByteVector4 i) => i.New(j => (byte)(byte.MaxValue - j));

    public static ByteVector4 operator -(ByteVector4 a, byte b) => a.Do(Operator.Subtract, b);

    public static ByteVector4 operator -(ByteVector4 a, IVector4<byte> b) => a.Do(Operator.Subtract, b);

    public static ByteVector4 operator --(ByteVector4 i) => i.Do(Operator.Subtract, (byte)1);

    public static ByteVector4 operator *(ByteVector4 a, byte b) => a.Do(Operator.Multiply, b);

    public static ByteVector4 operator *(ByteVector4 a, IVector4<byte> b) => a.Do(Operator.Multiply, b);

    public static ByteVector4 operator /(ByteVector4 a, byte b) => a.Do(Operator.Divide, b);

    public static ByteVector4 operator /(ByteVector4 a, IVector4<byte> b) => a.Do(Operator.Divide, b);

    public static ByteVector4 operator %(ByteVector4 a, byte b) => a.Do(Operator.Modulo, b);

    public static ByteVector4 operator %(ByteVector4 a, IVector4<byte> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public byte[] ToArray() => [X, Y, Z, W];

    /// <see cref="IArray{,}"/>

    static ByteVector4 IArray<ByteVector4, byte>.Create(ByteVector4 oldSelf, Array newSelf) => new(IVector4.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<byte>).GetEnumerator();

    readonly IEnumerator<byte> IEnumerable<byte>.GetEnumerator() => (ToArray() as IEnumerable<byte>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.Hexadecimal2, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
    {
        if (format == StringFormatHexShortSymbol)
        {
            var i = $"{A.ToString(NumberFormat.Hexadecimal2)}{R.ToString(NumberFormat.Hexadecimal2)}{G.ToString(NumberFormat.Hexadecimal2)}{B.ToString(NumberFormat.Hexadecimal2)}";
            if (i[0] == i[1] && i[2] == i[3] && i[4] == i[5] && i[6] == i[7])
                return string.Format(StringFormatHexShort, $"{i[0]}{i[2]}{i[4]}{i[6]}");
        }

        if (format == NumberFormat.Hexadecimal2)
            return string.Format(StringFormatHex, A.ToString(format), R.ToString(format), G.ToString(format), B.ToString(format));

        return string.Format(StringFormat, A.ToString(format), R.ToString(format), G.ToString(format), B.ToString(format));
    }

    /// <see cref="IVector{,}"/>

    static ByteVector4 IVector<ByteVector4, byte>.Create(VectorType type, IEnumerable<byte> value) => new(IVector4.Format(value, type));
}