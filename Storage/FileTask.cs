using Ion.Core;
using Ion.Numeral;
using Ion.Threading;
using System;
using System.Threading.Tasks;

namespace Ion.Storage;

public record class FileTask : Taskable
{
    public enum Statuses { Active, Inactive }

    ///

    public DateTime Added { get => Get(DateTime.Now); set => Set(value); }

    public long Size { get => Get(0L); set => Set(value); }

    public long SizeRead { get => Get(0L); set => Set(value); }

    public string Source { get => Get(""); set => Set(value); }

    public double Speed => Duration.TotalSeconds == 0 ? 0 : Convert.ToDouble(SizeRead) / Duration.TotalSeconds;

    public Statuses Status { get => Get(Statuses.Inactive); set => Set(value); }

    public string Target { get => Get(""); set => Set(value); }

    public FileTaskType Type { get => Get(FileTaskType.Create); set => Set(value); }

    ///

    public FileTask(FileTaskType type, string source, string target, TaskManaged action) : base(action)
    {
        Added
            = DateTime.Now;
        Type
            = type;
        Source
            = source;
        Target
            = target;

        if (System.IO.File.Exists(source))
        {
            _ = Try.Do(() =>
            {
                var fileInfo = new System.IO.FileInfo(source);
                Size = fileInfo.Length;
            });
        }
    }

    public override void OnSettingProperty(PropertySettingEventArgs e)
    {
        base.OnSettingProperty(e);
        if (e.PropertyName == nameof(Duration))
            e.NewValue = TimeSpan.FromSeconds(e.NewValue.To<TimeSpan>().TotalSeconds.Round());
    }

    public override void OnSetProperty(PropertySetEventArgs e)
    {
        base.OnSetProperty(e);
        if (e.PropertyName == nameof(Duration) || e.PropertyName == nameof(SizeRead))
            Reset(() => Speed);
    }

    new public async Task Start()
    {
        Status = Statuses.Active;
        await base.Start();
        Status = Statuses.Inactive;
    }
}