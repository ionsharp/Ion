using Ion.Input;

namespace Ion.Analysis;

/// <summary>
/// A <see cref="Result"/> that indicates a message.
/// </summary>
public record class Message(object text = null) : Result(text)
{
    public sealed override ResultType Type => ResultType.Message;

    public Message() : this(null) { }

    public static implicit operator Message(string i) => new(i);

    public static implicit operator string(Message i) => i.Text;
}

public class MessageEventArgs(Message input) : EventArgs<Message>(input) { }

public delegate void MessageEventHandler(object sender, MessageEventArgs e);