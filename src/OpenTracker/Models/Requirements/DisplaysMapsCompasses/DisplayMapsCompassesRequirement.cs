using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.DisplaysMapsCompasses;

/// <summary>
///     This class contains display maps/compasses setting requirement data.
/// </summary>
[DependencyInjection]
public sealed class DisplayMapsCompassesRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private LayoutSettings LayoutSettings { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new display maps and compasses requirements.
    /// </summary>
    public delegate DisplayMapsCompassesRequirement Factory(bool expectedValue);

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
        LayoutSettings layoutSettings, bool expectedValue)
    {
        LayoutSettings = layoutSettings;

        this.WhenAnyValue(x => x.LayoutSettings.DisplayMapsCompasses)
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