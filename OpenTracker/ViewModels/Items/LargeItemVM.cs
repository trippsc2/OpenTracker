using Avalonia.Input;
using OpenTracker.Utils;
using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels.Items
{
    public class LargeItemVM : ViewModelBase, ILargeItemVM
    {
        public IItemVM Item { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public LargeItemVM(IItemVM item)
        {
            Item = item;
            HandleClick = Item.HandleClick;
        }
    }
}