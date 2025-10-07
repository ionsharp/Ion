using System;
using System.Threading.Tasks;

namespace Ion;

/// <summary>
/// Do <see cref="Action"/> <see langword="while"/> <see cref="Condition"/> is <see langword="true"/>.
/// </summary>
[Private]
public static class While
{
    /// <summary>
    /// Do given <see cref="Void"/> <see langword="while"/> given <see cref="Condition"/> is <see langword="true"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    public static void Do(Condition DoIf, Void DoWhile = null, Void DoAfter = null)
    {
        Throw.IfNull(DoIf, nameof(DoIf));
        while (DoIf())
            DoWhile?.Invoke();

        DoAfter?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> <see langword="while"/> given <see cref="Condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="DoIf">On new thread!</param>
    /// <param name="DoWhile">On new thread!</param>
    /// <param name="DoAfter">Not on new thread!</param>
    /// <exception cref="ArgumentNullException"/>
    public static async Task DoAwait(Condition DoIf, Void DoWhile = null, Void DoAfter = null)
    {
        Throw.IfNull(DoIf, nameof(DoIf));
        await Task.Run(() =>
        {
            while (DoIf())
                DoWhile?.Invoke();
        });
        DoAfter?.Invoke();
    }

    /// <summary>
    /// Do given <see cref="Void"/> with <see cref="{Value}"/> <see langword="while"/> given <see cref="Condition"/> is <see langword="true"/>.
    /// </summary>
    /// <param name="DoIf">On new thread!</param>
    /// <param name="DoWhile">On new thread!</param>
    /// <param name="DoAfter">Not on new thread!</param>
    /// <exception cref="ArgumentNullException"/>
    public static async Task DoAwait<Value>(Value i, Condition<Value> DoIf, Void<Value> DoWhile = null, Void<Value> DoAfter = null)
    {
        Throw.IfNull(DoIf, nameof(DoIf));
        
        var target = i;
        await Task.Run(() =>
        {
            while (DoIf(target))
                DoWhile?.Invoke(target);
        });
        DoAfter?.Invoke(target);
    }
}