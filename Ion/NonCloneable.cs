using System;

namespace Ion;

/// <summary>Indicates something isn't cloneable.</summary>
///<remarks><see cref="AttributeTargets.Class"/> | <see cref="AttributeTargets.Field"/> | <see cref="AttributeTargets.Property"/> | <see cref="AttributeTargets.Struct"/></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct)]
public sealed class NonCloneableAttribute() : Attribute() { }