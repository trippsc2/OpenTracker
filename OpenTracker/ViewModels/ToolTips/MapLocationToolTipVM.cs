using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.ToolTips;

/// <summary>
/// This class contains the map location tooltip control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MapLocationToolTipVM : ViewModel, IMapLocationToolTipVM
{
    private ILayoutSettings LayoutSettings { get; }
    private ILocation Location { get; }
    public IMapLocationToolTipMarkingVM? SectionMarking { get; }
    public IMapLocationToolTipNotes Notes { get; }
    
    [ObservableAsProperty]
    public double Scale { get; }
    [ObservableAsProperty]
    public string Name { get; } = string.Empty;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="markingFactory">
    /// An Autofac factory for creating marking controls.
    /// </param>
    /// <param name="notesFactory">
    /// An Autofac factory for creating tooltip notes controls.
    /// </param>
    /// <param name="location">
    /// The map location.
    /// </param>
    public MapLocationToolTipVM(
        ILayoutSettings layoutSettings,
        IMapLocationToolTipMarkingVM.Factory markingFactory,
        IMapLocationToolTipNotes.Factory notesFactory,
        ILocation location)
    {
        LayoutSettings = layoutSettings;
        Location = location;

        var section = Location.Sections[0];
            
        if (section.Marking is not null)
        {
            SectionMarking = markingFactory(section.Marking);
        }

        Notes = notesFactory(Location);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.UIScale)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Scale)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Location.Name)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Name)
                .DisposeWith(disposables);
        });
    }
}