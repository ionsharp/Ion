using System.Runtime.InteropServices;

namespace Ion;

/// <summary>
/// Definitions in a <see langword="class"/> or <see langword="struct"/> (by order of preference).
/// </summary>
public static class Region
{
    /// <summary>
    /// A <see langword="class"/> defined in another <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Class;

    /// <summary>
    /// A <see langword="delegate"/> defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Delegate;

    public class Key;

    /// <summary>
    /// An <see langword="event"/> defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Event;

    /// <summary>
    /// A field defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Field { public class Static; }

    /// <summary>
    /// A property defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Property
    {
        public class Indexor;

        /// <summary>
        /// Any property marked <see langword="internal"/>.
        /// </summary>
        public class Internal;

        /// <summary>
        /// Any property marked <see langword="private"/>.
        /// </summary>
        public class Private;

        /// <summary>
        /// Any property marked <see langword="protected"/>.
        /// </summary>
        public class Protected { public class Abstract; public class Override; public class Virtual; }

        /// <summary>
        /// Any property marked <see langword="public"/>.
        /// </summary>
        public class Public { public class Abstract; public class Override; public class Virtual; }
    }

    /// <summary>
    /// A constructor defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Constructor;

    /// <summary>
    /// An <see langword="operator"/> defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Operator;

    /// <summary>
    /// A method defined in a <see langword="class"/> or <see langword="struct"/>
    /// </summary>
    public class Method
    {
        /// <summary>
        /// Any method marked with <see cref="DllImportAttribute"/>.
        /// </summary>
        public class Import;

        /// <summary>
        /// Any method marked <see langword="internal"/>.
        /// </summary>
        public class Internal;

        /// <summary>
        /// Any method marked <see langword="private"/>.
        /// </summary>
        public class Private;

        /// <summary>
        /// Any method marked <see langword="protected"/>.
        /// </summary>
        public class Protected { public class Abstract; public class Override; public class Virtual; }

        /// <summary>
        /// Any method marked <see langword="public"/>.
        /// </summary>
        public class Public { public class Abstract; public class Override; public class Virtual; }

        /// <summary>
        /// Any static method.
        /// </summary>
        public class Static;
    }
}