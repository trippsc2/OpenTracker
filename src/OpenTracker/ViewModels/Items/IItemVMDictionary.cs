using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This interface contains the dictionary container for all large item control ViewModel data.
    /// </summary>
    public interface IItemVMDictionary : IDictionary<LargeItemType, IItemVM>
    {
    }
}