using System;

namespace Ion;

/// <summary>
/// A <see langword="static class"/> that extends a <see cref="Type"/> (name must begin with <i>X</i>).
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
#pragma warning disable CA1813 // Avoid unsealed attributes
public class ExtendAttribute(params Type[] Types) : Attribute() { public Type[] Types { get; set; } = Types; }
#pragma warning restore CA1813

/// <remarks>
/// <para><b>See <see cref="ExtendAttribute"/> for generic interfaces and other <see langword="static"/> classes.</b></para>
/// </remarks>
/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class ExtendAttribute<T>() : ExtendAttribute(typeof(T)) { }