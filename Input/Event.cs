using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ion.Input;

public class Event<Data> : Object;

public class WeakEvent<Data> : Event<Data> where Data : EventArgs
{
    readonly List<WeakDelegate> handlers;

    public WeakEvent() => handlers = [];

    public void Raise(object sender, Data e)
    {
        lock (handlers)
        {
            _ = handlers.RemoveAll(h => !h.Invoke(sender, e));
        }
    }

    public void Subscribe(EventHandler<Data> handler)
    {
        var weakHandlers = Enumerable            .Select(handler
            .GetInvocationList()
, d => new WeakDelegate(d))
            .ToList();

        lock (handlers)
        {
            handlers.AddRange(weakHandlers);
        }
    }

    public void Unsubscribe(EventHandler<Data> handler)
    {
        lock (handlers)
        {
            int index = handlers.FindIndex(h => h.IsMatch(handler));
            if (index >= 0)
                handlers.RemoveAt(index);
        }
    }

    sealed class WeakDelegate
    {
        #region Open handler generation and cache

        delegate void OpenEventHandler(object target, object sender, Data e);

        // ReSharper disable once StaticMemberInGenericType (by design)
        static readonly ConcurrentDictionary<MethodInfo, OpenEventHandler> _openHandlerCache =
            new();

        static OpenEventHandler CreateOpenHandler(MethodInfo method)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var sender = Expression.Parameter(typeof(object), "sender");
            var e = Expression.Parameter(typeof(Data), "e");

            if (method.IsStatic)
            {
                var expr = Expression.Lambda<OpenEventHandler>(
                    Expression.Call(
                        method,
                        sender, e),
                    target, sender, e);
                return expr.Compile();
            }
            else
            {
                var expr = Expression.Lambda<OpenEventHandler>(
                    Expression.Call(
                        Expression.Convert(target, method.DeclaringType),
                        method,
                        sender, e),
                    target, sender, e);
                return expr.Compile();
            }
        }

        #endregion

        readonly MethodInfo method;

        readonly OpenEventHandler openHandler;

        readonly WeakReference weakTarget;

        public WeakDelegate(Delegate handler)
        {
            weakTarget = handler.Target != null ? new WeakReference(handler.Target) : null;
            method = handler.GetMethodInfo();
            openHandler = _openHandlerCache.GetOrAdd(method, CreateOpenHandler);
        }

        public bool Invoke(object sender, Data e)
        {
            object target = null;
            if (weakTarget != null)
            {
                target = weakTarget.Target;
                if (target is null)
                    return false;
            }
            openHandler(target, sender, e);
            return true;
        }

        public bool IsMatch(EventHandler<Data> handler)
        {
            return ReferenceEquals(handler.Target, weakTarget?.Target)
                && handler.GetMethodInfo().Equals(method);
        }
    }
}