using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <inheritdoc cref="IVector2{}"/>
[Description(IVector2.Description)]
public readonly record struct Vector2
    : IVector2<Vector2, double>, IVectorAlias<Vector2<double>, double>, IVectorImmutable<double>, IVectorShort<double>, IVectorUnfixedAlias<Vector2, Vector, double>, System.Numerics.IMinMaxValue<Vector2>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="double.MaxValue"/>
    public static Vector2 MaxValue => new(double.MaxValue);

    /// <inheritdoc cref="double.MinValue"/>
    public static Vector2 MinValue => new(double.MinValue);

    ///

    public static Vector2 One => new(1);

    public static Vector2 Zero => new(0);

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector2.X"/>
    public readonly double X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly double Y { get; }

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector2.Format{double}(in IEnumerable{double}, VectorType)"/>
    private Vector2((double X, double Y, VectorType Type) i) => (X, Y, Type) = i;

    /// <inheritdoc cref="IVector2.Format{double}(in double, VectorType)"/>
    public Vector2(double xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{double}(in double, in double, VectorType)"/>
    public Vector2(double x, double y, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(x, y, type);

    /// <inheritdoc cref="IVector2.Format{double}(in ValueTuple{double, double}, VectorType)"/>
    public Vector2((double X, double Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xy, type);

    /// <inheritdoc cref="IVector2.Format{double}(in ValueTuple{double, double, double}, VectorType)"/>
    public Vector2((double X, double Y, double Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyz, type);

    /// <inheritdoc cref="IVector2.Format{double}(in ValueTuple{double, double, double, double}, VectorType)"/>
    public Vector2((double X, double Y, double Z, double W) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Type) = IVector2.Format(xyzw, type);

    /// <inheritdoc cref="IVector2.Format{double}(in IVector2{double}, VectorType?)"/>
    public Vector2(in IVector2<double> xy, VectorType? type = null)
        => (X, Y, Type) = IVector2.Format(xy, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Vector(Vector2 i) => new(i);

    public static implicit operator Vector<double>(Vector2 i) => new(i);

    public static implicit operator VectorM<double>(Vector2 i) => new(i);

    public static implicit operator Vector2<double>(Vector2 i) => new(i);

    public static implicit operator Vector2M<double>(Vector2 i) => new(i);

    public static implicit operator Vector3<double>(Vector2 i) => new(i);

    public static implicit operator Vector3M<double>(Vector2 i) => new(i);

    public static implicit operator Vector4<double>(Vector2 i) => new(i);

    public static implicit operator Vector4M<double>(Vector2 i) => new(i);

    ///

    public static implicit operator Vector2(Vector3 i) => new(i);

    public static implicit operator Vector2(Vector4 i) => new(i);

    public static implicit operator Vector2(Vector2<double> i) => new(i);

    public static implicit operator Vector2(Vector2M<double> i) => new(i);

    public static implicit operator Vector2(Vector3<double> i) => new(i);

    public static implicit operator Vector2(Vector3M<double> i) => new(i);

    public static implicit operator Vector2(Vector4<double> i) => new(i);

    public static implicit operator Vector2(Vector4M<double> i) => new(i);

    public static implicit operator Vector2((double X, double Y) i) => new(i.X, i.Y);

    public static implicit operator Vector2((double X, double Y, double Z) i) => new(i);

    public static implicit operator Vector2((double X, double Y, double Z, double W) i) => new(i);

    ///

    public static Vector2 operator +(Vector2 a, double b) => a.Do(Operator.Add, b);

    public static Vector2 operator -(Vector2 a, double b) => a.Do(Operator.Subtract, b);

    public static Vector2 operator *(Vector2 a, double b) => a.Do(Operator.Multiply, b);

    public static Vector2 operator /(Vector2 a, double b) => a.Do(Operator.Divide, b);

    public static Vector2 operator %(Vector2 a, double b) => a.Do(Operator.Modulo, b);

    ///

    public static Vector2 operator +(Vector2 i) => i;

    public static Vector2 operator +(Vector2 a, IVector2<double> b) => a.Do(Operator.Add, b);

    public static Vector2 operator ++(Vector2 i) => i.Do(Operator.Add, 1);

    public static Vector2 operator -(Vector2 i) => i.Do(Operator.Multiply, -1);

    public static Vector2 operator -(Vector2 a, IVector2<double> b) => a.Do(Operator.Subtract, b);

    public static Vector2 operator --(Vector2 i) => i.Do(Operator.Subtract, 1);

    public static Vector2 operator *(Vector2 a, IVector2<double> b) => a.Do(Operator.Multiply, b);

    public static Vector2 operator /(Vector2 a, IVector2<double> b) => a.Do(Operator.Divide, b);

    public static Vector2 operator %(Vector2 a, IVector2<double> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public double[] ToArray() => [X, Y];

    /// <see cref="IArray{,}"/>

    static Vector2 IArray<Vector2, double>.Create(Vector2 oldSelf, Array newSelf) => new(IVector2.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<double>).GetEnumerator();

    readonly IEnumerator<double> IEnumerable<double>.GetEnumerator() => (ToArray() as IEnumerable<double>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector2.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector2 IVector<Vector2, double>.Create(VectorType type, IEnumerable<double> value) => new(IVector2.Format(value, type));
}