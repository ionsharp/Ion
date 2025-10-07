using Ion.Core;
using Ion.Numeral;
using System;
using System.Timers;
using System.Windows.Input;

namespace Ion.Analysis;

/// <summary>An entry that expires.</summary>
[Description(Description)]
public record class Notification() : Updatable(), IComparable, IEntry
{
    public const string Description = "An entry that expires.";

    public static readonly TimeSpan DefaultExpiration = TimeSpan.FromSeconds(3);

    /// <see cref="Region.Event"/>

    public event EventHandler<EventArgs> Expired;

    /// <see cref="Region.Property"/>

    public DateTime Added { get; } = DateTime.Now;

    public ICommand Command { get => Get<ICommand>(null, false); set => Set(value, false); }

    public TimeSpan Expire { get => Get(TimeSpan.Zero); set => Set(value); }

    public string Icon { get => Get(""); set => Set(value); }

    public bool IsRead { get => Get(false); set => Set(value); }

    public EntryLevel Level { get => Get(EntryLevel.Normal); private set => Set(value); }

    public int Line { get => Get(0); private set => Set(value); }

    public string Member { get => Get(""); private set => Set(value); }

    public Result Result { get => Get<Result>(); private set => Set(value); }

    public string Sender { get => Get(""); private set => Set(value); }

    public string Title { get => Get(""); private set => Set(value); }

    /// <see cref="Region.Constructor"/>

    internal Notification(EntryLevel level, string title, Result result, TimeSpan expire, string sender, string member, int line) : this()
    {
        Level
            = level;
        Title
            = title;
        Result
            = result;
        Expire
            = expire;
        Sender
            = sender;
        Member
            = member;
        Line
            = line;

        if (expire > TimeSpan.Zero)
        {
            Reset(1.Seconds(), true);
            timer.Enabled = true;
        }
    }

    /// <see cref="Region.Method"/>

    protected override void OnUpdate(ElapsedEventArgs e)
    {
        base.OnUpdate(e);
        XModel.Reset(this, () => Added);
        if (e.SignalTime - Added >= Expire)
        {
            OnExpired();
            return;
        }
    }

    protected virtual void OnExpired() => Expired?.Invoke(this, EventArgs.Empty);

    /// <see cref="IComparable"/>

    int IComparable.CompareTo(object a)
    {
        if (a is Notification b)
            return Added.CompareTo(b.Added);

        return 0;
    }
}