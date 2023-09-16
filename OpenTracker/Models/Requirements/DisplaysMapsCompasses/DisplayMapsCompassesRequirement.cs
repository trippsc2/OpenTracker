using System.ComponentModel;
using OpenTracker.Autofac;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.DisplaysMapsCompasses;

/// <summary>
///     This class contains display maps/compasses setting requirement data.
/// </summary>
[DependencyInjection]
public sealed class DisplayMapsCompassesRequirement : BooleanRequirement, IDisplayMapsCompassesRequirement
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly bool _expectedValue;
        
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The layout settings.
    /// </param>
    /// <param name="expectedValue">
    ///     A boolean representing the expected value.
    /// </param>
    public DisplayMapsCompassesRequirement(
        ILayoutSettings layoutSettings, bool expectedValue)
    {
        _layoutSettings = layoutSettings;
        _expectedValue = expectedValue;

        _layoutSettings.PropertyChanged += OnLayoutChanged;

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.DisplayMapsCompasses))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _layoutSettings.DisplayMapsCompasses == _expectedValue;
    }
}