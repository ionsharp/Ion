namespace Ion;

/// <summary>
/// An <see cref="IArray1D"/> with a rank of <i>1</i> (maximum of 1 dimensions).
/// </summary>
/// <remarks>
/// <para><b><see cref="IArray1D"/></b></para>
/// ✔ <see cref="IArray"/><br/>
/// ✔ <see cref="IArray1DRank"/><br/>
/// ✖ <see cref="IArray2DRank"/><br/>
/// ✖ <see cref="IArray3DRank"/>
/// <para><b><see cref="IArray2D"/></b></para>
/// ✔ <see cref="IArray"/><br/>
/// ✔ <see cref="IArray1D"/><br/>
/// ✖ <see cref="IArray1DRank"/><br/>
/// ✔ <see cref="IArray2DRank"/><br/>
/// ✖ <see cref="IArray3DRank"/>
/// <para><b><see cref="IArray3D"/></b></para>
/// ✔ <see cref="IArray"/><br/>
/// ✔ <see cref="IArray1D"/><br/>
/// ✔ <see cref="IArray2D"/><br/>
/// ✖ <see cref="IArray1DRank"/><br/>
/// ✖ <see cref="IArray2DRank"/><br/>
/// ✔ <see cref="IArray3DRank"/>
/// </remarks>
public interface IArray1DRank : IArray1D;

/// <inheritdoc/>
public interface IArray1DRank<T> : IArray1DRank, IArray1D<T>;

/// <summary>
/// An <see cref="IArray2D"/> with a rank of <i>2</i> (maximum of 2 dimensions).
/// </summary>
/// <inheritdoc cref="IArray1DRank"/>
public interface IArray2DRank : IArray2D;

/// <inheritdoc/>
public interface IArray2DRank<T> : IArray2DRank, IArray2D<T>;

/// <summary>
/// An <see cref="IArray3D"/> with a rank of <i>3</i> (maximum of 3 dimensions).
/// </summary>
/// <inheritdoc cref="IArray1DRank"/>
public interface IArray3DRank : IArray3D;

/// <inheritdoc/>
public interface IArray3DRank<T> : IArray3DRank, IArray3D<T>;