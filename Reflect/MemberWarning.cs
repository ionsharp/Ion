using Ion.Analysis;
using System;

namespace Ion.Reflect;

public record class MemberMissingAttributeWarning<T>(Type type, string name = null)
    : Warning(name is null ? $"Type '{type.FullName}' is missing '{typeof(T).FullName}' attribute." : $"Member '{name}' of type '{type.FullName}' is missing '{typeof(T).FullName}' attribute.") where T : Attribute;