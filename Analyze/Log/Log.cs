using Ion.Reflect;
using System;
using System.Runtime.CompilerServices;

namespace Ion.Analysis;

public static class Log
{
    /// <summary>The string format used to write output to the <see cref="Console"/>.</summary>
    /// <remarks>{sender}.{member} ({line}):{result}</remarks>
    public const string StringFormat = "{0}.{1} ({2}):{3}";

    ///

    public static event LogEventHandler Added;

    public static LogOptions Options { get; set; }

    ///

    private static void OnAdded(IEntry entry)
    {
        if (Options == null || Options.EnableConsole)
            Console.WriteLine(StringFormat.F(entry.Sender, entry.Member, entry.Line, entry.Result));

        Added?.Invoke(new LogEventArgs(entry));
    }

    private static void OnAdding(EntryLevel level, ResultType type, ref bool cancel)
    {
        if (Options is not null)
        {
            var a = level == EntryLevel.None || Options.Level.HasFlag(level);
            var b = Options.Type.HasFlag(type);
            var c = Options.Enable;
            cancel = !a || !b || !c;
        }
        else cancel = false;
    }

    /// <see cref="Entry"/>

    public static void Write(Result result, EntryLevel level = EntryLevel.Normal, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
    {
        var cancel = false;
        OnAdding(level, result.Type, ref cancel);

        if (!cancel)
        {
            var entry = new Entry(level, result, sender, member, line);
            OnAdded(entry);
        }
    }

    public static void Write<T>(object message, EntryLevel level = EntryLevel.Normal, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
    {
        var result = typeof(T).Create<Result>();
        result.Text = $"{message}";

        Write(result, level, sender, member, line);
    }

    public static void Write<T>(EntryLevel level = EntryLevel.Normal, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) where T : Result
    {
        Result result = typeof(T).Create<Result>();
        Write(result, level, sender, member, line);
    }

    /// <see cref="Notification"/>

    public static void Notify<T>(string title, TimeSpan expire, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) where T : Result
        => Notify(title, typeof(T).Create<Result>(), expire, sender, member, line);

    public static void Notify(string title, Result result, TimeSpan expire, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        => Notify(EntryLevel.Normal, title, result, expire, sender, member, line);

    public static void Notify<T>(EntryLevel level, string title, TimeSpan expire, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0) where T : Result
        => Notify(level, title, typeof(T).Create<Result>(), expire, sender, member, line);

    public static void Notify(EntryLevel level, string title, Result result, TimeSpan expire, [CallerFilePath] string sender = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
    {
        var cancel = false;
        OnAdding(level, result.Type, ref cancel);

        if (!cancel)
        {
            IEntry entry = new Notification(level, title, result, expire, sender, member, line);
            OnAdded(entry);
        }
    }
}