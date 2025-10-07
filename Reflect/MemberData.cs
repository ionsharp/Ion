using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Ion.Reflect;

/// <summary>Data for a <see cref="MemberInfo"/> (all declared attributes).</summary>
/// <param name="member">An instance of <see cref="MemberInfo"/>.</param>
public class MemberData(MemberInfo member) : ReadOnlyCollection<Attribute>(member.GetAttributes().ToList())
{
    /// <summary>The <see cref="MemberInfo"/> this instance encapsulates.</summary>
    public readonly MemberInfo Member = member;
}