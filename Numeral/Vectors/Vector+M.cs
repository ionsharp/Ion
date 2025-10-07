using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ion.Numeral;

/// <remarks>See <see cref="Vector{}"/> for immutable equivalent.</remarks>
/// <inheritdoc cref="IVectorMutable"/>
[CollectionBuilder(typeof(VectorMutableBuilder), nameof(VectorMutableBuilder.Create))]
[Description(IVector.Description)]
public record class VectorM<T>
    : IVector<VectorM<T>, T>, IVectorAlias<VectorM<T>, T>, IVectorLong<T>, IVectorMutable<T>, IVectorUnfixed<T>, IVectorUnfixedAlias<VectorM<T>, VectorM<T>, T>
{    
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector.Length"/>
    public int Length => _Value.Length;

    /// <inheritdoc cref="IVector.Type"/>
    public VectorType Type { get; }

    protected T[] _Value;

    /// <see cref="Region.Property.Indexor"/>

    public T this[int i] => _Value[i];

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector.Format{T}(VectorType, in IEnumerable{T})"/>
    public VectorM(VectorType type, in IEnumerable<T> i)
        => (_Value, Type) = IVector.Format(type, i);

    /// <inheritdoc cref="IVector.Format{T}(in IEnumerable{T}, VectorType)"/>
    public VectorM(in IEnumerable<T> i, VectorType type)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(T[])"/>
    public VectorM(params T[] i)
        => (_Value, Type) = IVector.Format(i);

    /// <inheritdoc cref="IVector.Format{T}(in IArray1D{T}, VectorType)"/>
    public VectorM(in IArray1D<T> i, VectorType type = default)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IMatrix{T}, VectorType?)"/>
    public VectorM(in IMatrix<T> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    /// <inheritdoc cref="IVector.Format{T}(in IVector{T}, VectorType?)"/>
    public VectorM(in IVector<T> i, VectorType? type = null)
        => (_Value, Type) = IVector.Format(i, type);

    #endregion

    /// <see cref="Region.Operator"/>

    public static implicit operator VectorM<T>(T[] i) => new(i);

    public static implicit operator VectorM<T>(Vector<T> i) => new(i);

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. _Value];

    public T[] ToArray() => [.. _Value];

    /// <see cref="IArray{,}"/>

    Array IArray<VectorM<T>, T>.GetArray() => _Value;

    static VectorM<T> IArray<VectorM<T>, T>.Create(VectorM<T> oldSelf, Array newSelf)
    {
        if (newSelf is T[] result)
            oldSelf._Value = result;

        return oldSelf;
    }

    /// <see cref="IEnumerable{}"/>

    public IEnumerator<T> GetEnumerator() => (_Value as IEnumerable<T>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _Value.GetEnumerator();

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IVector.ToString{T}(IVector{T}, string, IFormatProvider)"/>
    public string ToString(string format, IFormatProvider provider) => IVector.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static VectorM<T> IVector<VectorM<T>, T>.Create(VectorType type, IEnumerable<T> value) => new(type, value);
}