using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Memory;

/// <summary>
/// Represents the logic to provide cached memory address data.
/// </summary>
public interface IMemoryAddressProvider
{
    /// <summary>
    /// A <see cref="Dictionary{TKey,TValue}"/> containing <see cref="List{T}"/> of <see cref="MemoryAddress"/>
    /// indexed by <see cref="MemorySegmentType"/> representing the memory segments to polled in a single request.
    /// </summary>
    Dictionary<MemorySegmentType, List<MemoryAddress>> MemorySegments { get; }
        
    /// <summary>
    /// A <see cref="Dictionary{TKey,TValue}"/> containing <see cref="MemoryAddress"/> indexed by
    /// <see cref="ulong"/> representing memory address values by address.
    /// </summary>
    Dictionary<ulong, MemoryAddress> MemoryAddresses { get; }

    /// <summary>
    /// Resets all memory addresses to their initial values.
    /// </summary>
    void Reset();
}