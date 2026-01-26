using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values;

/// <summary>
/// This interface contains the auto-tracking result value data.
/// </summary>
public interface IAutoTrackValue : IReactiveObject
{
    /// <summary>
    /// A nullable <see cref="int"/> representing the current result value.
    /// </summary>
    int? CurrentValue { get; }
}