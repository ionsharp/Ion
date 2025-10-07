using Ion.Input;
using Ion.Reflect;
using System;
using System.ComponentModel;

namespace Ion.Analysis;

/// <summary>
/// A <see cref="Result"/> that indicates an error.
/// </summary>
/// <remarks>
///  <b>Encapsulates <see cref="Exception"/>.</b>
/// </remarks>
public record class Error : Result
{
    public string FullName { get => Get(""); set => Set(value); }

    public Error Inner { get => Get<Error>(); set => Set(value); }

    public string Name { get => Get(""); set => Set(value); }

    public string StackTrace { get => Get(""); set => Set(value); }

    public sealed override ResultType Type => ResultType.Error;

    public Error() : this(new Exception()) { }

    public Error(object message) : this(new Exception($"{message}")) { }

    public Error(Exception e) : base(e.Message)
    {
        e ??= new Exception();

        Inner
            = e.InnerException != null
            ? new Error(e.InnerException)
            : null;

        Text
            = e.Message;
        Name
            = e.GetType().GetAttribute<DisplayNameAttribute>()?.DisplayName ?? e.GetType().Name;
        FullName
            = e.GetType().FullName;
        StackTrace
            = e.StackTrace;
    }

    public Error(object message, Exception e) : this(message) => Inner = new Error(e);

    public static implicit operator Error(Exception e) => new(e);

    public static implicit operator Error(String i) => new(i);

    public static implicit operator Exception(Error e) => new(e.Text);

    public static implicit operator String(Error i) => i.Text;
}

public class ErrorEventArgs(Error input) : EventArgs<Error>(input) { }

public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);