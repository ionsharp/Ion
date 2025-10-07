using System;
using System.Linq;

namespace Ion.Collect;

public class EnumerableEmpty(string name, Exception e = null)
    : Exception(Throw.Message(nameof(Enumerable), name, "{0} is empty"));

public class EnumerableMismatch(string aName, string bName)
    : Exception(Throw.Message(nameof(Enumerable), aName, "{0} must have same number of elements as {1}", bName));