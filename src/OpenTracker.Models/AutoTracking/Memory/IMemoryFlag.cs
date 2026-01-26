using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory;

/// <summary>
/// This interface contains SNES memory flag data.
/// </summary>
public interface IMemoryFlag : IReactiveObject
{
    /// <summary>
    /// A nullable <see cref="bool"/> representing whether the flag is set.
    /// </summary>
    bool? Status { get; }

    /// <summary>
    /// A factory for creating new <see cref="IMemoryFlag"/> objects.
    /// </summary>
    /// <param name="memoryAddress">
    ///     The <see cref="IMemoryAddress"/> containing the flag.
    /// </param>
    /// <param name="flag">
    ///     A <see cref="byte"/> representing the bitwise flag.
    /// </param>
    /// <returns>
    ///     A new <see cref="IMemoryFlag"/> object.
    /// </returns>
    delegate IMemoryFlag Factory(IMemoryAddress memoryAddress, byte flag);
}