using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Memory;

/// <summary>
/// This interface contains the memory address data provider.
/// </summary>
public interface IMemoryAddressProvider
{
    /// <summary>
    /// A <see cref="IDictionary{TKey,TValue}"/> containing the memory segments to be sent to the SNES connector
    /// indexed by <see cref="MemorySegmentType"/>.
    /// </summary>
    IDictionary<MemorySegmentType, IList<IMemoryAddress>> MemorySegments { get; }
        
    /// <summary>
    /// A <see cref="IDictionary{TKey,TValue}"/> containing the <see cref="IMemoryAddress"/> objects indexed by
    /// <see cref="ulong"/> address.
    /// </summary>
    IDictionary<ulong, IMemoryAddress> MemoryAddresses { get; }

    /// <summary>
    /// Returns the starting <see cref="ulong"/> address of the specified <see cref="MemorySegmentType"/>.
    /// </summary>
    /// <param name="type">
    ///     The <see cref="MemorySegmentType"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="ulong"/> representing the first address of the segment.
    /// </returns>
    ulong GetMemorySegmentStart(MemorySegmentType type);

    /// <summary>
    /// Resets all <see cref="IMemoryAddress"/> objects to their initial values.
    /// </summary>
    void Reset();
}