using System;

namespace Ion.Reflect;

public class TypeInvalidException(Type typeWrong, Type typeRight, Exception e = null)
: Exception($"{nameof(Type)} '{typeWrong.FullName}' isn't valid. {nameof(Type)} '{typeRight.FullName}' is expected.", e);

public class TypeMismatchException(Type aType, Type bType, Exception e = null)
    : Exception($"{nameof(Object)} with {nameof(Type).ToLower()} '{aType.FullName}' doesn't match {nameof(Object).ToLower()} with {nameof(Type).ToLower()} '{bType.FullName}'.", e);