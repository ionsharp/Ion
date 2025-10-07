using System;

namespace Ion;

/// <summary>Indicates something isn't serializable.</summary>
///<remarks><see cref="AttributeTargets.Property"/></remarks>
[AttributeUsage(AttributeTargets.Property)]
public sealed class NonSerializableAttribute() : Attribute() { }