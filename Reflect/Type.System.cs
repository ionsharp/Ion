using System;

namespace Ion.Reflect;

public static class SystemType
{
    public static Type Boolean
        => typeof(bool);

    public static Type Byte
        => typeof(byte);

    public static Type DateTime
        => typeof(DateTime);

    public static Type Decimal
        => typeof(decimal);

    public static Type Double
        => typeof(double);

    public static Type Int16
        => typeof(short);

    public static Type Int32
        => typeof(int);

    public static Type Int64
        => typeof(long);

    public static Type Single
        => typeof(float);

    public static Type String
        => typeof(string);

    public static Type TimeSpan
        => typeof(TimeSpan);

    public static Type UInt16
        => typeof(ushort);

    public static Type UInt32
        => typeof(uint);

    public static Type UInt64
        => typeof(ulong);
}