using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Values;

/// <summary>
/// This base class contains the auto-tracking result value data.
/// </summary>
public abstract class AutoTrackValueBase : ReactiveObject, IAutoTrackValue
{
    private int? _currentValue;
    public int? CurrentValue
    {
        get => _currentValue;
        private set => this.RaiseAndSetIfChanged(ref _currentValue, value);
    }

    /// <summary>
    /// Returns the new value for the <see cref="CurrentValue"/> property.
    /// </summary>
    /// <returns>
    ///     A nullable <see cref="int"/> representing the new auto-tracking result value.
    /// </returns>
    protected abstract int? GetNewValue();

    /// <summary>
    ///     Updates the <see cref="CurrentValue"/> property.
    /// </summary>
    protected void UpdateValue()
    {
        CurrentValue = GetNewValue();
    }
}