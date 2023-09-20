using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Notes;

/// <summary>
/// This class contains pinned location note control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class PinnedLocationNoteVM : ViewModel, IPinnedLocationNoteVM
{
    private IMarking Marking { get; }

    public object Model => Marking;

    public INoteMarkingSelectVM MarkingSelect { get; }

    [ObservableAsProperty]
    public IMarkingImageVMBase Image { get; } = default!;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="markingImages">
    /// The marking image control dictionary.
    /// </param>
    /// <param name="marking">
    /// The marking to be noted.
    /// </param>
    /// <param name="markingSelect">
    /// The note marking select control.
    /// </param>
    public PinnedLocationNoteVM(
        IMarkingImageDictionary markingImages,
        IMarking marking,
        INoteMarkingSelectVM markingSelect)
    {
        Marking = marking;
        MarkingSelect = markingSelect;
            
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Marking.Mark)
                .Select(x => markingImages[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Image)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            MarkingSelect.PopupOpen = true;
        }
    }
}