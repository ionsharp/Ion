using System;
using System.Reflection;

namespace Ion.Reflect;

public interface IMember : IMemberInfo
{
    Type DeclaringType { get; }

    Type MemberType { get; }

    MemberInfo Info { get; }
}