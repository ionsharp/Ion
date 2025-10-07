using System;

namespace Ion.Analysis;

/// <summary>
/// The type of a <see cref="Result"/>.
/// </summary>
[Flags]
public enum ResultType
{
    [Hide]
    None = 0,
    Error = 1,
    Message = 2,
    Success = 4,
    Warning = 8,
    [Hide]
    All = Error | Message | Success | Warning
}