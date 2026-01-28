using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Layout;
using Avalonia.Threading;
using DynamicData;
using DynamicData.Binding;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations;

/// <summary>
/// This class contains the pinned location panel control ViewModel data.
/// </summary>
public class PinnedLocationsPanelVM : ViewModelBase, IPinnedLocationsPanelVM
{
    private readonly ILayoutSettings _layoutSettings;

    public Orientation Orientation => _layoutSettings.CurrentLayoutOrientation;

    public ReadOnlyObservableCollection<IPinnedLocationVM> Locations { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="pinnedLocations">
    /// The pinned location collection.
    /// </param>
    /// <param name="viewModelFactory">
    /// A factory for creating pinned location ViewModels.
    /// </param>
    public PinnedLocationsPanelVM(
        ILayoutSettings layoutSettings,
        IPinnedLocationCollection pinnedLocations,
        IPinnedLocationVMFactory viewModelFactory)
    {
        _layoutSettings = layoutSettings;

        ((PinnedLocationCollection)pinnedLocations)
            .ToObservableChangeSet()
            .Transform(viewModelFactory.GetLocationControlVM)
            .Bind(out var locations)
            .Subscribe();

        Locations = locations;

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
        if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
        }
    }
}