using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Ion.Numeral;

/// <remarks>Extends <see cref="Math"/>.</remarks>
/// <inheritdoc cref="Math"/>
[Extend(typeof(Math))]
public static partial class Number
{
    /// <see cref="Region.Field"/>

    private static readonly Random random = new();

    /// <see cref="Region.Method"/>

    /// Odds
    #region

    /// <summary>
    /// Get odds based on 50/50 probability.
    /// </summary>
    public static bool Odds<T>() where T : INumber<T> => Random(2) == 1;

    /// <summary>Get odds based on given probability.</summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static bool Odds<T>(T odds) 
        where T : IMinMaxValue<T>, INumber<T>
    {
        Throw.IfNotNormalized(odds, nameof(odds));
        return random.NextDouble() < odds.ToDouble();
    }

    #endregion

    /// Random
    #region

    /// <summary>
    /// Range ≡ <see cref="INumberBase{TSelf}.Zero"/> ⇒ <see cref="IMinMaxValue{TSelf}.MaxValue"/>
    /// </summary>
    public static T Random<T>() where T : IMinMaxValue<T>, INumber<T>
        => Random(T.Zero, T.MaxValue);

    /// <summary>
    /// Range ≡ <see cref="INumberBase{TSelf}.Zero"/> ⇒ <b>T</b> maximum
    /// </summary>
    public static T Random<T>(T maximum) where T : INumber<T>
        => Random(T.Zero, maximum);

    /// <summary>
    /// Range ≡ <b>T</b> minimum ⇒ <b>T</b> maximum
    /// </summary>
    public static T Random<T>(T minimum, T maximum) where T : INumber<T>
        => (random.NextDouble() * (maximum.ToDouble() - minimum.ToDouble()) + minimum.ToDouble()).Create<T>();

    /// <summary>
    /// Range ≡ <see cref="IRange{T}"/> range
    /// </summary>
    public static T Random<T>(in IRange<T> range) where T : INumber<T>
        => Random(range.Minimum, range.Maximum);

    public static T Random<T>(in IEnumerable<T> i) where T : INumber<T>
        => i.OrderBy(j => random.Next()).Take(1).FirstOrDefault<T>();

    /// <summary>Get a random sequence of characters with a given minimum and maximum length.</summary>
    /// <param name="characters">The characters that the sequence will be comprised of.</param>
    /// <param name="minimum">The minimum length of the character sequence.</param>
    /// <param name="maximum">The maximum length of the character sequence.</param>
    /// <param name="unique">Only include unique characters (length may shorten).</param>
    /// <returns>A random sequence of characters with the given minimum and maximum length.</returns>
    public static string Random(this string characters, int minimum, int maximum, bool unique = false)
    {
        var x = new char[maximum];
        int setLength = characters.Length;

        int length = Random(minimum, maximum + 1);
        for (int i = 0; i < length; ++i)
            x[i] = characters[Random(setLength)];

        string y = new string(x, 0, length);
        return unique ? string.Concat(y.Distinct()) : y;
    }

    #endregion
}