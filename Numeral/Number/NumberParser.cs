using System;
using System.Data;
using System.Text;

namespace Ion.Numeral;

/// <summary>A parser for math equations.</summary>
/// <remarks><see cref="NumberParser">Legacy</see>: Source unknown!</remarks>
public class NumberParser
{
    /// <summary>Specifies level of difficulty.</summary>
    public enum Difficulty : int
    {
        None,
        Easy,
        Normal,
        Hard
    }

    private static char Operator(Difficulty difficulty)
    {
        var n = 4;
        switch (difficulty)
        {
            //+, -
            case Difficulty.Easy:
                n = 2;
                break;
            //+, -, *
            case Difficulty.Normal:
                n = 3;
                break;
            //+, -, *, /
            case Difficulty.Hard:
                n = 4;
                break;
        }

        return Number.Random(n) switch
        {
            0 => '+',
            1 => '-',
            2 => '*',
            3 => '/',
            _ => default,
        };
    }

    public static string Clean(string input)
    {
        var result = new StringBuilder();
        foreach (var i in input)
        {
            if (i == ' ')
                continue;

            switch (i)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case ')':
                case '(':
                case ',':
                case '.':
                    result.Append(i);
                    break;

                case '+':
                case '-':
                case '*':
                case '/':
                case '\\':
                    result.Append(' ');
                    result.Append(i);
                    result.Append(' ');
                    break;
            }
        }
        return result.ToString();
    }

    public static string Create(Difficulty difficulty)
    {
        var result = string.Empty;

        int parenthesis = 0;
        int lastParenthesis = 0;

        int m = 0, n = 0;
        switch (difficulty)
        {
            case Difficulty.Easy:
                n = Number.Random(2, 3);
                m = 21;
                break;
            case Difficulty.Normal:
                n = Number.Random(2, 4);
                m = 51;
                break;
            case Difficulty.Hard:
                n = Number.Random(3, 5);
                m = 101;
                break;
        }

        for (var i = 0; i < n; i++)
        {
            result += $"{Number.Random(m)}";

            if (parenthesis > 0)
            {
                lastParenthesis++;
                if (lastParenthesis > 1)
                {
                    if (Number.Random(2) == 1)
                    {
                        result += ")";
                        parenthesis--;
                        lastParenthesis = 0;
                    }
                }
            }

            if (i + 1 == n)
            {
                if (parenthesis > 0)
                {
                    for (var j = 0; j < parenthesis; j++)
                    {
                        result += ")";
                    }
                }
            }
            else if (i + 1 < n)
            {
                result += $" {Operator(difficulty)} ";
                if (i + 2 < n)
                {
                    if (Number.Random(2) == 1)
                    {
                        result += "(";
                        parenthesis++;
                        lastParenthesis = 0;
                    }
                }
            }
        }

        return result;
    }

    public static double Solve(string equation)
    {
        var result = new DataTable();
        return Convert.ToDouble(result.Compute(equation, string.Empty));
    }
}