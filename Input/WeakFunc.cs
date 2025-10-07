using System;
using System.Reflection;

namespace Ion.Input;

/// <inheritdoc cref="IWeakFunc"/>
/// <remarks>
/// Stores <see cref="Func" /> without causing hard reference to <see cref="Func"/> owner. Owner can be garbage collected at any time.
/// </remarks>
public class WeakFunc<Return> : IWeakFunc
{
    Func<Return> _Static;

    /// <summary>
    /// Get or set <see cref="WeakReference"/> to this instance's <see cref="FunctionTarget"/>.
    /// This is not necessarily the same as <see cref="Reference" /> (for example, if method is anonymous).
    /// </summary>
    protected WeakReference FunctionReference { get; set; }

    /// <summary>
    /// Get owner of <see cref="Func"/> passed to constructor.
    /// This is not necessarily the same as <see cref="Target" /> (for example, if method is anonymous).
    /// </summary>
    protected object FunctionTarget
    {
        get
        {
            if (FunctionReference == null)
            {
                return null;
            }

            return FunctionReference.Target;
        }
    }

    /// <summary>
    /// Get or set <see cref="MethodInfo" /> of <see cref="Func"/> passed to constructor.
    /// </summary>
    protected MethodInfo Method { get; set; }

    /// <summary>
    /// Get or set <see cref="WeakReference"/> of target passed to constructor. 
    /// This is not necessarily the same as <see cref="FunctionReference" /> (for example, if method is anonymous).
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
            return (_func != null && _func.Target == null)
                || _staticFunc != null;
#else
            return _Static != null;
#endif
        }
    }

    /// <summary>
    /// Get <see cref="Func"/> owner (stored as <see cref="WeakReference" />).
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
    /// Get if <see cref="Func"/> owner is still alive (or not garbage collected).
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
    /// Get name of <see cref="MethodInfo"/> corresponding to encapsulated <see cref="Func"/>.
    /// </summary>
    public virtual string MethodName
    {
        get
        {
            if (_Static != null)
            {
#if NETFX_CORE
                return _staticFunc.GetMethodInfo().Name;
#else
                return _Static.Method.Name;
#endif
            }

#if SILVERLIGHT
            if (_func != null)
            {
                return _func.Method.Name;
            }

            if (Method != null)
            {
                return Method.Name;
            }

            return string.Empty;
#else
            return Method.Name;
#endif
        }
    }

    /// <summary>
    /// Get new instance of <see cref="WeakFunc{Return}"/>.
    /// </summary>
    protected WeakFunc() { }

    /// <summary>
    /// Get new instance of <see cref="WeakFunc{Return}"/>.
    /// </summary>
    /// <param name="function">The <see cref="Func"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakFunc(Func<Return> function) : this(function?.Target, function) { }

    /// <summary>
    /// Get new instance of <see cref="WeakFunc{Return}"/>.
    /// </summary>
    /// <param name="target">The <see cref="Func"/> owner.</param>
    /// <param name="function">The <see cref="Func"/> associated with this instance.</param>
    /// <exception cref="ArgumentNullException"/>
    public WeakFunc(object target, Func<Return> function)
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
    /// Execute, but only if owner is still alive.
    /// </summary>
    public Return Execute()
    {
        if (_Static != null)
        {
            return _Static();
        }

        var funcTarget = FunctionTarget;

        if (IsAlive)
        {
            if (Method != null
                && FunctionReference != null
                && funcTarget != null)
            {
                return (Return)Method.Invoke(funcTarget, null);
            }

#if SILVERLIGHT
            if (_func != null)
            {
                return _func();
            }
#endif
        }

        return default;
    }

    /// <summary>
    /// Set reference this instance stores to null.
    /// </summary>
    public void MarkForDeletion()
    {
        Reference = null;
        FunctionReference = null;
        Method = null;
        _Static = null;

#if SILVERLIGHT
        _func = null;
#endif
    }

    object IWeakFunc.Execute() => Execute();
}