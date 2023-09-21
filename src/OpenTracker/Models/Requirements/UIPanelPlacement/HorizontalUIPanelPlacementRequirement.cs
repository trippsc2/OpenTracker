using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.UIPanelPlacement;

/// <summary>
///     This class contains horizontal UI panel placement requirement data.
/// </summary>
[DependencyInjection]
public sealed class HorizontalUIPanelPlacementRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private LayoutSettings LayoutSettings { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }
    
    public event EventHandler? ChangePropagated;
        
    /// <summary>
    /// A factory method for creating new horizontal UI panel placement requirements.
    /// </summary>
    public delegate HorizontalUIPanelPlacementRequirement Factory(Dock expectedValue);

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The layout settings data.
    /// </param>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    public HorizontalUIPanelPlacementRequirement(LayoutSettings layoutSettings, Dock expectedValue)
    {
        LayoutSettings = layoutSettings;

        this.WhenAnyValue(x => x.LayoutSettings.HorizontalUIPanelPlacement)
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