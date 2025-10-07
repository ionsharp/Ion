using System;

namespace Ion.Input;

/// <summary>
/// A <see cref="WeakFunc{Return}"/> with a parameter.
/// </summary>
/// <inheritdoc/>
public class WeakFunc<Parameter, Return> : WeakFunc<Return>, IWeakFunctionWithParameter
{
    private Func<Parameter, Return> _Static;

    /// <inheritdoc/>
    public override string MethodName
    {
        get
        {
            if (_Static != null)
                return _Static.Method.Name;

            return Method.Name;
        }
    }

    /// <inheritdoc/>
    public override bool IsAlive
    {
        get
        {
            if (_Static == null && Reference == null)
                return false;

            if (_Static != null)
            {
                if (Reference != null)
                    return Reference.IsAlive;

                return true;
            }

            return Reference.IsAlive;
        }
    }

    /// <summary>
    /// Get new instance of <see cref="WeakFunc{Return}"/>.
    /// </summary>
    /// <param name="function">The <see cref="Func"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakFunc(Func<Parameter, Return> function) : this(function?.Target, function) { }

    /// <summary>
    /// Get new instance of <see cref="WeakFunc{Return}"/>.
    /// </summary>
    /// <param name="target">The <see cref="Func"/> owner.</param>
    /// <param name="function">The <see cref="Func"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakFunc(object target, Func<Parameter, Return> function)
    {
        Throw.IfNull(target, nameof(target));
        Throw.IfNull(function, nameof(function));

        if (function.Method.IsStatic)
        {
            _Static = function;

            if (target != null)
            {
                // Keep a reference to the target to control the
                // WeakAction's lifetime.
                Reference = new WeakReference(target);
            }

            return;
        }

        Method = function.Method;

        FunctionReference = new WeakReference(function.Target);
        Reference = new WeakReference(target);
    }

    /// <summary>
    /// Execute, but only if owner is still alive (with <see langword="default"/> parameter).
    /// </summary>
    public new Return Execute() => Execute(default);

    /// <summary>
    /// Execute, but only if owner is still alive (with given <b>parameter</b>).
    /// </summary>
    public Return Execute(Parameter parameter)
    {
        if (_Static != null)
            return _Static(parameter);

        var funcTarget = FunctionTarget;

        if (IsAlive)
        {
            if (Method != null && FunctionReference != null && funcTarget != null)
            {
                return (Return)Method.Invoke(funcTarget, new object[]
                {
                        parameter
                });
            }
        }

        return default;
    }

    /// <summary>
    /// Set all actions this instance contains to <see langword="null"/>, which is signal for containing objects that this instance should be deleted.
    /// </summary>
    public new void MarkForDeletion()
    {
        _Static = null;
        base.MarkForDeletion();
    }

    object IWeakFunctionWithParameter.Execute(object parameter) => Execute((Parameter)parameter);
}