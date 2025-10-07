using System;

namespace Ion.Text;

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
public sealed class SymbolAttribute(object symbol) : Attribute()
{
    public readonly object Symbol = symbol;
}