using RxVoid = ReactiveUI.Primitives.RxVoid;
using Avalonia.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items;

public interface IItemVM
{
    ReactiveCommand<PointerReleasedEventArgs, RxVoid> HandleClick { get; }

    delegate IItemVM Factory(IItemAdapter item, IRequirement? requirement = null);
}