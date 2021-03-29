using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This interface contains the auto-tracking result value of a comparison of a SNES memory address to a given
    ///         value.
    /// </summary>
    public interface IAutoTrackAddressBool : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result value boolean comparisons.
        /// </summary>
        /// <param name="address">
        ///     The memory address for the comparison.
        /// </param>
        /// <param name="comparison">
        ///     An 8-bit unsigned integer representing the value to which the memory address is compared.
        /// </param>
        /// <param name="trueValue">
        ///     A 32-bit signed integer representing the resultant value, if the comparison is true.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value boolean comparison.
        /// </returns>
        delegate IAutoTrackAddressBool Factory(IMemoryAddress address, byte comparison, int trueValue);
    }
}