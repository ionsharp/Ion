using System;

namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector3"/> of <see cref="byte"/>.
/// </summary>
public interface IVector3Byte : IVector2Byte, IVector3<byte>
{
    public byte B { get; }

    public (byte R, byte G) RG { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format()
        => Format(byte.MinValue);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(byte rgb, VectorType type)
        => Format(rgb, rgb, rgb, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(byte r, byte g, VectorType type)
        => Format(r, g, default, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format((byte r, byte g) rg, VectorType type)
        => Format(rg.r, rg.g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(byte r, byte g, byte b, VectorType type)
        => (r, g, b, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format((byte r, byte g, byte b) rgb, VectorType type)
        => Format(rgb.r, rgb.g, rgb.b, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(byte r, byte g, byte b, byte a, VectorType type)
        => Format(r, g, b, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format((byte r, byte g, byte b, byte a) rgba, VectorType type)
        => Format(rgba.r, rgba.g, rgba.b, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(in IVector2<byte> rg, VectorType? type)
    {
        Throw.IfNull(rg, nameof(rg));
        return (rg.X, rg.Y, byte.MinValue, type ?? rg.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(in IVector3<byte> rgb, VectorType? type)
    {
        Throw.IfNull(rgb, nameof(rgb));
        return (rgb.X, rgb.Y, rgb.Z, type ?? rgb.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(in IVector4<byte> rgba, VectorType? type)
    {
        Throw.IfNull(rgba, nameof(rgba));
        return (rgba.X, rgba.Y, rgba.Z, type ?? rgba.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector3Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, VectorType Type) Format(in String i, VectorType type)
    {
        Throw.IfNullOrEmpty(i, nameof(i));
        var j = IVector4Byte.Parse(i, type);
        return (j.X, j.Y, j.Z, type);
    }
}