using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the interface representing auto-tracking result value.
    /// </summary>
    public interface IAutoTrackValue : IReactiveObject
    {
        /// <summary>
        /// A nullable 32-bit signed integer representing the current result value.
        /// </summary>
        int? CurrentValue { get; }
    }
}