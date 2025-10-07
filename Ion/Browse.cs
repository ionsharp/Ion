using System;

namespace Ion;

[Flags]
public enum Browse
{
    None = 0, Hidden = 1, Visible = 2,
    All = Hidden | Visible
}