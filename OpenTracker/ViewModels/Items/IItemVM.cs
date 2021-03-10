using Avalonia.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels.Items
{
    public interface IItemVM
    {
        ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        delegate IItemVM Factory(IItemAdapter item, IRequirement requirement);
    }
}