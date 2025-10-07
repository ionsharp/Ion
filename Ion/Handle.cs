using Ion.Threading;
using System;
using System.Threading.Tasks;

namespace Ion;

/// <summary>
/// Do <see cref="Action"/> when it's safe.
/// </summary>
/// <remarks>
/// <para><b>If <see langword="false"/>, it's <i>safe</i>.</b></para>
/// <para>Prevents infinite loop if <see cref="Action"/> triggers <see langword="event"/> that calls same <see cref="Action"/>.</para>
/// </remarks>
public class Handle()
{
    private bool _Value;

    Handle(bool i) : this() => _Value = i;

    public static implicit operator Handle(bool i) => new(i);

    public void Do(Action action)
    {
        _Value = true;
        action();
        _Value = false;
    }

    public void DoInternal(Action @internal, Action external = null)
    {
        if (!_Value)
        {
            _Value = true;
            @internal();
            _Value = false;
        }
        else external?.Invoke();
    }

    async public Task DoInternalAwait(VoidAsync @internal, VoidAsync external = null)
    {
        if (!_Value)
        {
            _Value = true;
            await @internal();
            _Value = false;
        }
        else if (external != null)
            await external();
    }
}