using System.Reactive;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This class contains the marking select button control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingSelectButtonVM : ViewModel, IMarkingSelectItemVMBase
{
    private readonly IUndoRedoManager _undoRedoManager;
    
    private readonly IMarking _marking;
    private readonly MarkType? _mark;
    
    public IMarkingImageVMBase? Image { get; }

    public ReactiveCommand<Unit, Unit> ChangeMarkCommand { get; }

    public delegate MarkingSelectButtonVM Factory(IMarking marking, MarkType? mark);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    ///     The <see cref="IUndoRedoManager"/>
    /// </param>
    /// <param name="markingImages">
    ///     The marking image control dictionary.
    /// </param>
    /// <param name="marking">
    ///     The marking to be represented by this button.
    /// </param>
    /// <param name="mark"></param>
    public MarkingSelectButtonVM(IUndoRedoManager undoRedoManager, IMarkingImageDictionary markingImages, IMarking marking, MarkType? mark)
    {
        _undoRedoManager = undoRedoManager;
        _marking = marking;
        _mark = mark;

        if (_mark is not null)
        {
            Image = markingImages[_mark.Value];
        }
        
        ChangeMarkCommand = ReactiveCommand.Create(ChangeMark);
    }
    
    private void ChangeMark()
    {
        if (_mark is null)
        {
            return;
        }
        
        _undoRedoManager.NewAction(_marking.CreateChangeMarkingAction(_mark.Value));
    }
}