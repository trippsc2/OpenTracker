using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.ShowItemCountsOnMap;

/// <summary>
///     This class contains show item counts on map setting requirement data.
/// </summary>
[DependencyInjection]
public sealed class ShowItemCountsOnMapRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private TrackerSettings TrackerSettings { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new show item counts on map requirements.
    /// </summary>
    public delegate ShowItemCountsOnMapRequirement Factory(bool expectedValue);

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="trackerSettings">
    ///     The tracker settings data.
    /// </param>
    /// <param name="expectedValue">
    ///     A boolean representing the expected value.
    /// </param>
    public ShowItemCountsOnMapRequirement(TrackerSettings trackerSettings, bool expectedValue)
    {
        TrackerSettings = trackerSettings;

        this.WhenAnyValue(x => x.TrackerSettings.ShowItemCountsOnMap)
            .Select(x => x == expectedValue)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
    }
}