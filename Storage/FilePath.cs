using Ion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ion.Storage;

/// <summary>A file or folder path.</summary>
/// <param name="Path">A file or folder path.</param>
public readonly record struct FilePath(string Path) : IFormattable
{
    /// <see cref="Region.Field"/>

    public const string DefaultCloneFormat = "{0} [{1}]";

    public const string Root = @"\";

    public const string RootName = "This PC";

    public const string StringFormat = "{0}";

    /// <see cref="Region.Property"/>

    public static IReadOnlyCollection<char> InvalidCharacters { get; } = System.IO.Path.GetInvalidFileNameChars();

    private readonly string Path { get; } = Path;

    /// <see cref="Region.Operator"/>

    public static implicit operator string(FilePath i) => i.Path;

    public static implicit operator FilePath(string i) => new(i);

    /// <see cref="Region.Method"/>

    public static string CleanName(string fileName)
        => System.IO.Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));

    /// <summary>Get a clone of the file path.</summary>
    /// <param name="filePath">A file path.</param>
    /// <param name="nameFormat">How to format the file name.</param>
    /// <param name="exists">How to check if a file exists.</param>
    /// <returns>A clone of file path.</returns>
    public static string CloneName(string filePath, string nameFormat, Predicate<string> exists)
    {
        var parent = System.IO.Path.GetDirectoryName(filePath);

        var extension = System.IO.Path.GetExtension(filePath);
        var name = System.IO.Path.GetFileNameWithoutExtension(filePath);

        var n = name;
        string result() => $@"{parent}\{n}{extension}".Replace(@"\\", @"\");

        var i = 0;
        while (exists(result()))
        {
            n = nameFormat.F(name, i);
            i++;
        }

        return result();
    }

    /// <summary>Get the file extension of a file path (starting with last <see cref="char"/>, everything after first period, if one is found).</summary>
    /// <param name="path">A file path.</param>
    /// <returns>A file extension.</returns>
    public static string GetExtension(string path)
    {
        if (path.IsEmpty())
            return null;

        var f = string.Empty;
        for (var i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] == '.')
                return f.IsEmpty() ? null : f;

            f = $"{path[i]}{f}";
        }
        return null;
    }

    /// <see cref="IFormattable"/>

    public readonly override string ToString() => ToString(Numeral.NumberFormat.General, CultureInfo.CurrentCulture);

    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString(string format, IFormatProvider provider)
    {
        return format switch
        {
            FilePathFormat.ExtensionWithout => StringFormat.F($@"{System.IO.Path.GetDirectoryName(Path)}\{System.IO.Path.GetFileNameWithoutExtension(Path)}"),
            _ => StringFormat.F(Path),
        };
    }
}