using Ion.Analysis;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Ion.Numeral;

public record class CurveCompile : Curve
{
    public static readonly string CodeClassName = $"{nameof(CurveCompile)}d";

    public static readonly string CodeClassNameFull = $"{CodeNamespace}.{CodeClassName}";

    public const string CodeMethodName = "Execute";

    public static readonly string CodeNamespace = $"{nameof(Ion)}.{nameof(Numeral)}";

    public const string CodeVarX = "x";

    public const string CodeVarY = "y";

    public const string CodeVarValue = "value";

    public static readonly string CodeFormat = "using System;" +
    $"namespace {CodeNamespace}" +
    '{' +
        $"public sealed class {CodeClassName}" +
        '{' +
            $"public static double {CodeMethodName}(double {CodeVarX}, double {CodeVarY}, double {CodeVarValue})" + '{' + "{0}" + '}' +
        '}' +
    '}';

    public const string StringFormat = "{0}";

    /// <summary>
    /// Compile code in memory.
    /// </summary>
    public static CSharpCodeProvider Compile(string code, out MethodInfo method)
    {
        var provider = new CSharpCodeProvider();

        var parameters = new CompilerParameters { GenerateExecutable = false, GenerateInMemory = true };
        parameters.ReferencedAssemblies.AddRange(["System.dll", "mscorlib.dll"]);

        var result = provider.CompileAssemblyFromSource(parameters, code);

        method = result.CompiledAssembly.GetType(CodeClassNameFull).GetMethod(CodeMethodName);
        return provider;
    }

    /// <summary>
    /// Run code compiled in memory.
    /// </summary>
    public static double Run(double a, double b, double value, string code)
    {
        double result = 0;
        using (var provider = Compile(code, out MethodInfo method))
            result = method?.Invoke(null, [a, b, value]) is double i ? i : 0;

        return result;
    }

    /// <summary>
    /// <b>Code to perform arithmetic (C#)</b>
    /// <para>Available variables are <see cref="CodeVarX"/>, <see cref="CodeVarY"/>, and <see cref="CodeVarValue"/>.</para>
    /// <para><b>Example</b> (variation of <see cref="EasingCurves.EaseInQuad"/>):</para>
    /// <code>end -= start; start -= 0.02; return end * (end - value) * value + start;</code>
    /// </summary> 
    public virtual string Code { get => Get(""); set => Set(value); }

    public override double Do(double x, double y, double value)
        => Try.Get(() => Run(x, y, value, CodeFormat.F(Code)), e => Log.Write(e));

    /// <see cref="IFormattable"/>

    public override string ToString(string format, IFormatProvider provider) => StringFormat.F(Code);
}