using System.Collections.Generic;
using OpenTracker.Models.Dropdowns;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.ViewModels.Dropdowns
{
    public interface IDropdownVMFactory
    {
        List<ILargeItemVM> GetDropdownVMs();
        ILargeItemVM GetDropdownVM(DropdownID id);

        delegate IDropdownVMFactory Factory();
    }
}