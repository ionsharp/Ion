using Ion.Core;

namespace Ion.Text;

public record class FormatText : Model
{
    public Format Format { get => Get<Format>(); private set => Set(value); }

    public object Text { get => Get<object>(); private set => Set(value); }

    public FormatText(Format format, object text)
    {
        Format = format; Text = text;
    }
}