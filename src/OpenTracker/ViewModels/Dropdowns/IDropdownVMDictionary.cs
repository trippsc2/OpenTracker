using System.Collections.Generic;
using OpenTracker.Models.Dropdowns;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.ViewModels.Dropdowns
{
    public interface IDropdownVMDictionary : IDictionary<DropdownID, ILargeItemVM>
    {
    }
}