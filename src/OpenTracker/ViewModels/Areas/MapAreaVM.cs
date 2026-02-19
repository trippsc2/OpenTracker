using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Layout;
using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.MapLocations;
using OpenTracker.ViewModels.Maps;
using ReactiveUI;

namespace OpenTracker.ViewModels.Areas;

/// <summary>
/// This is the ViewModel of the map area control.
/// </summary>
public class MapAreaVM : ViewModelBase, IMapAreaVM
{
    private readonly ILayoutSettings _layoutSettings;

    public Orientation Orientation => _layoutSettings.CurrentMapOrientation;

    public List<IMapVM> Maps { get; }
    public ReadOnlyObservableCollection<IMapConnectionVM> Connectors { get; }
    public List<IMapLocationVM> MapLocations { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public MapAreaVM(
        ILayoutSettings layoutSettings,
        IMapAreaFactory factory,
        IMapConnectionCollection connections,
        IMapConnectionVM.Factory connectionFactory)
    {
        _layoutSettings = layoutSettings;

        Maps = factory.GetMapControlVMs();
        MapLocations = factory.GetMapLocationControlVMs();

        connections.Connections
            .ToObservableChangeSet()
            .Transform(connection => connectionFactory(connection))
            .Bind(out var connectors)
            .Subscribe();

        Connectors = connectors;

        _layoutSettings.PropertyChanged += OnLayoutChanged;
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the LayoutSettings class.
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
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
        }
    }
}