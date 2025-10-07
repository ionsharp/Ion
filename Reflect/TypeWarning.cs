using Ion.Analysis;
using System;

namespace Ion.Reflect;

public record class TypeMissingParameterlessConstructorWarning(Type type)
    : Warning($"Type '{type.FullName}' is missing a parameterless constructor.");
