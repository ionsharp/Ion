using Ion.Core;

namespace Ion.Analysis;

public record class LogOptions() : Model()
{
    public bool Enable { get => Get(true); set => Set(value); }

    public bool EnableConsole { get => Get(true); set => Set(value); }

    public EntryLevel Level { get => Get(EntryLevel.All); set => Set(value); }

    public ResultType Type { get => Get(ResultType.All); set => Set(value); }
}