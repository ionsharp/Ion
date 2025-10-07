using Ion.Core;
using System;

namespace Ion.Reflect;

public interface IMemberInfo : IPropertySet, ISubscribe
{
    MemberData Data { get; }

    int Depth { get; }

    bool Log { get; }

    string Name { get; }

    object Value { get; set; }

    Type ValueType { get; }

    void Reset(object value);
}