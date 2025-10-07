using System;
using System.Reflection;

namespace Ion.Reflect;

/// <summary>
/// A context for an <see cref="System.Reflection.Assembly"/>.
/// </summary>
public class AssemblyContext
{
    /// <summary>
    /// A reference to the <see cref="System.AppDomain"/>.
    /// </summary>
    public AppDomain AppDomain { get; set; }

    /// <summary>
    /// A reference to an <see cref="System.Reflection.Assembly"/>.
    /// </summary>
    public Assembly Assembly { get; set; }

    /// <summary>
    /// A unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    AssemblyContext() { }

    /// <summary>
    /// Get new instance of the <see cref="AssemblyContext"/> class.
    /// </summary>
    public AssemblyContext(Guid id, Assembly assembly, AppDomain appDomain)
    {
        Id = id;
        Assembly = assembly;
        AppDomain = appDomain;
    }
}