using System;

namespace Ion.Reflect;

[Flags]
public enum MemberValueType
{
    None = 0,
    Field = 1, 
    Property = 2, 
    Both = Field | Property
}