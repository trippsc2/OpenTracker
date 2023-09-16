using System.ComponentModel;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Maps;

/// <summary>
/// This is the ViewModel of the map control.
/// </summary>
[DependencyInjection]
public sealed class MapVM : ViewModel, IMapVM
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly IMode _mode;

    private readonly MapID _id;

    public Thickness Margin =>
        _layoutSettings.CurrentMapOrientation switch
        {
            Orientation.Horizontal => new Thickness(10, 20),
            _ => new Thickness(20, 10)
        };
    public string ImageSource
    {
        get
        {
            var worldState = _mode.WorldState == WorldState.Inverted ?
                WorldState.Inverted : WorldState.StandardOpen;

            return $"avares://OpenTracker/Assets/Images/Maps/" +
                   worldState.ToString().ToLowerInvariant() +
                   $"_{_id.ToString().ToLowerInvariant()}.png";
        }
    }

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
    public MapVM(ILayoutSettings layoutSettings, IMode mode, MapID id)
    {
        _layoutSettings = layoutSettings;
        _mode = mode;
        _id = id;

        _mode.PropertyChanged += OnModeChanged;
        _layoutSettings.PropertyChanged += OnLayoutChanged;
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.CurrentMapOrientation))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Margin)));
        }
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IMode interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMode.WorldState))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }
    }
}