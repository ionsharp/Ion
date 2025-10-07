using System;

namespace Ion.Numeral;

/// <summary>
/// An <see cref="IVector4"/> of <see cref="byte"/>.
/// </summary>
public interface IVector4Byte : IVector3Byte, IVector4<byte>
{
    public byte A { get; }

    public (byte R, byte G, byte B) RGB { get; }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format()
        => Format(byte.MinValue, default);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(byte rgba, VectorType type)
        => Format(rgba, rgba, rgba, rgba, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(byte r, byte g, VectorType type)
        => Format(r, g, default, byte.MaxValue, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format((byte r, byte g) rg, VectorType type)
        => Format(rg.r, rg.g, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(byte r, byte g, byte b, VectorType type)
        => Format(r, g, b, byte.MaxValue, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format((byte r, byte g, byte b) rgb, VectorType type)
        => Format(rgb.r, rgb.g, rgb.b, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(byte r, byte g, byte b, byte a, VectorType type)
        => (r, g, b, a, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format((byte r, byte g, byte b, byte a) rgba, VectorType type)
        => Format(rgba.r, rgba.g, rgba.b, rgba.a, type);

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(in IVector2<byte> rg, VectorType? type)
    {
        Throw.IfNull(rg, nameof(rg));
        return (rg.X, rg.Y, byte.MinValue, byte.MaxValue, type ?? rg.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(in IVector3<byte> rgb, VectorType? type)
    {
        Throw.IfNull(rgb, nameof(rgb));
        return (rgb.X, rgb.Y, rgb.Z, byte.MaxValue, type ?? rgb.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(in IVector4<byte> rgba, VectorType? type)
    {
        Throw.IfNull(rgba, nameof(rgba));
        return (rgba.X, rgba.Y, rgba.Z, rgba.W, type ?? rgba.Type);
    }

    /// <summary>
    /// Get new instance compatible with <see cref="IVector4Byte"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    new public static (byte X, byte Y, byte Z, byte W, VectorType Type) Format(in String i, VectorType type)
    {
        Throw.IfNullOrEmpty(i, nameof(i));
        var j = Parse(i, type);
        return (j.X, j.Y, j.Z, j.W, type);
    }

    /// <summary>
    /// Get <see cref="ValueTuple"/>&lt;<see cref="byte"/>, <see cref="byte"/>, <see cref="byte"/>, <see cref="byte"/>> from given (<see cref="NumberBase.Hexadecimal"/>) <see cref="string"/>.
    /// </summary>
    /// <exception cref="VectorNotParsable"></exception>
    new public static (byte X, byte Y, byte Z, byte W) Parse(in String i, VectorType type = VectorType.X)
    {
        Throw.IfNull(i, nameof(i));
        Throw.If<VectorNotParsable>(i.Length == 0 || i.Length > 8);

        var style = System.Globalization.NumberStyles.HexNumber;

        /// #[000] => [000]

        var j = i[0] == '#' ? i[1..] : i;

        /// [000] => [000000] => [00000000]

        j = j.Length switch
        {
            3 => "{0}{1}{2}{3}"
                .F("FF", new string(j[0], 2), new string(j[1], 2), new string(j[2], 2)),
            6 => "{0}{1}"
                .F("FF", j),
            _ => string.Concat(j, new string('0', 8 - j.Length)),
        };

        /// [00000000] => [rgba]

        int x = int.Parse(j.Substring(2, 2), style),
            y = int.Parse(j.Substring(4, 2), style),
            z = int.Parse(j.Substring(6, 2), style),
            w = int.Parse(j[..2], style);

        return (Convert.ToByte(x), Convert.ToByte(y), Convert.ToByte(z), Convert.ToByte(w));
    }
}