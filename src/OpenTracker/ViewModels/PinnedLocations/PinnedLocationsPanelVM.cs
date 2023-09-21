using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations;

/// <summary>
/// This class contains the pinned location panel control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class PinnedLocationsPanelVM : ViewModel, IPinnedLocationsPanelVM
{
    private LayoutSettings LayoutSettings { get; }

    public IPinnedLocationVMCollection Locations { get; }

    [ObservableAsProperty]
    public Orientation Orientation { get; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="locations">
    /// The pinned location control collection.
    /// </param>
    public PinnedLocationsPanelVM(LayoutSettings layoutSettings, IPinnedLocationVMCollection locations)
    {
        LayoutSettings = layoutSettings;
        Locations = locations;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.CurrentLayoutOrientation)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Orientation)
                .DisposeWith(disposables);
        });
    }
}