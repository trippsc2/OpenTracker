using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This class contains the marking select popup control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingSelectVM : ViewModel, IMarkingSelectVM
{
    private readonly IUndoRedoManager _undoRedoManager;
        
    private ILayoutSettings LayoutSettings { get; }
    private IMarking Marking { get; }
    public double Width { get; }
    public double Height { get; }
    public List<IMarkingSelectItemVMBase> Buttons { get; }

    [Reactive]
    public bool PopupOpen { get; set; }
    [ObservableAsProperty]
    public double Scale { get; }

    public ReactiveCommand<Unit, Unit> ClearMarkingCommand { get; }

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
    /// The observable collection of marking select button ViewModel instances.
    /// </param>
    /// <param name="width">
    /// The width of the popup.
    /// </param>
    /// <param name="height">
    /// The height of the popup.
    /// </param>
    public MarkingSelectVM(
        ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager, IMarking marking,
        List<IMarkingSelectItemVMBase> buttons, double width, double height)
    {
        LayoutSettings = layoutSettings;
        _undoRedoManager = undoRedoManager;

        Marking = marking;

        Buttons = buttons;
        Width = width;
        Height = height;

        ClearMarkingCommand = ReactiveCommand.Create(ClearMarking);
        
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

    private void ClearMarking()
    {
        _undoRedoManager.NewAction(Marking.CreateChangeMarkingAction(MarkType.Unknown));
    }
}