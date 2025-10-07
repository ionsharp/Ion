using Ion.Reflect;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Ion.Analysis;

/// <inheritdoc/>
public class TypeAnalyzer : Analyzer<Type>
{
    /// <summary>
    /// Get <see cref="TypeAnalyzerOptions"/> used to analyze <see cref="Type"/>.
    /// </summary>
    public TypeAnalyzerOptions Options { get; }

    /// <summary>
    /// Get new instance with given <see cref="TypeAnalyzerOptions"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public TypeAnalyzer(TypeAnalyzerOptions options) : base()
    {
        Throw.IfNull(options, nameof(options));
        Options = options;
    }

    private void Analyze(bool isMember, [NotNull] MemberInfo info)
    {
        foreach (var attribute in info.GetAttributes())
        {
            foreach (var j in Options.Attributes)
            {
                if (attribute.GetType() == j.A)
                {
                    var pName = isMember ? nameof(Type) : nameof(MemberInfo);
                    var cName = isMember ? info.Name : $"{info.DeclaringType}.{info.Name}";

                    var message = $"{pName} '{cName}' is marked '{j.A.ToString().Replace(nameof(Attribute), string.Empty)}'";

                    if (attribute is ObsoleteAttribute a)
                        message += $": {a.Message}";

                    if (attribute is IAttributeWithMessage b)
                        message += $": {b.Message}";

                    else message += ".";

                    Result result = j.B switch
                    {
                        ResultType.Error => new Error(message),
                        ResultType.Message => new Message(message),
                        ResultType.Success => new Success(message),
                        ResultType.Warning => new Warning(message),
                    };
                    Log.Write(result, j.C);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Analyze the specified <see cref="Type"/>.
    /// </summary>
    public void Analyze<X>() => Analyze(typeof(X));

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public override void Analyze(Type type)
    {
        Throw.IfNull(type, nameof(type));
        if (Options?.Attributes.Length > 0)
        {
            Analyze(false, type);
            if (Options.IncludeMembers)
                type.GetMembers(Options.MemberFlags, Options.MemberTypes, null, true).ForEach(i => Analyze(true, i));
        }
    }
}