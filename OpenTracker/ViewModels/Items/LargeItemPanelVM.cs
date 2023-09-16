using System.Collections.Generic;
using OpenTracker.Autofac;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This class contains the large item panel body control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class LargeItemPanelVM : ViewModel, ILargeItemPanelVM
{
    public List<ILargeItemVM> Items { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items">
    /// The item control dictionary.
    /// </param>
    public LargeItemPanelVM(List<ILargeItemVM> items)
    {
        Items = items;
    }
}