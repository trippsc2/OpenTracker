using System.ComponentModel;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.AutoTracking;

/// <summary>
/// This class contains <see cref="IAutoTracker.RaceIllegalTracking"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class RaceIllegalTrackingRequirement : BooleanRequirement, IRaceIllegalTrackingRequirement
{
    private readonly IAutoTracker _autoTracker;
    private readonly bool _expectedValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTracker">
    ///     The <see cref="IAutoTracker"/>.
    /// </param>
    public RaceIllegalTrackingRequirement(IAutoTracker autoTracker)
    {
        _autoTracker = autoTracker;
        _expectedValue = true;

        _autoTracker.PropertyChanged += OnAutoTrackerChanged;

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the <see cref="IAutoTracker.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _autoTracker.RaceIllegalTracking == _expectedValue;
    }
}