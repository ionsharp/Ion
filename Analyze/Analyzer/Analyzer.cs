using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ion.Analysis;

/// <inheritdoc cref="IAnalyze{T}"/>
public abstract class Analyzer<T>() : IAnalyze<T>
{
    /// <inheritdoc cref="IAnalyze{T}.Analyze(T)"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual void Analyze(T subject) { }

    /// <inheritdoc cref="IAnalyze{T}.Analyze(IEnumerable{T})"/>
    /// <exception cref="ArgumentNullException"/>
    public void Analyze(IEnumerable<T> subjects)
    {
        Throw.IfNull(subjects, nameof(subjects));
        subjects.ForEach(Analyze);
    }

    /// <inheritdoc cref="IAnalyze.Analyze(object)"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="TypeInvalidException"/>
    void IAnalyze.Analyze(object subject)
    {
        Throw.IfNull(subject, nameof(subject));
        Throw.If<TypeInvalidException>(subject is not T, nameof(subject));
        Analyze((T)subject);
    }
}

/// <inheritdoc/>
public abstract class Analyzer<TSubject, TAnalysis> : Analyzer<TSubject>, IAnalyze<TSubject, TAnalysis> where TAnalysis : IAnalysis
{
    /// <inheritdoc cref="IAnalyze{TSubject, TAnalysis}.Analyze(TSubject)"/>
    /// <exception cref="ArgumentNullException"/>
    new public abstract TAnalysis Analyze(TSubject subject);

    /// <inheritdoc cref="IAnalyze{TSubject, TAnalysis}.Analyze(IEnumerable{TSubject})"/>
    /// <exception cref="ArgumentNullException"/>
    new public IEnumerable<TAnalysis> Analyze(IEnumerable<TSubject> subjects)
    {
        Throw.IfNull(subjects, nameof(subjects));
        return subjects.Select(Analyze);
    }

    /// <inheritdoc cref="IAnalyze{TSubject, TAnalysis}.Analyze(TSubject)"/>
    /// <exception cref="ArgumentNullException"/>
    TAnalysis IAnalyze<TSubject, TAnalysis>.Analyze(TSubject subject)
    {
        Throw.IfNull(subject, nameof(subject));
        return Analyze(subject);
    }
}

/// <inheritdoc/>
public class AssemblyAnalyzer : Analyzer<Assembly>
{
    /// <summary>
    /// Get <see cref="Analysis.TypeAnalyzer"/> used to analyze each <see cref="Type"/> in an <see cref="Assembly"/>.
    /// </summary>
    public TypeAnalyzer TypeAnalyzer { get; }

    /// <summary>
    /// Get new instance with given <see cref="TypeAnalyzerOptions"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public AssemblyAnalyzer(TypeAnalyzerOptions options) : base()
    {
        Throw.IfNull(options, nameof(options));
        TypeAnalyzer = new(options);
    }

    /// <summary>
    /// Occurs after a <see cref="Type"/> is analyzed.
    /// </summary>
    protected virtual void OnTypeAnalyzed(Type type) { }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    public override void Analyze(Assembly assembly)
    {
        Throw.IfNull(assembly, nameof(assembly));
        XEnumerable.ForEach(assembly.GetTypes(), i => { TypeAnalyzer.Analyze(i); OnTypeAnalyzed(i); });
    }
}