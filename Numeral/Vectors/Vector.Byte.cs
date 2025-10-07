using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <remarks>See <see cref="ByteVectorMutable"/> for mutable equivalent.</remarks>
/// <inheritdoc cref="IVectorByte"/>
[Description(IVector.Description)]
public readonly record struct ByteVector
    : IVectorByte, IVector<ByteVector, byte>, IVectorAlias<Vector<byte>, byte>, IVectorImmutable<byte>, IVectorLong<byte>, IVectorUnfixed<byte>, IVectorUnfixedAlias<ByteVector, ByteVector, byte>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector.Length"/>
    public readonly int Length => _Value.Length;

    /// <inheritdoc cref="IVector.Type"/>
    public readonly VectorType Type { get; }

    private readonly byte[] _Value { get; }

    /// <see cref="Region.Property.Indexor"/>

    public readonly byte this[int index] => _Value[index];

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector.Format{T}(VectorType, in IEnumerable{T})"/>
    public ByteVector(VectorType type, in IEnumerable<byte> i)
        => (_Value, Type) = IVector.Format(type, i);

    /// <inheritdoc cref="IVector.Format{T}(in IEnumerable{T}, VectorType)"/>
    public ByteVector(in IEnumerable<byte> i, VectorType type)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public ByteVector(params byte[] i)
        => (_Value, Type) = IVector.Format(i);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public ByteVector(in IArray1D<byte> i, VectorType type = default)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IMatrix{T}, VectorType?)"/>
    public ByteVector(in IMatrix<byte> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IVector{T}, VectorType?)"/>
    public ByteVector(in IVector<byte> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator ByteVector(byte[] i) => new(i);

    public static implicit operator Vector<byte>(ByteVector i) => new(i);

    public static implicit operator ByteVector(Vector<byte> i) => new(i);

    public static implicit operator VectorM<byte>(ByteVector i) => new(i);

    public static implicit operator ByteVector(VectorM<byte> i) => new(i);

    ///

    public static ByteVector operator +(ByteVector a, byte b) => a.Do(Operator.Add, b);

    public static ByteVector operator -(ByteVector a, byte b) => a.Do(Operator.Subtract, b);

    public static ByteVector operator *(ByteVector a, byte b) => a.Do(Operator.Multiply, b);

    public static ByteVector operator /(ByteVector a, byte b) => a.Do(Operator.Divide, b);

    public static ByteVector operator %(ByteVector a, byte b) => a.Do(Operator.Modulo, b);

    ///

    public static ByteVector operator +(ByteVector i) => i;

    public static ByteVector operator +(ByteVector a, IVector<byte> b) => a.Do(Operator.Add, b);

    public static ByteVector operator ++(ByteVector i) => i.Do(Operator.Add, (byte)1);

    public static ByteVector operator -(ByteVector i) => i.New(j => (byte.MaxValue - j).ToByte());

    public static ByteVector operator -(ByteVector a, IVector<byte> b) => a.Do(Operator.Subtract, b);

    public static ByteVector operator --(ByteVector i) => i.Do(Operator.Subtract, (byte)1);

    public static ByteVector operator *(ByteVector a, IVector<byte> b) => a.Do(Operator.Multiply, b);

    public static ByteVector operator /(ByteVector a, IVector<byte> b) => a.Do(Operator.Divide, b);

    public static ByteVector operator %(ByteVector a, IVector<byte> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public byte[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<ByteVector, byte>.GetArray() => new byte[Length];

    static ByteVector IArray<ByteVector, byte>.Create(ByteVector oldSelf, Array newSelf) => new((byte[])newSelf);

    /// <see cref="IEnumerable{}"/>

    public IEnumerator<byte> GetEnumerator() => (_Value as IEnumerable<byte>).GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IVector.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static ByteVector IVector<ByteVector, byte>.Create(VectorType type, IEnumerable<byte> value) => new(type, value);
}