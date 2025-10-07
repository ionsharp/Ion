using System;
using System.Globalization;

namespace Ion.Storage;

/// <summary>
/// A separated list of file or folder extensions.
/// </summary>
public readonly record struct FileExtensions : IFormattable
{
    /// <see cref="Region.Field"/>

    public const char Delimiter = ';';

    public const string StringFormat = "{0}";

    public const string StringFormatDelimit = ", ";

    public static readonly FileExtensions Empty = new(string.Empty);

    /// <see cref="Region.Property"/>

    private readonly string Value { get; }

    public readonly int Count { get; }

    /// <see cref="Region.Constructor"/>

    /// <exception cref="ArgumentNullException"/>
    public FileExtensions(string i) : this([i]) { }

    /// <exception cref="ArgumentNullException"/>
    public FileExtensions(params string[] i)
    {
        Throw.IfNull(i, nameof(i));

        Value = string.Join(Delimiter, i);
        Count = ToArray().Length;
    }

    public static implicit operator string(FileExtensions i) => i.Value;

    public static implicit operator FileExtensions(string i) => new(i);

    public static FileExtensions operator +(FileExtensions a, FileExtensions b) => $"{a}{Delimiter}{b}";

    /// <see cref="Region.Method"/>

    public readonly string[] ToArray() => Value.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(Numeral.NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider) => StringFormat.F(ToArray().ToString(StringFormatDelimit));
}