using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Maps;

/// <summary>
/// This is the ViewModel of the map control.
/// </summary>
[DependencyInjection]
public sealed class MapVM : ViewModel, IMapVM
{
    private IMode Mode { get; }
    private LayoutSettings LayoutSettings { get; }
    
    [ObservableAsProperty]
    public Thickness Margin { get; }
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="mode">
    /// The mode settings data.
    /// </param>
    /// <param name="id">
    /// The map identity.
    /// </param>
    public MapVM(LayoutSettings layoutSettings, IMode mode, MapID id)
    {
        LayoutSettings = layoutSettings;
        Mode = mode;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.CurrentMapOrientation)
                .Select(x => x == Orientation.Horizontal
                    ? new Thickness(10, 20)
                    : new Thickness(20, 10))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Margin)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Mode.WorldState)
                .Select(x =>
                    $"avares://OpenTracker/Assets/Images/Maps/{x.ToString().ToLowerInvariant()}_{id.ToString().ToLowerInvariant()}.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }
}