using System;
using System.Globalization;

namespace Ion.Storage;

public readonly record struct FileFormat(string Extension, bool IsReadable, bool IsWritable) : IFormattable
{
    public readonly string Extension { get; } = Extension;

    public readonly bool IsReadable { get; } = IsReadable;

    public readonly bool IsWritable { get; } = IsWritable;

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(Text.StringFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => Extension;
}