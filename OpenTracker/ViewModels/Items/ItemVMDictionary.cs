using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This class contains the dictionary container for all large item control ViewModel data.
/// </summary>
public class ItemVMDictionary : LazyDictionary<LargeItemType, IItemVM>, IItemVMDictionary
{
    private readonly Lazy<IItemVMFactory> _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    /// A factory for creating large item controls.
    /// </param>
    public ItemVMDictionary(IItemVMFactory.Factory factory) : base(new Dictionary<LargeItemType, IItemVM>())
    {
        _factory = new Lazy<IItemVMFactory>(() => factory());
    }

    protected override IItemVM Create(LargeItemType key)
    {
        return _factory.Value.GetLargeItemVM(key);
    }
}