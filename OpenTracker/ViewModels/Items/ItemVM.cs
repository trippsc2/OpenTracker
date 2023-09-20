using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This class contains the item control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class ItemVM : ViewModel, IItemVM
{
    private IItemAdapter Item { get; }
    private IRequirement? Requirement { get; }

    [ObservableAsProperty]
    public bool Visible { get; } = true;
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    [ObservableAsProperty]
    public bool LabelVisible { get; }
    [ObservableAsProperty]
    public string? Label { get; }
    [ObservableAsProperty]
    public SolidColorBrush? LabelColor { get; }
    [ObservableAsProperty]
    public BossSelectPopupVM? BossSelect { get; }
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    /// The item to be represented.
    /// </param>
    /// <param name="requirement">
    /// The visibility requirement for the item.
    /// </param>
    public ItemVM(IItemAdapter item, IRequirement? requirement)
    {
        Item = item;
        Requirement = requirement;

        HandleClickCommand = Item.HandleClickCommand;
        
        this.WhenActivated(disposables =>
        {
            item.Activator
                .Activate()
                .DisposeWith(disposables);
            
            this.WhenAnyValue(
                    x => x.Requirement,
                    x => x.Requirement!.Met,
                    (_, _) => Requirement?.Met ?? true)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible, initialValue: true)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Item.ImageSource)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Item.Label)
                .Select(x => x is not null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.LabelVisible)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Item.Label)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Label)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Item.LabelColor)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.LabelColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Item.BossSelect)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.BossSelect)
                .DisposeWith(disposables);
        });
    }
}