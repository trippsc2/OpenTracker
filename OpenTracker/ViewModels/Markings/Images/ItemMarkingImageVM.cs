using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Items;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Markings.Images;

/// <summary>
/// This class contains the non-static item marking image control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class ItemMarkingImageVM : ViewModel, IMarkingImageVMBase
{
    private IItem Item { get; }

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public delegate ItemMarkingImageVM Factory(IItem item, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    /// The item to be represented.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the base image source.
    /// </param>
    public ItemMarkingImageVM(IItem item, string imageSourceBase)
    {
        Item = item;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Item.Current)
                .Select(x => $"{imageSourceBase}{x.ToString().ToLowerInvariant()}.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }
}