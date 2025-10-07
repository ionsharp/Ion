using System;

namespace Ion.Input;

/// <summary>
/// A <see cref="WeakAction"/> with a parameter.
/// </summary>
/// <inheritdoc/>
public class WeakAction<Parameter> : WeakAction, IWeakActionWithParameter
{
    private Action<Parameter> _Static;

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
    /// Get new instance of <see cref="WeakAction"/>.
    /// </summary>
    /// <param name="action">The <see cref="Action"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakAction(Action<Parameter> action) : this(action?.Target, action) { }

    /// <summary>
    /// Get new instance of <see cref="WeakAction"/>.
    /// </summary>
    /// <param name="target">The <see cref="Action"/> owner.</param>
    /// <param name="action">The <see cref="Action"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakAction(object target, Action<Parameter> action)
    {
        Throw.IfNull(target, nameof(target));
        Throw.IfNull(action, nameof(action));

        if (action.Method.IsStatic)
        {
            _Static = action;

            if (target != null)
            {
                // Keep a reference to the target to control the
                // WeakAction's lifetime.
                Reference = new WeakReference(target);
            }

            return;
        }

        Method = action.Method;

        ActionReference = new WeakReference(action.Target);
        Reference = new WeakReference(target);
    }

    /// <summary>
    /// Execute, but only if owner is still alive (with <see langword="default"/> parameter).
    /// </summary>
    public new void Execute() => Execute(default);

    /// <summary>
    /// Execute, but only if owner is still alive (with given <b>parameter</b>).
    /// </summary>
    public void Execute(Parameter parameter)
    {
        if (_Static != null)
        {
            _Static(parameter);
            return;
        }

        var actionTarget = ActionTarget;

        if (IsAlive)
        {
            if (Method != null && ActionReference != null && actionTarget != null)
            {
                Method.Invoke(actionTarget, new object[]
                {
                        parameter
                });
            }
        }
    }

    /// <summary>
    /// Set all actions this instance contains to <see langword="null"/>, which is signal for containing objects that this instance should be deleted.
    /// </summary>
    public new void MarkForDeletion()
    {
        _Static = null;
        base.MarkForDeletion();
    }

    void IWeakActionWithParameter.Execute(object parameter) => Execute((Parameter)parameter);
}