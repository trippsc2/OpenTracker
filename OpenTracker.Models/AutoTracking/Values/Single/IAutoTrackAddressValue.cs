using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a memory address value.
    /// </summary>
    public interface IAutoTrackAddressValue : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result value of a memory address.
        /// </summary>
        /// <param name="address">
        ///     The memory address for the comparison.
        /// </param>
        /// <param name="maximum">
        ///     An 8-bit unsigned integer representing the maximum valid value of the memory address.
        /// </param>
        /// <param name="adjustment">
        ///     A 32-bit signed integer representing the amount that the result value should be adjusted from the actual
        ///         value.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value of a memory address.
        /// </returns>
        public delegate IAutoTrackAddressValue Factory(IMemoryAddress address, byte maximum, int adjustment);
    }
}