using System;

namespace Ion.Reflect;

public static class Primitives
{
    public static readonly Type[] All = [Boolean, Byte, Char, Double, Int16, Int32, Int64, IntPtr, SByte, Single, UInt16, UInt32, UInt64, UIntPtr];

    public static readonly Type Boolean
        = typeof(bool);

    public static readonly Type Byte
        = typeof(byte);

    public static readonly Type Char
        = typeof(char);

    public static readonly Type Double
        = typeof(double);

    public static readonly Type Int16
        = typeof(short);

    public static readonly Type Int32
        = typeof(int);

    public static readonly Type Int64
        = typeof(long);

    public static readonly Type IntPtr
        = typeof(IntPtr);

    public static readonly Type SByte
        = typeof(sbyte);

    public static readonly Type Single
        = typeof(float);

    public static readonly Type UInt16
        = typeof(ushort);

    public static readonly Type UInt32
        = typeof(uint);

    public static readonly Type UInt64
        = typeof(ulong);

    public static readonly Type UIntPtr
        = typeof(UIntPtr);
}