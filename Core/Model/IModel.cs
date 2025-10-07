using System;

namespace Ion.Core;

/// <summary>
/// A representation of something.
/// </summary>
public interface IModel : IChange, ICloneable, IComparable, IFormattable, IPropertyGet, IPropertySet { }