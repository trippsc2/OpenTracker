using Avalonia.Input;
using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels.Items
{
    public interface ILargeItemVM
    {
        ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        delegate ILargeItemVM Factory(IItemVM item);
    }
}