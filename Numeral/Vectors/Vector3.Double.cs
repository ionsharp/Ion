using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Ion.Numeral;

/// <inheritdoc cref="IVector3{}"/>
[Description(IVector3.Description)]
public readonly record struct Vector3
    : IVector3<Vector3, double>, IVectorAlias<Vector3<double>, double>, IVectorImmutable<double>, IVectorShort<double>, IVectorUnfixedAlias<Vector3, Vector, double>, System.Numerics.IMinMaxValue<Vector3>
{
    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="double.MaxValue"/>
    public static Vector3 MaxValue => new(double.MaxValue);

    /// <inheritdoc cref="double.MinValue"/>
    public static Vector3 MinValue => new(double.MinValue);

    ///

    public static Vector3 One => new(1);

    public static Vector3 Zero => new(0);

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="IVector2.X"/>
    public readonly double X { get; }

    /// <inheritdoc cref="IVector2.Y"/>
    public readonly double Y { get; }

    /// <inheritdoc cref="IVector3.Z"/>
    public readonly double Z { get; }

    /// <inheritdoc cref="IVector3{T}.XY"/>
    public readonly (double X, double Y) XY => (X, Y);

    /// <inheritdoc cref="VectorType"/>
    public readonly VectorType Type { get; }

    /// <see cref="Region.Constructor"/>
    #region

    /// <inheritdoc cref="IVector3.Format{double}(in IEnumerable{double}, VectorType)"/>
    private Vector3((double X, double Y, double Z, VectorType Type) i) => (X, Y, Z, Type) = i;

    /// <inheritdoc cref="IVector3.Format{double}(in double, VectorType)"/>
    public Vector3(double xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{double}(in double, in double, VectorType)"/>
    public Vector3(double x,  double y, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, type);

    /// <inheritdoc cref="IVector3.Format{double}(in ValueTuple{double, double}, VectorType)"/>
    public Vector3((double x, double y) xy, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{double}(in double, in double, in double, VectorType)"/>
    public Vector3(double x,  double y,  double z, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(x, y, z, type);

    /// <inheritdoc cref="IVector3.Format{double}(in ValueTuple{double, double, double}, VectorType)"/>
    public Vector3((double x, double y, double z) xyz, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    /// <inheritdoc cref="IVector3.Format{double}(in ValueTuple{double, double, double}, VectorType)"/>
    public Vector3(in (double x, double y, double z, double w) xyzw, VectorType type = IVector.DefaultType)
        => (X, Y, Z, Type) = IVector3.Format(xyzw, type);

    /// <inheritdoc cref="IVector3.Format{double}(in IVector2{double}, VectorType?)"/>
    public Vector3(in IVector2<double> xy, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xy, type);

    /// <inheritdoc cref="IVector3.Format{double}(in IVector3{double}, VectorType?)"/>
    public Vector3(in IVector3<double> xyz, VectorType? type = null)
        => (X, Y, Z, Type) = IVector3.Format(xyz, type);

    #endregion

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator Vector(Vector3 i) => new(i);

    public static implicit operator Vector<double>(Vector3 i) => new(i);

    public static implicit operator VectorM<double>(Vector3 i) => new(i);

    public static implicit operator Vector2<double>(Vector3 i) => new(i);

    public static implicit operator Vector2M<double>(Vector3 i) => new(i);

    public static implicit operator Vector3<double>(Vector3 i) => new(i);

    public static implicit operator Vector3M<double>(Vector3 i) => new(i);

    public static implicit operator Vector4<double>(Vector3 i) => new(i);

    public static implicit operator Vector4M<double>(Vector3 i) => new(i);

    ///

    public static implicit operator Vector3(Vector2 i) => new(i);

    public static implicit operator Vector3(Vector4 i) => new(i);

    public static implicit operator Vector3(Vector2<double> i) => new(i);

    public static implicit operator Vector3(Vector2M<double> i) => new(i);

    public static implicit operator Vector3(Vector3<double> i) => new(i);

    public static implicit operator Vector3(Vector3M<double> i) => new(i);

    public static implicit operator Vector3(Vector4<double> i) => new(i);

    public static implicit operator Vector3(Vector4M<double> i) => new(i);

    public static implicit operator Vector3((double X, double Y) i) => new(i.X, i.Y);

    public static implicit operator Vector3((double X, double Y, double Z) i) => new(i);

    public static implicit operator Vector3((double X, double Y, double Z, double W) i) => new(i);

    ///

    public static Vector3 operator +(Vector3 a, double b) => a.Do(Operator.Add, b);

    public static Vector3 operator -(Vector3 a, double b) => a.Do(Operator.Subtract, b);

    public static Vector3 operator *(Vector3 a, double b) => a.Do(Operator.Multiply, b);

    public static Vector3 operator /(Vector3 a, double b) => a.Do(Operator.Divide, b);

    public static Vector3 operator %(Vector3 a, double b) => a.Do(Operator.Modulo, b);

    ///

    public static Vector3 operator +(Vector3 i) => i;

    public static Vector3 operator +(Vector3 a, IVector3<double> b) => a.Do(Operator.Add, b);

    public static Vector3 operator ++(Vector3 i) => i.Do(Operator.Add, 1);

    public static Vector3 operator -(Vector3 i) => i.Do(Operator.Multiply, -1);

    public static Vector3 operator -(Vector3 a, IVector3<double> b) => a.Do(Operator.Subtract, b);

    public static Vector3 operator --(Vector3 i) => i.Do(Operator.Subtract, 1);

    public static Vector3 operator *(Vector3 a, IVector3<double> b) => a.Do(Operator.Multiply, b);

    public static Vector3 operator /(Vector3 a, IVector3<double> b) => a.Do(Operator.Divide, b);

    public static Vector3 operator %(Vector3 a, IVector3<double> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray1D"/>

    object[] IArray1D.ToArray() => [.. ToArray()];

    /// <inheritdoc cref="IArray1D{T}.ToArray()"/>
    public double[] ToArray() => [X, Y, Z];

    /// <see cref="IArray{,}"/>

    static Vector3 IArray<Vector3, double>.Create(Vector3 oldSelf, Array newSelf) => new(IVector3.Format(oldSelf, newSelf));

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<double>).GetEnumerator();

    readonly IEnumerator<double> IEnumerable<double>.GetEnumerator() => (ToArray() as IEnumerable<double>).GetEnumerator();

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => IVector3.ToString(this, format, provider);

    /// <see cref="IVector{,}"/>

    static Vector3 IVector<Vector3, double>.Create(VectorType type, IEnumerable<double> value) => new(IVector3.Format(value, type));
}