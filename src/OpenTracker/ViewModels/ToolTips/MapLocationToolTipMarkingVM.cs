using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Markings.Images;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.ToolTips;

/// <summary>
/// This class contains the map location tooltip marking control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MapLocationToolTipMarkingVM : ViewModel, IMapLocationToolTipMarkingVM
{
    private IMarking Marking { get; }
    public object Model => Marking;

    [ObservableAsProperty]
    public IMarkingImageVMBase Image { get; } = default!;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="markingImages">
    /// The marking image control dictionary.
    /// </param>
    /// <param name="marking">
    /// The marking to be represented.
    /// </param>
    public MapLocationToolTipMarkingVM(IMarkingImageDictionary markingImages, IMarking marking)
    {
        Marking = marking;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Marking.Mark)
                .Select(x => markingImages[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Image)
                .DisposeWith(disposables);
        });
    }
}