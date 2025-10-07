using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ion.Numeral;

/// <summary>
/// A set of lines that form a unique pattern.
/// </summary>
public interface IPattern : IArray1D, IArray1DRank, IEnumerable, IFormattable
{
    public const string Description = "A set of lines that form a unique pattern";

    public const string StringFormat = "Line: {0}";

    new public int Length { get; }

    int IArray.Length => Length;

    int IArray1D.XLength => Length;

    public static bool Equals<T>(IPattern<T> i, IPattern<T> j) where T : ILine
    {
        if (XEquatable.Check(i, j))
        {
            if (i.Length == j.Length)
            {
                for (var k = 0; k < i.Length; k++)
                {
                    if (!EqualityComparer<T>.Default.Equals(i[k], j[k]))
                        return false;
                }
                return true;
            }
        }
        return false;
    }

    public static int GetHashCode<T>(IPattern<T> i) where T : ILine
    {
        unchecked
        {
            var hash = Number.Prime19;
            foreach (var j in i)
                hash = hash * 31 + j.GetHashCode();

            return hash;
        }
    }

    public static string ToString<T>(IPattern<T> i, string format, IFormatProvider provider) where T : ILine
    {
        var result = new StringBuilder();
        i.ForEach(j => result.AppendLine(string.Format(StringFormat, j.ToString(format, provider))));
        return result.ToString();
    }
}

/// <inheritdoc/>
public interface IPattern<T> : IPattern, IArray1D<T>, IArray1DRank<T>, IEnumerable<T> where T : ILine;

/// <inheritdoc/>
public interface IPattern<TSelf, TValue> : IPattern<TValue>, IArray1D<TSelf, TValue> where TSelf : IPattern<TSelf, TValue> where TValue : ILine;