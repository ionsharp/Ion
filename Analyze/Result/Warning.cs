using Ion.Input;

namespace Ion.Analysis;

/// <summary>
/// A <see cref="Result"/> that indicates a warning.
/// </summary>
public record class Warning(object message = null) : Result(message)
{
    public sealed override ResultType Type => ResultType.Warning;

    public Warning() : this(null) { }

    public static implicit operator Warning(string i) => new(i);

    public static implicit operator string(Warning i) => i.Text;
}

public class WarningEventArgs(Warning input) : EventArgs<Warning>(input) { }

public delegate void WarningEventHandler(object sender, WarningEventArgs e);