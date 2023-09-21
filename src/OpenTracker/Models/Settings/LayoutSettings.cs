using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains GUI layout settings data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class LayoutSettings : ReactiveObject
{
    [Reactive]
    public bool DisplayMapsCompasses { get; set; }
    [Reactive]
    public bool AlwaysDisplayDungeonItems { get; set; }
    [Reactive]
    public Orientation CurrentDynamicOrientation { get; set; }
    [Reactive]
    public Orientation? LayoutOrientation { get; set; }
    [Reactive]
    public Orientation? MapOrientation { get; set; }
    [Reactive]
    public Dock HorizontalUIPanelPlacement { get; set; }
    [Reactive]
    public Dock VerticalUIPanelPlacement { get; set; }
    [Reactive]
    public Dock HorizontalItemsPlacement { get; set; }
    [Reactive]
    public Dock VerticalItemsPlacement { get; set; }
    [Reactive]
    public double UIScale { get; set; }
    
    [ObservableAsProperty]
    public Orientation CurrentLayoutOrientation { get; }
    [ObservableAsProperty]
    public Orientation CurrentMapOrientation { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public LayoutSettings()
    {
        HorizontalUIPanelPlacement = Dock.Bottom;
        HorizontalItemsPlacement = Dock.Left;
        VerticalItemsPlacement = Dock.Bottom;
        UIScale = 1.0;
        
        this.WhenAnyValue(
                x => x.LayoutOrientation,
                x => x.CurrentDynamicOrientation,
                (layoutOrientation, dynamicOrientation) => layoutOrientation ?? dynamicOrientation)
            .ToPropertyEx(this, x => x.CurrentLayoutOrientation);
        this.WhenAnyValue(
                x => x.MapOrientation,
                x => x.CurrentDynamicOrientation,
                (mapOrientation, dynamicOrientation) => mapOrientation ?? dynamicOrientation)
            .ToPropertyEx(this, x => x.CurrentMapOrientation);
    }
}