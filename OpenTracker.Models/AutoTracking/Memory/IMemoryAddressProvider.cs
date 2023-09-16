using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This interface contains the memory address data provider.
    /// </summary>
    public interface IMemoryAddressProvider
    {
        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> containing the memory segments to be sent to the SNES connector
        /// indexed by <see cref="MemorySegmentType"/>.
        /// </summary>
        IDictionary<MemorySegmentType, IList<MemoryAddress>> MemorySegments { get; }
        
        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> containing the <see cref="MemoryAddress"/> objects indexed by
        /// <see cref="ulong"/> address.
        /// </summary>
        IDictionary<ulong, MemoryAddress> MemoryAddresses { get; }

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
        /// Resets all <see cref="MemoryAddress"/> objects to their initial values.
        /// </summary>
        void Reset();
    }
}