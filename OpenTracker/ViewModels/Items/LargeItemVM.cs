using System.Reactive;
using Avalonia.Input;
using OpenTracker.Autofac;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This class contains the large item control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class LargeItemVM : ViewModel, ILargeItemVM
{
    public IItemVM Item { get; }
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">
    /// The item control.
    /// </param>
    public LargeItemVM(IItemVM item)
    {
        Item = item;
        HandleClick = Item.HandleClick;
    }
}