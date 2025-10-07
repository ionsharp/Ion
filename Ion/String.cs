using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Ion;

/// <summary>
/// Extends <see cref="String"/>.
/// </summary>
[Extend<String>]
public static class XString
{
    /// <summary>Get the <see cref="string"/> that occurs after the given <see cref="string"/>.</summary>
    /// <returns>The <see cref="string"/> that occurs after the given <see cref="string"/>.</returns>
    public static string After(this string input, string i)
    {
        var pos_a = input.LastIndexOf(i);

        if (pos_a == -1)
            return string.Empty;

        var adjusted = pos_a + i.Length;

        return adjusted >= input.Length ? string.Empty : input[adjusted..];
    }

    /// <summary>Get the <see cref="string"/> that occurs before the given <see cref="string"/>.</summary>
    /// <returns>The <see cref="string"/> that occurs before the given <see cref="string"/>.</returns>
    public static string Before(this string input, string i)
    {
        var result = input.IndexOf(i);
        return result == -1 ? string.Empty : input[..result];
    }

    /// <summary>Get the <see cref="string"/> that occurs between the two given <see cref="string"/>s.</summary>
    /// <returns>The <see cref="string"/> that occurs between the two given <see cref="string"/>s.</returns>
    public static string Between(this string input, string a, string b)
    {
        var pos_a = input.IndexOf(a);
        var pos_b = input.LastIndexOf(b);

        if (pos_a == -1)
            return string.Empty;

        if (pos_b == -1)
            return string.Empty;

        var adjusted = pos_a + a.Length;
        return adjusted >= pos_b ? string.Empty : input[adjusted..pos_b];
    }

    /// <summary>Replace the <see cref="string"/> between two characters within another <see cref="string"/>.</summary>
    /// <param name="a">The first occurring character.</param>
    /// <param name="b">The second occurring character.</param>
    /// <param name="replace">The <see cref="string"/> to replace with.</param>
    public static string Between(this string input, char a, char b, string replace)
    {
        int? i0 = null;
        int? i1 = null;
        var length = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (i0 is null)
            {
                if (input[i] == a)
                    i0 = i;
            }
            else
            {
                if (input[i] == b)
                {
                    i1 = i;
                    break;
                }
                length++;
            }
        }

        if (i0 != null && i1 != null)
            return input[..(i0.Value + 1)] + replace + input[i1.Value..];

        return string.Empty;
    }

    /// <summary>Capitalize a <see cref="string"/>.</summary>
    /// <param name="i">A <see cref="string"/> to capitalize.</param>
    /// <returns>A capitalized <see cref="string"/>.</returns>
    /// <remarks>See <see cref="System.Globalization.TextInfo.ToTitleCase(string)"/></remarks>
    public static string Capitalize(this string i)
        => System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(i.ToLower());

    /// <summary>Get number of times a <see cref="char"/> occurs in a <see cref="string"/>.</summary>
    /// <param name="character">A <see cref="char"/> to check.</param>
    /// <returns>The number of times the <see cref="char"/> occurs in the <see cref="string"/>.</returns>
    public static int Count(this string i, char character)
    {
        var count = 0;
        foreach (var j in i)
        {
            if (j.Equals(character))
                count++;
        }
        return count;
    }

    /// <inheritdoc cref="string.Format(string, object[])"/>
    public static string F(this string input, params object[] arguments) => string.Format(input, arguments);

    /// <summary>Do an action for each line in a <see cref="string"/>.</summary>
    /// <param name="action">An action to do for each line in the <see cref="string"/>.</param>
    public static void ForEachLine(this string input, Action<string> action)
    {
        foreach (var i in input.GetLines())
            action(i);
    }

    /// <summary>Get a <see cref="bool"/> from a <see cref="string"/> (not case sensitive!).</summary>
    /// <remarks><para><b>False / F / 0</b> | <b>True / T / 1</b></para></remarks>
    /// <returns>A <see cref="bool"/>.</returns>
    public static bool? GetBoolean(this string input)
    {
        return input.ToLower() switch
        {
            "true" or "t" or "1" => true,
            "false" or "f" or "0" => false,
            _ => null,
        };
    }

    /// <summary>Get a <see cref="string"/> with spaces between each lower and upper character.</summary>
    /// <returns>A <see cref="string"/> with spaces between each lower and upper character.</returns>
    public static string GetCamel(this string input)
    {
        //New way: Previous character must be lowercase and current character must be uppercase.
        var result = new System.Text.StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            if (result.Length > 0)
            {
                if (char.IsLetter(input[i - 1]) && char.IsLetter(input[i]))
                {
                    if (char.IsLower(input[i - 1]) && char.IsUpper(input[i]))
                        _ = result.Append(' ');
                }
            }
            _ = result.Append(input[i]);
        }
        return result.ToString();
        //Old way: Regex.Replace(Regex.Replace(input, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static IEnumerable<string> GetLines(this string input)
    {
        if (input is null)
            yield break;

        using StringReader reader = new(input);
        string line;
        while ((line = reader.ReadLine()) != null)
            yield return line;
    }

    public static int GetLineCount(this string input) => input.Split('\n').Length;

    public static int GetWordCount(this string input)
        => input.Split((char[])[' ', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Length;

    /// <summary>Get if <see cref="string"/> is alphabetical.</summary>
    /// <param name="input">The <see cref="string"/> to check.</param>
    /// <returns>If the <see cref="string"/> is alphabetical.</returns>
    /// <remarks><see cref="Regex.IsMatch(string)"/>: <b>^[a-zA-Z]+$</b></remarks>
    public static bool IsAlpha(this string input)
        => Regex.IsMatch(input, @"^[a-zA-Z]+$");

    /// <summary>Get if <see cref="string"/> is alphanumerical.</summary>
    /// <param name="input">The <see cref="string"/> to check.</param>
    /// <returns>If the <see cref="string"/> is alphanumerical.</returns>
    /// <remarks><see cref="Regex.IsMatch(string)"/>: <b>^[a-zA-Z0-9]+$</b></remarks>
    public static bool IsAlphaNumeric(this string input)
        => Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");

    /// <inheritdoc cref="string.IsNullOrEmpty(string)"/>
    public static bool IsEmpty(this string input) => string.IsNullOrEmpty(input);

    /// <summary>Get if <see cref="string"/> is numeric.</summary>
    /// <param name="input">The <see cref="string"/> to check.</param>
    /// <returns>If the <see cref="string"/> is numerical.</returns>
    /// <remarks><see cref="Regex.IsMatch(string)"/>: <b>^[0-9]+$</b></remarks>
    public static bool IsNumeric(this string input) => Regex.IsMatch(input, @"^[0-9]+$");

    /// <inheritdoc cref="string.IsNullOrWhiteSpace(string)"/>
    public static bool IsWhite(this string input) => string.IsNullOrWhiteSpace(input);

    /// <summary>Get if the given character is the only occuring character.</summary>
    public static bool OnlyContains(this string input, char character)
    {
        if (input.IsEmpty())
            return false;

        foreach (var i in input)
        {
            if (!i.Equals(character))
                return false;
        }
        return true;
    }

    /// <inheritdoc cref="string.PadLeft(int, char)"/>
    public static string PadLeft(this string i, char j, int repeat) => i.PadLeft(i.Length + repeat, j);

    /// <inheritdoc cref="string.PadRight(int, char)"/>
    public static string PadRight(this string i, char j, int repeat) => i.PadRight(i.Length + repeat, j);

    /// <inheritdoc cref="Enum.Parse(Type, string, bool)"/>
    public static T Parse<T>(this string input, bool ignoreCase = true) where T : Enum
        => (T)Enum.Parse(typeof(T), input, ignoreCase);

    /// <inheritdoc cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/>
    public static bool TryParse<T>(this string input, out T result, bool ignoreCase = true) where T : struct, IFormattable, IComparable, IConvertible
        => Enum.TryParse(input, ignoreCase, out result);

    /// <summary>Remove digits from a <see cref="string"/>.</summary>
    /// <returns>A <see cref="string"/> with no digits.</returns>
    /// <remarks><see cref="Regex.Replace(string, string, string)"/>: <b>[\d-]</b></remarks>
    public static string TrimDigit(this string i) => Regex.Replace(i, @"[\d-]", string.Empty);

    /// <summary>Remove white space from a <see cref="string"/>.</summary>
    /// <returns>A <see cref="string"/> with no white space.</returns>
    /// <remarks><see cref="Regex.Replace(string, string, string)"/>: <b>\s+</b></remarks>
    public static string TrimWhite(this string i) => Regex.Replace(i, @"\s+", string.Empty);

    /// <summary>Remove trailing zeros from a <see cref="string"/>.</summary>
    /// <remarks>Example, <b>1.0.0.0</b> to <b>1.0</b>.</remarks>
    /// <returns>A <see cref="string"/> with no trailing zeros.</returns>
    public static string TrimZero(this string i)
    {
        var j = i;
        while (j.EndsWith(".0"))
            j = j[..^2];

        return j;
    }
}