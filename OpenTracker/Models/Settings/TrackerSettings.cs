using OpenTracker.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains GUI tracking settings data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TrackerSettings : ReactiveObject, ITrackerSettings
{
    private bool _displayAllLocations;
    public bool DisplayAllLocations
    {
        get => _displayAllLocations;
        set => this.RaiseAndSetIfChanged(ref _displayAllLocations, value);
    }

    private bool _showItemCountsOnMap = true;
    public bool ShowItemCountsOnMap
    {
        get => _showItemCountsOnMap;
        set => this.RaiseAndSetIfChanged(ref _showItemCountsOnMap, value);
    }
}