using System.ComponentModel;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus;

[DependencyInjection]
public sealed class MenuItemCheckBoxVM : ViewModel, IMenuItemIconVM
{
    private readonly IRequirement _requirement;

    public bool Checked => _requirement.Met;

    public delegate MenuItemCheckBoxVM Factory(IRequirement requirement);

    public MenuItemCheckBoxVM(IRequirement requirement)
    {
        _requirement = requirement;

        _requirement.PropertyChanged += OnRequirementChanged;
    }

    private async void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Met))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Checked)));
        }
    }
}