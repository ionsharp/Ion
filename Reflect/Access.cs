using System;

namespace Ion.Reflect;

[Flags]
public enum Access
{
    [Hide]
    None = 0,
    Undefined = 1,
    Private = 2,
    Protected = 4,
    ProtectedInternal = 8,
    Internal = 16,
    Public = 32,
    [Hide]
    All = Undefined | Private | Protected | ProtectedInternal | Internal | Public
}