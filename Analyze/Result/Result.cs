using Ion.Core;
using System;

namespace Ion.Analysis;

/// <summary>
/// A result of something.
/// </summary>
public abstract record class Result : Model
{
    public const string StringFormat = "{0}";

    public virtual EntryLevel DefaultLevel => EntryLevel.Normal;

    public string Text { get => Get(""); set => Set(value); }

    public abstract ResultType Type { get; }

    protected Result() : base() { }

    protected Result(object text) : this() => Text = $"{text}";

    public static implicit operator bool(Result a) => a?.Type == ResultType.Success;

    public static implicit operator Result(Exception e) => new Error(e);

    public static implicit operator Result(string i) => new Message(i);

    public static implicit operator Result(bool i) => i ? (Result)new Success() : new Error();

    public override string ToString(string format, IFormatProvider provider) => StringFormat.F(Text);
}