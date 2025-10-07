using Ion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ion.Reflect;

/// <summary>
/// Extends <see cref="Assembly"/>.
/// </summary>
[Extend<Assembly>]
public static class XAssembly
{
    /// <see cref="Region.Field"/>

    public const string Name = nameof(Ion);

    private static readonly Cache<string, AssemblyInfo> cache = new(i => Get(i) is Assembly assembly ? new AssemblyInfo(assembly) : null);

    /// <see cref="Region.Property"/>

    /// <inheritdoc cref="AppDomain.GetAssemblies"/>
    public static IEnumerable<Assembly> All => AppDomain.CurrentDomain.GetAssemblies();

    /// <inheritdoc cref="Assembly.GetCallingAssembly"/>
    public static Assembly Calling => Assembly.GetCallingAssembly();

    /// <inheritdoc cref="Assembly.GetEntryAssembly"/>
    public static Assembly Entry => Assembly.GetEntryAssembly();

    /// <inheritdoc cref="Assembly.GetExecutingAssembly"/>
    public static Assembly Executing => Assembly.GetExecutingAssembly();

    /// <see cref="Region.Method"/>

    /// <see cref="Assembly"/>

    public static Assembly Get(AssemblySource i) 
        => i switch { AssemblySource.Calling => Calling, AssemblySource.Entry => Entry, AssemblySource.Executing => Executing };

    public static Assembly Get(String name) 
        => All.FirstOrDefault(i => i.GetName().Name == name);

    /// <see cref="IEnumerable{Assembly}"/>

    public static IEnumerable<Assembly> GetAssemblies(this Assembly assembly)
    {
        var list = new List<string>();

        var stack = new Stack<Assembly>();
        stack.Push(assembly);

        do
        {
            var x = stack.Pop();
            yield return x;

            foreach (var i in x.GetReferencedAssemblies())
            {
                if (!list.Contains(i.FullName))
                {
                    stack.Push(Assembly.Load(i));
                    list.Add(i.FullName);
                }
            }
        }
        while (stack.Count > 0);
    }

    public static IEnumerable<Assembly> GetAssemblies(AssemblySource i) 
        => Get(i).GetAssemblies();

    public static IEnumerable<Assembly> GetAssemblies(String i) 
        => Get(i).GetAssemblies();

    /// <see cref="AssemblyInfo"/>

    public static AssemblyInfo GetInfo(Assembly i) 
        => GetInfo(i.GetName().Name);

    public static AssemblyInfo GetInfo(AssemblySource i) 
        => GetInfo(Get(i));

    public static AssemblyInfo GetInfo(String name) 
        => cache[name ?? Entry.GetName().Name];

    /// <see cref="Attribute"/>

    public static T GetAttribute<T>(this Assembly i) 
        where T : Attribute => i.GetCustomAttributes(typeof(T)).FirstOrDefault<T>();

    public static T GetAttribute<T>(AssemblySource i) 
        where T : Attribute => Get(i).GetAttribute<T>();

    public static T GetAttribute<T>(String name) 
        where T : Attribute => Get(name).GetAttribute<T>();

    /// <see cref="MethodInfo"/>

    /// <summary>
    /// Get extension methods of given <see cref="Type"/> in all assemblies.
    /// </summary>
    ///<remarks>https://stackoverflow.com/questions/299515/reflection-to-identify-extension-methods</remarks>
    public static IEnumerable<MethodInfo> GetExtensions(Type type) => All.SelectMany(i => i.GetExtensions(type));

    /// <summary>
    /// Get extension methods of given <see cref="Type"/> in given <see cref="Assembly"/>.
    /// </summary>
    ///<remarks>https://stackoverflow.com/questions/299515/reflection-to-identify-extension-methods</remarks>
    public static IEnumerable<MethodInfo> GetExtensions(this Assembly assembly, Type type) => from i in assembly.GetTypes()
        where i.IsSealed && !i.IsGenericType && !i.IsNested
        from method in i.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        where method.IsDefined(typeof(ExtensionAttribute), false)
        where method.GetParameters()[0].ParameterType == type
        select method;

    /// <see cref="Type"/>

    /// <summary>
    /// Get derived types in <b>ALL</b> assemblies.
    /// </summary>
    public static IEnumerable<Type> GetDerivedTypes<T>(string @namespace, bool @abstract = false, bool hidden = false) 
        => All.SelectMany(i => i.GetDerivedTypes<T>(@namespace, @abstract, hidden));

    /// <summary>
    /// Get types in <b>ALL</b> assemblies.
    /// </summary>
    public static IEnumerable<Type> GetTypes(string @namespace, Predicate<Type> where = null) 
        => All.SelectMany(i => i.GetTypes(@namespace, where));

    /// <summary>
    /// Get types in <b>ALL</b> assemblies.
    /// </summary>
    public static IEnumerable<Type> GetTypes(Predicate<Type> where) 
        => GetTypes(null, where);

    /// <summary>
    /// Get derived types in <b>given</b> assembly.
    /// </summary>
    public static IEnumerable<Type> GetDerivedTypes<T>(this Assembly assembly, bool @abstract = false, bool hidden = false)
        => assembly.GetDerivedTypes<T>(null, @abstract, hidden);

    /// <summary>
    /// Get derived types in <b>given</b> assembly.
    /// </summary>
    public static IEnumerable<Type> GetDerivedTypes<T>(this Assembly assembly, string @namespace, bool @abstract = false, bool hidden = false)
    {
        var result = from type in assembly.GetTypes()
        where
            (type.Inherits<T>() || type.Implements<T>())
            &&
            (@namespace is null || type.Namespace == @namespace)
            &&
            (@abstract || !type.IsAbstract)
            &&
            (hidden || !type.HasAttribute<HideAttribute>())
        select type;
        foreach (var i in result)
            yield return i;
    }

    /// <summary>
    /// Get types in <b>given</b> assembly.
    /// </summary>
    public static IEnumerable<Type> GetTypes(this Assembly input, string @namespace, Predicate<Type> where = null)
        => from type in input.GetTypes() where type.Namespace == @namespace && @where?.Invoke(type) == true select type;
}