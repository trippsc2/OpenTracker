using System.ComponentModel;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.ShowItemCountsOnMap;

/// <summary>
///     This class contains show item counts on map setting requirement data.
/// </summary>
public class ShowItemCountsOnMapRequirement : BooleanRequirement, IShowItemCountsOnMapRequirement
{
    private readonly ITrackerSettings _trackerSettings;
    private readonly bool _expectedValue;
        
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="trackerSettings">
    ///     The tracker settings data.
    /// </param>
    /// <param name="expectedValue">
    ///     A boolean representing the expected value.
    /// </param>
    public ShowItemCountsOnMapRequirement(ITrackerSettings trackerSettings, bool expectedValue)
    {
        _trackerSettings = trackerSettings;
        _expectedValue = expectedValue;

        _trackerSettings.PropertyChanged += OnTrackerSettingsChanged;
            
        UpdateValue();
    }

    /// <summary>
    ///     Subscribes to the PropertyChanged event on the ITrackerSettings interface.
    /// </summary>
    /// <param name="sender">
    ///     The sending object of the event.
    /// </param>
    /// <param name="e">
    ///     The arguments of the PropertyChanged event.
    /// </param>
    private void OnTrackerSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ITrackerSettings.ShowItemCountsOnMap))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _trackerSettings.ShowItemCountsOnMap == _expectedValue;
    }
}