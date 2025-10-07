using System.Reflection;

namespace Ion.Reflect;

public static partial class Instance
{
    public static class Flag
    {
        public const BindingFlags Private
        = BindingFlags.Instance | BindingFlags.NonPublic;

        public const BindingFlags Public
        = BindingFlags.Instance | BindingFlags.Public;

        public const BindingFlags PublicDeclared
        = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        public const MemberTypes Types
        = MemberTypes.Event | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property;
    }
}