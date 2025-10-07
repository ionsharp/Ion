using Ion.Analysis;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Ion;

/// <summary>
/// <see cref="Try"/> and <see langword="catch"/> an <see cref="Exception"/>.
/// </summary>
[Using(typeof(Log))]
public static class Try
{
    /// <see cref="Region.Field"/>

    private const bool DefaultLog = true;

    private const bool DefaultResearch = false;

    private const string DefaultResearchFormat = "http://stackoverflow.com/search?q=[c#]+{0}";

    /// <see cref="Region.Method"/>

    private static void _Research([NotNull] Exception e, bool Research) => Research.If(() => _ = System.Diagnostics.Process.Start(DefaultResearchFormat.F(e.Message)));

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Result Do(Void Try, Void Catch = null, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Do(Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static Result Do(Void Try, Void<Exception> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        try { Try(); return new Success(); }
        catch (Exception e)
        {
            if (Log) Analysis.Log.Write(e);
            Catch?.Invoke(e);

            _Research(e, Research);
            return new Error(e);
        }
        finally { Finally?.Invoke(); }
    }

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(Void Try, Void Catch = null, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => await DoAwait(Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(Void Try, Void<Exception> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await Task.Run(() => Do(Try, Catch, Finally, Log, Research));
    }

    #pragma warning disable CA1068 /// Preference
    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(CancellationToken token, Void<CancellationToken> Try, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await DoAwait(token, Try, default(Void), Log, Research);
    }

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(CancellationToken token, Void<CancellationToken> Try, Void Catch = null, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await DoAwait(token, Try, _ => Catch(), Log, Research);
    }

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(CancellationToken token, Void<CancellationToken> Try, Void<Exception> Catch, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await DoAwait(token, Try, Catch, null, Log, Research);
    }

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(CancellationToken token, Void<CancellationToken> Try, Void Catch, Void Finally, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await DoAwait(token, Try, _ => Catch(), Finally, Log, Research);
    }

    /// <summary>
    /// <see cref="Try"/> to do <see cref="Void"/> on new <see cref="Thread"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static async Task<Result> DoAwait(CancellationToken token, Void<CancellationToken> Try, Void<Exception> Catch, Void Finally, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));
        return await Task.Run(() => Do(() => Try(token), Catch, Finally, Log, Research), token);
    }
#pragma warning restore CA1068

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(Func<T> Try, Void Catch = null, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(Func<T> Try, Void<Exception> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(Try, e => { Catch(e); return default; }, Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(Func<T> Try, Func<T> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(Func<T> Try, Func<Exception, T> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(out _, Try, Catch, Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(out Result result, Func<T> Try, Void Catch = null, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(out result, Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(out Result result, Func<T> Try, Void<Exception> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(out result, Try, e => { Catch(e); return default; }, Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(out Result result, Func<T> Try, Func<T> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
        => Get(out result, Try, _ => Catch(), Finally, Log, Research);

    /// <summary>
    /// <see cref="Try"/> to get something.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static T Get<T>(out Result result, Func<T> Try, Func<Exception, T> Catch, Void Finally = null, bool Log = DefaultLog, bool Research = DefaultResearch)
    {
        Throw.IfNull(Try, nameof(Try));

        try
        {
            var i = Try();
            result = new Success();
            return i;
        }
        catch (Exception e)
        {
            result = new Error(e);
            if (Log) Analysis.Log.Write(e);

            _Research(e, Research);
            if (Catch is not null)
                return Catch(e);
        }
        finally { Finally?.Invoke(); }
        return default;
    }
}