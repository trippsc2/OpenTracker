using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Menus;

public sealed class MenuItemCheckBoxVM : ViewModel, IMenuItemIconVM
{
    private IRequirement Requirement { get; }

    [ObservableAsProperty]
    public bool Checked { get; }

    public MenuItemCheckBoxVM(IRequirement requirement)
    {
        Requirement = requirement;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Requirement.Met)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Checked)
                .DisposeWith(disposables);
        });
    }
}