using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.MapLocations;
using OpenTracker.ViewModels.Maps;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Areas;

/// <summary>
/// This is the ViewModel of the map area control.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class MapAreaVM : ViewModel, IMapAreaVM
{
    private ILayoutSettings LayoutSettings { get; }

    public List<IMapVM> Maps { get; }
    public IMapConnectionCollection Connectors { get; }
    public List<IMapLocationVM> MapLocations { get; }

    [ObservableAsProperty]
    public Orientation Orientation { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public MapAreaVM(ILayoutSettings layoutSettings, IMapAreaFactory factory, IMapConnectionCollection connectors)
    {
        LayoutSettings = layoutSettings;

        Maps = factory.GetMapControlVMs();
        Connectors = connectors;
        MapLocations = factory.GetMapLocationControlVMs();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.CurrentMapOrientation)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Orientation)
                .DisposeWith(disposables);
        });
    }
}