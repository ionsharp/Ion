using System;

namespace Ion;

public class EqualException(string message, Exception e = null) : Exception(message, e)
{
    public EqualException() : this(null, null) { }

    public EqualException(string a, string b, Exception e = null) 
        : this($"{nameof(Object)} '{a}' isn't equal to {nameof(Object)} '{b}'.", e) { }
}

public class NotEqualException(string message, Exception e = null) : Exception(message, e)
{
    public NotEqualException() : this(null, null) { }

    public NotEqualException(string a, string b, Exception e = null)
        : this($"{nameof(Object)} '{a}' isn't equal to {nameof(Object)} '{b}'.", e) { }
}