using Ion.Core;
using System;

namespace Ion.Analysis;

/// <inheritdoc cref="IEntry"/>
[Description(IEntry.Description)]
public record class Entry() : Model(), IEntry
{
    /// <see cref="Region.Property"/>

    public DateTime Added { get; } = DateTime.Now;

    public EntryLevel Level { get; }

    public int Line { get;  }

    public string Member { get; }

    public Result Result { get; }

    public string Sender { get; }

    public string Text => Result?.Text;

    public ResultType Type => Result?.Type ?? ResultType.None;

    /// <see cref="Region.Constructor"/>

    public Entry(EntryLevel level, Result result, string sender, string member, int line) : this()
    {
        Level
            = level;
        Result
            = result;
        Sender
            = sender;
        Member
            = member;
        Line
            = line;
    }

    /// <see cref="IComparable"/>

    public override int CompareTo(object a)
    {
        if (a is Entry b)
            return Added.CompareTo(b.Added);

        return base.CompareTo(a);
    }
}