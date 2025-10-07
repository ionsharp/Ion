using Ion.Input;
using System.Collections.Generic;

namespace Ion.Analysis;

/// <summary>
/// A <see cref="Result"/> that indicates success.
/// </summary>
public record class Success<T>(T Data, object Message = null) : Result(Message)
{
    public readonly T Data = Data;

    public override ResultType Type => ResultType.Success;

    public Success() : this(default) { }

    public static implicit operator Success<T>(string i) => new(i);

    public static implicit operator string(Success<T> i) => i.Text;
}

/// <inheritdoc/>
public partial record class Success(object Data, object Message = null) : Success<object>(Data, Message)
{
    public Success() : this(default(string)) { }

    public Success(string message) : this(null, message) { }
}

public partial record class Success
{
    public sealed record class FileConvert(string oldFilePath, string newFilePath)
    : Success($"Converted '{oldFilePath}' to '{newFilePath}'.")
    { }

    public sealed record class FileRename(string oldFilePath, string newFilePath)
        : Success($"Renamed '{oldFilePath}' to '{newFilePath}'.")
    { }

    public abstract record class ItemSuccess : Success
    {
        protected virtual string ActionText { get; }

        protected abstract string PastActionText { get; }

        protected ItemSuccess(IReadOnlyCollection<object> items) : base() => Text = items?.Count == 1
            ? $"Item was {PastActionText.ToLower()}."
            : $"Items were {PastActionText.ToLower()}.";
    }

    public record class ItemAdd() : Success("Item was added.") { }

    public record class ItemClear() : ItemSuccess(default(IReadOnlyCollection<object>))
    {
        protected override string ActionText => "Clear";

        protected override string PastActionText => "Cleared";
    }

    public record class ItemClone() : Success("Item was cloned.") { }

    public record class ItemCopy(IReadOnlyCollection<object> items) : ItemSuccess(items)
    {
        protected override string ActionText => "Copy";

        protected override string PastActionText => "Copied";
    }

    public record class ItemMove(IReadOnlyCollection<object> items) : ItemSuccess(items)
    {
        protected override string ActionText => "Move";

        protected override string PastActionText => "Moved";
    }

    public record class ItemPaste(IReadOnlyCollection<object> items) : ItemSuccess(items)
    {
        protected override string ActionText => "Paste";

        protected override string PastActionText => "Pasted";
    }

    public record class ItemRemove() : Success("Item was removed.") { }
}

public class SuccessEventArgs(Success input) : EventArgs<Success>(input) { }

public delegate void SuccessEventHandler(object sender, SuccessEventArgs e);