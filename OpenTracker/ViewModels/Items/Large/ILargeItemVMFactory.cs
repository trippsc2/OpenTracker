using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items.Large
{
    public interface ILargeItemVMFactory
    {
        List<ILargeItemVMBase> GetLargeItemControlVMs();
    }
}