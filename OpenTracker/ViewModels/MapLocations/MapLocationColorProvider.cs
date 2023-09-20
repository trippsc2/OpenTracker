using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace OpenTracker.ViewModels.MapLocations;

[DependencyInjection]
public sealed class MapLocationColorProvider : ViewModel, IMapLocationColorProvider
{
    private static readonly SolidColorBrush HighlightedBorderColor = SolidColorBrush.Parse("#ffffff");
    private static readonly SolidColorBrush NormalBorderColor = SolidColorBrush.Parse("#000000");
    
    private IColorSettings ColorSettings { get; }
    private IMapLocation MapLocation { get; }

    [Reactive]
    private bool Highlighted { get; set; }
    [ObservableAsProperty]
    private ReactiveProperty<SolidColorBrush> AccessibilityColor { get; } = default!;
    [ObservableAsProperty]
    public SolidColorBrush BorderColor { get; } = NormalBorderColor;
    [ObservableAsProperty]
    public SolidColorBrush Color { get; } = default!;

    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    /// The color settings data.
    /// </param>
    /// <param name="mapLocation">
    /// The map location data.
    /// </param>
    public MapLocationColorProvider(IColorSettings colorSettings, IMapLocation mapLocation)
    {
        ColorSettings = colorSettings;
        MapLocation = mapLocation;
        
        HandlePointerEnterCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnter);
        HandlePointerLeaveCommand = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeave);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.MapLocation.Location.Accessibility)
                .Select(x => ColorSettings.AccessibilityColors[x])
                .ToPropertyEx(this, x => x.AccessibilityColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Highlighted)
                .Select(x => x ? HighlightedBorderColor : NormalBorderColor)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BorderColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.AccessibilityColor,
                    x => x.AccessibilityColor.Value,
                    (_, color) => color)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Color)
                .DisposeWith(disposables);
        });
    }

    private void HandlePointerEnter(PointerEventArgs e)
    {
        Highlight();
    }

    private void HandlePointerLeave(PointerEventArgs e)
    {
        Unhighlight();
    }

    private void Highlight()
    {
        Highlighted = true;
    }

    private void Unhighlight()
    {
        Highlighted = false;
    }

}