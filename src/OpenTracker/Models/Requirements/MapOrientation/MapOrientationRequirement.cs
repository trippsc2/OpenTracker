using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Layout;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.MapOrientation;

/// <summary>
///     This class contains map orientation setting requirement data.
/// </summary>
[DependencyInjection]
public sealed class MapOrientationRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private LayoutSettings LayoutSettings { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;
        
    /// <summary>
    /// A factory method for creating new map orientation requirements.
    /// </summary>
    public delegate MapOrientationRequirement Factory(Orientation? expectedValue);

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The layout settings.
    /// </param>
    /// <param name="expectedValue">
    ///     The expected orientation value.
    /// </param>
    public MapOrientationRequirement(LayoutSettings layoutSettings, Orientation? expectedValue)
    {
        LayoutSettings = layoutSettings;

        this.WhenAnyValue(x => x.LayoutSettings.MapOrientation)
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