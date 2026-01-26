using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This interface contains the auto-tracking result value of a memory address bitwise integer.
/// </summary>
public interface IAutoTrackBitwiseIntegerValue : IAutoTrackValue
{
    /// <summary>
    /// A factory for creating new <see cref="IAutoTrackBitwiseIntegerValue"/> objects.
    /// </summary>
    /// <param name="address">
    ///     The <see cref="IMemoryAddress"/> to be represented.
    /// </param>
    /// <param name="mask">
    ///     A <see cref="byte"/> representing the bitwise mask of relevant bits.
    /// </param>
    /// <param name="shift">
    ///     A <see cref="int"/> representing the number of bitwise digits to right shift the address value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAutoTrackBitwiseIntegerValue"/> object.
    /// </returns>
    delegate IAutoTrackBitwiseIntegerValue Factory(IMemoryAddress address, byte mask, int shift);
}