using System;

namespace Ion;

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct)]
public sealed class HideAttribute() : Attribute() { }