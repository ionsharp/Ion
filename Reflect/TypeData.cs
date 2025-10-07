using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// Data of a <see cref="Type"/> (all declared attributes and members).
/// </summary>
public class TypeData(Type Type) : object
{
    /// <summary>
    /// All attributes declared by the <see cref="Type"/>.
    /// </summary>
    public readonly MemberData Attributes = new(Type);

    /// <summary>
    /// All members declared by the <see cref="Type"/>.
    /// </summary>
    /// <remarks>Includes all declared attributes of each <see cref="MemberInfo"/>.</remarks>
    public readonly Dictionary<MemberInfo, MemberData> Members = [];
}