using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    ///     This interface provides the memory address data.
    /// </summary>
    public interface IMemoryAddressProvider
    {
        /// <summary>
        ///     A dictionary containing the memory segments to be sent to the SNES connector for updating data.
        /// </summary>
        Dictionary<MemorySegmentType, List<IMemoryAddress>> MemorySegments { get; }
        
        /// <summary>
        ///     A dictionary containing the memory addresses indexed by address.
        /// </summary>
        Dictionary<ulong, IMemoryAddress> MemoryAddresses { get; }

        /// <summary>
        ///     Returns the starting address of the specified memory segment.
        /// </summary>
        /// <param name="type">
        ///     The memory segment type.
        /// </param>
        /// <returns>
        ///     A 64-bit unsigned integer representing the starting memory address of the segment.
        /// </returns>
        ulong GetMemorySegmentStart(MemorySegmentType type);

        /// <summary>
        ///     Resets all memory addresses to their initial values.
        /// </summary>
        void Reset();
    }
}