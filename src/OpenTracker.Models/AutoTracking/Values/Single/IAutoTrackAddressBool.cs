using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    /// This interface contains the auto-tracking result value of a comparison of a SNES memory address to a given
    /// value.
    /// </summary>
    public interface IAutoTrackAddressBool : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackAddressBool"/> objects.
        /// </summary>
        /// <param name="address">
        ///     The <see cref="IMemoryAddress"/> for the comparison.
        /// </param>
        /// <param name="comparison">
        ///     A <see cref="byte"/> representing the value to which the memory address is compared.
        /// </param>
        /// <param name="trueValue">
        ///     A <see cref="int"/> representing the resultant value, if the comparison is true.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackAddressBool"/> object.
        /// </returns>
        delegate IAutoTrackAddressBool Factory(IMemoryAddress address, byte comparison, int trueValue);
    }
}