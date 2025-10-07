using System;

namespace Ion.Numeral;

/// <summary>
/// <para><b>Example</b></para>
/// [1, 2, 3]<br/>
/// [4, 5, 6]<br/>
/// [7, 8, 9]
/// 
/// <para><see cref="Matrix3DFillSide.Left"/></para>
/// <para><i>Z = 0</i></para>
/// [3, 3, 3]<br/>
/// [6, 6, 6]<br/>
/// [9, 9, 9]
/// <para><i>Z = 1</i></para>
/// [2, 2, 2]<br/>
/// [5, 5, 5]<br/>
/// [8, 8, 8]
/// <para><i>Z = 2</i></para>
/// [1, 1, 1]<br/>
/// [4, 4, 4]<br/>
/// [7, 7, 7]
/// 
/// <para><see cref="Matrix3DFillSide.Right"/></para>
/// <para><i>Z = 0</i></para>
/// [1, 1, 1]<br/>
/// [4, 4, 4]<br/>
/// [7, 7, 7]
/// <para><i>Z = 1</i></para>
/// [2, 2, 2]<br/>
/// [5, 5, 5]<br/>
/// [8, 8, 8]
/// <para><i>Z = 2</i></para>
/// [3, 3, 3]<br/>
/// [6, 6, 6]<br/>
/// [9, 9, 9]
/// 
/// <para><see cref="Matrix3DFillSide.Top"/></para>
/// <para><i>Z = 0</i></para>
/// [7, 8, 9]<br/>
/// [7, 8, 9]<br/>
/// [7, 8, 9]
/// <para><i>Z = 1</i></para>
/// [4, 5, 6]<br/>
/// [4, 5, 6]<br/>
/// [4, 5, 6]
/// <para><i>Z = 2</i></para>
/// [1, 2, 3]<br/>
/// [1, 2, 3]<br/>
/// [1, 2, 3]
/// </summary>
public static class Matrix3DFillSideExampleA;

/// <summary>
/// <para><b>Example</b></para>
/// [1, 2, 3]<br/>
/// [4, 5, 6]<br/>
/// [7, 8, 9]
/// 
/// <para><see cref="Matrix3DFillSide.Back"/></para>
/// <para><i>Z = [0, 2]</i></para>
/// [3, 2, 1]<br/>
/// [6, 5, 4]<br/>
/// [9, 8, 7]
/// 
/// <para><see cref="Matrix3DFillSide.Bottom"/></para>
/// <para><i>Z = 0</i></para>
/// [1, 2, 3]<br/>
/// [1, 2, 3]<br/>
/// [1, 2, 3]
/// <para><i>Z = 1</i></para>
/// [4, 5, 6]<br/>
/// [4, 5, 6]<br/>
/// [4, 5, 6]
/// <para><i>Z = 2</i></para>
/// [7, 8, 9]<br/>
/// [7, 8, 9]<br/>
/// [7, 8, 9]
/// 
/// <para><see cref="Matrix3DFillSide.Front"/></para>
/// <para><i>Z = [0, 2]</i></para>
/// [1, 2, 3]<br/>
/// [4, 5, 6]<br/>
/// [7, 8, 9]
/// </summary>
public static class Matrix3DFillSideExampleB;

/// <summary>
/// How to fill 1/6 sides of <see cref="IMatrix3D"/> where each value is repeated in opposing direction.
/// </summary>
/// <remarks>
/// <b>See <see cref="Matrix3DFillSideExampleA"/> and <see cref="Matrix3DFillSideExampleB"/> for example.</b>
/// </remarks>
[Flags]
public enum Matrix3DFillSide
{
    /// <summary>
    /// = <see cref="Front"/>
    /// </summary>
    Default = 0,
    /// <summary>
    /// <see cref="Front"/> (flipped horizontally) and repeated backward.
    /// </summary>
    Back = 1,
    /// <summary>
    /// <see cref="Front"/> rotated down (top forward) and repeated up.
    /// </summary>
    Bottom = 2,
    /// <summary>
    /// Extrude forward.
    /// </summary>
    Front = 4,
    /// <summary>
    /// <see cref="Front"/> rotated clockwise and repeated right.
    /// </summary>
    Left = 8,
    /// <summary>
    /// <see cref="Front"/> rotated counterclockwise and repeated left.
    /// </summary>
    Right = 16,
    /// <summary>
    /// <see cref="Front"/> rotated up (top backward) and repeated down.
    /// </summary>
    Top = 32,
    /// <summary>
    /// 
    /// </summary>
    All = Back | Bottom | Front | Left | Right | Top
}