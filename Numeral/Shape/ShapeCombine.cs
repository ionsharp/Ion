namespace Ion.Numeral;

/// <summary>
/// How to combine one <see cref="IShape"/> with another.
/// </summary>
public enum ShapeCombine 
{ 
    Complement, 
    Replace,
    Xor, 
    Intersect, 
    Union, 
    Exclude 
}