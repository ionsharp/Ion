using System;

namespace Ion.Reflect;

[Flags]
public enum MemberInstanceType
{
    [Hide]
    None = 0,
    Event = 1,
    Field = 2,
    Method = 4,
    Property = 8,
    [Hide]
    All = Event | Field | Method | Property
}