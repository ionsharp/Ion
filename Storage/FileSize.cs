using Ion.Numeral;
using System;
using System.Globalization;
using System.Numerics;

namespace Ion.Storage;

public readonly record struct FileSize(long Value) : IFormattable, IMinMaxValue<FileSize>
{
    public const string Byte = "B";

    public const string StringFormat = "{0} {1}"; /// 0 B

    public const long Upper = 1000;

    public const long UpperBinary = 1024;

    public static readonly string[] Label = [Byte, "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB"];

    public static readonly string[] LabelSI = [Byte, "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

    public static readonly long Max = ulong.MaxValue.ToInt64();

    public static readonly long Min = ulong.MinValue.ToInt64();

    /// <inheritdoc cref="ulong.MaxValue"/>
    public static FileSize MaxValue => new(Max);

    /// <inheritdoc cref="ulong.MinValue"/>
    public static FileSize MinValue => new(Min);

    public readonly long Value { get; } = Value.Clamp(Min, Max);

    /// <see cref="Region.Operator"/>
    #region

    public static implicit operator FileSize(long i) => new(i);

    public static implicit operator FileSize(int i) => new(i);

    public static implicit operator long(FileSize i) => i.Value;

    public static implicit operator int(FileSize i) => i.Value.ToInt32();

    ///

    public static bool operator <(FileSize a, FileSize b) => a.Value < b.Value;

    public static bool operator >(FileSize a, FileSize b) => a.Value > b.Value;

    public static bool operator <=(FileSize a, FileSize b) => a.Value <= b.Value;

    public static bool operator >=(FileSize a, FileSize b) => a.Value >= b.Value;

    ///

    public static FileSize operator +(FileSize i) => +i.Value;

    public static FileSize operator +(FileSize a, FileSize b) => a.Value + b.Value;

    public static FileSize operator ++(FileSize i) => i.Value + 1;

    public static FileSize operator -(FileSize i) => i.Value;

    public static FileSize operator -(FileSize a, FileSize b) => a.Value - b.Value;

    public static FileSize operator --(FileSize i) => i.Value - 1;

    public static FileSize operator /(FileSize a, FileSize b) => a.Value / b.Value;

    public static FileSize operator *(FileSize a, FileSize b) => a.Value * b.Value;

    public static FileSize operator %(FileSize a, FileSize b) => a.Value % b.Value;

    #endregion

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(Text.StringFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => ToString(FileSizeFormat.BinaryUsingSI, 1, format, provider);

    public readonly string ToString(FileSizeFormat sizeFormat, int round = 1, string format = null, IFormatProvider provider = null)
    {
        if (sizeFormat == FileSizeFormat.Bytes)
            return Value.ToString(format, provider);

        var label = sizeFormat switch { FileSizeFormat.BinaryUsingSI => LabelSI, FileSizeFormat.DecimalUsingSI => LabelSI, FileSizeFormat.IECBinary => Label };
        if (Value == 0)
            return StringFormat.F(0.ToString(format, provider), Byte);

        var f = sizeFormat == FileSizeFormat.BinaryUsingSI || sizeFormat == FileSizeFormat.IECBinary ? UpperBinary : Upper;

        var m = (int)Math.Log(Value, f);
        var a = (decimal)Value / (1L << (m * 10));

        if (Math.Round(a, round) >= 1000)
        {
            m += 1;
            a /= f;
        }

        format ??= "n" + round;
        return StringFormat.F(a.ToString(format, provider), label[m]);
    }
}