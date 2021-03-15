using ReactiveUI;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This interface contains SNES memory flag data.
    /// </summary>
    public interface IMemoryFlag : IReactiveObject
    {
        bool Status { get; }

        delegate IMemoryFlag Factory(IMemoryAddress memoryAddress, byte flag);
    }
}