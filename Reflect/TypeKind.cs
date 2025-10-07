using System;

namespace Ion.Reflect;

[Flags]
public enum TypeKind
{
    [Hide]
    None = 0,
    Unknown = 1,
    Class = 2,
    Delegate = 4,
    Enum = 8,
    Interface = 16,
    Struct = 32,
    [Hide]
    All = Unknown | Class | Delegate | Enum | Interface | Struct
}