namespace OpenTracker.Models.Reset;

/// <summary>
/// This interface contains the logic for resetting the object to its starting value.
/// </summary>
public interface IResettable
{
    /// <summary>
    /// Resets the object to its starting value.
    /// </summary>
    void Reset();
}