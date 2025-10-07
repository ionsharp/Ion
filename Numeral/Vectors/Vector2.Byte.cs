using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <inheritdoc cref="IVector2Byte"/>
[Description(Description)]
public readonly record struct ByteVector2
    : IVector2Byte, IVector2<ByteVector2, byte>, IVectorAlias<Vector2<byte>, byte>, IVectorImmutable<byte>, IVectorUnfixedAlias<ByteVector2, Vector<byte>, byte>, IVectorShort<byte>, System.Numerics.IMinMaxValue<ByteVector2>
{
    public const string Description = "A set of 2 bytes.";

    /// <see cref="Region.Field"/>

    public const string StringFormat = "R = {0}, G = {1}";

    public const string StringFormatHex = "#{0}{1}";

    public const string StringFormatHexShort = "#{0}";

    public const string StringFormatHexShortSymbol = "short";

    /// <see cref="Region.Property"/>

    /// <remarks>See <see cref="White"/>.</remarks>
    /// <inheritdoc cref="byte.MaxValue"/>
    public static ByteVector2 MaxValue => new(byte.MaxValue);

    /// <remarks>See <see cref="Black"/>.</remarks>
    /// <inheritdoc cref="byte.MinValue"/>
    public static ByteVector2 MinValue => new(byte.MinValue);

    ///

    public static ByteVector2 Black => MinValue;

    public static ByteVector2 White => MaxValue;

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector2.X"/>
    public readonly byte X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly byte Y { get; }

    /// <inheritdoc cref="IVector2Byte.R"/>
    public readonly byte R => X;

    /// <inheritdoc cref="IVector2Byte.G"/>
    public readonly byte G => Y;

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector2.Format{byte}(in IEnumerable{byte}, VectorType)"/>
    private ByteVector2((byte X, byte Y, VectorType Type) i) => (X, Y, Type) = i;

    /// <inheritdoc cref="IVector2Byte.Format()"/>
    public ByteVector2()
        => (X, Y, Type) = IVector2Byte.Format();

    /// <inheritdoc cref="IVector2Byte.Format(byte, VectorType)"/>
    public ByteVector2(byte i, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(i, type);

    /// <inheritdoc cref="IVector2Byte.Format(byte, byte, VectorType)"/>
    public ByteVector2(byte r, byte g, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(r, g, type);

    /// <inheritdoc cref="IVector2Byte.Format(ValueTuple{byte, byte}, VectorType)"/>
    public ByteVector2((byte r, byte g) rg, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(rg, type);

    /// <inheritdoc cref="IVector2Byte.Format(byte, byte, byte, VectorType)"/>
    public ByteVector2(byte r, byte g, byte b, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(r, g, b, type);

    /// <inheritdoc cref="IVector2Byte.Format(ValueTuple{byte, byte, byte}, VectorType)"/>
    public ByteVector2((byte r, byte g, byte b) rgb, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(rgb, type);

    /// <inheritdoc cref="IVector2Byte.Format(byte, byte, byte, byte, VectorType)"/>
    public ByteVector2(byte r, byte g, byte b, byte a, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(r, g, b, a, type);

    /// <inheritdoc cref="IVector2Byte.Format(ValueTuple{byte, byte, byte, byte}, VectorType)"/>
    public ByteVector2((byte r, byte g, byte b, byte a) rgba, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(rgba, type);

    /// <inheritdoc cref="IVector2Byte.Format(in IVector2{byte}, VectorType?)"/>
    public ByteVector2(in IVector2<byte> rg, VectorType? type = null)
        => (X, Y, Type) = IVector2Byte.Format(rg, type);

    /// <inheritdoc cref="IVector2Byte.Format(in IVector3{byte}, VectorType?)"/>
    public ByteVector2(in IVector3<byte> rgb, VectorType? type = null)
        => (X, Y, Type) = IVector2Byte.Format(rgb, type);

    /// <inheritdoc cref="IVector2Byte.Format(in IVector4{byte}, VectorType?)"/>
    public ByteVector2(in IVector4<byte> rgba, VectorType? type = null)
        => (X, Y, Type) = IVector2Byte.Format(rgba, type);

    /// <inheritdoc cref="IVector2Byte.Format(in string, VectorType)"/>
    public ByteVector2(in String i, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2Byte.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Vector<byte>(ByteVector2 i) => new(i);

    public static implicit operator VectorM<byte>(ByteVector2 i) => new(i);

    public static implicit operator Vector2<byte>(ByteVector2 i) => new(i);

    public static implicit operator Vector2M<byte>(ByteVector2 i) => new(i);

    public static implicit operator ByteVector(ByteVector2 i) => new(i);

    ///

    public static implicit operator ByteVector2(ByteVector3 i) => new(i);

    public static implicit operator ByteVector2(ByteVector4 i) => new(i);

    public static implicit operator ByteVector2(String i) => new(i);

    public static implicit operator ByteVector2(Vector2<byte> i) => new(i);

    public static implicit operator ByteVector2(Vector2M<byte> i) => new(i);

    public static implicit operator ByteVector2(Vector3<byte> i) => new(i);

    public static implicit operator ByteVector2(Vector3M<byte> i) => new(i);

    public static implicit operator ByteVector2(Vector4<byte> i) => new(i);

    public static implicit operator ByteVector2(Vector4M<byte> i) => new(i);

    public static implicit operator ByteVector2((byte X, byte Y) i) => new(i.X, i.Y);

    public static implicit operator ByteVector2((byte X, byte Y, byte Z) i) => new(i);

    public static implicit operator ByteVector2((byte X, byte Y, byte Z, byte W) i) => new(i);

    ///

    public static ByteVector2 operator +(ByteVector2 a, byte b) => a.Do(Operator.Add, b);

    public static ByteVector2 operator -(ByteVector2 a, byte b) => a.Do(Operator.Subtract, b);

    public static ByteVector2 operator *(ByteVector2 a, byte b) => a.Do(Operator.Multiply, b);

    public static ByteVector2 operator /(ByteVector2 a, byte b) => a.Do(Operator.Divide, b);

    public static ByteVector2 operator %(ByteVector2 a, byte b) => a.Do(Operator.Modulo, b);

    ///

    public static ByteVector2 operator +(ByteVector2 i) => i;

    public static ByteVector2 operator +(ByteVector2 a, IVector2<byte> b) => a.Do(Operator.Add, b);

    public static ByteVector2 operator ++(ByteVector2 i) => i.Do(Operator.Add, (byte)1);

    public static ByteVector2 operator -(ByteVector2 i) => i.New(j => (byte)(byte.MaxValue - j));

    public static ByteVector2 operator -(ByteVector2 a, IVector2<byte> b) => a.Do(Operator.Subtract, b);

    public static ByteVector2 operator --(ByteVector2 i) => i.Do(Operator.Subtract, (byte)1);

    public static ByteVector2 operator *(ByteVector2 a, IVector2<byte> b) => a.Do(Operator.Multiply, b);

    public static ByteVector2 operator /(ByteVector2 a, IVector2<byte> b) => a.Do(Operator.Divide, b);

    public static ByteVector2 operator %(ByteVector2 a, IVector2<byte> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public byte[] ToArray() => [X, Y];

    /// <see cref="IArray{,}"/>

    static ByteVector2 IArray<ByteVector2, byte>.Create(ByteVector2 oldSelf, Array newSelf) => new(IVector2.Format(oldSelf, newSelf));

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
            var i = $"{R.ToString(NumberFormat.Hexadecimal2)}{G.ToString(NumberFormat.Hexadecimal2)}";
            if (i[0] == i[1] && i[2] == i[3])
                return string.Format(StringFormatHexShort, $"{i[0]}{i[2]}");
        }

        if (format == NumberFormat.Hexadecimal2)
            return string.Format(StringFormatHex, R.ToString(format), G.ToString(format));

        return string.Format(StringFormat, R.ToString(format), G.ToString(format));
    }

    /// <see cref="IVector{,}"/>

    static ByteVector2 IVector<ByteVector2, byte>.Create(VectorType type, IEnumerable<byte> value) => new(IVector2.Format(value, type));
}