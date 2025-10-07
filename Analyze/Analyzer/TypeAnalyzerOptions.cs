using Ion.Reflect;
using System;
using System.Reflection;

namespace Ion.Analysis;

public class TypeAnalyzerOptions
{
    public Value<Type, ResultType, EntryLevel>[] Attributes =
    [
        new Value<Type, ResultType, EntryLevel>(typeof(NotAccurateAttribute),
            ResultType.Warning, EntryLevel.Normal),
        new Value<Type, ResultType, EntryLevel>(typeof(NotCompleteAttribute),
            ResultType.Warning, EntryLevel.High),
        new Value<Type, ResultType, EntryLevel>(typeof(NotImplementedAttribute),
            ResultType.Warning, EntryLevel.High),
        new Value<Type, ResultType, EntryLevel>(typeof(NotStableAttribute),
            ResultType.Error, EntryLevel.High),
        new Value<Type, ResultType, EntryLevel>(typeof(NotTestedAttribute),
            ResultType.Warning, EntryLevel.Normal),
        new Value<Type, ResultType, EntryLevel>(typeof(RefactorAttribute),
            ResultType.Warning, EntryLevel.Low),
    ];

    public bool IncludeMembers = true;

    public BindingFlags MemberFlags = Instance.Flag.Public;

    public MemberTypes MemberTypes = MemberTypes.All;
}