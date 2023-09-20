using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.MapLocations;

[DependencyInjection]
public sealed class MapLocationMarkingVM : ViewModel, IMapLocationMarkingVM
{
    private IMarking Marking { get; }
    
    public IMarkingSelectVM MarkingSelect { get; }

    [ObservableAsProperty]
    public IMarkingImageVMBase Image { get; } = default!;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="markingImages">
    /// The marking image control dictionary.
    /// </param>
    /// <param name="markingSelectFactory">
    /// A factory for creating marking select controls.
    /// </param>
    /// <param name="section">
    /// The section marking to be represented.
    /// </param>
    public MapLocationMarkingVM(
        IMarkingImageDictionary markingImages, IMarkingSelectFactory markingSelectFactory, ISection section)
    {
        Marking = section.Marking!;
        MarkingSelect = markingSelectFactory.GetMarkingSelectVM(section);
        
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