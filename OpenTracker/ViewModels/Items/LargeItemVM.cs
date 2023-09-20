using System.Reactive;
using Avalonia.Input;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This class contains the large item control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class LargeItemVM : ViewModel, ILargeItemVM
{
    public IItemVM Item { get; }

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    /// The item control.
    /// </param>
    public LargeItemVM(IItemVM item)
    {
        Item = item;
        
        HandleClickCommand = Item.HandleClickCommand;
    }
}