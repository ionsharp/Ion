using System;
using System.Reflection;

namespace Ion.Input;

/// <inheritdoc cref="IWeakAction"/>
/// <remarks>
/// Stores <see cref="Action" /> without causing hard reference to <see cref="Action"/> owner. Owner can be garbage collected at any time.
/// </remarks>
public class WeakAction : IWeakAction
{
    Action _Static;

    /// <summary>
    /// Get or set <see cref="WeakReference"/> to this instance's <see cref="ActionTarget"/>.
    /// This is not necessarily the same as <see cref="Reference" /> (for example, if method is anonymous).
    /// </summary>
    protected WeakReference ActionReference { get; set; }

    /// <summary>
    /// Get owner of <see cref="Action"/> passed to constructor.
    /// This is not necessarily the same as <see cref="Target" /> (for example, if method is anonymous).
    /// </summary>
    protected object ActionTarget
    {
        get
        {
            if (ActionReference == null)
            {
                return null;
            }
            return ActionReference.Target;
        }
    }

    /// <summary>
    /// Get or set <see cref="MethodInfo" /> of <see cref="Action"/> passed to constructor.
    /// </summary>
    protected MethodInfo Method { get; set; }

    /// <summary>
    /// Get or set <see cref="WeakReference"/> of target passed to constructor. 
    /// This is not necessarily the same as <see cref="ActionReference" /> (for example, if method is anonymous).
    /// </summary>
    protected WeakReference Reference { get; set; }

    /// <summary>
    /// Get if this instance is <see langword="static"/>.
    /// </summary>
    public bool IsStatic
    {
        get
        {
#if SILVERLIGHT
                return (_action != null && _action.Target == null)
                    || _staticAction != null;
#else
            return _Static != null;
#endif
        }
    }

    /// <summary>
    /// Get <see cref="Action"/> owner (stored as <see cref="WeakReference" />).
    /// </summary>
    public object Target
    {
        get
        {
            if (Reference == null)
            {
                return null;
            }

            return Reference.Target;
        }
    }

    /// <summary>
    /// Get if <see cref="Action"/> owner is still alive (or not garbage collected).
    /// </summary>
    public virtual bool IsAlive
    {
        get
        {
            if (_Static == null
                && Reference == null)
            {
                return false;
            }

            if (_Static != null)
            {
                if (Reference != null)
                {
                    return Reference.IsAlive;
                }

                return true;
            }

            return Reference.IsAlive;
        }
    }

    /// <summary>
    /// Get name of <see cref="MethodInfo"/> corresponding to encapsulated <see cref="Action"/>.
    /// </summary>
    public virtual string MethodName
    {
        get
        {
            if (_Static != null)
                return _Static.Method.Name;

            return Method.Name;
        }
    }

    /// <summary>
    /// Get new instance of <see cref="WeakAction"/>.
    /// </summary>
    protected WeakAction() { }

    /// <summary>
    /// Get new instance of <see cref="WeakAction"/>.
    /// </summary>
    /// <param name="action">The <see cref="Action"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakAction(Action action) : this(action?.Target, action) { }

    /// <summary>
    /// Get new instance of <see cref="WeakAction"/>.
    /// </summary>
    /// <param name="target">The <see cref="Action"/> owner.</param>
    /// <param name="action">The <see cref="Action"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakAction(object target, Action action)
    {
        Throw.IfNull(target, nameof(target));
        Throw.IfNull(action, nameof(action));

#if NETFX_CORE
            if (action.GetMethodInfo().IsStatic)
#else
        if (action.Method.IsStatic)
#endif
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

#if SILVERLIGHT
            if (!action.Method.IsPublic
                || (action.Target != null
                    && !action.Target.GetType().IsPublic
                    && !action.Target.GetType().IsNestedPublic))
            {
                _action = action;
            }
            else
            {
                var name = action.Method.Name;

                if (name.Contains("<")
                    && name.Contains(">"))
                {
                    // Anonymous method
                    _action = action;
                }
                else
                {
                    Method = action.Method;
                    ActionReference = new WeakReference(action.Target);
                }
            }
#else
#if NETFX_CORE
            Method = action.GetMethodInfo();
#else
        Method = action.Method;
#endif
        ActionReference = new WeakReference(action.Target);
#endif

        Reference = new WeakReference(target);
    }

    /// <summary>
    /// Execute, but only if owner is still alive.
    /// </summary>
    public void Execute()
    {
        if (_Static != null)
        {
            _Static();
            return;
        }

        var actionTarget = ActionTarget;

        if (IsAlive)
        {
            if (Method != null
                && ActionReference != null
                && actionTarget != null)
            {
                Method.Invoke(actionTarget, null);

                // ReSharper disable RedundantJumpStatement
                return;
                // ReSharper restore RedundantJumpStatement
            }

#if SILVERLIGHT
                if (_action != null)
                {
                    _action();
                }
#endif
        }
    }

    /// <summary>
    /// Set reference this instance stores to null.
    /// </summary>
    public void MarkForDeletion()
    {
        Reference = null;
        ActionReference = null;
        Method = null;
        _Static = null;

#if SILVERLIGHT
            _action = null;
#endif
    }
}