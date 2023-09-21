using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains GUI tracking settings data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TrackerSettings : ReactiveObject
{
    [Reactive]
    public bool DisplayAllLocations { get; set; }
    [Reactive]
    public bool ShowItemCountsOnMap { get; set; }
}