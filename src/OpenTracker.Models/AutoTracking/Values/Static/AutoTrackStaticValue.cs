using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values.Static;

/// <summary>
/// This class contains the auto-tracking result of a static value.
/// </summary>
public class AutoTrackStaticValue : ReactiveObject, IAutoTrackStaticValue
{
    public int? CurrentValue { get; }
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">
    ///     A <see cref="int"/> for the static value.
    /// </param>
    public AutoTrackStaticValue(int value)
    {
        CurrentValue = value;
    }
}