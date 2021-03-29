using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    ///     This interface contains SNES memory address data.
    /// </summary>
    public interface IMemoryAddress : IReactiveObject
    {
        /// <summary>
        ///     An 8-bit unsigned integer representing the value of the SNES memory address.
        /// </summary>
        byte? Value { get; set; }

        /// <summary>
        ///     A factory for creating memory addresses.
        /// </summary>
        delegate IMemoryAddress Factory();

        /// <summary>
        ///     Resets the memory address to its starting value.
        /// </summary>
        void Reset();
    }
}