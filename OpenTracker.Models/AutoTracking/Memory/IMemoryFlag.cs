using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    ///     This interface contains SNES memory flag data.
    /// </summary>
    public interface IMemoryFlag : IReactiveObject
    {
        /// <summary>
        ///     A boolean representing whether the flag is set.
        /// </summary>
        bool? Status { get; }

        /// <summary>
        ///     A factory for creating a memory flag.
        /// </summary>
        /// <param name="memoryAddress">
        ///     The memory address containing the flag.
        /// </param>
        /// <param name="flag">
        ///     An 8-bit unsigned integer representing the bitwise flag.
        /// </param>
        delegate IMemoryFlag Factory(IMemoryAddress memoryAddress, byte flag);
    }
}