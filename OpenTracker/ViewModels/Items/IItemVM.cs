using System.Reactive;
using Avalonia.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items;

public interface IItemVM
{
    ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    delegate IItemVM Factory(IItemAdapter item, IRequirement? requirement = null);
}