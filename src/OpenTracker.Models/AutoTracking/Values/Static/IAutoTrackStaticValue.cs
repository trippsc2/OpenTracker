namespace OpenTracker.Models.AutoTracking.Values.Static;

/// <summary>
/// This interface contains the auto-tracking result of a static value.
/// </summary>
public interface IAutoTrackStaticValue : IAutoTrackValue
{
    /// <summary>
    /// A factory for creating new <see cref="IAutoTrackStaticValue"/> objects.
    /// </summary>
    /// <param name="value">
    ///     A <see cref="int"/> for the static value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAutoTrackStaticValue"/> object.
    /// </returns>
    public delegate IAutoTrackStaticValue Factory(int value);
}