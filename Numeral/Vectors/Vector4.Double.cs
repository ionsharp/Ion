using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <inheritdoc cref="IVector4{}"/>
[Description(IVector4.Description)]
public readonly record struct Vector4
    : IVector4<Vector4, double>, IVectorAlias<Vector4<double>, double>, IVectorImmutable<double>, IVectorShort<double>, IVectorUnfixedAlias<Vector4, Vector, double>, System.Numerics.IMinMaxValue<Vector4>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="double.MaxValue"/>
    public static Vector4 MaxValue => new(double.MaxValue);

    /// <inheritdoc cref="double.MinValue"/>
    public static Vector4 MinValue => new(double.MinValue);

    ///

    public static Vector4 One => new(1);

    public static Vector4 Zero => new(0);

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector2.X"/>
    public readonly double X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly double Y { get; }

    /// <inheritdoc cref="IVector3.Z"/>
    public readonly double Z { get; }

    /// <inheritdoc cref="IVector4.W"/>
    public readonly double W { get; }

    /// <inheritdoc cref="IVector4{T}.XY"/>
    public readonly (double X, double Y) XY => (X, Y);

    /// <inheritdoc cref="IVector4{T}.XYZ"/>
    public readonly (double X, double Y, double Z) XYZ => (X, Y, Z);

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector4.Format{double}(IEnumerable{double}, VectorType)"/>
    private Vector4((double X, double Y, double Z, double W, VectorType Type) i) => (X, Y, Z, W, Type) = i;

    /// <inheritdoc cref="IVector4.Format{double}(in double, VectorType)"/>
    public Vector4(double xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{double}(in double, in double, VectorType)"/>
    public Vector4(double x,  double y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, type);

    /// <inheritdoc cref="IVector4.Format{double}(in ValueTuple{double, double}, VectorType)"/>
    public Vector4((double X, double Y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{double}(in double, in double, in double, VectorType)"/>
    public Vector4(double x, in double y, in double z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, type);

    /// <inheritdoc cref="IVector4.Format{double}(in ValueTuple{double, double, double}, VectorType)"/>
    public Vector4((double X, double Y, double Z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{double}(in double, in double, in double, in double, VectorType)"/>
    public Vector4(double x,  double y,  double z,  double w, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(x, y, z, w, type);

    /// <inheritdoc cref="IVector4.Format{double}(in ValueTuple{double, double, double, double}, VectorType)"/>
    public Vector4((double X, double Y, double Z, double W) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    /// <inheritdoc cref="IVector4.Format{double}(in IVector2{double}, VectorType?)"/>
    public Vector4(in IVector2<double> xy, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xy, type);

    /// <inheritdoc cref="IVector4.Format{double}(in IVector3{double}, VectorType?)"/>
    public Vector4(in IVector3<double> xyz, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyz, type);

    /// <inheritdoc cref="IVector4.Format{double}(in IVector4{double}, VectorType?)"/>
    public Vector4(in IVector4<double> xyzw, VectorType? type = null)
        => (X, Y, Z, W, Type) = IVector4.Format(xyzw, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Vector(Vector4 i) => new(i);

    public static implicit operator Vector<double>(Vector4 i) => new(i);

    public static implicit operator VectorM<double>(Vector4 i) => new(i);

    public static implicit operator Vector2<double>(Vector4 i) => new(i);

    public static implicit operator Vector2M<double>(Vector4 i) => new(i);

    public static implicit operator Vector3<double>(Vector4 i) => new(i);

    public static implicit operator Vector3M<double>(Vector4 i) => new(i);

    public static implicit operator Vector4<double>(Vector4 i) => new(i);

    public static implicit operator Vector4M<double>(Vector4 i) => new(i);

    ///

    public static implicit operator Vector4(Vector2 i) => new(i);

    public static implicit operator Vector4(Vector3 i) => new(i);

    public static implicit operator Vector4(Vector2<double> i) => new(i);

    public static implicit operator Vector4(Vector2M<double> i) => new(i);

    public static implicit operator Vector4(Vector3<double> i) => new(i);

    public static implicit operator Vector4(Vector3M<double> i) => new(i);

    public static implicit operator Vector4(Vector4<double> i) => new(i);

    public static implicit operator Vector4(Vector4M<double> i) => new(i);

    public static implicit operator Vector4((double X, double Y) i) => new(i.X, i.Y);

    public static implicit operator Vector4((double X, double Y, double Z) i) => new(i);

    public static implicit operator Vector4((double X, double Y, double Z, double W) i) => new(i);

    ///

    public static Vector4 operator +(Vector4 a, double b) => a.Do(Operator.Add, b);

    public static Vector4 operator -(Vector4 a, double b) => a.Do(Operator.Subtract, b);

    public static Vector4 operator *(Vector4 a, double b) => a.Do(Operator.Multiply, b);

    public static Vector4 operator /(Vector4 a, double b) => a.Do(Operator.Divide, b);

    public static Vector4 operator %(Vector4 a, double b) => a.Do(Operator.Modulo, b);

    ///

    public static Vector4 operator +(Vector4 i) => i;

    public static Vector4 operator +(Vector4 a, IVector4<double> b) => a.Do(Operator.Add, b);

    public static Vector4 operator ++(Vector4 i) => i.Do(Operator.Add, 1);

    public static Vector4 operator -(Vector4 i) => i.Do(Operator.Multiply, -1);

    public static Vector4 operator -(Vector4 a, IVector4<double> b) => a.Do(Operator.Subtract, b);

    public static Vector4 operator --(Vector4 i) => i.Do(Operator.Subtract, 1);

    public static Vector4 operator *(Vector4 a, IVector4<double> b) => a.Do(Operator.Multiply, b);

    public static Vector4 operator /(Vector4 a, IVector4<double> b) => a.Do(Operator.Divide, b);

    public static Vector4 operator %(Vector4 a, IVector4<double> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public double[] ToArray() => [X, Y, Z, W];

    /// <see cref="IArray{,}"/>

    static Vector4 IArray<Vector4, double>.Create(Vector4 oldSelf, Array newSelf) => new(IVector4.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<double>).GetEnumerator();

    readonly IEnumerator<double> IEnumerable<double>.GetEnumerator() => (ToArray() as IEnumerable<double>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector4.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector4 IVector<Vector4, double>.Create(VectorType type, IEnumerable<double> value) => new(IVector4.Format(value, type));
}