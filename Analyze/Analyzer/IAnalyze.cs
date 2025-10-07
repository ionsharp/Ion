using System.Collections.Generic;

namespace Ion.Analysis;

/// <summary>
/// An <see cref="object"/> that can analyze another.
/// </summary>
public interface IAnalyze
{
    /// <summary>
    /// Analyze the given <b>subject</b>.
    /// </summary>
    void Analyze(object subject);
}

/// <summary>
/// An <see cref="object"/> that can analyze <see cref="{T}"/>.
/// </summary>
public interface IAnalyze<T> : IAnalyze
{
    /// <summary>
    /// Analyze the given <b>subject</b>.
    /// </summary>
    void Analyze(T subject);

    /// <summary>
    /// Analyze the given <b>subjects</b>.
    /// </summary>
    void Analyze(IEnumerable<T> subjects);
}

/// <summary>
/// An <see cref="object"/> that can analyze <see cref="{TSubject}"/> and return <see cref="{TResult}"/>.
/// </summary>
public interface IAnalyze<TSubject, TAnalysis> : IAnalyze<TSubject> where TAnalysis : IAnalysis
{
    /// <summary>
    /// Analyze given <see cref="{TSubject}"/> and return <see cref="{TResult}"/>.
    /// </summary>
    new TAnalysis Analyze(TSubject subject);

    /// <summary>
    /// Analyze given <see cref="{TSubject}"/> and return <see cref="{TResult}"/>.
    /// </summary>
    new IEnumerable<TAnalysis> Analyze(IEnumerable<TSubject> subjects);
}