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

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains the section marking icon control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingSectionIconVM : ViewModel, ISectionIconVM
{
    private IMarking Marking { get; }
    public IMarkingSelectVM MarkingSelect { get; }
    
    [ObservableAsProperty]
    public IMarkingImageVMBase Image { get; } = default!;
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

    public delegate MarkingSectionIconVM Factory(ISection section);

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
    /// The marking to be represented.
    /// </param>
    public MarkingSectionIconVM(
        IMarkingImageDictionary markingImages, IMarkingSelectFactory markingSelectFactory, ISection section)
    {
        Marking = section.Marking!;

        MarkingSelect = markingSelectFactory.GetMarkingSelectVM(section);
            
        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Marking.Mark)
                .Select(x => markingImages[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Image)
                .DisposeWith(disposables);
        });
    }

    private void HandleClickImpl(PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
        {
            MarkingSelect.PopupOpen = true;
        }
    }
}