using Ion.Core;
using Ion.Text;
using System;
using System.Text;

namespace Ion.Numeral;

/// <see cref="NumberStyle"/>
#region

public abstract record class NumberStyle : Model
{
    public abstract NumberStyles Type { get; }

    public abstract string Convert(int index);
}

#endregion

/// <see cref="DefaultNumberStyle"/>
#region

public record class DefaultNumberStyle() : NumberStyle()
{
    public override NumberStyles Type => NumberStyles.Default;

    public NumberBase Base { get => Get(NumberBase.Decimal); set => Set(value); }

    public override string Convert(int index)
    {
        return Base switch
        {
            NumberBase.Decimal => $"{index}",
            _ => $"{index.ToBaseX(System.Convert.ToInt32(Base.GetAttribute<SymbolAttribute>().Symbol))}"
        };
    }
}

#endregion

/// <see cref="LetterNumberStyle"/>
#region

public record class LetterNumberStyle() : NumberStyle()
{
    public override NumberStyles Type => NumberStyles.Letter;

    public string Letters { get => Get("ABCDEFGHIJKLMNOPQRSTUVWXYZ"); set => Set(value); }

    public override string Convert(int index)
    {
        const int columns = 26;
        //ceil(log26(Int32.Max))
        const int digitMaximum = 7;

        if (index <= 0)
            throw new IndexOutOfRangeException("index must be a positive number");

        if (index <= columns)
            return Letters[index - 1].ToString();

        var result = new StringBuilder().Append(' ', digitMaximum);

        var current = index;
        var offset = digitMaximum;
        while (current > 0)
        {
            result[--offset] = Letters[--current % columns];
            current /= columns;
        }

        return result.ToString(offset, digitMaximum - offset);
    }
}

#endregion

/// <see cref="OrdinalNumberStyle"/>
#region

public record class OrdinalNumberStyle() : NumberStyle()
{
    public override NumberStyles Type => NumberStyles.Ordinal;

    public string ST { get => Get(nameof(ST).ToLower()); set => Set(value); }

    public string ND { get => Get(nameof(ND).ToLower()); set => Set(value); }

    public string RD { get => Get(nameof(RD).ToLower()); set => Set(value); }

    public string TH { get => Get(nameof(TH).ToLower()); set => Set(value); }

    public override string Convert(int i)
    {
        var result = i switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th",
        };
        return $"{i}{result}";
    }
}

#endregion

/// <see cref="RomanNumberStyle"/>
#region

public record class RomanNumberStyle() : NumberStyle()
{
    public override NumberStyles Type => NumberStyles.Roman;

    public string I { get => Get(nameof(I)); set => Set(value); }

    public string IV { get => Get(nameof(IV)); set => Set(value); }

    public string V { get => Get(nameof(V)); set => Set(value); }

    public string IX { get => Get(nameof(IX)); set => Set(value); }

    public string X { get => Get(nameof(X)); set => Set(value); }

    public string XL { get => Get(nameof(XL)); set => Set(value); }

    public string L { get => Get(nameof(L)); set => Set(value); }

    public string XC { get => Get(nameof(XC)); set => Set(value); }

    public string C { get => Get(nameof(C)); set => Set(value); }

    public string CD { get => Get(nameof(CD)); set => Set(value); }

    public string D { get => Get(nameof(D)); set => Set(value); }

    public string CM { get => Get(nameof(CM)); set => Set(value); }

    public string M { get => Get(nameof(M)); set => Set(value); }

    public override string Convert(int index)
    {
        if ((index < 0) || (index > 3999))
            throw new ArgumentOutOfRangeException(nameof(index), "Insert value betwheen 1 and 3999");

        if (index < 1)
            return string.Empty;

        if (index >= 1000)
            return M + Convert(index - 1000);

        if (index >= 900)
            return CM + Convert(index - 900);

        if (index >= 500)
            return D + Convert(index - 500);

        if (index >= 400)
            return CD + Convert(index - 400);

        if (index >= 100)
            return C + Convert(index - 100);

        if (index >= 90)
            return XC + Convert(index - 90);

        if (index >= 50)
            return L + Convert(index - 50);

        if (index >= 40)
            return XL + Convert(index - 40);

        if (index >= 10)
            return X + Convert(index - 10);

        if (index >= 9)
            return IX + Convert(index - 9);

        if (index >= 5)
            return V + Convert(index - 5);

        if (index >= 4)
            return IV + Convert(index - 4);

        if (index >= 1)
            return I + Convert(index - 1);

        throw new ArgumentOutOfRangeException(nameof(index));
    }
}

#endregion