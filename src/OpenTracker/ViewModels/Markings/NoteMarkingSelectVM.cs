using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This class contains the note marking select popup control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class NoteMarkingSelectVM : ViewModel, INoteMarkingSelectVM
{
    private readonly IUndoRedoManager _undoRedoManager;

    private LayoutSettings LayoutSettings { get; }
    private readonly ILocation _location;

    private IMarking Marking { get; }
    public List<IMarkingSelectItemVMBase> Buttons { get; }

    [Reactive]
    public bool PopupOpen { get; set; }
    [ObservableAsProperty]
    public double Scale => LayoutSettings.UIScale;

    public ReactiveCommand<Unit, Unit> RemoveNoteCommand { get; }

    public delegate INoteMarkingSelectVM Factory(
        IMarking marking,
        List<IMarkingSelectItemVMBase> buttons,
        ILocation location);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="marking">
    /// The marking to be represented.
    /// </param>
    /// <param name="buttons">
    /// The observable collection of marking select buttons.
    /// </param>
    /// <param name="location">
    /// The location.
    /// </param>
    public NoteMarkingSelectVM(
        LayoutSettings layoutSettings,
        IUndoRedoManager undoRedoManager,
        IMarking marking,
        List<IMarkingSelectItemVMBase> buttons,
        ILocation location)
    {
        _undoRedoManager = undoRedoManager;
        _location = location;
        LayoutSettings = layoutSettings;
        Marking = marking;
        Buttons = buttons;
        
        RemoveNoteCommand = ReactiveCommand.Create(RemoveNote);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.LayoutSettings.UIScale)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Scale)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Marking.Mark)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => PopupOpen = false)
                .DisposeWith(disposables);
        });
    }

    private void RemoveNote()
    {
        _undoRedoManager.NewAction(_location.CreateRemoveNoteAction(Marking));
        PopupOpen = false;
    }
}