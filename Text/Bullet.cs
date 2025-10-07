using Ion.Numeral;
using System;

namespace Ion.Text;

public enum Bullet
{
    [Hide]
    None,
    /// <summary>
    /// ■
    /// </summary>
    [Symbol("■")]
    Square,
    /// <summary>
    /// □
    /// </summary>
    [Symbol("□")]
    SquareOutline,
    /// <summary>
    /// ●
    /// </summary>
    [Symbol("●")]
    Circle,
    /// <summary>
    /// ○
    /// </summary>
    [Symbol("○")]
    CircleOutline,
    /// <summary>
    /// ◆
    /// </summary>
    [Symbol("◆")]
    Diamond,
    /// <summary>
    /// ◇
    /// </summary>
    [Symbol("◇")]
    DiamondOutline,
    /// <summary>
    /// A., B., C.
    /// </summary>
    [Symbol("A.")]
    LetterUpperPeriod,
    /// <summary>
    /// A), B), C)
    /// </summary>
    [Symbol("A)")]
    LetterUpperParenthesis,
    /// <summary>
    /// a., b., c.
    /// </summary>
    [Symbol("a.")]
    LetterLowerPeriod,
    /// <summary>
    /// a), b), c)
    /// </summary>
    [Symbol("a)")]
    LetterLowerParenthesis,
    /// <summary>
    /// 1., 2., 3.
    /// </summary>
    [Symbol("1.")]
    NumberPeriod,
    /// <summary>
    /// 1), 2), 3)
    /// </summary>
    [Symbol("1)")]
    NumberParenthesis,
    /// <summary>
    /// I., II., III
    /// </summary>
    [Symbol("I.")]
    RomanNumberUpperPeriod,
    /// <summary>
    /// I), II), III)
    /// </summary>
    [Symbol("I)")]
    RomanNumberUpperParenthesis,
    /// <summary>
    /// i., ii., iii.
    /// </summary>
    [Symbol("i.")]
    RomanNumberLowerPeriod,
    /// <summary>
    /// i), ii), iii)
    /// </summary>
    [Symbol("i)")]
    RomanNumberLowerParenthesis
}

[Extend<Bullet>]
public static class XBullet
{
    public static object ToString(this Bullet i, double index)
    {
        return Try.Get(() =>
        {
            var j = Convert.ToInt32(index);
            return i switch
            {
                Bullet.LetterUpperPeriod
                    => $"{new LetterNumberStyle().Convert(j)}.".ToUpper(),
                Bullet.LetterUpperParenthesis
                    => $"{new LetterNumberStyle().Convert(j)})".ToUpper(),
                Bullet.LetterLowerPeriod
                    => $"{new LetterNumberStyle().Convert(j)}.".ToLower(),
                Bullet.LetterLowerParenthesis
                    => $"{new LetterNumberStyle().Convert(j)})".ToLower(),
                Bullet.NumberPeriod
                    => $"{index}.",
                Bullet.NumberParenthesis
                    => $"{index})",
                Bullet.RomanNumberUpperPeriod
                    => $"{new RomanNumberStyle().Convert(j)}.".ToUpper(),
                Bullet.RomanNumberUpperParenthesis
                    => $"{new RomanNumberStyle().Convert(j)})".ToUpper(),
                Bullet.RomanNumberLowerPeriod
                    => $"{new RomanNumberStyle().Convert(j)}.".ToLower(),
                Bullet.RomanNumberLowerParenthesis
                    => $"{new RomanNumberStyle().Convert(j)})".ToLower(),
                _ => i.GetAttribute<SymbolAttribute>()?.Symbol?.ToString(),
            };
        });
    }
}