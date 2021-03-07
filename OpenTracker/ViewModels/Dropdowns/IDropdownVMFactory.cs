using System.Collections.Generic;

namespace OpenTracker.ViewModels.Dropdowns
{
    public interface IDropdownVMFactory
    {
        List<IDropdownVM> GetDropdownVMs();
    }
}