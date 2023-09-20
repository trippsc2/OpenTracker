using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.Items;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This class contains the small item control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class DungeonItemVM : ViewModel, IDungeonItemVM
{
    private IRequirement? Requirement { get; }
    public IItemVM? Item { get; }
    
    [ObservableAsProperty]
    public bool Visible => Requirement?.Met ?? true;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirement">
    /// The visibility requirement for the control.
    /// </param>
    /// <param name="item">
    /// The item control.
    /// </param>
    public DungeonItemVM(IRequirement? requirement, IItemVM? item)
    {
        Requirement = requirement;
        Item = item;

        HandleClickCommand = Item?.HandleClickCommand ??
                             ReactiveCommand.Create<PointerReleasedEventArgs>(_ => { });
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Requirement,
                    x => x.Requirement!.Met,
                    (_, _) => Requirement?.Met ?? true)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible, initialValue: true)
                .DisposeWith(disposables);
        });
    }
}