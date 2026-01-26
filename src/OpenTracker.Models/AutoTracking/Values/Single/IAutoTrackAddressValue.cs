using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This interface contains the auto-tracking result value of a memory address value.
/// </summary>
public interface IAutoTrackAddressValue : IAutoTrackValue
{
    /// <summary>
    /// A factory for creating new <see cref="IAutoTrackAddressValue"/> objects.
    /// </summary>
    /// <param name="address">
    ///     The <see cref="IMemoryAddress"/> for the comparison.
    /// </param>
    /// <param name="maximum">
    ///     A <see cref="byte"/> representing the maximum valid value of the memory address.
    /// </param>
    /// <param name="adjustment">
    ///     A <see cref="int"/> representing the amount that the result value should be adjusted from the actual
    ///     value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAutoTrackAddressValue"/> object.
    /// </returns>
    public delegate IAutoTrackAddressValue Factory(IMemoryAddress address, byte maximum, int adjustment);
}