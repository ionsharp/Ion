using System;

namespace Ion;

/// <summary>
/// A <see langword="method"/> that is recursive (calls itself).
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class RecursiveAttribute(params object[] Messages) : Attribute();