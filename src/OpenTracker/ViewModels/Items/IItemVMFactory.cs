using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items;

public interface IItemVMFactory
{
    List<ILargeItemVM> GetLargeItemControlVMs();
    IItemVM GetLargeItemVM(LargeItemType type);

    delegate IItemVMFactory Factory();
}