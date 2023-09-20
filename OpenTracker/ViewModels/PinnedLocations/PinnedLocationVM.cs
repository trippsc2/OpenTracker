using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations;

/// <summary>
/// This class contains the pinned location control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class PinnedLocationVM : ViewModel, IPinnedLocationVM
{
    private readonly IUndoRedoManager _undoRedoManager;

    private ILayoutSettings LayoutSettings { get; }
    private ILocation Location { get; }
    public object Model => Location;
    public List<ISectionVM> Sections { get; }
    public IPinnedLocationNoteAreaVM Notes { get; }

    [ObservableAsProperty]
    public double Scale { get; }
    [ObservableAsProperty]
    public string Name { get; } = string.Empty;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="location">
    /// The location to be represented.
    /// </param>
    /// <param name="sections">
    /// The observable collection of section control ViewModels.
    /// </param>
    /// <param name="notes">
    /// The pinned location note area control.
    /// </param>
    public PinnedLocationVM(
        ILayoutSettings layoutSettings,
        IUndoRedoManager undoRedoManager,
        ILocation location,
        List<ISectionVM> sections,
        IPinnedLocationNoteAreaVM notes)
    {
        _undoRedoManager = undoRedoManager;
        LayoutSettings = layoutSettings;
        Location = location;
        Sections = sections;
        Notes = notes;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
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

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            UnpinLocation();
        }
    }

    private void UnpinLocation()
    {
        _undoRedoManager.NewAction(Location.CreateUnpinLocationAction());
    }
}