using System;

namespace Ion.Analysis;

/// <summary>
/// A <see cref="Analysis.Result"/> with more detail.
/// </summary>
public interface IEntry
{
    public const string Description = "A result with more detail.";

    DateTime Added { get; }

    string FileName => Sender.IfNotNullGet<string>(System.IO.Path.GetFileName);

    string FilePath => Sender;

    EntryLevel Level { get; }

    int Line { get; }

    string Member { get; }

    Result Result { get; }

    string Sender { get; }
}