using System;

namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector2"/> of <see cref="byte"/>.
/// </summary>
public interface IVector2Byte : IVectorByte, IVector2<byte>
{
    public byte R { get; }

    public byte G { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format()
        => Format(byte.MinValue, default);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format(byte i, VectorType type)
        => Format(i, i, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format(byte r, byte g, VectorType type)
        => (r, g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format((byte r, byte g) rg, VectorType type)
        => Format(rg.r, rg.g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format(byte r, byte g, byte b, VectorType type)
        => Format(r, g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format((byte r, byte g, byte b) rgb, VectorType type)
        => Format(rgb.r, rgb.g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format(byte r, byte g, byte b, byte a, VectorType type)
        => Format(r, g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    public static (byte X, byte Y, VectorType Type) Format((byte r, byte g, byte b, byte a) rgba, VectorType type)
        => Format(rgba.r, rgba.g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (byte X, byte Y, VectorType Type) Format(in IVector2<byte> rg, VectorType? type = null)
    {
        Throw.IfNull(rg, nameof(rg));
        return (rg.X, rg.Y, type ?? rg.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (byte X, byte Y, VectorType Type) Format(in IVector3<byte> rgb, VectorType? type = null)
    {
        Throw.IfNull(rgb, nameof(rgb));
        return (rgb.X, rgb.Y, type ?? rgb.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (byte X, byte Y, VectorType Type) Format(in IVector4<byte> rgba, VectorType? type = null)
    {
        Throw.IfNull(rgba, nameof(rgba));
        return (rgba.X, rgba.Y, type ?? rgba.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector2Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static (byte X, byte Y, VectorType Type) Format(in String i, VectorType type = IVector.DefaultType)
    {
        Throw.IfNullOrEmpty(i, nameof(i));
        var j = IVector4Byte.Parse(i, type);
        return (j.X, j.Y, type);
    }
}