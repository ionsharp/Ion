using System.Reflection;

namespace Ion.Reflect;

public record class AssemblyInfo : IImmutable
{
    /// <see cref="Region.Property"/>

    public string Company { get; }

    public string Copyright { get; }

    public string Description { get; }

    public string FileVersion { get; }

    public string Name { get; }

    public string Product { get; }

    public string Title { get; }

    public string Version { get; }

    /// <see cref="Region.Constructor"/>

    public AssemblyInfo(Assembly assembly) : base()
    {
        Name = assembly.GetName().Name;

        ///

        Company
            = assembly.GetAttribute<AssemblyCompanyAttribute>()
            ?.Company;
        Copyright
            = assembly.GetAttribute<AssemblyCopyrightAttribute>()
            ?.Copyright;
        Description
            = assembly.GetAttribute<AssemblyDescriptionAttribute>()
            ?.Description;
        FileVersion
            = assembly.GetAttribute<AssemblyFileVersionAttribute>()
            ?.Version;
        Product
            = assembly.GetAttribute<AssemblyProductAttribute>()
            ?.Product;
        Title
            = assembly.GetAttribute<AssemblyTitleAttribute>()
            ?.Title;
        Version
            = assembly.GetAttribute<AssemblyVersionAttribute>()
            ?.Version;
    }
}