using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a memory address bitwise integer.
    /// </summary>
    public interface IAutoTrackBitwiseIntegerValue : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result value of a memory address bitwise integer.
        /// </summary>
        /// <param name="address">
        ///     The memory address to be represented.
        /// </param>
        /// <param name="mask">
        ///     An 8-bit unsigned integer representing the bitwise mask of relevant bits.
        /// </param>
        /// <param name="shift">
        ///     A 32-bit signed integer representing the number of bitwise digits to right shift the address value.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of a memory address bitwise integer.
        /// </returns>
        delegate IAutoTrackBitwiseIntegerValue Factory(IMemoryAddress address, byte mask, int shift);
    }
}